using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cxc_OrdenServicio
    {
        public int IdServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string Cliente { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int TipoSolicitud { get; set; }
        public decimal Importe { get; set; }
        public string ImporteVista { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int Periodos { get; set; }
        public int TipoPeriodo { get; set; }
        public string CondicionPago { get; set; }
        public int CondicionPagoDias { get; set; }
        public string Proyecto { get; set; }
        public string TipoMoneda { get; set; }
        public int IdCatServicio { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public int IdUsr { get; set; }
        public int UnidadNegocio { get; set; }
        public int Contrato { get; set; }
        public int EnviaCorreoClte { get; set; }
        public Enumeradores.EstadoOrdSvc Estado { get; set; }
        public int Especial { get; set; }

        
    }
}
