using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Models;
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
            Driver driver;
            _logger.LogInformation($"Receive message type: {message.Type}");
            try
            {
                driver = GetDriver(message.Chat.Username);
            }
            catch
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Acsess denied");
                return;
            }
            message.Text = message.Text.Trim();

            var action = message.Text.Split(' ').First() switch
            {
                "/start" => StartOfWork(_botClient, message, driver),
                "/set" => SetRoute(_botClient, message, driver),
                "/continue" => SendReplyAddKeyboard(_botClient, message, driver),
                "-" => RemoveKeyboard(_botClient, message, driver),
                "+" => SendReplyMinusKeyboard(_botClient, message, driver),
                "/end" => EndRoute(_botClient, message, driver),
                _ => Help(_botClient, message)
            };

            var sentMessage = await action;
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

                    return await ChooseDirection(bot, message, driver);
                }
            }
            else
            {
                answer = "You are alredy on a Route. To cancel it use /end";
            }

            return await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }
        static async Task<Message> ChooseDirection(ITelegramBotClient bot, Message message, Driver driver)
        {
            //TODO Direction By Inline Markup

            var text = driver.OrdinalRoute.Stops.Split(";");
            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {

                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(text[0], text[0]),
                        InlineKeyboardButton.WithCallbackData(text[^1], text[^1]),
                    }
                });

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                text: "Choose your station",
                replyMarkup: inlineKeyboard);
        }
        async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            Driver driver;
            try
            {
                driver = GetDriver(callbackQuery.From.Username);
            }
            catch
            {
                await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Acsess denied");
                return;
            }
            string[] stops = driver.OrdinalRoute.Stops.Split(";");

            if (callbackQuery.Data == stops[0]) driver.NewRoute(true);
            else if (callbackQuery.Data == stops[^1]) driver.NewRoute(false);
            else { await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Wrong station please use /start once again or contact administrator"); return; }

            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message.Chat.Id,
                text: $"Route started");
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

                var t = driver.HistoryRoutes.Last();
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
                var t = driver.HistoryRoutes.Last();
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
                var t = driver.HistoryRoutes.Last();
                t.AddLeaving((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                await db.SaveChangesAsync();
            }
            return driver.HistoryRoutes.Last().IsFinished() ? await EndRoute(bot, message, driver)
            : await bot.SendTextMessageAsync(message.Chat.Id, "Keep going", replyMarkup: new ReplyKeyboardRemove());
        }

        static async Task<Message> EndRoute(ITelegramBotClient bot, Message message, Driver driver)
        {
            using (var db = new DriverContextFactory().CreateDbContext())
            {

                if (driver.HistoryRoutes.Last().IsFinished())
                {
                    await db.SaveChangesAsync();
                    return await bot.SendTextMessageAsync(message.Chat.Id,
                    "You succesfully completed the route",
                    replyMarkup: new ReplyKeyboardRemove());

                }

                db.Update(driver);
                db.MyCurRoutes.Remove(driver.HistoryRoutes.Last());
                driver.HistoryRoutes.Remove(driver.HistoryRoutes.Last());
                await db.SaveChangesAsync();
                return await bot.SendTextMessageAsync(message.Chat.Id,
                    "You succesfully canceled route",
                    replyMarkup: new ReplyKeyboardRemove());
            }

        }

        static async Task<Message> SetRoute(ITelegramBotClient bot, Message message, Driver driver)
        {
            if (driver.HistoryRoutes.Count != 0 && !driver.HistoryRoutes.Last().IsFinished()) return await bot.SendTextMessageAsync(message.Chat.Id, "End your route firstly");

            if (! driver.SetRoute(message.Text.Split(" ")[^1]).Result) return await bot.SendTextMessageAsync(message.Chat.Id, "Bad route id, change denied");

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
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
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

        static Driver GetDriver(string username)
        {
            Driver driver;
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                driver = db.MyDrivers.Include(d => d.OrdinalRoute).Include(d=>d.HistoryRoutes).ThenInclude(cr=>cr.Route).AsNoTracking().First(d=>d.DriverId==username);

            }

            return driver;
        }
    }
}
