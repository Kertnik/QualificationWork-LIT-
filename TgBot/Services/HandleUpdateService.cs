using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Models;
using static TgBot.Program;
namespace TgBot.Services
{
    public class HandleUpdateService
    {
        readonly ITelegramBotClient _botClient;
        readonly ILogger<HandleUpdateService> _logger;

        public HandleUpdateService(ITelegramBotClient botClient, ILogger<HandleUpdateService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }
        async Task BotOnMessageReceived(Message message)
        {
            _logger.LogInformation($"Receive message type: {message.Type}");
            Driver driver;
            using (var db = new DriverContextFactory().CreateDbContext())
            {//Explicit loading
                driver = db.MyDrivers.Single(d => d.DriverId == message.Chat.Username);
                db.Entry(driver).Reference(d => d.OrdinalRoute).Load();
                db.Entry(driver).Collection(d => d.HistoryRoutes).Load();

            }

            Task<Message>? action;
            switch (message.Text.Split(' ').First())
            {
                case "/start":
                    action = StartOfWork(_botClient, message, driver);
                    break;
                case "/set":
                    action = SetRoute(_botClient, message, driver);
                    break;
                case "/continue":
                    action = SendReplyAddKeyboard(_botClient, message, driver);
                    break;
                case "-":
                    action = RemoveKeyboard(_botClient, message, driver);
                    break;
                case "+":
                    action = SendReplyMinusKeyboard(_botClient, message, driver);
                    break;
                case "/end":
                    action = EndRoute(_botClient, message, driver, false);
                    break;
                case "🡳":
                case "🡱":
                    action = SetDirection(_botClient, message, driver); 
                    break;
                default:
                    action = Help(_botClient, message);
                    break;
            }

            Message sentMessage = await action;
            _logger.LogInformation("The message was sent with id: {sentMessageId}", sentMessage.MessageId);


        }
        static async Task<Message> StartOfWork(ITelegramBotClient bot, Message message, Driver driver)
        {
            string answer;
            if (driver.HistoryRoutes.Count == 0 || driver.HistoryRoutes.Last().IsFinished())
            {
                if (driver.OrdinalRoute == null)
                {
                    answer = "Use /set to set your Route";
                }
                else
                {

                    driver.NewRoute();
                    return await ChooseDirection(bot, message);
                }
            }
            else
            {
                answer = "You are alredy on a Route. To cancel it use /cancel";
            }

            return await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }
        static async Task<Message> ChooseDirection(ITelegramBotClient bot, Message message)
        {
            //TODO Direction By Inline Markup
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { "🡱", "🡳" }
                })
            {
                ResizeKeyboard = true
            };

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Choose direction",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> SetDirection(ITelegramBotClient bot, Message message, Driver driver)
        {
            bool direction = message.Text == "🡱" ? true : false
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                var t = db.MyCurRoutes.Find(driver.HistoryRoutes.Last().RecordID);
                db.Update(t);
                t.AddLeaving((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                await db.SaveChangesAsync();
            }
            return driver.HistoryRoutes.Last().IsFinished() ? await EndRoute(bot, message, driver, true)
                : await bot.SendTextMessageAsync(message.Chat.Id, "Keep going", replyMarkup: new ReplyKeyboardRemove());
        }


        static async Task<Message> SendReplyAddKeyboard(ITelegramBotClient bot, Message message, Driver driver)
        {

            if (driver.OrdinalRoute == null)
                return await bot.SendTextMessageAsync(message.Chat.Id, "You must choose your route firstly");
            if (driver.HistoryRoutes == null || driver.HistoryRoutes.Count == 0)
                return await bot.SendTextMessageAsync(message.Chat.Id, "You must start your route firstly");
            if (driver.HistoryRoutes.Last().IsFinished())
            {
                return await bot.SendTextMessageAsync(message.Chat.Id,
                    "Your route have ended, to start a new one type /start");
            }
            await using (var db = new DriverContextFactory().CreateDbContext())
            {

                var t = db.MyCurRoutes.Find(driver.HistoryRoutes.Last().RecordID);
                db.Update(t);
                t.AddTimeOfStop(message.Date);
                await db.SaveChangesAsync();
            }
            driver.HistoryRoutes.Last().AddTimeOfStop(message.Date);
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { "+ 0", "+ 1", "+ 2" },
                    new KeyboardButton[] { "+ 3", "+ 4", "+ 5" },
                    new KeyboardButton[] { "+ 6", "+ 7", "+ 8" },
                    new KeyboardButton[] { "+ 9", "+ 10", "+ 11" }
                })
            {
                ResizeKeyboard = true
            };

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Choose number of income",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> SendReplyMinusKeyboard(ITelegramBotClient bot, Message message, Driver driver)
        {

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { "- 0", "- 1", "- 2" },
                    new KeyboardButton[] { "- 3", "- 4", "- 5" },
                    new KeyboardButton[] { "- 6", "- 7", "- 8" },
                    new KeyboardButton[] { "- 9", "- 10", "- 11" }
                })
            {
                ResizeKeyboard = true
            };
            await using (var db = new DriverContextFactory().CreateDbContext())
            {
                var t = db.MyCurRoutes.Find(driver.HistoryRoutes.Last().RecordID);
                db.Update(t);
                t.AddIncoming((byte)Convert.ToInt16(message.Text.Split(" ")[1]));

                await db.SaveChangesAsync();
            }

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Choose number of outcome",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> RemoveKeyboard(ITelegramBotClient bot, Message message, Driver driver)
        {

            using (var db = new DriverContextFactory().CreateDbContext())
            {
                var t = db.MyCurRoutes.Find(driver.HistoryRoutes.Last().RecordID);
                db.Update(t);
                t.AddLeaving((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                await db.SaveChangesAsync();
            }
            return driver.HistoryRoutes.Last().IsFinished() ? await EndRoute(bot, message, driver, true)
            : await bot.SendTextMessageAsync(message.Chat.Id, "Keep going", replyMarkup: new ReplyKeyboardRemove());
        }

        static async Task<Message> EndRoute(ITelegramBotClient bot, Message message, Driver driver, bool success)
        {
            using (var db = new DriverContextFactory().CreateDbContext())
            {

                if (success)
                {
                    await db.SaveChangesAsync();
                    return await bot.SendTextMessageAsync(message.Chat.Id,
                    "You succesfully completed the route",
                    replyMarkup: new ReplyKeyboardRemove());

                }
                else
                {
                    driver.HistoryRoutes.Remove(driver.HistoryRoutes.Last());
                    await db.SaveChangesAsync();
                    return await bot.SendTextMessageAsync(message.Chat.Id,
                        "You succesfully canceled route",
                        replyMarkup: new ReplyKeyboardRemove());

                }
            }

        }

        static async Task<Message> SetRoute(ITelegramBotClient bot, Message message, Driver driver)
        {
            try
            {
                driver.SetRoute(message.Text.Split(" ")[1]);
            }
            catch (ArgumentNullException)
            {

                return await bot.SendTextMessageAsync(message.Chat.Id, "Bad route id, change denied");
            }
            return await bot.SendTextMessageAsync(message.Chat.Id, "Changed");
        }
        static async Task<Message> Help(ITelegramBotClient bot, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/start  - starts new route\n" +
                                 "/continue - marks a stop\n" +
                                 "/end - cancels current route\n" +
                                 "/set [idOfRoute] - sets your route \n";

            return await bot.SendTextMessageAsync(message.Chat.Id,
                usage,
                replyMarkup: new ReplyKeyboardRemove());
        }

        public async Task EchoAsync(Update update)
        {
            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.EditedMessage),
                _ => UnknownUpdateHandlerAsync(update)
            };
            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception);
            }

        }




        Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.LogInformation($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        public Task HandleErrorAsync(Exception exception)
        {
            string? ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
