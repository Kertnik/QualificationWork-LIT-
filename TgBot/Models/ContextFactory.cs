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
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyTgBot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
            optionsBuilder.EnableSensitiveDataLogging();
            return new DriverContext(optionsBuilder.Options);
        }
    }
}
