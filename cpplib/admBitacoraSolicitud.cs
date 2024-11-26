using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace cpplib
{
   public class admBitacoraSolicitud
    {
        public bool Registrar(Bitacora pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO BitacoraSolicitud (IdSolicitud,FechaRegistro,Estado,IdUsr,Nombre,Importe)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdSolicitud);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.Importe);
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public bool RegistrarPago(Bitacora pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO BitacoraSolicitud (IdSolicitud,FechaRegistro,Estado,IdUsr,Nombre,Importe)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdSolicitud);
            SqlCmd.Append(",'" + pDatos.FechaRegistro.ToString("dd/MM/yyyy")  + "'");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.Importe);
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        private Bitacora arma(DataRow pRegistro)
        {
            Bitacora respuesta = new Bitacora();
            if (!pRegistro.IsNull("IdSolicitud")) respuesta.IdSolicitud = Convert.ToInt32(pRegistro["IdSolicitud"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (Solicitud.solEstado)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            return respuesta;
        }

        public List<Bitacora> daSeguimientoBitacora(int pIdSolicitud)
        {
            List<Bitacora> respuesta = new List<Bitacora>();
            mbd.BD BD = new mbd.BD();
            StringBuilder SqlCmd = new StringBuilder("SELECT *  FROM BitacoraSolicitud where IdUsr>=0 and IdSolicitud=" + pIdSolicitud.ToString ());
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(arma(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }
    }

    public class Bitacora
    {
        private int mIdSolicitud = 0;
        public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private Solicitud.solEstado mEstado = 0;
        public Solicitud.solEstado Estado { get { return mEstado; } set { mEstado = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private Decimal mImporte = 0;
        public Decimal Importe { get { return mImporte; } set {mImporte=value; } }
    }
}
