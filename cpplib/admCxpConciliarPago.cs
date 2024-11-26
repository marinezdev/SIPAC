using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
   public class admCxpConciliarPago
    {
        public int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO trf_ConciliarPagoCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            if (BD.EjecutaCmd(SqlCmd)){
                DataTable Datos = BD.LeeDatos("Select @@Identity as Id");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("Id")) { Id = Convert.ToInt32(Datos.Rows[0]["Id"]); }
                }
            }
            return Id;
        }
        
        public bool  Registra(cxpPagos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_ConciliarPago (");
            SqlCmd.Append("Id");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",Referencia");
            SqlCmd.Append(",Banco");
            SqlCmd.Append(",FechaPago");
            SqlCmd.Append(",TipoCambio");
            SqlCmd.Append(",Importe");
            SqlCmd.Append(",Moneda");
            SqlCmd.Append(",Idusr");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdPago.ToString ());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + pDatos.Referencia + "'");
            SqlCmd.Append(",'" + pDatos.Banco + "'");
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.TipoCambio);
            SqlCmd.Append("," + pDatos.Importe);
            SqlCmd.Append("," + pDatos.Moneda.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr.ToString ());
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            
            return resultado;
        }

        private cxpPagos arma(DataRow pRegistro)
        {
            cxpPagos respuesta = new cxpPagos();
            if (!pRegistro.IsNull("IdPago")) respuesta.IdPago = Convert.ToInt32(pRegistro["IdPago"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Referencia")) respuesta.Referencia = Convert.ToString(pRegistro["Referencia"]);
            if (!pRegistro.IsNull("Banco")) respuesta.Banco = Convert.ToString(pRegistro["Banco"]);
            if (!pRegistro.IsNull("FechaPago")) respuesta.FechaPago = Convert.ToDateTime(pRegistro["FechaPago"]);
            if (!pRegistro.IsNull("TipoCambio")) respuesta.TipoCambio = Convert.ToDecimal(pRegistro["TipoCambio"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = (cxpPagos.enMoneda)(pRegistro["Moneda"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (cxpPagos.enEstado)(pRegistro["Estado"]);
            
            return respuesta;
        }
    }
        
    public class cxpPagos {
        private int mIdPago = 0;
        public int IdPago { get { return mIdPago; } set { mIdPago = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mReferencia = String.Empty;
        public string Referencia { get { return mReferencia; } set { mReferencia = value; } }
        private string mBanco = String.Empty;
        public string Banco { get { return mBanco; } set { mBanco = value; } }
        private DateTime mFechaPago = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaPago { get { return mFechaPago; } set { mFechaPago = value; } }
        private decimal mTipoCambio = 0;
        public decimal TipoCambio { get { return mTipoCambio; } set { mTipoCambio = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private enMoneda mMoneda = enMoneda.Pesos ;
        public enMoneda Moneda { get { return mMoneda; } set { mMoneda = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private enEstado mEstado = enEstado.Pendiente;
        public enEstado Estado { get { return mEstado; } set { mEstado = value; } }

        public enum enMoneda { Pesos = 1, Dolares = 2 }
        public enum enEstado { Pendiente = 10, Conciliado = 20 }   
    }
   
}
