using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using TgBot.Migrations;
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
            _logger.LogInformation($"Receive message type: {message.Type}");


            using (var db = new DriverContextFactory().CreateDbContext())
            {
                if(db.MyDrivers.FirstOrDefault(d=>d.DriverId==message.From.Username)==null )
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Acsess denied");
                    return;
                }
            }
            message.Text = message.Text.Trim();

            var action = message.Text.Split(' ').First() switch
            {
                "/start" => StartOfWork(_botClient, message),
                "/continue" => SendReplyAddKeyboard(_botClient, message),
                "-" => RemoveKeyboard(_botClient, message),
                "+" => SendReplyMinusKeyboard(_botClient, message),
                "/end" => EndRoute(_botClient, message),
                _ => Help(_botClient, message)
            };

            var sentMessage = await action;
            _logger.LogInformation("The message was sent with id: {sentMessageId}", sentMessage.MessageId);


        }

        static async Task<Message> StartOfWork(ITelegramBotClient bot, Message message)
        {
            string answer;
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                var driver = (db.MyDrivers.Include(d => d.RoutesList).ThenInclude(cr => cr.Route).AsNoTracking().First(d => d.DriverId == message.From.Username));

                if (driver.RoutesList is null || driver.RoutesList.Count == 0 || driver.RoutesList.Last().IsFinished())
                {

                    string routeId = message.Text.Split(" ")[1];
                    var inputRoute = db.MyRoutes.FirstOrDefault(r => r.RouteId == routeId);
                    if (inputRoute == null)
                    {
                        answer = "Use /start correctly";
                    }
                    else
                    {
                        db.MyCurRoutes.Add(new CurRoute(DateTime.Now, inputRoute.RouteId, driver.DriverId));
                        await db.SaveChangesAsync();
                        string[] text = inputRoute.Stops.Split(";");
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
                }
                else
                {
                    answer = "You are alredy on a Route. To cancel it use /end";
                }
            }

            return await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }

        async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {

            await using (var db = new DriverContextFactory().CreateDbContext())
            {
                CurRoute curRoute;
                try
                {
                    curRoute = db.MyDrivers.Include(d => d.RoutesList)
                                .Single(d => d.DriverId == callbackQuery.From.Username).RoutesList.Last();
                    db.Entry(curRoute).Reference(c => c.Route).Load();
                }
                catch
                {
                    await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Acsess denied");
                    return;
                }
                if (curRoute.IsFromFirstStop != null)
                {
                    await _botClient.SendTextMessageAsync(
chatId: callbackQuery.Message.Chat.Id,
text: $"Route first station is alredy set, if you want change it then /end current and create needed");
                    return;
                }

                string[] stops = curRoute.Route.Stops.Split(";");
                db.Update(curRoute);
                if (callbackQuery.Data == stops[0])
                {
                    curRoute.IsFromFirstStop = true;
                }
                else if (callbackQuery.Data == stops[^1]) curRoute.IsFromFirstStop = true;
                else
                {
                    await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                        "Please choose station from given options");
                    return;
                }

                await db.SaveChangesAsync();
            }


            await _botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: $"Route started");



        }


        static async Task<Message> SendReplyAddKeyboard(ITelegramBotClient bot, Message message)
        {

            await using (var db = new DriverContextFactory().CreateDbContext())
            {
                var curRoutes = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username).RoutesList;

                if (curRoutes == null || curRoutes.Count == 0)
                    return await bot.SendTextMessageAsync(message.Chat.Id, "You must start your route firstly");

                db.Entry(curRoutes.Last()).Reference(c => c.Route).Load();

                if (curRoutes.Last().IsFromFirstStop == null || curRoutes.Last().IsFinished())
                    return await bot.SendTextMessageAsync(message.Chat.Id, "You must choose your route firstly");
          
            }
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

        static async Task<Message> SendReplyMinusKeyboard(ITelegramBotClient bot, Message message)
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
                var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username).RoutesList.Last();
                db.Update(lastRoute);

                lastRoute.AddIncoming((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                await db.SaveChangesAsync();
            }

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Choose number of outcome",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> RemoveKeyboard(ITelegramBotClient bot, Message message)
        {

            using (var db = new DriverContextFactory().CreateDbContext())
            {
                var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username).RoutesList.Last();
                db.Update(lastRoute);
                lastRoute.AddLeaving((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                lastRoute.AddTimeOfStop(message.Date);
                await db.SaveChangesAsync();
                db.Entry(lastRoute).Reference(c => c.Route).Load();
                if (lastRoute.IsFinished())
                    return await bot.SendTextMessageAsync(message.Chat.Id, "You succesfully finished route",
                          replyMarkup: new ReplyKeyboardRemove());
                else
                    return await bot.SendTextMessageAsync(message.Chat.Id, "Keep going",
                           replyMarkup: new ReplyKeyboardRemove());

            }

        }

        static async Task<Message> EndRoute(ITelegramBotClient bot, Message message)
        {
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username).RoutesList.Last();
                db.Entry(lastRoute).Reference(c => c.Route).Load();
                if (lastRoute.IsFinished()) return await bot.SendTextMessageAsync(message.Chat.Id, "Didn`t find a route",
                    replyMarkup: new ReplyKeyboardRemove());
                db.MyCurRoutes.Remove(lastRoute);
                await db.SaveChangesAsync();
                return await bot.SendTextMessageAsync(message.Chat.Id,
                    "You succesfully canceled route",
                    replyMarkup: new ReplyKeyboardRemove());
            }

        }
        static async Task<Message> Help(ITelegramBotClient bot, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/start [idOfRoute] - starts new route\n" +
                                 "/continue - marks a stop\n" +
                                 "/end - cancels current route\n";

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
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogError(exception, errorMessage);
            return Task.CompletedTask;
        }


    }
}
