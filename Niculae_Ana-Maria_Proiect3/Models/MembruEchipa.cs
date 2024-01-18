using Niculae_Ana_Maria_Proiect3.Models.View;
using System.ComponentModel.DataAnnotations;

namespace Niculae_Ana_Maria_Proiect3.Models
{
    public class MembruEchipa
    {
        public int MembruEchipaId { get; set; }

        [Required(ErrorMessage = "Introduceti numele membrului echipei")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Alegeti functia membrului echipei")]
        public Functie Functie { get; set; } 

        // ID-ul managerului asociat
        public int? ManagerId { get; set; }

        // Navigational property pentru Manager
        public Manager Manager { get; set; }

        public ICollection<SarcinaMembruEchipa> SarcinaMembriEchipa { get; set; } // Relația many-to-many

        public MembruEchipa()
        {
            SarcinaMembriEchipa = new List<SarcinaMembruEchipa>();
        }
    }

    public enum Functie
    {
        Manager,
        Programator,
        HR,
        Designer,
        Analist
    }
}