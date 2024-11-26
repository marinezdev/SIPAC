using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.Sistema
{
    public class cat_UnidadNegocio : Datos.Tablas.cat_UnidadNegocio
    {
        public mod.cat_UnidadNegocio SeleccionarPorId(int id)
        {
            return Seleccionar_PorId(id);
        }
    }
}
