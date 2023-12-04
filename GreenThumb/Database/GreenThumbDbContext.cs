using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    public class GreenThumbDbContext : DbContext
    {
        private readonly IEncryptionProvider _provider;

        public GreenThumbDbContext()
        {
            _provider = new GenerateEncryptionProvider("DettaArEnSakerStrang!!!!");
        }

        public DbSet<Garden> Garden { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GreenThumbDb;Trusted_Connection=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseEncryption(_provider);

            //sätter namnet på plants till unikt.

            modelBuilder.Entity<Plant>()
                .HasIndex(p => p.Name)
                .IsUnique();
            //sätter namnet på users till unikt.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();


            //Delete behaviour på instructions
            modelBuilder.Entity<Plant>()
                .HasMany(i => i.Instructions)
                .WithOne(i => i.Plant)
                .OnDelete(DeleteBehavior.Cascade);

            //seed:ar Plants
            modelBuilder.Entity<Plant>().HasData(
                new Plant()
                {
                    PlantId = 1,
                    Name = "flower",
                },
                new Plant()
                {
                    PlantId = 2,
                    Name = "strawberry",

                },
                new Plant()
                {
                    PlantId = 3,
                    Name = "cactus",

                }
                );

            //seed:ar instructions
            modelBuilder.Entity<Instruction>().HasData(
                new Instruction()
                {
                    InstructionId = 1,
                    Description = "water the plant",
                    PlantId = 1,
                },
                new Instruction()
                {
                    InstructionId = 2,
                    Description = "harvest the plant",
                    PlantId = 2,
                },
                new Instruction()
                {
                    InstructionId = 3,
                    Description = "fertilize the plant",
                    PlantId = 3,
                }
            );
        }

    }
}
