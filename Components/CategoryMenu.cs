using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePasieka.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly IBeeGardenRepository _beeGardenRepository;

        public CategoryMenu(IBeeGardenRepository beeGardenRepository)
        {
            this._beeGardenRepository = beeGardenRepository;
        }

        public IViewComponentResult Invoke()
        {
            var allBeeGardens = _beeGardenRepository.AllBeeGardens();
            var vm = new ViewModel
            {
                beeGardens = allBeeGardens
            };
            return View(vm);
        }


    }
}
