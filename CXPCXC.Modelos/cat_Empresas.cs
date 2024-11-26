using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class cat_Empresas
    {
        public int Id { get; set;}
        public DateTime FechaRegistro { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public int Activo { get; set; }
        public string Logo { get; set; }
    }
}
