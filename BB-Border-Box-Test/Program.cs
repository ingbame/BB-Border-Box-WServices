using BB_Border_Box_CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB_Border_Box_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramServiceProcess.Instance.IniciarServicio();
            Console.ReadKey();
        }
    }
}
