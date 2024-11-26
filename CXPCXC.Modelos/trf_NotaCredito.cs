using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_NotaCredito
    {
        public int IdNotaCredito { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime Fecha { get; set; }
        public string Rfc { get; set; }
        public string Proveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public string Moneda { get; set; }
        public decimal ImportePendiente { get; set; }
        public Enumeradores.enEstado2 Estado { get; set; }
        public int IdUsr { get; set; }
        public int IdSolicitudOrigen { get; set; }
        public int IdSolicitudAsignada { get; set; }

    }
}
