using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

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

      
    }
}