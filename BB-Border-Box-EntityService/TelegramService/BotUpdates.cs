using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BB_Border_Box_EntityService.TelegramService
{
    public class BotUpdates
    {
        public Update botUpdate { get; set; }
        public string typedCommand
        {
            get
            {
                if (botUpdate != null)
                    if (!string.IsNullOrEmpty(botUpdate.Message.Text))
                        return botUpdate.Message.Text;
                return null;
            }
        }
        public string returnResponse { get; set; }
        public bool needHuman { get; set; }
        public bool talkingWithHuman { get; set; }
        public long EmployeeId { get; set; }
    }
}
