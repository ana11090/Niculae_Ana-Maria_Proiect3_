using Niculae_Ana_Maria_Proiect3.Models.View;

namespace Niculae_Ana_Maria_Proiect3.Models
{
    public class MembruEchipa
    {
        public int MembruEchipaId { get; set; }
        public string Nume { get; set; }
        public string Functie { get; set; }
        public ICollection<SarcinaMembruEchipa> SarcinaMembriEchipa { get; set; } // Relația many-to-many


    }
}