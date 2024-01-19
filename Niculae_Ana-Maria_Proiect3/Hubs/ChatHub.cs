using Microsoft.AspNetCore.SignalR;
using Niculae_Ana_Maria_Proiect3.Data;
using Niculae_Ana_Maria_Proiect3.Models;

namespace Niculae_Ana_Maria_Proiect3.Hubs
{
    public class ChatHub : Hub
    {
        private readonly LibraryContext _context;

        public ChatHub(LibraryContext context)
        {
            _context = context;
        }

        //public async Task SendMessage(string user, string message, string sarcinaId)
        //{
        //    var comentariu = new Comentariu
        //    {
        //        Text = message,
        //        DataOra = DateTime.Now,
        //        AutorId = 1, // Placeholder for actual AutorId
        //        SarcinaId = 1
        //    };

        //    _context.Comentarii.Add(comentariu);
        //    await _context.SaveChangesAsync();

        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        public async Task SendMessage(string user, string message, string sarcinaId)
        {
            await BroadcastMessage(user, message);
            await SaveComment(user, message, sarcinaId);
        }

        private async Task BroadcastMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        private async Task SaveComment(string user, string message, string sarcinaId)
        {
            int sarcina = Int32.Parse(sarcinaId);
            var comentariu = new Comentariu
            {
                Text = message,
                DataOra = DateTime.Now,
                AutorId = sarcina, // Utilizează direct variabila int
                SarcinaId = sarcina // Utilizează direct variabila int
            };

            _context.Comentarii.Add(comentariu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while saving the comment: {ex.Message}");
                // Consider how you want to handle the exception. Re-throwing or logging.
            }
        }

    }
}