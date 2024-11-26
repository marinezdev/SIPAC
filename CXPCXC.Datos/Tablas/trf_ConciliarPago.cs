using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class trf_ConciliarPago
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        public bool Agregar(mod.trf_ConciliarPago items)
        {
            b.ExecuteCommandSP("trf_ConciliarPago_Agregar");
            b.AddParameter("@referencia", items.Referencia, SqlDbType.VarChar, 50);
            b.AddParameter("@banco", items.Banco, SqlDbType.VarChar, 50);
            b.AddParameter("@fechapago", items.FechaPago, SqlDbType.DateTime);
            b.AddParameter("@tipocambio", items.TipoCambio, SqlDbType.Int);
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            b.AddParameter("@moneda", items.Moneda, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
