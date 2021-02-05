using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ePasieka.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public List<BeeGarden> BeeGardens { get; set; }
        [PersonalData]
        public int BeeGardenID { get; set; }
        [PersonalData]
        public List<CalendarEvent> CalendarEvents { get; set; }
        [PersonalData]
        public int CalendarEventID { get; set; }
        [PersonalData]
        public DateTime StartDate { get; set; }

    }
}
