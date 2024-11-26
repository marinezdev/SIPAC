using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class EmpresasUnidadNegocio
    {
        public int IdEmpresa { get; set; }
        public int IdUDN { get; set; }
        public bool Activo { get; set; }

        public string NombreEmpresa { get; set; }
        public string TituloUDN { get; set; }
    }
}
