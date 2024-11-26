using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
   public class admCuenta
    {
       public bool nuevo(Cuenta pDatos)
        {
         
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO CuentasProveedor (Id,FechaRegistro,Banco,Cuenta,CtaClabe,Sucursal,Moneda)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.Id);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + pDatos.Banco + "'");
            SqlCmd.Append(",'" + pDatos.NoCuenta + "'");
            SqlCmd.Append(",'" + pDatos.CtaClabe + "'"); 
            SqlCmd.Append(",'" + pDatos.Sucursal + "'");
            SqlCmd.Append(",'" + pDatos.Moneda + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
           return resultado;
        }

        public Cuenta carga(int pIdProveedor, string pCuenta)
        {
            Cuenta respuesta = new Cuenta();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM CuentasProveedor WHERE Id=" + pIdProveedor.ToString() + " and Cuenta='" + pCuenta + "'");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<Cuenta> ListaCuentas(string pIdProveedor)
        {
            List<Cuenta> respuesta = new List<Cuenta>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM CuentasProveedor where Id=" + pIdProveedor.ToString());
            foreach ( DataRow reg in datos.Rows ){
                respuesta.Add (arma(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public Cuenta DaPrimerCuenta(int pIdProveedor)
        {
            Cuenta respuesta = new Cuenta();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT top 1 *  FROM CuentasProveedor where Id=" + pIdProveedor.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private Cuenta arma(DataRow pRegistro)
        {
            Cuenta respuesta = new Cuenta();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Banco")) respuesta.Banco = Convert.ToString(pRegistro["Banco"]);
            if (!pRegistro.IsNull("Cuenta")) respuesta.NoCuenta = Convert.ToString(pRegistro["Cuenta"]);
            if (!pRegistro.IsNull("CtaClabe")) respuesta.CtaClabe = Convert.ToString(pRegistro["CtaClabe"]);
            if (!pRegistro.IsNull("Sucursal")) respuesta.Sucursal = Convert.ToString(pRegistro["Sucursal"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = Convert.ToString(pRegistro["Moneda"]);
            return respuesta;
        }

        public bool Existe(string pIdProveedor,  string pCuenta)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM CuentasProveedor WHERE Id=" + pIdProveedor + " and Cuenta='" + pCuenta + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void Eliminar(string pIdProveedor, string pCuenta)
        {
            StringBuilder SqlCmd = new StringBuilder("DELETE CuentasProveedor WHERE Id=" + pIdProveedor + " and Cuenta='" + pCuenta + "'");
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }

    }

    public class Cuenta
      {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mBanco = String.Empty;
        public string Banco { get { return mBanco; } set { mBanco = value; } }
        private string mNoCuenta = String.Empty;
        public string NoCuenta { get { return mNoCuenta; } set { mNoCuenta = value; } }
        private string mCtaClabe = String.Empty;
        public string CtaClabe { get { return mCtaClabe; } set { mCtaClabe = value; } }
        private string mSucursal = String.Empty;
        public string Sucursal { get { return mSucursal; } set { mSucursal = value; } }
        private string mMoneda = String.Empty;
        public string Moneda { get { return mMoneda; } set { mMoneda = value; } }
    }
    
}
