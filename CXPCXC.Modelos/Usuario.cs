using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class Usuario
    {
        public int IdUsr { get; set; }
        public string Usuario_ { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string UnidadNegocio { get; set; }
        public int Grupo { get; set; }
        public int Estado { get; set; }
        public string Conectado { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpresa { get; set; }
        public string correo { get; set; }
        public int TipoRecCorreo { get; set; }
    }
}
