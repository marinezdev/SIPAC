using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_NotaCredito_trf_NotaCreditoAsignacion
    {
        public trf_NotaCredito trf_NotaCredito;
        public trf_NotaCreditoAsignacion trf_NotaCreditoAsignacion;

        public trf_NotaCredito_trf_NotaCreditoAsignacion()
        {
            trf_NotaCredito = new trf_NotaCredito();
            trf_NotaCreditoAsignacion = new trf_NotaCreditoAsignacion();
        }

    }
}
