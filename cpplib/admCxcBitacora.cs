using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
   public  class admCxcBitacora
    {
        public bool Registrar(cxcBitacora pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_Bitacora (IdServicio,IdOrdenFactura,FechaRegistro,Estado,IdUsr,Nombre)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdServicio );
            SqlCmd.Append("," + pDatos.IdOrdenFactura);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public bool RegistrarPago(cxcBitacora pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_Bitacora (IdServicio,IdOrdenFactura,FechaRegistro,Estado,IdUsr,Nombre)");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdServicio);
            SqlCmd.Append("," + pDatos.IdOrdenFactura);
            SqlCmd.Append(",'" + pDatos.FechaRegistro.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        private cxcBitacora arma(DataRow pRegistro)
        {
            cxcBitacora respuesta = new cxcBitacora();
            if (!pRegistro.IsNull("IdServicio")) respuesta.IdServicio = Convert.ToInt32(pRegistro["IdServicio"]);
            if (!pRegistro.IsNull("IdOrdenFactura")) respuesta.IdOrdenFactura = Convert.ToInt32(pRegistro["IdOrdenFactura"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (OrdenFactura.EstadoOrdFac)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public List<cxcBitacora> daSeguimientoBitacora(int pIdSolicitud)
        {
            List<cxcBitacora> respuesta = new List<cxcBitacora>();
            mbd.BD BD = new mbd.BD();
            StringBuilder SqlCmd = new StringBuilder("SELECT *  FROM cxc_Bitacora where IdUsr>=0 and IdOrdenFactura=" + pIdSolicitud.ToString());
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows){respuesta.Add(arma(reg));}
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Eliminar( int IdOrdenFactura)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Delete cxc_Bitacora where IdOrdenFactura=" +  IdOrdenFactura);
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }
    }
    public class cxcBitacora
    {
        private int mIdServicio = 0;
        public int IdServicio { get { return mIdServicio; } set { mIdServicio = value; } }
        private int mIdOrdenFactura = 0;
        public int IdOrdenFactura { get { return mIdOrdenFactura; } set { mIdOrdenFactura = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private OrdenFactura.EstadoOrdFac mEstado = 0;
        public OrdenFactura.EstadoOrdFac Estado { get { return mEstado; } set { mEstado = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
