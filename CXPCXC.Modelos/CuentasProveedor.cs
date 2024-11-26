using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class CuentasProveedor
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Banco { get; set; }
        public string Cuenta { get; set; }
        public string CtaClabe { get; set; }
        public string Sucursal { get; set; }
        public string Moneda { get; set; }
    }
}
