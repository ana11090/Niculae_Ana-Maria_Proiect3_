using System.ComponentModel.DataAnnotations;

namespace Niculae_Ana_Maria_Proiect3.Models
{
    public class Proiect
    {
       
        public int ProiectID { get; set; }

        [Required(ErrorMessage = "Introduceti numele proiectului")]
        public string Nume { get; set; }

        public string? Descriere { get; set; }

        public DateTime? DataIncepere { get; set; }

        public DateTime? DataFinalizare { get; set; }

        [Required(ErrorMessage = "Alegeti statusul proiectului")]
        public StatusProiect Status { get; set; }

        [Required(ErrorMessage = "Alegeti managerul proiectului")]
        public int ManagerID { get; set; }

        public Manager ManagerProiect { get; set; }

        public ICollection<Sarcina> Sarcini { get; set; }


        public Proiect()
        {
            Sarcini = new List<Sarcina>();
            Status = StatusProiect.InAsteptare;
        }
    }

    public enum StatusProiect
    {
        InAsteptare,
        InDesfasurare,
        Completat,
        Suspendat
    }
}