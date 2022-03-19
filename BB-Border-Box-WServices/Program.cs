using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BB_Border_Box_WServices
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            //Descomentar para el servicio
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TelegramService()
            };
            ServiceBase.Run(ServicesToRun);

            //Descomentar para iniciar el Form y hacer pruebas
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TelegramServiceTest());

        }
    }
}
