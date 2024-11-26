using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace cpplib
{
    public class admArchivosCxc
    {
        public int daNumeroComprobante(int pIdOrdenFact)
        {
            int Id = 1;
            String SqlCmd = "Select Max(IdDocumento) AS Id from cxc_Archivos  where IdOrdenFactura=" + pIdOrdenFact.ToString() + " and Tipo=" + cxcTipoArchivo.Comprobante.ToString("d");
            mbd.BD BD = new mbd.BD();
            DataTable Datos = BD.LeeDatos(SqlCmd);
            if (Datos.Rows.Count > 0)
            {
                if (!Datos.Rows[0].IsNull("Id")) { Id = (Convert.ToInt32(Datos.Rows[0]["Id"]) + 1); }
            }
            return Id;
        }

        public bool Agrega(cxcArchivo pDatos)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_Archivos (");
            SqlCmd.Append("IdOrdenFactura");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",Tipo");
            SqlCmd.Append(",IdDocumento");
            SqlCmd.Append(",ArchivoOrigen");
            SqlCmd.Append(",ArchivoDestino");
            SqlCmd.Append(",Nota");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdOrdenFactura.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.Tipo.ToString("d"));
            SqlCmd.Append("," + pDatos.IdDocumento.ToString());
            SqlCmd.Append(",'" + pDatos.ArchvioOrigen + "'");
            SqlCmd.Append(",'" + pDatos.ArchivoDestino + "'");
            SqlCmd.Append(",'" + pDatos.Nota.ToString() + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        private cxcArchivo arma(DataRow pRegistro)
        {
            cxcArchivo respuesta = new cxcArchivo();
            if (!pRegistro.IsNull("IdOrdenFactura")) respuesta.IdOrdenFactura = Convert.ToInt32(pRegistro["IdOrdenFactura"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Tipo")) respuesta.Tipo = (cxcTipoArchivo)(pRegistro["Tipo"]);
            if (!pRegistro.IsNull("IdDocumento")) respuesta.IdDocumento = Convert.ToInt32(pRegistro["IdDocumento"]);
            if (!pRegistro.IsNull("ArchivoOrigen")) respuesta.ArchvioOrigen = Convert.ToString(pRegistro["ArchivoOrigen"]);
            if (!pRegistro.IsNull("ArchivoDestino")) respuesta.ArchivoDestino = Convert.ToString(pRegistro["ArchivoDestino"]);
            if (!pRegistro.IsNull("Nota")) respuesta.Nota = pRegistro["Nota"].ToString();
            return respuesta;
        }

        public List<cxcArchivo> ListaComprobantes(int pIdOrdenFac)
        {
            List<cxcArchivo> respuesta = new List<cxcArchivo>();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_Archivos Where IdOrdenFactura=" + pIdOrdenFac.ToString();
            SqlCmd += " and Tipo=" + cxcTipoArchivo.Comprobante.ToString("d") + "  order by IdDocumento";
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public cxcArchivo cargaFactura(int pIdOrdenFac)
        {
            cxcArchivo respuesta = new cxcArchivo();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_Archivos  WHERE IdOrdenFactura=" + pIdOrdenFac.ToString();
            SqlCmd += " and tipo=" + cxcTipoArchivo.Factura.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public cxcArchivo cargaDocumentoPago(int pIdOrdenFac, int Pagina)
        {
            cxcArchivo respuesta = new cxcArchivo();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_Archivos  WHERE IdOrdenFactura=" + pIdOrdenFac.ToString();
            SqlCmd += " and tipo=" + cxcTipoArchivo.Comprobante.ToString("d");
            SqlCmd += " and IdDocumento=" + Pagina.ToString() ;
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public cxcArchivo cargaComprobante(int pIdOrdenFactura, int IdDocto)
        {
            cxcArchivo respuesta = new cxcArchivo();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_Archivos  WHERE IdOrdenFactura=" + pIdOrdenFactura.ToString() + " and IdDocumento=" + IdDocto.ToString();
            SqlCmd += " and tipo=" + cxcTipoArchivo.Comprobante.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<cxcArchivo> ListaArchivosSolicitud(int pIdOrdenFactura)
        {
            List<cxcArchivo> respuesta = new List<cxcArchivo>();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM cxc_Archivos Where IdOrdenFactura=" + pIdOrdenFactura.ToString();
            SqlCmd += " order by Tipo";
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Eliminar(int IdOrdenFactura)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Delete cxc_Archivos where IdOrdenFactura=" + IdOrdenFactura);
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

    }
        public class cxcArchivo
        {
            private int mIdOrdenFactura = 0;
            public int IdOrdenFactura { get { return mIdOrdenFactura; } set { mIdOrdenFactura = value; } }
            private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
            public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
            private cxcTipoArchivo mTipo = cxcTipoArchivo.Factura;
            public cxcTipoArchivo Tipo { get { return mTipo; } set { mTipo = value; } }
            private int mIdDocumento = 0;
            public int IdDocumento { get { return mIdDocumento; } set { mIdDocumento = value; } }
            private String mArchivoOrigen = String.Empty;
            public String ArchvioOrigen { get { return mArchivoOrigen; } set { mArchivoOrigen = value; } }
            private String mArchivoDestino = String.Empty;
            public String ArchivoDestino { get { return mArchivoDestino; } set { mArchivoDestino = value; } }
            private Decimal mCantiadPorPagar= 0;
            public Decimal CantiadPorPagar { get { return mCantiadPorPagar; } set { mCantiadPorPagar = value; } }
            private String mNota = String.Empty;
            public String Nota { get { return mNota; } set { mNota = value; } }
        }

        public enum cxcTipoArchivo { Factura = 1, Xml = 2, Comprobante = 3 }
}
