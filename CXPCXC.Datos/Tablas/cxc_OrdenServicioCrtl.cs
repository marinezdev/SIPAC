using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cxc_OrdenServicioCtrl
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        protected int AgregarSeleccionarId()
        {
            b.ExecuteCommandSP("cxc_OrdenServicioCtrl_Seleccionar_Id");
            b.AddParameterWithReturnValue("@idobtenido");
            return b.InsertWithReturnValue();            
        }
    }
}
