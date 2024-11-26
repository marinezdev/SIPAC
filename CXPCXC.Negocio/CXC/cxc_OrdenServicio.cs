using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.CXC
{
    public class cxc_OrdenServicio : Datos.Tablas.cxc_OrdenServicio
    {
        public bool Agregar_Registro(mod.cxc_OrdenServicio items)
        {            
            if (Agregar(items) == -1)
                return true;
            else
                return false;
        }
        public int Pruebas()
        {
            return Prueba();
        }
    }
}
