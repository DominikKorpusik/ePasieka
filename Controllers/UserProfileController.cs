using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePasieka.Controllers
{
    /*
        Controller UserProfile
     */

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly ICalendarEventRepository _calendarEventRepository;

        public UserProfileController(
            ICalendarEventRepository calendarEventRepository)
        {
            this._calendarEventRepository = calendarEventRepository;
        }
        public IActionResult Index()
        {
            var calendarEvents = _calendarEventRepository.AllCalendarEvents();
            var data = new ViewModel
            {
                calendarEvents = calendarEvents
            };

            return View(data);
        }
    }
}