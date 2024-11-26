using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_NotaCreditoAsignacion
    {
        public DateTime FechaRegistro { get; set; }
        public int IdNotaCredito {get; set;}
        public int IdSolicitud { get; set; }
        public decimal Monto { get; set; }
        public int IdUsr { get; set; }
    }
}
