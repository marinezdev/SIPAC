using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Negocio.Sistema
{
    public class EmpresasClientes : Datos.Tablas.EmpresasClientes
    {
        public void SeleccionarPorEmpresa(string idempresa)
        {
            Seleccionar_PorEmpresa(idempresa);
        }
    }
}
