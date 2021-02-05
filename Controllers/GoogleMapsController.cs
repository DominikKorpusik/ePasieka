using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePasieka.Controllers
{
    /*
        Dostęp do kontenera GoogleMaps jest umożliwiony tylko osobom zalogowanym.
        Kontener GoogleMaps, odpowiada za przekazanie danych o pasiekach użytkownika, 
        które następnie zostaną oznaczone na mapie.
     
     */


    [Authorize]
    public class GoogleMapsController : Controller
    {
        private readonly IBeeGardenRepository _beeGardenRepository;

        public GoogleMapsController(IBeeGardenRepository beeGardenRepository)
        {
            this._beeGardenRepository = beeGardenRepository;
        }
        public IActionResult Index()
        {
            var beeGardens = new ViewModel
            {
                beeGardens = _beeGardenRepository.AllBeeGardens()
            };

            return View(beeGardens);
        }
    }
}
