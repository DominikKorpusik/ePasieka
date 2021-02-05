using ePasieka.Models;
using ePasieka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ePasieka.Controllers
{

    /*
        Dostęp do kontrolera Beehive jest umożliwiony tylko osobom zalogowanym.
        Wykorzystano FullCalendar.
        Obsługa:
            1. Wyświetlenia strony głównej 
            2. Wyświetlania strony kontaktowej
            3. Przekazania informacji odnośnie prywatnego kalendarza użytkownika, następnie wyświetlenie go
            4. Wyświetlania wszystki zdarzeń dodanych do kolendarza
            5. Edycji, usunięcia danych typu Calendar
            6. Dodawania konkretnego sposobu wychowu matek pszczelich. 
     */

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(
            ICalendarEventRepository calendarEventRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this._calendarEventRepository = calendarEventRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult jSON()
        {
            var events = _calendarEventRepository.AllCalendarEvents().Select(e => new
            {
                id = e.calendarEventID,
                title = e.title,
                description = e.description,
                start = e.start,
                end = e.end,
                allDay = e.allDay,
                color = e.color,
            }).ToList();

            return new JsonResult(events);

        }


        [HttpGet]
        public IActionResult Calendar()
        {
            var user = new ViewModel
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };


            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calendar(CalendarEvent calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _calendarEventRepository.AddCalendarEvent(calendarEvent);
                return RedirectToAction("Calendar");

            }
            else
            {
                var CalEvent = new ViewModel
                {
                    calendarEvent = calendarEvent
                };

                return View(CalEvent);
            }

        }


        public IActionResult List()
        {
            var CalEvents = new ViewModel
            {
                calendarEvents = _calendarEventRepository.AllCalendarEvents(),

            };
            return View(CalEvents);
        }

        public IActionResult Index()
        {
            var CalEvents = new ViewModel
            {
                calendarEvents = _calendarEventRepository.AllCalendarEvents()
            };

            return View(CalEvents);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditCalendarEvent(int id)
        {
            var CalEvent = _calendarEventRepository.CalendarEventByID(id);

            if (CalEvent == null)
            {
                return NotFound();
            }

            return View(CalEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCalendarEvent(CalendarEvent calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _calendarEventRepository.UpdateCalendarEvent(calendarEvent);
                return RedirectToAction("EditCalendarEvent");
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteCalendarEvent(int id)
        {
            var CalEvent = _calendarEventRepository.CalendarEventByID(id);

            if (CalEvent == null)
            {
                return NotFound();
            }

            return View(CalEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCalendarEvent(int id, IFormCollection form)
        {

            _calendarEventRepository.DeleteCalendarEvent(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult QueenRearing()
        {
            var user = new ViewModel
            {
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QueenRearing(CalendarEvent calendarEvent)
        {
            _calendarEventRepository.AddQueenRearing(calendarEvent);
            return RedirectToAction("QueenRearing");
        }
    }
}
