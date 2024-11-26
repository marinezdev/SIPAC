using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class BitacoraSolicitud
    {
        public int IdSolicitud { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Enumeradores.solEstado Estado { get; set; }
        public int IdUsr { get; set; }
        public string Nombre { get; set; }
        public Decimal Importe { get; set; }
    }
}
