using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class Enumeradores
    {
        public enum cxcTipoArchivo
        {
            Factura = 1,
            Xml = 2,
            Comprobante = 3
        }
        public enum TipoArchivo
        {
            Factura = 1,
            Xml = 2,
            Comprobante = 3
        }
        public enum solEstado 
        { 
            Solicitud = 10, 
            Autorizacion = 20, 
            Fondos = 25, 
            Captura = 30, 
            PagoParcial = 40, 
            Pagado = 50, 
            Contabilidad = 60, 
            Rechazada = 70 
        }
        public enum enConFactura 
        { 
            NO = 0, 
            SI = 1 
        }
        public enum enTpSolicitud 
        { 
            Solicitud = 1, 
            Reembolso = 2 
        }
        public enum enSinFactura 
        { 
            NO = 0, 
            SI = 1 
        }
        public enum EstadoOrdFac 
        { 
            Solicitud = 10, 
            Generacion_Factura = 20, 
            En_Cobro = 30, 
            Pagado = 40, 
            Cancelado = 100 
        }
        public enum enMoneda 
        { 
            Pesos = 1, 
            Dolares = 2 
        }
        public enum enEstado 
        { 
            Pendiente = 10, 
            Conciliado = 20 
        }
        public enum enEstado2
        { 
            Activa = 10, 
            Parcial = 20, 
            Asignada = 30 
        }
        public enum TipoArchivo2 
        { 
            PDF = 1, 
            XML = 2 
        }
        public enum EstadoOrdSvc 
        { 
            Abierto = 10, 
            Cerrado = 20, 
            Cancelado = 30 
        }
        public enum enTipoSolicitud 
        { 
            Fijo = 1, 
            Variable = 2 
        }

    }
}
