using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class trf_ConciliarPago
    {
        public int IdPago { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Referencia { get; set; }
        public string Banco { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Importe { get; set; }
        public Enumeradores.enMoneda Moneda { get; set; }
        public int IdUsr { get; set; }
        public Enumeradores.enEstado Estado { get; set; }
    }
}
