using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.CXC
{
    public class cxc_ArchivosContrato : Datos.Tablas.cxc_ArchivoContrato
    {
        public bool Agregar_Registro(mod.cxc_ArchivoContrato items)
        {
            return Agregar(items);
        }
    }
}
