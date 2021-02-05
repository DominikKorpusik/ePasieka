using ePasieka.Models;
using System;
using System.Collections.Generic;

namespace ePasieka.ViewModels
{
    public class ViewModel
    {
        public IEnumerable<Beehive> beehives { set; get; }
        public Beehive beehive { set; get; }

        public IEnumerable<BeeGarden> beeGardens { set; get; }
        public BeeGarden beeGarden { set; get; }

        public IEnumerable<Nuc> nucs { get; set; }
        public Nuc nuc { get; set; }

        public IEnumerable<CalendarEvent> calendarEvents { get; set; }
        public CalendarEvent calendarEvent { get; set; }

        public string userID { get; set; }
        public DateTime DateTimeNow
        {
            get { return DateTime.Now; }
        }


    }
}
