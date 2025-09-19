using ServerConsole.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole.ModelsDTO
{
    public class AtencionMedicosDTO
    {
        public int IdAtencionMedicos { get; set; }
        public ResponsabilidadDTO Responsabilidad { get; set; }
        public string ResponsabilidadConcepto
        {
            get => Responsabilidad?.responsabilidad;
            set
            {
                if (Responsabilidad == null)
                    Responsabilidad = new ResponsabilidadDTO();

                Responsabilidad.responsabilidad = value;
            }
        }

        public EstatusDTO Estatus { get; set; }
        public string EstatusEstado
        {
            get => Estatus?.estatus;
            set
            {
                if (Estatus == null)
                    Estatus = new EstatusDTO();

                Estatus.estatus = value;
            }
        }

        public DateTime? Ingreso { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public DateTime? fecha_entrega_tab { get; set; }
        public DateTime? fecha_solicitado { get; set; }
        public DateTime? fecha_entregado { get; set; }
        public string siniestro { get; set; }
        public string paciente { get; set; }
        public string cuenta { get; set; }
        public MedicoDTO medico { get; set; }
        public string MedicoNombre
        {
            get => medico?.nombre;
            set
            {
                if (medico == null)
                    medico = new MedicoDTO();

                medico.nombre = value;
            }
        }

        public AseguradoraDTO Aseguradora { get; set; }
        public string AseguradoraNombre
        {
            get => Aseguradora?.compania_aseguradora;
            set
            {
                if (Aseguradora == null)
                    Aseguradora = new AseguradoraDTO();

                Aseguradora.compania_aseguradora = value;
            }
        }

        public string concepto_honorario { get; set; }
        public double? importe { get; set; }
        public double? isr { get; set; }
        public double? renta_importe_cedular { get; set; }
        public double? total { get; set; }
        public string factura { get; set; }
        public int? cancelado_recibo { get; set; }
        public string cancelado { get; set; }
        public string recibe { get; set; }
        public DateTime? fecha_enviado { get; set; }
        public DateTime? fecha_reenviado { get; set; }
        public string folio_portal { get; set; }
        public DateTime? fecha_pago { get; set; }
        public string observaciones { get; set; }
        public string asistente { get; set; }
        public string correo { get; set; }
        /*public int IdAtencionMedicos { get; set; }
        public ResponsabilidadDTO Responsabilidad { get; set; }
        public EstatusDTO Estatus { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_entrega_tab { get; set; }
        public DateTime fecha_solicitado { get; set; }
        public DateTime fecha_entregado { get; set; }
        public string siniestro { get; set; }
        public string paciente { get; set; }
        public string cuenta { get; set; }
        public MedicoDTO medico { get; set; }
        public AseguradoraDTO Aseguradora { get; set; }
        public string concepto_honorario { get; set; }
        public double importe { get; set; }
        public double isr { get; set; }
        public double renta_importe_cedular { get; set; }
        public double total { get; set; }
        public string factura { get; set; }
        public string cancelado_recibo { get; set; }
        public string cancelado { get; set; }
        public string recibe { get; set; }
        public DateTime fecha_enviado { get; set; }
        public DateTime fecha_reenviado { get; set; }
        public string folio_portal { get; set; }
        public DateTime fecha_pago { get; set; }
        public string observaciones { get; set; }
        public string asistente { get; set; }
        public string correo { get; set; }*/
    }
}
