﻿// <auto-generated />
using System;
using GreenThumb.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenThumb.Migrations
{
    [DbContext(typeof(GreenThumbDbContext))]
    [Migration("20231204101355_initcreation")]
    partial class initcreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GardenPlant", b =>
                {
                    b.Property<int>("GardensGardenId")
                        .HasColumnType("int");

                    b.Property<int>("PlantsPlantId")
                        .HasColumnType("int");

                    b.HasKey("GardensGardenId", "PlantsPlantId");

                    b.HasIndex("PlantsPlantId");

                    b.ToTable("GardenPlant");
                });

            modelBuilder.Entity("GreenThumb.Models.Garden", b =>
                {
                    b.Property<int>("GardenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GardenId"), 1L, 1);

                    b.Property<int>("Level")
                        .HasColumnType("int")
                        .HasColumnName("level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int?>("Size")
                        .HasColumnType("int")
                        .HasColumnName("size");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("GardenId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Garden");
                });

            modelBuilder.Entity("GreenThumb.Models.Instruction", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("PlantId")
                        .HasColumnType("int")
                        .HasColumnName("plant_id");

                    b.HasKey("InstructionId");

                    b.HasIndex("PlantId");

                    b.ToTable("Instruction");

                    b.HasData(
                        new
                        {
                            InstructionId = 1,
                            Description = "water the plant",
                            PlantId = 1
                        },
                        new
                        {
                            InstructionId = 2,
                            Description = "harvest the plant",
                            PlantId = 2
                        },
                        new
                        {
                            InstructionId = 3,
                            Description = "fertilize the plant",
                            PlantId = 3
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.Plant", b =>
                {
                    b.Property<int>("PlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlantId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.HasKey("PlantId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Plant");

                    b.HasData(
                        new
                        {
                            PlantId = 1,
                            Name = "flower"
                        },
                        new
                        {
                            PlantId = 2,
                            Name = "strawberry"
                        },
                        new
                        {
                            PlantId = 3,
                            Name = "cactus"
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("UserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("GardenPlant", b =>
                {
                    b.HasOne("GreenThumb.Models.Garden", null)
                        .WithMany()
                        .HasForeignKey("GardensGardenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenThumb.Models.Plant", null)
                        .WithMany()
                        .HasForeignKey("PlantsPlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreenThumb.Models.Garden", b =>
                {
                    b.HasOne("GreenThumb.Models.User", "User")
                        .WithOne("Garden")
                        .HasForeignKey("GreenThumb.Models.Garden", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenThumb.Models.Instruction", b =>
                {
                    b.HasOne("GreenThumb.Models.Plant", "Plant")
                        .WithMany("Instructions")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("GreenThumb.Models.Plant", b =>
                {
                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("GreenThumb.Models.User", b =>
                {
                    b.Navigation("Garden");
                });
#pragma warning restore 612, 618
        }
    }
}
