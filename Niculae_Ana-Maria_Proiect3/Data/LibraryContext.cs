using Microsoft.EntityFrameworkCore;
using Niculae_Ana_Maria_Proiect3.Models.View;
using Niculae_Ana_Maria_Proiect3.Models;
using Microsoft.AspNetCore.Identity;

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
        public DbSet<Utilizator> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            // Proiect to Manager relationship
            modelBuilder.Entity<Proiect>()
                .HasOne(p => p.ManagerProiect)
                .WithMany(m => m.Proiecte)
                .HasForeignKey(p => p.ManagerId);

            // Sarcina to Proiect relationship
            modelBuilder.Entity<Sarcina>()
                .HasOne(s => s.ProiectAsociat)
                .WithMany(p => p.Sarcini)
                .HasForeignKey(s => s.ProiectId);

            // Sarcina to Comentariu relationship
            modelBuilder.Entity<Comentariu>()
                .HasOne(c => c.Sarcina)
                .WithMany(s => s.Comentarii)
                .HasForeignKey(c => c.SarcinaId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: to prevent cascade delete


            // Configuring many-to-many relationship between Sarcina and MembruEchipa
            modelBuilder.Entity<SarcinaMembruEchipa>()
                .HasKey(sm => new { sm.SarcinaId, sm.MembruEchipaId });

            modelBuilder.Entity<SarcinaMembruEchipa>()
                .HasOne(sm => sm.Sarcina)
                .WithMany(s => s.SarcinaMembriEchipa)
                .HasForeignKey(sm => sm.SarcinaId);

            modelBuilder.Entity<SarcinaMembruEchipa>()
                .HasOne(sm => sm.MembruEchipa)
                .WithMany(m => m.SarcinaMembriEchipa)
                .HasForeignKey(sm => sm.MembruEchipaId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.MembriEchipa)
                .WithOne(me => me.Manager)
                .HasForeignKey(me => me.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Table mappings
            modelBuilder.Entity<Proiect>().ToTable("Proiect");
            modelBuilder.Entity<Sarcina>().ToTable("Sarcina");
            modelBuilder.Entity<Comentariu>().ToTable("Comentariu");
            modelBuilder.Entity<MembruEchipa>().ToTable("MembruEchipa");
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<SarcinaMembruEchipa>().ToTable("SarcinaMembruEchipa");
            modelBuilder.Entity<Utilizator>().ToTable("Users");


        }

    }
}