using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cxc_Bitacora
    {
        public int IdServicio { get; set; }
        public int IdOrdenFactura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Enumeradores.EstadoOrdFac Estado { get; set; }
        public int IdUsr { get; set; }
        public string Nombre { get; set; }
    }
}
