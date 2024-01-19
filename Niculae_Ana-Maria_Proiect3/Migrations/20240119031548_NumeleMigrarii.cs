using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    /// <inheritdoc />
    public partial class NumeleMigrarii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AutorId",
                table: "Comentariu",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Utilizator",
                columns: table => new
                {
                    IdUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeUtilizator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailUtilizator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInregistrarii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumarTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizator", x => x.IdUtilizator);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilizator");

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Comentariu",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
