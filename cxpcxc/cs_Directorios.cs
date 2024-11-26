using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cxpcxc
{
    public class cs_Directorios
    {
        public string cxp_Fondos { get {return @"documentos\cxp\Fondos\"; } }
        public string cxp_Fondeo_Web { get { return @"documentos\\cxp\\Fondos\\"; } }

        public string cxp_Factura { get { return @"documentos\cxp\"; } }
        public string cxp_Factura_Web { get { return @"documentos\\cxp\\"; } }
                

        public string cxc_Factura { get { return @"documentos\cxc\"; } }
        public string cxc_Factura_Web{ get { return @"documentos\\cxc\\"; } }

        public string cxc_Contratos { get { return @"documentos\cxc\Contratos\"; } }
        public string cxc_Contratos_Web { get { return @"documentos\cxc\\Contratos\\"; } }
    }
}