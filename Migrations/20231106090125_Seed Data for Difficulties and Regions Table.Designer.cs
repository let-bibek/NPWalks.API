﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NPWalks.API.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NPWalks.Migrations
{
    [DbContext(typeof(NPWalksDBContext))]
    [Migration("20231106090125_Seed Data for Difficulties and Regions Table")]
    partial class SeedDataforDifficultiesandRegionsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NPWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("335367c5-70b1-4d7f-9163-f181a8f30a4c"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("a04cf69b-9964-4cf6-b9a1-1767e6b2f406"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("89a57987-2e21-46d7-8a20-40fd8b3dab52"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("NPWalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c4dc6da0-01b4-449b-a883-eb80540c377b"),
                            Code = "KTM",
                            Name = "Kathmandu",
                            RegionImageUrl = "images/kathmandu.png"
                        },
                        new
                        {
                            Id = new Guid("e87999c6-345e-4741-a109-0f09ae53a558"),
                            Code = "PKR",
                            Name = "Pokhara",
                            RegionImageUrl = "images/pokhara.png"
                        },
                        new
                        {
                            Id = new Guid("d62652a9-f70c-40c8-9b63-24e7a0c945cc"),
                            Code = "DHG",
                            Name = "Dhading",
                            RegionImageUrl = "images/dhading.png"
                        },
                        new
                        {
                            Id = new Guid("6976f82d-d489-4d6a-ac79-ce834574fda8"),
                            Code = "BKT",
                            Name = "Bhaktapur",
                            RegionImageUrl = "images/Bhaktapur.png"
                        },
                        new
                        {
                            Id = new Guid("60deef7b-d42d-4adc-85a7-248436c36ea2"),
                            Code = "LTPR",
                            Name = "Lalitpur",
                            RegionImageUrl = "images/lalitpur.png"
                        },
                        new
                        {
                            Id = new Guid("a4db467f-36e7-42a4-b2ee-1df41d0addb6"),
                            Code = "NKWT",
                            Name = "Nuwakot",
                            RegionImageUrl = "images/nuwakot.png"
                        },
                        new
                        {
                            Id = new Guid("75916009-756f-43da-80f4-a08506c14e95"),
                            Code = "CHTWN",
                            Name = "Chitwan",
                            RegionImageUrl = "images/chitwan.png"
                        },
                        new
                        {
                            Id = new Guid("75916009-756f-43da-80f4-a08506c14e85"),
                            Code = "NWPR",
                            Name = "Nawalpur",
                            RegionImageUrl = "images/nawalpur.png"
                        });
                });

            modelBuilder.Entity("NPWalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uuid");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NPWalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("NPWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NPWalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
