using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cat_Proveedor
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Cp { get; set; }
        public string Contacto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Extencion { get; set; }
        public Enumeradores.enSinFactura SinFactura { get; set; }        
        public int Activo { get; set; } 
    }
}
