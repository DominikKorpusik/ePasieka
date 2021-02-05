using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ePasieka.Models
{
    public class CalendarEventRepository : ICalendarEventRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalendarEventRepository(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._appDbContext = appDbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public void AddCalendarEvent(CalendarEvent calendarEvent)
        {
            _appDbContext.calendarEvents.Add(calendarEvent);
            _appDbContext.SaveChanges();
        }

        public void AddQueenRearing(CalendarEvent calendarEvent)
        {
            var start = calendarEvent.start;
            var color = calendarEvent.color;
            var userID = calendarEvent.userID;

            _appDbContext.AddRange(
                    new CalendarEvent { title = "Dzień 1: jajo stojące", start = start, end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 2: jajo pochylone", start = start.AddDays(1), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 3: jajo leżące", start = start.AddDays(2), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 4: wykluwająca się larwa (przekładanie larwy)", start = start.AddDays(3), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 5: Nie wykonywać żadnych prac", start = start.AddDays(4), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 6: Nie wykonywać żadnych prac", start = start.AddDays(5), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 7: Nie wykonywać żadnych prac", start = start.AddDays(6), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 8: pszczoły zaczynają zasklepiać matecznik", start = start.AddDays(7), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 9: larwa się prostuje i zaczyna prząść kokon", start = start.AddDays(8), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 10: larwa przedząca", start = start.AddDays(9), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 11: przedpoczwarka", start = start.AddDays(10), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 12: poczwarka", start = start.AddDays(11), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 13: Nie wykonywać żadnych prac", start = start.AddDays(12), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 14: (izolowanie mateczników w klateczkach)", start = start.AddDays(13), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 15: Nie wykonywać żadnych prac", start = start.AddDays(14), end = start, color = color, userID = userID, allDay = true },
                    new CalendarEvent { title = "Dzień 16: wygryzające się matki (znakowanie matek)", start = start.AddDays(15), end = start, color = color, userID = userID, allDay = true }
                );

            _appDbContext.SaveChanges();
        }

        public IEnumerable<CalendarEvent> AllCalendarEvents()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.calendarEvents.Where(e => e.userID == user).OrderByDescending(e => e.start);
        }

        public CalendarEvent CalendarEventByID(int id)
        {
            return _appDbContext.calendarEvents.FirstOrDefault(e => e.calendarEventID == id);
        }

        public void DeleteCalendarEvent(int id)
        {

            var calendarEvent = _appDbContext.calendarEvents.Find(id);
            _appDbContext.calendarEvents.Remove(calendarEvent);
            _appDbContext.SaveChanges();
        }

        public void UpdateCalendarEvent(CalendarEvent calendarEvent)
        {
            //optymistyczna współbieżność
            var entry = _appDbContext.Entry(calendarEvent);
            entry.State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
