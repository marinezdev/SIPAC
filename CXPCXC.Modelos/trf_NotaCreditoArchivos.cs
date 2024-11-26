using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_NotaCreditoArchivos
    {
        public int IdNotaCredito { get; set; }
        public Enumeradores.TipoArchivo2 Tipo { get; set; }
        public string Nombre { get; set; }

    }
}
