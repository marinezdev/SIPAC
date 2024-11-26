using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.Sistema
{
    public class cat_Empresas :Datos.Tablas.cat_Empresas
    {
        public List<mod.cat_Empresas> Seleccionar_Registros()
        {
            return Seleccionar();
        }

        public mod.cat_Empresas SelecionarPorId(int id)
        {
            return Selecionar_PorId(id);
        }
    }
}
