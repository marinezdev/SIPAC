using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cxc_ArchivoContrato
    {
        public int IdServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ArchivoDestino { get; set; }
        public bool Existe { get; set; }
        public string UbicacionTmp { get; set; }
    }
}
