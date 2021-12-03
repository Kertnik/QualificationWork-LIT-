using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace TgBot.Models
{
    public class TgBotContext : DbContext
    {
        public TgBotContext(DbContextOptions<TgBotContext> options)
            : base(options)
        {
        }

        public TgBotContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.ConnectionString);
            }

            ;
        }

        public DbSet<Driver> MyDrivers { get; set; }
        public DbSet<Route> MyRoutes { get; set; }
        public DbSet<CurRoute> MyCurRoutes { get; set; }

      
    }
}