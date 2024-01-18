namespace Niculae_Ana_Maria_Proiect3.Models.View
{
    public class SarcinaViewModel
    {
        public Sarcina Sarcina { get; set; }
        public List<MembruEchipaCheckboxViewModel> MembriiEchipa { get; set; }

        public SarcinaViewModel()
        {
            MembriiEchipa = new List<MembruEchipaCheckboxViewModel>();
        }
    }
}
