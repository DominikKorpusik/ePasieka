using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ePasieka.Controllers
{
    /*
        Dostęp do kontrolera BeeGarden jest umożliwiony tylko osobom zalogowanym.
        Obsługa:
            1. Wyświetlania wszystkich obiektów typu BeeGarden, należacych do konkretnego użytkownika
            2. Wprowadzania nowego obiektu typu BeeGarden do bazy, utworzonego przez użytkownika
            3. Edycji konkretnego obiektu typu BeeGarden
            4. Usunięcie konkretnego obiektu typu BeeGarden
     
     */


    [Authorize]
    public class BeeGardenController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBeeGardenRepository _beeGardenRepository;
        private readonly IBeehiveRepository _beehiveRepository;

        public BeeGardenController(IHttpContextAccessor httpContextAccessor, IBeeGardenRepository beeGardenRepository, IBeehiveRepository beehiveRepository)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._beeGardenRepository = beeGardenRepository;
            this._beehiveRepository = beehiveRepository;
        }



        [HttpGet]
        public IActionResult BeeGardens()
        {
            var beeGardens = new ViewModel
            {
                beeGardens = _beeGardenRepository.AllBeeGardens(),
                beehives = _beehiveRepository.AllBeehives()
            };

            return View(beeGardens);
        }

        [HttpGet]
        public IActionResult AddBeeGarden()
        {
            var beeGarden = new ViewModel
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                beeGardens = _beeGardenRepository.AllBeeGardens()
            };

            return View(beeGarden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBeeGarden(BeeGarden beeGarden)
        {

            if (ModelState.IsValid)
            {
                _beeGardenRepository.AddBeeGarden(beeGarden);
                return RedirectToAction("BeeGardens");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditBeeGarden(int id)
        {
            var beeGarden = _beeGardenRepository.BeeGardenByID(id);
            if (beeGarden == null)
            {
                return NotFound();
            }
            return View(beeGarden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBeeGarden(BeeGarden beeGarden)
        {
            if (ModelState.IsValid)
            {
                _beeGardenRepository.UpdateBeeGarden(beeGarden);
                return RedirectToAction("BeeGardens");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBeeGarden(int id)
        {
            var beeGarden = _beeGardenRepository.BeeGardenByID(id);

            if (beeGarden == null)
            {
                return NotFound();
            }

            return View(beeGarden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBeeGarden(int id, IFormCollection form)
        {
            _beeGardenRepository.DeleteBeeGarden(id);
            return RedirectToAction("BeeGardens");
        }

    }
}
