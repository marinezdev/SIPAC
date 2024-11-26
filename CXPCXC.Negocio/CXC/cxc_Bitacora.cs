using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.CXC
{
    public class cxc_Bitacora : CXPCXC.Datos.Tablas.cxc_Bitacora
    {
        public bool Agregar_Registro(mod.cxc_Bitacora items)
        {
            return Agregar(items);
        }
    }
}
