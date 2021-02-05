using System;
using System.ComponentModel.DataAnnotations;

namespace ePasieka.Models
{
    public class CalendarEvent
    {
        public int calendarEventID { get; set; }

        [Required(ErrorMessage = "Podaj treść")]
        [StringLength(100)]
        public string title { get; set; }
        public string description { get; set; }

        [Required(ErrorMessage = "Data jest wymagana")]
        [DataType(DataType.DateTime, ErrorMessage = "Niepoprawna data.")]
        public DateTime start { get; set; }

        [Required(ErrorMessage = "Data jest wymagana")]
        [DataType(DataType.DateTime, ErrorMessage = "Niepoprawna data.")]
        public DateTime end { get; set; }
        public string color { get; set; }
        public bool allDay { get; set; }
        public User user { get; set; }
        public string userID { get; set; }
    }
}
