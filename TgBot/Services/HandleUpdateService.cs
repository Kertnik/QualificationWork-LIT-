using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Models;
using static TgBot.Program;
namespace TgBot.Services
{
    public class HandleUpdateService
    { readonly ITelegramBotClient _botClient;
        readonly ILogger<HandleUpdateService> _logger;

        public HandleUpdateService(ITelegramBotClient botClient, ILogger<HandleUpdateService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        static async Task<Message> SendReplyAddKeyboard(ITelegramBotClient bot, Message message, Driver driver)
        {
            var a = driver.MyRoutes.Last();
            if (a.TimeOfStops.Count == a.Route.NumberOfStops)
            {
                return await bot.SendTextMessageAsync(message.Chat.Id,
                    "To end Route type /end");
            }

            a.TimeOfStops.Add(message.Date);
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
            driver.MyRoutes.Last().NumberOfIncoming.Add((byte)Convert.ToInt16(message.Text.Split(" ")[1]));
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

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Choose number of outcome",
                replyMarkup: replyKeyboardMarkup);
        }

        static async Task<Message> RemoveKeyboard(ITelegramBotClient bot, Message message, Driver driver)
        {
            driver.MyRoutes.Last().NumberOfLeaveing.Add((byte)Convert.ToInt16(message.Text.Split(" ")[1]));

            return await bot.SendTextMessageAsync(message.Chat.Id,
                "Removing keyboard",
                replyMarkup: new ReplyKeyboardRemove());
        }

        async Task BotOnMessageReceived(Message message)
        {
            await _botClient.SendTextMessageAsync(message.Chat.Id,
                "Handled");
            _logger.LogInformation($"Receive message type: {message.Type}");
            Driver driver = null;
            if (!UserExists())
            {
                await _botClient.SendTextMessageAsync(message.Chat,
                    "Acsess denied");
                return;
            }

            var action = message.Text.Split(' ').First() switch
            {
                "/start" => StartOfWork(_botClient, message, driver),
                "/set" => ChangeRoute(_botClient, message, driver),
                "/continue" => SendReplyAddKeyboard(_botClient, message, driver),
                "-" => RemoveKeyboard(_botClient, message, driver),
                "+" => SendReplyMinusKeyboard(_botClient, message, driver),
                "/cancel" => SendReplyAddKeyboard(_botClient, message, driver),
                _ => Help(_botClient, message)
            };

            _logger.LogInformation($"The message was sent with id: {action.Result.MessageId}");

            bool UserExists()
            {
                foreach (var variable in GeneralContext.MyDrivers)
                    if (variable.DriverId == message.From.Username)
                    {
                        driver = variable;
                        return true;
                    }

                return false;
            }
        }

        static async Task<Message> StartOfWork(ITelegramBotClient bot, Message message, Driver driver)
        {
            string answer;
            if (driver.MyRoutes.Last().IsFinished())
            {
                if (driver.OrdinalRoute == null)
                {
                    answer = "Use /set to set your Route";
                }
                else
                {
                    answer = "New Route started";
                    driver.NewRoute();
                }
            }
            else
            {
                answer = "You alredy on Route. To cancel it use /cancel";
            }

            return await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }

        static async Task<Message> ChangeRoute(ITelegramBotClient bot, Message message, Driver driver)
        {
            driver.SetRoute(await GeneralContext.MyRoutes.FindAsync(message.Text.Split(" ")[1]));
            return await bot.SendTextMessageAsync(message.Chat.Id, "Changed");
        }

        static async Task<Message> EndRoute(ITelegramBotClient bot, Message message, Driver driver)
        {
            return await bot.SendTextMessageAsync(message.Chat.Id, "1");
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
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult),
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

        static async Task<Message> Help(ITelegramBotClient bot, Message message)
        {
            const string usage = "Usage:\n" +
                                 "/inline   - send inline keyboard\n" +
                                 "/keyboard - send custom keyboard\n" +
                                 "/remove   - remove custom keyboard\n" +
                                 "/photo    - send a photo\n" +
                                 "/request  - request location or contact";

            return await bot.SendTextMessageAsync(message.Chat.Id,
                usage,
                replyMarkup: new ReplyKeyboardRemove());
        }


        // Process Inline Keyboard callback data
        async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            await _botClient.AnswerCallbackQueryAsync(
                callbackQuery.Id,
                $"Received {callbackQuery.Data}");

            await _botClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Received {callbackQuery.Data}");
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

        #region Inline Mode

        async Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            _logger.LogInformation($"Received inline query from: {inlineQuery.From.Id}");

            InlineQueryResultBase[] results =
            {
                // displayed result
                new InlineQueryResultArticle(
                    "3",
                    "TgBots",
                    new InputTextMessageContent(
                        "hello"
                    )
                )
            };

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0);
        }

        Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            _logger.LogInformation($"Received inline result: {chosenInlineResult.ResultId}");
            return Task.CompletedTask;
        }

        #endregion
    }
}
