using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    /// <inheritdoc />
    public partial class thirtdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "MembruEchipa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembruEchipa_ManagerId",
                table: "MembruEchipa",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa");

            migrationBuilder.DropIndex(
                name: "IX_MembruEchipa_ManagerId",
                table: "MembruEchipa");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "MembruEchipa");
        }
    }
}
