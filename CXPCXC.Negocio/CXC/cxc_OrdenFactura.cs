using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.CXC
{
    public class cxc_OrdenFactura : Datos.Tablas.cxc_OrdenFactura
    {
        public bool Agregar_Registro(mod.cxc_OrdenFactura items, ref int idobtenido)
        {
            idobtenido = Agregar(items);
            if (idobtenido > 0)
                return true;
            else
                return false;
        }

        public bool Modificar_Estado(int idordenfactura, int estado)
        {
            if (ModificarEstado(idordenfactura, estado) > 0)
                return true;
            else
                return false;
        }
    }
}
