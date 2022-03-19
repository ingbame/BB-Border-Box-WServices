using System.ServiceProcess;
using BB_Border_Box_CoreService;

namespace BB_Border_Box_WServices
{
    public partial class TelegramService : ServiceBase
    {
        #region Metodos del servicio
        public TelegramService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TelegramServiceProcess.Instance.IniciarServicio();
        }

        protected override void OnStop()
        {
            TelegramServiceProcess.Instance.DetenerServicio();
        }
        #endregion        
    }
}
