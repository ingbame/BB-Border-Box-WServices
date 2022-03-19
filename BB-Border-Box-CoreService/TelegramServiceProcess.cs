using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
        #region Metodos privados
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
        private async Task UpdateHandler(ITelegramBotClient bot, Update _update, CancellationToken cancellToken)
        {
            if (_update.Type == UpdateType.Message)
                if (_update.Message.Type == MessageType.Text)
                {
                    var id = _update.Message.Chat.Id;
                    var firstName = _update.Message.Chat.FirstName;
                    var lastName = _update.Message.Chat.LastName;
                    var _text = _update.Message.Text;
                    //Result += $"{id} | {username} | {text}";
                    if (!string.IsNullOrEmpty(_text))
                        switch (_text.ToLower())
                        {
                            case "/rastreo":
                            case "rastreo":
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: $"Introduce el numero de rastreo que se te proporcionó"
                                    );
                                break;
                            case "/saludo":
                            case "saludo":
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: $"Hola {firstName} {lastName}, BB Boder Box Bot te da un gran saludo el desarrollador, Baruch Medina"
                                    );
                                break;
                            default:
                                await _botClient.SendTextMessageAsync(
                                    chatId: id,
                                    text: $"Hola soy BB Boder Box Bot, introduce lo que deseas hacer:\nRastreo\nSaludo"
                                    );
                                break;
                        }
                }
        }
        private Task ErrorHandler(ITelegramBotClient bot, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
        #endregion   
    }
}
