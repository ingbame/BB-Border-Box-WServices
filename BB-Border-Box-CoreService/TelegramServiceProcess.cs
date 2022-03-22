using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using BB_Border_Box_EntityService.TelegramService;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BB_Border_Box_CoreService
{
    public class TelegramServiceProcess
    {
        #region Patron de Diseño
        private static TelegramServiceProcess _instance;
        private static readonly object _instanceLock = new object();
        public static TelegramServiceProcess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new TelegramServiceProcess();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Variables
        private TelegramBotClient _botClient = new TelegramBotClient("5169232166:AAFbUOfAf58SGhEZrFFNHyMWQNiP-i-ZnSg");
        #endregion
        #region Metodos publicos
        public void IniciarServicio()
        {
            try
            {
                var me = _botClient.GetMeAsync();

                var _ReceiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                }
                };
                _botClient.StartReceiving(UpdateHandler, ErrorHandler, _ReceiverOptions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DetenerServicio()
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region Metodos privados
        private async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken cancellToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var id = update.Message.Chat.Id;
                if (update.Message.Type == MessageType.Text)
                {
                    var firstName = update.Message.Chat.FirstName;
                    var lastName = update.Message.Chat.LastName;
                    var _text = update.Message.Text;
                    //Result += $"{id} | {username} | {text}";
                    var returnResponse = string.Empty;
                    if (!string.IsNullOrEmpty(_text))
                        switch (_text.ToLower())
                        {
                            case "/rastreo":
                            case "rastreo":
                                returnResponse = $"Introduce el numero de rastreo que se te proporcionó";
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: returnResponse
                                    );
                                SaveUpdate(update, returnResponse);
                                break;
                            case "/saludo":
                            case "saludo":
                                returnResponse = $"Hola {firstName} {lastName}, BB Boder Box Bot te da un gran saludo el desarrollador, Baruch Medina";
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: returnResponse
                                    );
                                SaveUpdate(update, returnResponse);
                                break;
                            case "/ayuda":
                            case "ayuda":
                                returnResponse = $"Hola {firstName} {lastName}, transfiriendo con un asesor disponible.\n\n\n Baruch Medina sigue trabajando en estos métodos";
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: returnResponse
                                    );
                                SaveUpdate(update, returnResponse, true);
                                break;
                            case "/tabla":
                            case "tabla":
                                returnResponse = $"*Prueba 1* texto normal\n se hizo un brinco *Hi\\!\\!*";
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: returnResponse,
                                    parseMode: ParseMode.MarkdownV2
                                    );
                                SaveUpdate(update, returnResponse, true);
                                break;
                            default:
                                returnResponse = $"Hola soy BB Boder Box Bot, introduce lo que deseas hacer:\nRastreo\nSaludo\nAyuda";
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: returnResponse
                                    );
                                break;
                        }
                    else
                    {
                        returnResponse = $"Disulpa, es necesario ingresar texto. Intenta los siguientes comandos:\nRastreo\nSaludo\nAyuda";
                        await _botClient.SendTextMessageAsync(
                            chatId: id,
                            text: returnResponse
                            );
                    }
                }
                else
                    await _botClient.SendTextMessageAsync(
                        chatId: id,
                        text: $"Disulpa, por el momento solo respondo a texto. Intenta los siguientes comandos:\nRastreo\nSaludo\nAyuda"
                        );
            }
        }
        private Task ErrorHandler(ITelegramBotClient bot, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
        private async void SaveUpdate(Update update, string returnResponse, bool needHuman = false, bool talkingWithHuman = false)
        {
            var body = new
            {
                UpdateId = update.Id,
                MessageId = update.Message.MessageId,
                Date = update.Message.Date,
                MessageText = update.Message.Text,
                AppUserId = update.Message.From.Id,
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName,
                LanguageCode = update.Message.From.LanguageCode,
                ChatId = update.Message.Chat.Id,
                TypedCommand = update.Message.Text,
                ReturnResponse = returnResponse,
                NeedHuman = needHuman,
                TalkingWithHuman = talkingWithHuman
            };

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:7016/api/WServices/Telegram/BotIntegration"),
            };

            var jsoStr = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(jsoStr, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(request.RequestUri, httpContent))
            {
                response.EnsureSuccessStatusCode();
                
            }
        }
        #endregion   
    }
}
