using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Niculae_Ana_Maria_Proiect3.Models
{
    [Table("AspNetUsers")]
    public class Utilizator : IdentityUser
    {
        [Column("IdUtilizator")]
        public override string Id { get; set; }

        [Column("NumeUtilizator")]
        public override string UserName { get; set; }

        [Column("EmailUtilizator")]
        public override string Email { get; set; }

        [Column("DataInregistrarii")]
        public DateTime DataInregistrarii { get; set; }

        [Column("NumarTelefon")]
        public override string PhoneNumber { get; set; }

    }
}
