using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ServerConsole.Model;

namespace ServerConsole.Hubs
{
    public class AtencionMedicosHub : Hub
    {
        /*public void EnviarMensaje(string usuario, string mensaje)
        {
            Clients.All.RecibirMensaje(usuario, mensaje);
        }*/

        public override Task OnConnected()
        {
            string id = Context.ConnectionId;
            Console.WriteLine("Cliente conectado: " + id);

            // Notifica a los demás clientes
            Clients.Others.AlguienSeConecto(id);

            return base.OnConnected();
        }

        public async Task GetAll()
        {
            using (atencion_medicos_dbEntities entity = new atencion_medicos_dbEntities())
            {
                Console.WriteLine("Se obtuvo la lista de atencion a medicos completa");
                Console.WriteLine("Numero de registros: "+ entity.atencion_medicos.Count());
                Clients.Others.GetAll();

            }
        }

        public async Task<List<atencion_medicos>> GetItems()
        {
            try
            {
                using (atencion_medicos_dbEntities entity = new atencion_medicos_dbEntities())
                {
                    Console.WriteLine("Informacion se ha enviado al cliente");
                    return entity.atencion_medicos.Include(a => a.aseguradora).
                                                        Include(e => e.estatus1).
                                                        Include(r => r.responsabilidad1).
                                                        Include(m => m.medico1).ToList();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /*public void GetAllRequest()
        {
            Clients.All.GetAllRequest();
        }*/

    }
}
