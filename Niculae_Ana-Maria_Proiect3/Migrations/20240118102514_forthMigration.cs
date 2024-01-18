using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    /// <inheritdoc />
    public partial class forthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "MembruEchipa",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "MembruEchipa",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MembruEchipa_Manager_ManagerId",
                table: "MembruEchipa",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId");
        }
    }
}
