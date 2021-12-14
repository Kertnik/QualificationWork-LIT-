using System;
using System.Collections.Generic;
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


            await using (var db = new TgBotContext())
            {
                if (db.MyDrivers.FirstOrDefault(d => d.DriverId == message.From.Username) == null)
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Acsess denied");
                    return;
                }
            }

            message.Text = message.Text.Trim();

            Task<Message>? action = message.Text.Split(' ').First() switch
            {
                "/start" => StartOfWork(_botClient, message),
                "/continue" => SendReplyAddKeyboard(_botClient, message),
                "-" => RemoveKeyboard(_botClient, message),
                "+" => SendReplyMinusKeyboard(_botClient, message),
                "/end" => EndRoute(_botClient, message),
                "/routes" => ListRoute(_botClient, message),
                "/undo" => Undo(_botClient, message),
                _ => Help(_botClient, message)
            };

            var sentMessage = await action;
            _logger.LogInformation("The message was sent with id: {sentMessageId}", sentMessage.MessageId);
        }

        static async Task<Message> Undo(ITelegramBotClient botClient, Message message)
        {
            using (var db = new TgBotContext())
            {
                //    var driver = db.MyDrivers.Include(d => d.RoutesList).ThenInclude(cr => cr.Route).AsNoTracking()
                //                   .First(d => d.DriverId == message.From.Username);
                var driver = db.MyDrivers.Find(message.From.Username);
                await db.Entry(driver).Collection(d => d.RoutesList).LoadAsync();

                if (driver.RoutesList is null || (driver.RoutesList.Count == 0))
                {
                    return await botClient.SendTextMessageAsync(message.Chat.Id, "Даних для видалення не знайдено");
                }

                db.Entry(driver.RoutesList.Last()).Reference(r => r.Route).Load();
                if (driver.RoutesList.Last().IsFinished())return await botClient.SendTextMessageAsync(message.Chat.Id, "Даних для видалення не знайдено");
                if (driver.RoutesList.Last().DeleteFakeData())
                {
                    await db.SaveChangesAsync();
                    return await botClient.SendTextMessageAsync(message.Chat.Id, "Дані видалено",
                        replyMarkup: new ReplyKeyboardRemove());
                }
                else
                {
                    return await botClient.SendTextMessageAsync(message.Chat.Id, "Дані про першу станцію не можна редагувати \n Підказка: використовуйте /end для видалення поточного маршруту",
                        replyMarkup: new ReplyKeyboardRemove());
                }
            }
        }

        static async Task<Message> ListRoute(ITelegramBotClient botClient, Message message)
        {
            using (var db = new TgBotContext())
            {
                var arr = db.MyRoutes.ToArray();
                var inlineKeyboardMarkup = new InlineKeyboardButton[arr.Length][];
                for (int i = 0; i < inlineKeyboardMarkup.Length; i++)
                {
                    string[] stops = arr[i].Stops.Split(';');
                    inlineKeyboardMarkup[i] = new[] { InlineKeyboardButton.WithCallbackData(stops[0] + "→" + stops[^1], arr[i].RouteId) };
                }

                return await botClient.SendTextMessageAsync(
                    message.Chat.Id,
                    "Маршрут розпочато. Використовуйте /continue для позначання зупинки", replyMarkup: new InlineKeyboardMarkup(inlineKeyboardMarkup));
            }

        }
        private async Task BotOnCallbackQueryReceived(CallbackQuery updateCallbackQuery)
        {
            await using (var db = new TgBotContext())
            {
                await _botClient.SendTextMessageAsync(updateCallbackQuery.Message.Chat.Id, updateCallbackQuery.Data + ":\n" + string.Join('\n', db.MyRoutes.Find(updateCallbackQuery.Data).Stops.Split(";")));
            }
        }

        static async Task<Message> StartOfWork(ITelegramBotClient bot, Message message)
        {
            string answer;
            using (var db = new TgBotContext())
            {
                var driver = db.MyDrivers.Include(d => d.RoutesList).ThenInclude(cr => cr.Route).AsNoTracking()
                               .First(d => d.DriverId == message.From.Username);

                if (driver.RoutesList is null || (driver.RoutesList.Count == 0) || driver.RoutesList.Last().IsFinished())
                {
                    Route inputRoute;
                    byte startNumber;
                    try
                    {
                        string[] messageValues = message.Text.Split(" ");
                        inputRoute = db.MyRoutes.First(r => r.RouteId == messageValues[1]);
                        startNumber = Convert.ToByte(messageValues[2]);
                        if (startNumber < 0) throw new ArgumentException("Minus argument is not acceptable");
                    }
                    catch
                    {
                        return await bot.SendTextMessageAsync(message.Chat.Id, "Використовуйте /start коректно \nПриклад:/start M1 12");
                    }
                    var entity = new CurRoute(DateTime.Now, inputRoute.RouteId, driver.DriverId);
                    db.MyCurRoutes.Add(entity);
                    entity.AddIncoming(startNumber);
                    entity.AddLeaving(0);
                    await db.SaveChangesAsync();


                    return await bot.SendTextMessageAsync(
                         message.Chat.Id,
                         "Маршрут розпочато. Використовуйте /continue для позначання зупинки");

                }
                else
                {
                    answer = "Ви вже почали маршрут. Щоб завершити його уведіть /end";
                }
            }

            return await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }



        static async Task<Message> SendReplyAddKeyboard(ITelegramBotClient bot, Message message)
        {
            string nameOfStation;

            await using (var db = new TgBotContext())
            {
                var curRoutes = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username)
                                  .RoutesList;

                if ((curRoutes == null) || (curRoutes.Count == 0))
                {
                    return await bot.SendTextMessageAsync(message.Chat.Id, "Ви мусите спочатку почати маршрут");
                }

                db.Entry(curRoutes.Last()).Reference(c => c.Route).Load();
                if (curRoutes.Last().IsFinished()) return await bot.SendTextMessageAsync(message.Chat.Id, "Ви мусите спочатку почати маршрут");
                string[] numberOfLeaving = curRoutes.Last().NumberOfLeaving.Split(";");
                string[] numberOfIncoming = curRoutes.Last().NumberOfIncoming.Split(";");
                if(numberOfIncoming.Length!= numberOfLeaving.Length) return await bot.SendTextMessageAsync(message.Chat.Id, "Введіть усі дані");
                if (curRoutes.Last().NumberOfIncoming.Split(";").Length ==
                    curRoutes.Last().Route.Stops.Split(";").Length - 1)
                {
                    curRoutes.Last().AddIncoming(0);
                    curRoutes.Last().AddTimeOfStop(DateTime.Now);
                    curRoutes.Last().AddLeaving(
                        (byte)(numberOfIncoming.Select(x => Convert.ToInt32(x)).Sum()
                               - numberOfLeaving.Select(x => Convert.ToInt32(x)).Sum()));
                    await db.SaveChangesAsync();
                    return await bot.SendTextMessageAsync(message.Chat.Id, "Ви успішно завершили маршрут",
                        replyMarkup: new ReplyKeyboardRemove());
                }
                

                nameOfStation = curRoutes.Last().Route.Stops.Split(';')[numberOfLeaving.Length];

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
                "Станція:" + nameOfStation + ". Оберіть кількість нових пасажирів",
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
            await using (var db = new TgBotContext())
            {
                   var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username)
                                  .RoutesList.Last();
                   string[] numberOfLeaving = lastRoute.NumberOfLeaving.Split(";");
                   string[] numberOfIncoming = lastRoute.NumberOfIncoming.Split(";");
                   if(numberOfIncoming.Length!= numberOfLeaving.Length) return await bot.SendTextMessageAsync(message.Chat.Id, "Введіть усі дані");

                db.Update(lastRoute);

                lastRoute.AddIncoming((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                await db.SaveChangesAsync();
            }

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Оберіть кількість пасажирів, котрі покинули транспорт",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> RemoveKeyboard(ITelegramBotClient bot, Message message)
        {
            await using (var db = new TgBotContext())
            {
                var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username)
                                  .RoutesList.Last();
                string[] numberOfLeaving = lastRoute.NumberOfLeaving.Split(";");
                string[] numberOfIncoming = lastRoute.NumberOfIncoming.Split(";");
                if(numberOfIncoming.Length-1!= numberOfLeaving.Length) return await bot.SendTextMessageAsync(message.Chat.Id, "Введіть усі дані");

                db.Update(lastRoute);
                lastRoute.AddLeaving((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
                lastRoute.AddTimeOfStop(DateTime.Now);
                await db.SaveChangesAsync();
                await db.Entry(lastRoute).Reference(c => c.Route).LoadAsync();


                return await bot.SendTextMessageAsync(message.Chat.Id, "Продовжуйте маршрут (/continue)",
                    replyMarkup: new ReplyKeyboardRemove());
            }
        }

        static async Task<Message> EndRoute(ITelegramBotClient bot, Message message)
        {
            await using (var db = new TgBotContext())
            {
                var lastRoute = db.MyDrivers.Include(d => d.RoutesList).Single(d => d.DriverId == message.From.Username)
                                  .RoutesList.Last();
                db.Entry(lastRoute).Reference(c => c.Route).Load();
                if (lastRoute.IsFinished())
                {
                    return await bot.SendTextMessageAsync(message.Chat.Id, "Маршрут для видалення не знайдено",
                        replyMarkup: new ReplyKeyboardRemove());
                }

                db.MyCurRoutes.Remove(lastRoute);
                await db.SaveChangesAsync();
                return await bot.SendTextMessageAsync(message.Chat.Id,
                    "Ви, успішно скасували маршрут",
                    replyMarkup: new ReplyKeyboardRemove());
            }
        }

        static async Task<Message> Help(ITelegramBotClient bot, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/start [кодМаршрута] [кількістьПасажирів] - починає новий маршрут\n" +
                                 "/continue - позначає зупинку\n" +
                                 "/undo видаляє останні введені дані"+
                                 "/end - скасовує маршрут\n" +
                                 "/routes - список маршруты\n" + 
                                 "/usage - показує цей текст\n" +
                                 "Увага! Усі символи повинні вводитись латиницею";

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
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
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