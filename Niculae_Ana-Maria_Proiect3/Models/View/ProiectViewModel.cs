namespace Niculae_Ana_Maria_Proiect3.Models.View
{
    public class ProiectViewModel
    {
        public int ProiectId { get; set; }
        public string Nume { get; set; }
        public string? Descriere { get; set; }
        public DateTime? DataIncepere { get; set; }
        public DateTime? DataFinalizare { get; set; }
        public StatusProiect Status { get; set; }
        public int ManagerId { get; set; }
        public string ManagerNume { get; set; }
        public int NumarSarcini { get; set; }
    }

}
