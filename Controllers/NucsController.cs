using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePasieka.Controllers
{

    /*
       Dostęp do kontrolera Nucs jest umożliwiony tylko osobom zalogowanym.
       Obsługa:
           1. Wyświetlania wszystkich obiektów typu Nuc, należacych do konkretnego użytkownika
           2. Wprowadzania nowego obiektu typu Nuc do bazy, utworzonego przez użytkownika
           3. Edycji konkretnego obiektu typu Nuc
           4. Usunięcie konkretnego obiektu typu Nuc

    */

    [Authorize]
    public class NucsController : Controller
    {
        private readonly INucRepository _nucRepository;
        private readonly IBeeGardenRepository _beeGardenRepository;
        private readonly IBeehiveRepository _beehiveRepository;

        public NucsController(INucRepository nucRepository, IBeeGardenRepository beeGardenRepository, IBeehiveRepository beehiveRepository)
        {
            this._nucRepository = nucRepository;
            this._beeGardenRepository = beeGardenRepository;
            this._beehiveRepository = beehiveRepository;
        }


        public IActionResult Nucs()
        {
            var nuc = new ViewModel
            {
                nucs = _nucRepository.AllNucs(),
                beeGardens = _beeGardenRepository.AllBeeGardens(),
                beehives = _beehiveRepository.AllBeehives()
            };

            return View(nuc);

        }

        [HttpGet]
        public IActionResult EditNuc(int id)
        {
            var DownloadedNucs = _nucRepository.NucsByID(id);

            var nucs = new ViewModel
            {
                nuc = DownloadedNucs,
                beeGarden = _beeGardenRepository.BeeGardenByID(DownloadedNucs.beeGardenID)
            };

            if (nucs == null)
            {
                return NotFound();
            }

            return View(nucs);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNuc(Nuc nuc)
        {

            if (ModelState.IsValid)
            {
                _nucRepository.UpdateNuc(nuc);
                return RedirectToAction("Nucs");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AddNucToNucs()
        {
            var beeGardens = new ViewModel
            {
                beeGardens = _beeGardenRepository.AllBeeGardens()
            };

            return View(beeGardens);
        }


        [HttpGet]
        public IActionResult AddNuc(int id)
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
        public IActionResult AddNuc(Nuc nuc)
        {

            if (ModelState.IsValid)
            {
                _nucRepository.AddNuc(nuc);
                return RedirectToAction("Nucs");

            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteNuc(int id)
        {
            var nuc = _nucRepository.NucsByID(id);
            return View(nuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteNuc(int id, IFormCollection form)
        {
            _nucRepository.DeleteNuc(id);
            return RedirectToAction("Nucs");
        }

    }
}
