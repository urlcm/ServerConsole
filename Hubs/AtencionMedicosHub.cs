using Microsoft.AspNet.SignalR;
using ServerConsole.Model;
using ServerConsole.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            using (Model.atencion_medicos_dbEntities entity = new atencion_medicos_dbEntities())
            {
                Console.WriteLine("Se obtuvo la lista de atencion a medicos completa");
                Console.WriteLine("Numero de registros: " + entity.atencion_medicos.Count());
                Clients.Others.GetAll();

            }
        }

        public async Task<List<AtencionMedicosDTO>> GetItemsAsync()
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
                   ResponsabilidadConcepto = x.responsabilidad1.responsabilidad1,
                   EstatusEstado = x.estatus1.estatus1,
                   Responsabilidad = new ResponsabilidadDTO()
                   {
                       idResponsabilida = x.responsabilidad1.idresponsabilidad,
                       responsabilidad = x.responsabilidad1.responsabilidad1
                   },
                   Estatus = new EstatusDTO()
                   {
                       idEstatus = x.estatus1.idEstatus,
                       estatus = x.estatus1.estatus1
                   },
                   Ingreso = x.ingreso.Value,
                   fecha_ingreso = x.fecha_ingreso.Value,
                   fecha_entrega_tab = x.fecha_entrega_tab.Value,
                   fecha_solicitado = x.fecha_solicitado.Value,
                   fecha_entregado = x.fecha_entregado.Value,
                   siniestro = x.siniestro,
                   paciente = x.paciente,
                   cuenta = x.cuenta,
                   MedicoNombre = x.medico1.nombre,
                   AseguradoraNombre = x.aseguradora.CompDesc,
                   medico = new MedicoDTO()
                   { 
                       idMedico = x.medico1.idMedico,
                       nombre = x.medico1.nombre 
                   },
                   Aseguradora = new AseguradoraDTO()
                   {
                       idAseguradora = x.aseguradora.idComp,
                       compania_aseguradora = x.aseguradora.CompDesc
                   },
                   importe = x.importe,
                   isr = x.isr,
                   renta_importe_cedular = x.renta_importe_cedular,
                   total = x.total,
                   factura = x.factura,
                   cancelado_recibo = x.cancelado_recibo,
                   //cancelado = x.ca falta ese
                   recibe = x.recibe,
                   fecha_enviado = x.fecha_enviado,
                   fecha_reenviado = x.fecha_reenviado,
                   folio_portal = x.folio_portal,
                   fecha_pago = x.fecha_pago,
                   observaciones = x.observaciones,
                   asistente = x.asistente,
                   correo = x.correo
               }).ToList();

                Console.WriteLine("Datos enviados al cliente: " + resultado.Count);
                return resultado;
            }
        }

        public async Task<List<AtencionMedicosDTO>> GetItems()
        {
            return await GetItemsAsync();
        }


        public async Task addItem(AtencionMedicosDTO atencionMedicos)
        {
            try
            {
                atencion_medicos atencion_Medicos_entity = ConvertToAtencionMedicosDB(atencionMedicos);
                using (atencion_medicos_dbEntities atm = new atencion_medicos_dbEntities())
                {
                    atm.atencion_medicos.Add(atencion_Medicos_entity);
                    atm.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Se presentó el siguiente problema: "+e);
            }
        }



        public async Task<List<responsabilidad>> GetResponsabilidadesAsync()
        {
            List<responsabilidad> responsabilidades = new List<responsabilidad>();
            using (atencion_medicos_dbEntities db = new atencion_medicos_dbEntities())
            {
                responsabilidades = db.responsabilidad.ToList();
                return responsabilidades;
            }
        }

        public async Task<List<responsabilidad>> getResponsabilidades()
        {
            return await GetResponsabilidadesAsync();
        }

        public async Task<List<medico>> GetMedicosAsync()
        {
            List<medico> medicos = new List<medico>();
            using (atencion_medicos_dbEntities db = new atencion_medicos_dbEntities())
            {
                medicos = db.medico.ToList();
                return medicos;
            }
        }

        public async Task<List<medico>> getMedicos()
        {
            return await GetMedicosAsync();
        }

        public async Task<List<estatus>> GetEstatusAsync()
        {
            List<estatus> estatus = new List<estatus>();
            using (atencion_medicos_dbEntities db = new atencion_medicos_dbEntities())
            {
                estatus = db.estatus.ToList();
                return estatus;
            }
        }

        public async Task<List<estatus>> getEstatus()
        {
            return await GetEstatusAsync();
        }

        public async Task<List<aseguradora>> GetAseguradoraAsync()
        {
            List<aseguradora> aseguradoras = new List<aseguradora>();
            using (atencion_medicos_dbEntities db = new atencion_medicos_dbEntities())
            {
                aseguradoras = db.aseguradora.ToList();
                return aseguradoras;
            }
        }

        public async Task<List<aseguradora>> GetAseguradoras()
        {
            return await GetAseguradoraAsync();
        }

        public async Task UpdateFieldAsync (AtencionMedicosDTO item)
        {
            try
            {
                atencion_medicos atencion_Medicos = ConvertToAtencionMedicosDB(item);
                using (atencion_medicos_dbEntities db = new atencion_medicos_dbEntities())
                {
                    db.Entry(atencion_Medicos).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Console.WriteLine("Se agrego el objeto" + item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Se presentó el sigueinte error: "+e);
            }
            //Clients.Others.UpdateFieldAsync();
        }

        public async Task UpdateField(AtencionMedicosDTO item)
        {
            await UpdateFieldAsync(item);
        }

        public atencion_medicos ConvertToAtencionMedicosDB(AtencionMedicosDTO itemAtencionMedicosDTO)
        {
            atencion_medicos atencion_Medicos = new atencion_medicos
            {
                id_atencion_medicos = itemAtencionMedicosDTO.IdAtencionMedicos,
                responsabilidad1 = new responsabilidad()
                {

                    idresponsabilidad = itemAtencionMedicosDTO.Responsabilidad.idResponsabilida,
                    responsabilidad1 = itemAtencionMedicosDTO.Responsabilidad.responsabilidad
                },
                estatus1 = new estatus()
                {
                    idEstatus = itemAtencionMedicosDTO.Estatus.idEstatus,
                    estatus1 = itemAtencionMedicosDTO.Estatus.estatus
                },

                ingreso = itemAtencionMedicosDTO.Ingreso,
                fecha_ingreso = itemAtencionMedicosDTO.fecha_ingreso,
                fecha_entrega_tab = itemAtencionMedicosDTO.fecha_entrega_tab,
                fecha_solicitado = itemAtencionMedicosDTO.fecha_solicitado,
                fecha_entregado = itemAtencionMedicosDTO.fecha_entregado,
                siniestro = itemAtencionMedicosDTO.siniestro,
                paciente = itemAtencionMedicosDTO.paciente,
                cuenta = itemAtencionMedicosDTO.cuenta,
                medico1 = new medico()
                {
                    idMedico = itemAtencionMedicosDTO.medico.idMedico,
                    nombre = itemAtencionMedicosDTO.medico.nombre,
                },
                aseguradora = new aseguradora()
                {
                    idComp = itemAtencionMedicosDTO.Aseguradora.idAseguradora,
                    CompDesc = itemAtencionMedicosDTO.Aseguradora.compania_aseguradora
                },
                importe = itemAtencionMedicosDTO.importe,
                isr = itemAtencionMedicosDTO.isr != null ? (double)itemAtencionMedicosDTO.isr : 0,
                renta_importe_cedular = itemAtencionMedicosDTO.renta_importe_cedular,
                total = itemAtencionMedicosDTO.total != null ? (double)itemAtencionMedicosDTO.total : 0,
                factura = itemAtencionMedicosDTO.factura,
                cancelado_recibo = itemAtencionMedicosDTO.cancelado_recibo,
                //cancelado = x.ca falta ese
                recibe = itemAtencionMedicosDTO.recibe,
                fecha_enviado = itemAtencionMedicosDTO.fecha_enviado,
                fecha_reenviado = itemAtencionMedicosDTO.fecha_reenviado,
                folio_portal = itemAtencionMedicosDTO.folio_portal,
                fecha_pago = itemAtencionMedicosDTO.fecha_pago,
                observaciones = itemAtencionMedicosDTO.observaciones,
                asistente = itemAtencionMedicosDTO.asistente,
                correo = itemAtencionMedicosDTO.correo
            };
            return atencion_Medicos;
        }
    }
}
