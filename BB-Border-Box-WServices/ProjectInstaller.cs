using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace BB_Border_Box_WServices
{
    [RunInstaller(true)]
    public partial class TelegramBotService : System.Configuration.Install.Installer
    {
        public TelegramBotService()
        {
            InitializeComponent();
        }
    }
}
