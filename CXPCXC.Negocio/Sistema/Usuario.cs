using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.Sistema
{
    public class Usuario : Datos.Tablas.Usuario
    {
        public mod.Usuario Seleccionar_DetallePorId(int idusr)
        {
            return SeleccionarDetallePorId(idusr);
        }

        protected bool Agregar_Registro(mod.Usuario items)
        {
            if (Agregar(items) > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar_Registro(mod.Usuario items)
        {
            if (Modificar(items) > 0)
                return true;
            else
                return false;
        }

    }
}
