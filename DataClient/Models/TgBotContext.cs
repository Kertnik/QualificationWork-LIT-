using System.Data.Entity;
using DataClient.Models;

namespace DataClient;

public class TgBotContext : DbContext
{
    public TgBotContext()
        : base("name=TgBot")
    {
    }

    public virtual DbSet<MyCurRoute> MyCurRoutes { get; set; }
    public virtual DbSet<MyDriver> MyDrivers { get; set; }
    public virtual DbSet<MyRoute> MyRoutes { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyCurRoute>()
                    .Property(e => e.TimeOfStops)
                    .IsUnicode(false);

        modelBuilder.Entity<MyCurRoute>()
                    .Property(e => e.NumberOfLeaving)
                    .IsUnicode(false);

        modelBuilder.Entity<MyCurRoute>()
                    .Property(e => e.NumberOfIncoming)
                    .IsUnicode(false);
    }
}