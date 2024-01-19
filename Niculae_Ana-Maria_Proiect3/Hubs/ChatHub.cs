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
        
        public async Task SendMessage(string user, string message, string sarcinaId)
        {
            var comentariu = new Comentariu
            {
                Text = message,
                DataOra = DateTime.Now,
                AutorId = 1, // Placeholder for actual AutorId
                SarcinaId = 1
            };

            _context.Comentarii.Add(comentariu);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveComment", user, message);
        }

    }
}