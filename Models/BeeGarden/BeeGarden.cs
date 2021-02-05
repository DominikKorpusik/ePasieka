using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ePasieka.Models
{
    public class BeeGarden
    {
        public int beeGardenID { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string location { get; set; }

        [StringLength(30)]
        public string latitude { get; set; } 

        [StringLength(30)]
        public string longitude { get; set; } 

        [Required(ErrorMessage = "Data jest wymagana")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawna data.")]
        public DateTime treatmentDate { get; set; } 

        [StringLength(250)]
        public string annotation { get; set; }

        public List<Beehive> beehives { get; set; }

        public User user { get; set; }
        public string userID { get; set; }
    }
}
