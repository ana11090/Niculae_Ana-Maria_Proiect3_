﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Niculae_Ana_Maria_Proiect3.Data;

#nullable disable

namespace Niculae_Ana_Maria_Proiect3.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240118163144_2Migration")]
    partial class _2Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Comentariu", b =>
                {
                    b.Property<int>("ComentariuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComentariuId"));

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataOra")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SarcinaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComentariuId");

                    b.HasIndex("SarcinaId");

                    b.ToTable("Comentariu", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Manager", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerId"));

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerId");

                    b.ToTable("Manager", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.MembruEchipa", b =>
                {
                    b.Property<int>("MembruEchipaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MembruEchipaId"));

                    b.Property<int>("Functie")
                        .HasColumnType("int");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MembruEchipaId");

                    b.HasIndex("ManagerId");

                    b.ToTable("MembruEchipa", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Proiect", b =>
                {
                    b.Property<int>("ProiectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProiectId"));

                    b.Property<DateTime?>("DataFinalizare")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataIncepere")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descriere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProiectId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Proiect", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Sarcina", b =>
                {
                    b.Property<int>("SarcinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SarcinaId"));

                    b.Property<DateTime?>("DataFinalizare")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataIncepere")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descriere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeSarcina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProiectId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("SarcinaId");

                    b.HasIndex("ProiectId");

                    b.ToTable("Sarcina", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.View.SarcinaMembruEchipa", b =>
                {
                    b.Property<int>("SarcinaId")
                        .HasColumnType("int");

                    b.Property<int>("MembruEchipaId")
                        .HasColumnType("int");

                    b.HasKey("SarcinaId", "MembruEchipaId");

                    b.HasIndex("MembruEchipaId");

                    b.ToTable("SarcinaMembruEchipa", (string)null);
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Comentariu", b =>
                {
                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.Sarcina", "Sarcina")
                        .WithMany("Comentarii")
                        .HasForeignKey("SarcinaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Sarcina");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.MembruEchipa", b =>
                {
                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.Manager", "Manager")
                        .WithMany("MembriEchipa")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Proiect", b =>
                {
                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.Manager", "ManagerProiect")
                        .WithMany("Proiecte")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerProiect");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Sarcina", b =>
                {
                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.Proiect", "ProiectAsociat")
                        .WithMany("Sarcini")
                        .HasForeignKey("ProiectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProiectAsociat");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.View.SarcinaMembruEchipa", b =>
                {
                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.MembruEchipa", "MembruEchipa")
                        .WithMany("SarcinaMembriEchipa")
                        .HasForeignKey("MembruEchipaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Niculae_Ana_Maria_Proiect3.Models.Sarcina", "Sarcina")
                        .WithMany("SarcinaMembriEchipa")
                        .HasForeignKey("SarcinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MembruEchipa");

                    b.Navigation("Sarcina");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Manager", b =>
                {
                    b.Navigation("MembriEchipa");

                    b.Navigation("Proiecte");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.MembruEchipa", b =>
                {
                    b.Navigation("SarcinaMembriEchipa");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Proiect", b =>
                {
                    b.Navigation("Sarcini");
                });

            modelBuilder.Entity("Niculae_Ana_Maria_Proiect3.Models.Sarcina", b =>
                {
                    b.Navigation("Comentarii");

                    b.Navigation("SarcinaMembriEchipa");
                });
#pragma warning restore 612, 618
        }
    }
}
