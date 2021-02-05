using System;
using System.ComponentModel.DataAnnotations;

namespace ePasieka.Models
{
    public class Beehive
    {
        public int beeHiveID { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [StringLength(25)]
        public string name { get; set; }

        [Required(ErrorMessage = "Liczba ramek jest wymagana")]
        public int framesAmount { get; set; } 

        [Required(ErrorMessage = "Siła ula jest wymagana")]
        [Range(1, 10, ErrorMessage = "Skala (1-10)")]
        public int scale { get; set; }

        [Required(ErrorMessage = "Podaj datę")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowa data")]
        public DateTime inspectionDate { get; set; }

        [Required(ErrorMessage = "Podaj datę")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowa data")]
        public DateTime queenExchangeDate { get; set; }

        public string botttomBoard { get; set; } //dennica

        [StringLength(40)]
        public string breed { get; set; }

        [StringLength(250)]
        public string annotation { get; set; }

        [StringLength(40)]
        public string beehiveType { get; set; } 

        [Required(ErrorMessage = "Podaj dane")]
        public decimal honeyAmount { get; set; }

        public BeeGarden beeGarden { get; set; }
        public int beeGardenID { get; set; }
    }
}
