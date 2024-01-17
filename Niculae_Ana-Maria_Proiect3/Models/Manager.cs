namespace Niculae_Ana_Maria_Proiect3.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string Nume { get; set; }
        public ICollection<Proiect> Proiecte { get; set; }

        public Manager()
        {
            Proiecte = new List<Proiect>();
        }

    }
}
