using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cxc_Archivos
    {
        public int IdOrdenFactura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Enumeradores.cxcTipoArchivo Tipo { get; set; }
        public int IdDocumento { get; set; }
        public string ArchvioOrigen { get; set; }
        public string ArchivoDestino { get; set; }
        public decimal CantiadPorPagar { get; set; }
        public string Nota { get; set; }
    }
}
