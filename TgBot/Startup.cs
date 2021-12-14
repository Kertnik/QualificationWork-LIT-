using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TgBot.Models;
using TgBot.Services;

namespace TgBot
{
    public class Startup
    {
        public static string ConnectionString;

        public Startup(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("TgDb");
            Configuration = configuration;
            BotConfig = Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public IConfiguration Configuration { get; }
        BotConfiguration BotConfig { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TgBotContext>(options =>
                options.UseSqlServer(ConnectionString));
            var a = new TgBotContext(new DbContextOptionsBuilder<TgBotContext>()
                                     .UseSqlServer(ConnectionString).Options);

            if (a.Database.EnsureCreated()) throw new AccessViolationException();
            a.Database.Migrate();
            // There are several strategies for completing asynchronous tasks during startup.
            // Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
            // We are going to use IHostedService to add and later remove Webhook
            services.AddHostedService<ConfigureWebhook>();

            // Register named HttpClient to get benefits of IHttpClientFactory
            // and consume it with ITelegramBotClient typed client.
            // More read:
            //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
            //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient("tgwebhook")
                    .AddTypedClient<ITelegramBotClient>(httpClient
                         => new TelegramBotClient(BotConfig.BotToken, httpClient));

            // Dummy business-logic service
            services.AddScoped<HandleUpdateService>();

            // The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
            // incoming webhook updates and send serialized responses back.
            // Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
            //   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-5.0#add-newtonsoftjson-based-json-format-support
            services.AddControllers()
                    .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();


            app.UseEndpoints(endpoints =>
            {
                
                string? token = BotConfig.BotToken;
                endpoints.MapControllerRoute("tgwebhook",
                    $"bot/{token}",
                    new { controller = "Webhook", action = "Post" });
                endpoints.MapControllers();
            });
        }
    }
}