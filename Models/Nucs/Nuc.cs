using System;
using System.ComponentModel.DataAnnotations;

namespace ePasieka.Models
{
    public class Nuc
    {
        public int nucID { get; set; }

        [Required(ErrorMessage = "Podaj nazwę")]
        public string name { get; set; }

        [Required(ErrorMessage = "Podaj liczbę ramek")]
        public int framesAmount { get; set; }

        // Jak w skali od 1-10 oceniasz siłę ula
        [Required(ErrorMessage = "Podaj siłę odkładu")]
        [Range(1, 10, ErrorMessage = "Podaj dane pomiędzy 1 a 10")]
        public int scale { get; set; }

        [Required(ErrorMessage = "Podaj datę")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowa data")]
        public DateTime inspectionDate { get; set; }

        [StringLength(250)]
        public string annotation { get; set; }

        public BeeGarden beeGarden { get; set; }
        public int beeGardenID { get; set; }

    }
}
