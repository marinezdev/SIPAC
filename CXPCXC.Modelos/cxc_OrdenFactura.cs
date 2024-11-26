using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cxc_OrdenFactura
    {
        public int IdServicio { get; set; }
        public int IdOrdenFactura { get; set; }
        public DateTime FechaInicio { get; set; }
        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string Cliente { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int TipoSolicitud { get; set; }
        public string NumFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Importe { get; set; }
        public string CondicionPago { get; set; }
        public int CondicionPagoDias { get; set; }
        public string Proyecto { get; set; }
        public string TipoMoneda { get; set; }
        public int IdCatServicio { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Anotaciones { get; set; }
        public int IdUsr { get; set; }
        public int UnidadNegocio { get; set; }
        public string CteSolomon { get; set; }
        public int Estado { get; set; }
        public int Contrato { get; set; }
        public int Factura { get; set; }
        public int EnviaCorreoClte { get; set; }
        public int Especial { get; set; }
        public int Marcado { get; set; }
        public DateTime FechaCompromisoPago { get; set; }

        public string Semana { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Pesos { get; set; }
        public decimal Dolares { get; set; }
    }
}
