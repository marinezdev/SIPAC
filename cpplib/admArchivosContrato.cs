using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admArchivosContrato
    {
        public bool Agrega(ArchivoContrato pDatos)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_ArchivoContrato(");
            SqlCmd.Append("IdServicio");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",ArchivoDestino");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdServicio.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + pDatos.ArchivoDestino + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        private ArchivoContrato arma(DataRow pRegistro)
        {
            ArchivoContrato respuesta = new ArchivoContrato();
            if (!pRegistro.IsNull("IdServicio")) respuesta.IdServicio = Convert.ToInt32(pRegistro["IdServicio"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("ArchivoDestino")) respuesta.ArchivoDestino = Convert.ToString(pRegistro["ArchivoDestino"]);
            respuesta.Exite = true;
            return respuesta;
        }

        public ArchivoContrato carga(int pIdServicio)
        {
            ArchivoContrato respuesta = new ArchivoContrato();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_ArchivoContrato  WHERE IdServicio=" + pIdServicio.ToString();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Actualiza(ArchivoContrato pDatos)
        {
            bool respuesta = false;
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "UPDATE cxc_ArchivoContrato";
            SqlCmd += " SET ArchivoDestino='" + pDatos.ArchivoDestino + "'";
            SqlCmd += " WHERE IdServicio=" + pDatos.IdServicio.ToString();
            respuesta = BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
            return respuesta;
        }
    }
    public class ArchivoContrato {
        private int mIdServicio = 0;
        public int IdServicio { get { return mIdServicio; } set { mIdServicio = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private String mArchivoDestino = String.Empty;
        public String ArchivoDestino { get { return mArchivoDestino; } set { mArchivoDestino = value; } }
        public bool mExite = false;
        public bool Exite { get { return mExite; } set { mExite = value; } }
        private String mUbicacionTmp = String.Empty;
        public String UbicacionTmp { get { return mUbicacionTmp; } set { mUbicacionTmp = value; } }
    }
 }
