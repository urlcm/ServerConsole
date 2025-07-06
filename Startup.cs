using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ServidorSignalRConsola.Startup))]

namespace ServidorSignalRConsola
{
    internal class Startup
    {
        //GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); // Permitir CORS
            app.MapSignalR(); // Mapear rutas de SignalR
        }


    }
}

