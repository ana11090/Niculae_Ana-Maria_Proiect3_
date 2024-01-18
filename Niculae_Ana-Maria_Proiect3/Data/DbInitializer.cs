using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Models;

namespace Niculae_Ana_Maria_Proiect3.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                // Ensure the database is created
                context.Database.EnsureCreated();

                // Check if any projects are already in the database
                if (context.Proiecte.Any())
                {
                    return; // Database has been seeded already
                }

                // Add initial data here

                // Adding some Managers
                var managers = new Manager[]
                {
                    new Manager { Nume = "Ion Popescu" },
                    new Manager { Nume = "Maria Ionescu" }
                    // More managers
                };

                foreach (var manager in managers)
                {
                    context.Manageri.Add(manager);
                }
                context.SaveChanges();

                // Adding some Projects
                var projects = new Proiect[]
                {
                    new Proiect { Nume = "Proiect 1", ManagerId = managers[0].ManagerId, Status = StatusProiect.InAsteptare },
                    new Proiect { Nume = "Proiect 2", ManagerId = managers[1].ManagerId, Status = StatusProiect.Completat }
                    // More projects
                };

                foreach (var project in projects)
                {
                    context.Proiecte.Add(project);
                }
                context.SaveChanges();

                // Adding some Tasks (Sarcini)
                var tasks = new Sarcina[]
                {
                    new Sarcina { Descriere = "Sarcina 1 pentru Proiect 1", ProiectId = projects[0].ProiectId, Status = StatusSarcina.Neinceputa },
                    new Sarcina { Descriere = "Sarcina 2 pentru Proiect 2", ProiectId = projects[1].ProiectId, Status = StatusSarcina.InDesfasurare }
                    // More tasks
                };

                foreach (var task in tasks)
                {
                    context.Sarcini.Add(task);
                }
                context.SaveChanges();

                // You can continue adding initial data for other entities (Comentariu, MembruEchipa, SarcinaMembruEchipa, etc.)

                // Ensure to save the context after adding each entity type
            }
        }
    }
}