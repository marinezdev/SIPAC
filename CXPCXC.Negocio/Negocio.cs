using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Negocio
{
    public class Negocio
    {
        public CXC.CXC cxc;
        public CXP.CXP cxp;
        public Sistema.Sistema admin;

        public Negocio()
        {
            admin = new Sistema.Sistema();
            cxc = new CXC.CXC();
            cxp = new CXP.CXP();
        }
    }
}
