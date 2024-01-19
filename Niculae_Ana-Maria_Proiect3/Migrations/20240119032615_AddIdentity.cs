using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizator",
                table: "Utilizator");

            migrationBuilder.RenameTable(
                name: "Utilizator",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "IdUtilizator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Utilizator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizator",
                table: "Utilizator",
                column: "IdUtilizator");
        }
    }
}
