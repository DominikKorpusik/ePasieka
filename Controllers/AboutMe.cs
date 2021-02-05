using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePasieka.Controllers
{
    public class AboutMe : Controller
    {
        public IActionResult AboutMePage()
        {
            return View();
        }
    }
}
