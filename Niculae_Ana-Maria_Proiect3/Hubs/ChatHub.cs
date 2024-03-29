﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Data;
using Niculae_Ana_Maria_Proiect3.Models;

namespace Niculae_Ana_Maria_Proiect3.Hubs
{
    [Authorize]
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
            user = Context.User.Identity.Name;
            var dateTime = DateTime.Now.ToString(); 
            await Clients.All.SendAsync("ReceiveMessage", user, message, dateTime);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        private async Task SaveComment(string user, string message, string sarcinaId)
        {
            int sarcina = Int32.Parse(sarcinaId);
            var comentariu = new Comentariu
            {
                Text = message,
                DataOra = DateTime.Now,
                AutorId = Context.UserIdentifier, // Utilizează direct variabila int
                SarcinaId = sarcina // Utilizează direct variabila int
            };

            _context.Comentarii.Add(comentariu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception
                Console.WriteLine($"DbUpdateException occurred: {ex.Message}");

                // Examine the inner exception for more details
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }

                // Consider how you want to handle the exception. Re-throwing or logging.
            }
            catch (Exception ex)
            {
                // Handle other exceptions if needed
                Console.WriteLine($"An error occurred while saving the comment: {ex.Message}");
            }
        }

    }
}