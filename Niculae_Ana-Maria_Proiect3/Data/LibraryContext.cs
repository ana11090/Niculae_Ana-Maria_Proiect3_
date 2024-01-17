using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Models.View;
using Niculae_Ana_Maria_Proiect3.Models;

namespace Niculae_Ana_Maria_Proiect3.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) :
       base(options)
        {
        }
        public DbSet<Proiect> Proiecte { get; set; }
        public DbSet<Sarcina> Sarcini { get; set; }
        public DbSet<Comentariu> Comentarii { get; set; }
        public DbSet<MembruEchipa> MembriEchipa { get; set; }
        public DbSet<Manager> Manageri { get; set; }
        public DbSet<SarcinaMembruEchipa> SarcinaMembriEchipa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proiect>().ToTable("Proiect");
            modelBuilder.Entity<Sarcina>().ToTable("Sarcina");
            modelBuilder.Entity<Comentariu>().ToTable("Comentariu");
            modelBuilder.Entity<MembruEchipa>().ToTable("MembruEchipa");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<SarcinaMembruEchipa>().ToTable("SarcinaMembruEchipa")
                .HasKey(sme => new { sme.SarcinaId, sme.MembruEchipaId }); // Cheia compusă pentru relația many-to-many

        }

    }
}