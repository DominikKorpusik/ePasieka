using System.Collections.Generic;

namespace ePasieka.Models
{
    public interface ICalendarEventRepository
    {
        IEnumerable<CalendarEvent> AllCalendarEvents();
        CalendarEvent CalendarEventByID(int id);
        void AddCalendarEvent(CalendarEvent calendarEvent);
        void UpdateCalendarEvent(CalendarEvent calendarEvent);
        void DeleteCalendarEvent(int id);
        void AddQueenRearing(CalendarEvent calendarEvent);
    }
}
