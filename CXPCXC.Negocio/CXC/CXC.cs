using CXPCXC.Datos.Tablas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Negocio.CXC
{
    /// <summary>
    /// Procedimientos de negocio de cuentas por cobrar
    /// </summary>
    public class CXC
    {
        public cxc_ArchivosContrato cxcarchivoscontrato;
        public cxc_Bitacora cxcbitacora;
        public cxc_OrdenFactura cxcordenfactura;
        public cxc_OrdenServicio cxcordenservicio;
        public cxc_OrdenServicioCtrl cxcordenservicoctrl;
        public CXC()
        {
            cxcarchivoscontrato = new cxc_ArchivosContrato();
            cxcbitacora = new cxc_Bitacora();
            cxcordenfactura = new cxc_OrdenFactura();
            cxcordenservicio = new cxc_OrdenServicio();
            cxcordenservicoctrl = new cxc_OrdenServicioCtrl();
        }
    }
}
