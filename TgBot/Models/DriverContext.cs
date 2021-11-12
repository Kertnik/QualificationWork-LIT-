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

        public DbSet<Driver> MyDrivers { get; set; }
        public DbSet<Route> MyRoutes { get; set; }
        public DbSet<CurRoute> MyCurRoutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
       "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyTgBot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CurRoute>()
                .Property(b => b.Day)
                .HasDefaultValue(DateTime.Today);

        /*    modelBuilder.Entity<CurRoute>().HasOne(b => b.Driver).WithMany();
            modelBuilder.Entity<CurRoute>()
                .HasOne(b => b.Route)
                .WithMany();*/
        }
    }
}