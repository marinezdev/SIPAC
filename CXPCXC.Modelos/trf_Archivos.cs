using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_Archivos
    {
        public int IdSolicitud { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Enumeradores.TipoArchivo Tipo { get; set; }
        public int IdDocumento { get; set; }
        public decimal Cantidad { get; set; }
        public string ArchvioOrigen { get; set; }
        public string ArchivoDestino { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Pesos { get; set; }
        public decimal CantiadPorPagar { get; set; }
        public string Nota { get; set; }
        public int IdPago { get; set; }
    }

    public enum TipoArchivo 
    { 
        Factura = 1, 
        Xml = 2, 
        Comprobante = 3 
    }
}
