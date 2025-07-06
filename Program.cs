using Microsoft.Owin.Hosting;
using ServidorSignalRConsola;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "http://192.168.1.69:9090/"; // Escucha en todas las interfaces en el puerto 8080

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Servidor SignalR corriendo en: " + url);
                Console.WriteLine("Presiona cualquier tecla para cerrar...");
                Console.ReadKey();
            }
        }
    }
}
