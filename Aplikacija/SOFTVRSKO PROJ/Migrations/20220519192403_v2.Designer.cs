﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace SOFTVRSKO_PROJ.Migrations
{
    [DbContext(typeof(KonferencijaContext))]
    [Migration("20220519192403_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Feedback", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("date");

                    b.Property<int?>("KometarisaniPredavacID")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SlusalacKomentariseID")
                        .HasColumnType("int");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KometarisaniPredavacID");

                    b.HasIndex("SlusalacKomentariseID");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Models.Oblast", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Oblast");
                });

            modelBuilder.Entity("Models.Organizator", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Organizator");
                });

            modelBuilder.Entity("Models.Predavac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArchiveFlag")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OblastID")
                        .HasColumnType("int");

                    b.Property<int>("Ocena")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PutanjaSlike")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.Property<int?>("ZvanjeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OblastID");

                    b.HasIndex("ZvanjeID");

                    b.ToTable("Predavac");
                });

            modelBuilder.Entity("Models.Predavanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OblastID")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PredavacID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OblastID");

                    b.HasIndex("PredavacID");

                    b.ToTable("Predavanje");
                });

            modelBuilder.Entity("Models.ReportFeedback", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FeedbackID")
                        .HasColumnType("int");

                    b.Property<int?>("KomentarID")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FeedbackID");

                    b.HasIndex("KomentarID");

                    b.ToTable("ReportFeedback");
                });

            modelBuilder.Entity("Models.Slusalac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArchiveFlag")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Slusalac");
                });

            modelBuilder.Entity("Models.ZahtevPredavac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrganizatorID")
                        .HasColumnType("int");

                    b.Property<int?>("PredavacID")
                        .HasColumnType("int");

                    b.Property<int?>("PredavanjeID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrganizatorID");

                    b.HasIndex("PredavacID");

                    b.HasIndex("PredavanjeID");

                    b.ToTable("ZahtevPredavac");
                });

            modelBuilder.Entity("Models.ZahtevSlusalac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrganizatorID")
                        .HasColumnType("int");

                    b.Property<int?>("PredavanjeID")
                        .HasColumnType("int");

                    b.Property<int?>("SlusalacID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrganizatorID");

                    b.HasIndex("PredavanjeID");

                    b.HasIndex("SlusalacID");

                    b.ToTable("ZahtevSlusalac");
                });

            modelBuilder.Entity("Models.Zvanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Zvanje");
                });

            modelBuilder.Entity("PredavanjeSlusalac", b =>
                {
                    b.Property<int>("PredavanjeSlusalacID")
                        .HasColumnType("int");

                    b.Property<int>("PredavanjeSlusalacID1")
                        .HasColumnType("int");

                    b.HasKey("PredavanjeSlusalacID", "PredavanjeSlusalacID1");

                    b.HasIndex("PredavanjeSlusalacID1");

                    b.ToTable("PredavanjeSlusalac");
                });

            modelBuilder.Entity("Models.Feedback", b =>
                {
                    b.HasOne("Models.Predavac", "KometarisaniPredavac")
                        .WithMany("Feedbacks")
                        .HasForeignKey("KometarisaniPredavacID");

                    b.HasOne("Models.Slusalac", "SlusalacKomentarise")
                        .WithMany("feedback")
                        .HasForeignKey("SlusalacKomentariseID");

                    b.Navigation("KometarisaniPredavac");

                    b.Navigation("SlusalacKomentarise");
                });

            modelBuilder.Entity("Models.Predavac", b =>
                {
                    b.HasOne("Models.Oblast", "Oblast")
                        .WithMany()
                        .HasForeignKey("OblastID");

                    b.HasOne("Models.Zvanje", "Zvanje")
                        .WithMany()
                        .HasForeignKey("ZvanjeID");

                    b.Navigation("Oblast");

                    b.Navigation("Zvanje");
                });

            modelBuilder.Entity("Models.Predavanje", b =>
                {
                    b.HasOne("Models.Oblast", "Oblast")
                        .WithMany()
                        .HasForeignKey("OblastID");

                    b.HasOne("Models.Predavac", "Predavac")
                        .WithMany("PredavacPredmet")
                        .HasForeignKey("PredavacID");

                    b.Navigation("Oblast");

                    b.Navigation("Predavac");
                });

            modelBuilder.Entity("Models.ReportFeedback", b =>
                {
                    b.HasOne("Models.Feedback", "Feedback")
                        .WithMany()
                        .HasForeignKey("FeedbackID");

                    b.HasOne("Models.Predavac", "Komentar")
                        .WithMany("Reporteedbacks")
                        .HasForeignKey("KomentarID");

                    b.Navigation("Feedback");

                    b.Navigation("Komentar");
                });

            modelBuilder.Entity("Models.ZahtevPredavac", b =>
                {
                    b.HasOne("Models.Organizator", null)
                        .WithMany("ZahteviPredavac")
                        .HasForeignKey("OrganizatorID");

                    b.HasOne("Models.Predavac", "Predavac")
                        .WithMany("ZahteviPredavac")
                        .HasForeignKey("PredavacID");

                    b.HasOne("Models.Predavanje", "Predavanje")
                        .WithMany("ZahteviPredavac")
                        .HasForeignKey("PredavanjeID");

                    b.Navigation("Predavac");

                    b.Navigation("Predavanje");
                });

            modelBuilder.Entity("Models.ZahtevSlusalac", b =>
                {
                    b.HasOne("Models.Organizator", null)
                        .WithMany("ZahteviSlusalac")
                        .HasForeignKey("OrganizatorID");

                    b.HasOne("Models.Predavanje", "Predavanje")
                        .WithMany("ZahteviSlusalac")
                        .HasForeignKey("PredavanjeID");

                    b.HasOne("Models.Slusalac", "Slusalac")
                        .WithMany("ZahteviSlusalac")
                        .HasForeignKey("SlusalacID");

                    b.Navigation("Predavanje");

                    b.Navigation("Slusalac");
                });

            modelBuilder.Entity("PredavanjeSlusalac", b =>
                {
                    b.HasOne("Models.Predavanje", null)
                        .WithMany()
                        .HasForeignKey("PredavanjeSlusalacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Slusalac", null)
                        .WithMany()
                        .HasForeignKey("PredavanjeSlusalacID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Organizator", b =>
                {
                    b.Navigation("ZahteviPredavac");

                    b.Navigation("ZahteviSlusalac");
                });

            modelBuilder.Entity("Models.Predavac", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("PredavacPredmet");

                    b.Navigation("Reporteedbacks");

                    b.Navigation("ZahteviPredavac");
                });

            modelBuilder.Entity("Models.Predavanje", b =>
                {
                    b.Navigation("ZahteviPredavac");

                    b.Navigation("ZahteviSlusalac");
                });

            modelBuilder.Entity("Models.Slusalac", b =>
                {
                    b.Navigation("feedback");

                    b.Navigation("ZahteviSlusalac");
                });
#pragma warning restore 612, 618
        }
    }
}
