using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admcxpBitacoraEventos
    {
        public bool Registrar(BitacoraEventos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_BitacoraEventos (IdSolicitud,FechaRegistro,IdUsr,Nombre,Descripcion)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdSolicitud);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        private BitacoraEventos arma(DataRow pRegistro)
        {
            BitacoraEventos respuesta = new BitacoraEventos();
            if (!pRegistro.IsNull("IdSolicitud")) respuesta.IdSolicitud = Convert.ToInt32(pRegistro["IdSolicitud"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            return respuesta;
        }
    }
    public class BitacoraEventos
    {
        private int mIdSolicitud = 0;
        public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        
    }
}
