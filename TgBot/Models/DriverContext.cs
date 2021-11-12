using System;
using Microsoft.EntityFrameworkCore;

namespace TgBot.Models
{
    public class DriverContext : DbContext
    {
        public DriverContext(DbContextOptions<DriverContext> options)
            : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<CurPath> CurPaths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CurPath>()
                .Property(b => b.Day)
                .HasDefaultValue(DateTime.Today);
            modelBuilder.Entity<Driver>()
                .HasMany(b => b.MyPaths)
                .WithOne();
            modelBuilder.Entity<CurPath>()
                .HasOne(b => b.Path)
                .WithMany();
        }
    }
}