using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    /// <inheritdoc />
    public partial class _1Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "MembruEchipa",
                columns: table => new
                {
                    MembruEchipaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Functie = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembruEchipa", x => x.MembruEchipaId);
                    table.ForeignKey(
                        name: "FK_MembruEchipa_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Proiect",
                columns: table => new
                {
                    ProiectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIncepere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinalizare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proiect", x => x.ProiectId);
                    table.ForeignKey(
                        name: "FK_Proiect_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sarcina",
                columns: table => new
                {
                    SarcinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeSarcina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIncepere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinalizare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProiectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sarcina", x => x.SarcinaId);
                    table.ForeignKey(
                        name: "FK_Sarcina_Proiect_ProiectId",
                        column: x => x.ProiectId,
                        principalTable: "Proiect",
                        principalColumn: "ProiectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentariu",
                columns: table => new
                {
                    ComentariuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    SarcinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentariu", x => x.ComentariuId);
                    table.ForeignKey(
                        name: "FK_Comentariu_Sarcina_SarcinaId",
                        column: x => x.SarcinaId,
                        principalTable: "Sarcina",
                        principalColumn: "SarcinaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SarcinaMembruEchipa",
                columns: table => new
                {
                    SarcinaId = table.Column<int>(type: "int", nullable: false),
                    MembruEchipaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SarcinaMembruEchipa", x => new { x.SarcinaId, x.MembruEchipaId });
                    table.ForeignKey(
                        name: "FK_SarcinaMembruEchipa_MembruEchipa_MembruEchipaId",
                        column: x => x.MembruEchipaId,
                        principalTable: "MembruEchipa",
                        principalColumn: "MembruEchipaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SarcinaMembruEchipa_Sarcina_SarcinaId",
                        column: x => x.SarcinaId,
                        principalTable: "Sarcina",
                        principalColumn: "SarcinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentariu_SarcinaId",
                table: "Comentariu",
                column: "SarcinaId");

            migrationBuilder.CreateIndex(
                name: "IX_MembruEchipa_ManagerId",
                table: "MembruEchipa",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proiect_ManagerId",
                table: "Proiect",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sarcina_ProiectId",
                table: "Sarcina",
                column: "ProiectId");

            migrationBuilder.CreateIndex(
                name: "IX_SarcinaMembruEchipa_MembruEchipaId",
                table: "SarcinaMembruEchipa",
                column: "MembruEchipaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentariu");

            migrationBuilder.DropTable(
                name: "SarcinaMembruEchipa");

            migrationBuilder.DropTable(
                name: "MembruEchipa");

            migrationBuilder.DropTable(
                name: "Sarcina");

            migrationBuilder.DropTable(
                name: "Proiect");

            migrationBuilder.DropTable(
                name: "Manager");
        }
    }
}
