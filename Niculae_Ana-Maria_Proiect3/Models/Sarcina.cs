using Niculae_Ana_Maria_Proiect3.Models.View;

namespace Niculae_Ana_Maria_Proiect3.Models
{
    public class Sarcina
    {
        public int SarcinaId { get; set; }
        public string Descriere { get; set; }
        public DateTime? DataIncepere { get; set; }
        public DateTime? DataFinalizare { get; set; }
        public StatusSarcina Status { get; set; }
        public int ProiectId { get; set; } // Cheie străină pentru Proiect
        public Proiect? ProiectAsociat { get; set; } // Proprietatea de navigație
        public ICollection<Comentariu> Comentarii { get; set; } // Colecția de comentarii asociate sarcinii

        public ICollection<SarcinaMembruEchipa> SarcinaMembriEchipa { get; set; } // Relația many-to-many

        public Sarcina()
        {
            Status = StatusSarcina.Neinceputa;
            SarcinaMembriEchipa = new List<SarcinaMembruEchipa>();
        }
    }

    public enum StatusSarcina
    {
        Neinceputa,
        InDesfasurare,
        Finalizata
    }

}