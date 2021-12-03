using System;
using System.Collections.Generic;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace StatisticViewer.Models
{
    public partial class TgBotContext : DbContext
    {
        public TgBotContext()
        {
        }

        public TgBotContext(DbContextOptions<TgBotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MyCurRoute> MyCurRoutes { get; set; } = null!;
        public virtual DbSet<MyDriver> MyDrivers { get; set; } = null!;
        public virtual DbSet<MyRoute> MyRoutes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=tgbotserver.database.windows.net;Initial Catalog=TgBot;User ID=Kertnik;Password=qMu46LeSLe7KqTL;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
            /* public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
})
                   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }*/

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyCurRoute>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.HasIndex(e => e.DriverId, "IX_MyCurRoutes_DriverId");

                entity.HasIndex(e => e.RouteId, "IX_MyCurRoutes_RouteId");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.DriverId).HasDefaultValueSql("(N'')");

                entity.Property(e => e.NumberOfIncoming)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfLeaving)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RouteId).HasDefaultValueSql("(N'')");

                entity.Property(e => e.TimeOfStops).IsUnicode(false);

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.MyCurRoutes)
                    .HasForeignKey(d => d.DriverId);

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.MyCurRoutes)
                    .HasForeignKey(d => d.RouteId);
            });

            modelBuilder.Entity<MyDriver>(entity =>
            {
                entity.HasKey(e => e.DriverId);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<MyRoute>(entity =>
            {
                entity.HasKey(e => e.RouteId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
