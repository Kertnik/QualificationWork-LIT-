using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TgBot.Models;

namespace TgBot
{
    public static class Program
    {
        public static DriverContext GeneralContext;
        public static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
