using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TgBot.Models
{
    public class DriverContextFactory: IDesignTimeDbContextFactory<DriverContext>
    {


#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public DriverContext CreateDbContext(string[] args=null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            var optionsBuilder = new DbContextOptionsBuilder<DriverContext>();
            optionsBuilder.UseSqlServer("Data Source=tgbotserver.database.windows.net;Initial Catalog=TgBot;User ID=Kertnik;Password=qMu46LeSLe7KqTL;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            optionsBuilder.EnableSensitiveDataLogging();
            return new DriverContext(optionsBuilder.Options);
        }
    }
}
