using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace ePasieka.Controllers
{

    /*
        Dostęp do kontrolera Beehive jest umożliwiony tylko osobom zalogowanym.
        Obsługa:
            1. Wyświetlania szczegółowych danych, odnoszących się do konkretnego ula
            2. Wyświetlania wszystkich obiektów typu BeeGarden, należacych do konkretnego użytkownika
            3. Wprowadzania nowego obiektu typu Beehive do bazy, utworzonego przez użytkownika
            4. Edycji konkretnego obiektu typu Beehive
            5. Usunięcie konkretnego obiektu typu Beehive
            6. Odbiór danych z API openweathermap.org
     */


    [Authorize]
    public class BeehiveController : Controller
    {
        private readonly IBeehiveRepository _beehiveRepository;
        private readonly IBeeGardenRepository _beeGardenRepository;

        public BeehiveController(IBeehiveRepository beehiveRepository, IBeeGardenRepository beeGardenRepository)
        {
            this._beehiveRepository = beehiveRepository;
            this._beeGardenRepository = beeGardenRepository;
        }

        [HttpGet]
        public IActionResult BeeGardenDetails(int id)
        {
            var beehive = _beehiveRepository.BeehivesByBeeGarden(id);

            var viewModel = new ViewModel
            {
                beehives = beehive,
                beeGarden = _beeGardenRepository.BeeGardenByID(id)
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Weather5day3hourResult(int id)
        {
            var beeGarden = _beeGardenRepository.BeeGardenByID(id);

            string appID = "0d0f9df6e0e335b3fb9434e9a73e14f6";
            string longitude = beeGarden.longitude;
            string latitude = beeGarden.latitude;
            string units = "metric";
            string lang = "pl";
            string URL = "http://" + $"api.openweathermap.org/data/2.5/forecast?lat={latitude}" +
                                      $"&lon={longitude}&lang={lang}&units={units}&appid={appID}";

            //Test
            //string longt = "14.596941792769348";
            //string lat = "53.398200694241645";
            //string testURL = "http://api.openweathermap.org/data/2.5/forecast?lat=53.398200694241645&lon=14.596941792769348&lang=pl&units=metric&appid=0d0f9df6e0e335b3fb9434e9a73e14f6";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(URL);
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                Weather5day3hourViewModel rslt = new Weather5day3hourViewModel();
                var resultList = new List<Weather5day3hourViewModel>();

                foreach (var list in weatherInfo.list)
                {
                    var result = new Weather5day3hourViewModel()
                    {
                        Country = weatherInfo.city.country,
                        City = weatherInfo.city.name,
                        Lat = Convert.ToString(weatherInfo.city.coord.lat),
                        Lon = Convert.ToString(weatherInfo.city.coord.lon),
                        Description = list.weather[0].description,
                        Humidity = Convert.ToString(list.main.humidity),
                        Temp = Convert.ToString(list.main.temp),
                        TempFeelsLike = Convert.ToString(list.main.feels_like),
                        TempMax = Convert.ToString(list.main.temp_max),
                        TempMin = Convert.ToString(list.main.temp_min),
                        dt_txt = Convert.ToString(list.dt_txt),
                        WeatherIcon = list.weather[0].icon.ToString()
                    };

                    resultList.Add(result);
                }

                var jsonstring = new JavaScriptSerializer().Serialize(resultList);
                return new JsonResult(jsonstring);
            }
        }



        [HttpGet]
        public IActionResult EditBeehive(int id)
        {

            var beehive = _beehiveRepository.BeehiveByID(id);
            var beehives = new ViewModel
            {
                beehive = beehive,
                beeGarden = _beeGardenRepository.BeeGardenByID(beehive.beeGardenID)
            };

            if (beehive == null)
            {
                return NotFound();
            }

            return View(beehives);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBeehive(Beehive beehive)
        {
            if (ModelState.IsValid)
            {

                _beehiveRepository.UpdateBeehive(beehive);
                return RedirectToAction("BeeGardenDetails", new { id = beehive.beeGardenID });
            }

            return View();
        }


        [HttpGet]
        public IActionResult AddBeehiveToBeehives()
        {
            var beeGarden = new ViewModel
            {
                beeGardens = _beeGardenRepository.AllBeeGardens()
            };

            return View(beeGarden);
        }

        [HttpGet]
        public IActionResult AddBeehive(int id)
        {
            var beeGarden = new ViewModel
            {
                beeGarden = _beeGardenRepository.BeeGardenByID(id)
            };

            if (beeGarden == null)
            {
                return NotFound();
            }

            return View(beeGarden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBeehive(Beehive beehive, int howMany)
        {
            if (ModelState.IsValid)
            {
                _beehiveRepository.AddBeehive(beehive, howMany);
                return RedirectToAction("BeeGardenDetails", new { id = beehive.beeGardenID });
            }

            return View();

        }


        [HttpGet]
        public IActionResult DeleteBeehive(int id)
        {
            var beehive = _beehiveRepository.BeehiveByID(id);
            if (beehive == null)
            {
                return NotFound();
            }

            return View(beehive);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBeehive(int id, IFormCollection formCollection)
        {
            var beehive = _beehiveRepository.BeehiveByID(id);
            _beehiveRepository.DeleteBeehive(id);
            return RedirectToAction("BeeGardenDetails", new { id = beehive.beeGardenID });
        }


        [HttpGet]
        public IActionResult Beehives()
        {
            var beehives = new ViewModel
            {
                beeGardens = _beeGardenRepository.AllBeeGardens(),
                beehives = _beehiveRepository.AllBeehives()
            };

            return View(beehives);
        }




    }
}
