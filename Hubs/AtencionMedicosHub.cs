using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using ServerConsole.Model;
using ServerConsole.ModelsDTO;

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

        public async Task<List<AtencionMedicosDTO>> GetItems()
        {
            using (var entity = new atencion_medicos_dbEntities())
            {
                var resultado = entity.atencion_medicos
                    .Include(a => a.aseguradora)
                    .Include(e => e.estatus1)
                    .Include(r => r.responsabilidad1)
                    .Include(m => m.medico1)
                    .Select(x => new AtencionMedicosDTO
                    {
                        IdAtencionMedicos = x.id_atencion_medicos,
                        NombreMedico = x.medico1.nombre,
                        Aseguradora = x.aseguradora.CompDesc,
                        Estatus = x.estatus1.estatus1,
                        Responsabilidad = x.responsabilidad1.responsabilidad1
                    }).ToList();

                Console.WriteLine("Datos enviados al cliente: " + resultado.Count);
                return resultado;
            }
        }

    }
}
