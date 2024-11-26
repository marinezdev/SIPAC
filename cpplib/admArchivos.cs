using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admArchivos
    {
        public int daNumeroComprobante(int pIdSolicitud)
        {
            int Id = 1;
            String SqlCmd = "SELECT MAX(IdDocumento) AS Id FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString() + " AND Tipo=" + TipoArchivo.Comprobante.ToString("d");
            mbd.BD BD = new mbd.BD();
            DataTable Datos = BD.LeeDatos(SqlCmd);
            if (Datos.Rows.Count > 0)
            {
                if (!Datos.Rows[0].IsNull("Id")) 
                { 
                    Id = (Convert.ToInt32(Datos.Rows[0]["Id"]) + 1); 
                }
            }
            return Id;
        }

        public bool Agrega(Archivo pDatos)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_Archivos (");
            SqlCmd.Append("IdSolicitud");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",Tipo");
            SqlCmd.Append(",IdDocumento");
            SqlCmd.Append(",ArchivoOrigen");
            SqlCmd.Append(",ArchivoDestino");
            SqlCmd.Append(",Cantidad");
            SqlCmd.Append(",TipoCambio");
            SqlCmd.Append(",Pesos");
            SqlCmd.Append(",Nota");
            SqlCmd.Append(",IdPago");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdSolicitud.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.Tipo.ToString("d"));
            SqlCmd.Append("," + pDatos.IdDocumento.ToString());
            SqlCmd.Append(",'" + pDatos.ArchvioOrigen + "'");
            SqlCmd.Append(",'" + pDatos.ArchivoDestino + "'");
            SqlCmd.Append("," + pDatos.Cantidad.ToString());
            SqlCmd.Append("," + pDatos.TipoCambio.ToString());
            SqlCmd.Append("," + pDatos.Pesos.ToString());
            SqlCmd.Append(",'" + pDatos.Nota.ToString() + "'");
            SqlCmd.Append("," + pDatos.IdPago.ToString());
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        private Archivo arma(DataRow pRegistro)
        {
            Archivo respuesta = new Archivo();
            if (!pRegistro.IsNull("IdSolicitud")) respuesta.IdSolicitud = Convert.ToInt32(pRegistro["IdSolicitud"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Tipo")) respuesta.Tipo = (TipoArchivo)(pRegistro["Tipo"]);
            if (!pRegistro.IsNull("IdDocumento")) respuesta.IdDocumento = Convert.ToInt32(pRegistro["IdDocumento"]);
            if (!pRegistro.IsNull("Cantidad")) respuesta.Cantidad = Convert.ToDecimal(pRegistro["Cantidad"]);
            if (!pRegistro.IsNull("ArchivoOrigen")) respuesta.ArchvioOrigen = Convert.ToString(pRegistro["ArchivoOrigen"]);
            if (!pRegistro.IsNull("ArchivoDestino")) respuesta.ArchivoDestino = Convert.ToString(pRegistro["ArchivoDestino"]);
            if (!pRegistro.IsNull("TipoCambio")) respuesta.TipoCambio = Convert.ToDecimal(pRegistro["TipoCambio"]);
            if (!pRegistro.IsNull("Pesos")) respuesta.Pesos = Convert.ToDecimal(pRegistro["Pesos"]);
            if (!pRegistro.IsNull("Nota")) respuesta.Nota = pRegistro["Nota"].ToString();
            if (!pRegistro.IsNull("IdPago")) respuesta.IdPago = Convert.ToInt32(pRegistro["IdPago"]);
            return respuesta;
        }

        public List<Archivo> ListaComprobantes(int pIdSolicitud)
        {
            List<Archivo> respuesta = new List<Archivo>();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString();
            SqlCmd += " AND Tipo=" + TipoArchivo.Comprobante.ToString("d") + " ORDER BY IdDocumento";
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) 
            { 
                respuesta.Add(arma(reg)); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public Archivo cargaFactura(int pIdSolicitud)
        {
            Archivo respuesta = new Archivo();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString();
            SqlCmd += "and tipo=" + TipoArchivo.Factura.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) 
            { 
                respuesta = arma(datos.Rows[0]); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public Archivo cargaComprobante(int pIdSolicitud, int IdDocto)
        {
            Archivo respuesta = new Archivo();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString();
            SqlCmd += " AND IdDocumento=" + IdDocto.ToString();
            SqlCmd += " AND tipo=" + TipoArchivo.Comprobante.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) 
            { 
                respuesta = arma(datos.Rows[0]); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<Archivo> ListaArchivosSolicitud(int pIdSolicitud)
        {
            List<Archivo> respuesta = new List<Archivo>();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString() + " ORDER BY Tipo";
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) 
            { 
                respuesta.Add(arma(reg)); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public Decimal DaImporteTotalComprobantes(int pIdSolicitud)
        {
            Decimal respuesta = 0;
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT SUM(Cantidad) AS total  FROM trf_Archivos WHERE IdSolicitud=" + pIdSolicitud.ToString();
            SqlCmd += "and tipo=" + TipoArchivo.Comprobante.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull("total")) 
                { 
                    respuesta = Convert.ToDecimal(datos.Rows[0]["total"]); 
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }
    }
        public class Archivo
        {
            private int mIdSolicitud = 0;
            public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
            private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
            public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
            private TipoArchivo mTipo = TipoArchivo.Factura;
            public TipoArchivo Tipo { get { return mTipo; } set { mTipo = value; } }
            private int mIdDocumento = 0;
            public int IdDocumento { get { return mIdDocumento; } set { mIdDocumento = value; } }
            private Decimal mCantidad = 0;
            public Decimal Cantidad { get { return mCantidad; } set { mCantidad = value; } }
            private String mArchivoOrigen = String.Empty;
            public String ArchvioOrigen { get { return mArchivoOrigen; } set { mArchivoOrigen = value; } }
            private String mArchivoDestino = String.Empty;
            public String ArchivoDestino { get { return mArchivoDestino; } set { mArchivoDestino = value; } }
            private Decimal mTipoCambio = 0;
            public Decimal TipoCambio { get { return mTipoCambio; } set { mTipoCambio = value; } }
            private Decimal mPesos = 0;
            public Decimal Pesos { get { return mPesos; } set { mPesos = value; } }
            private Decimal mCantiadPorPagar= 0;
            public Decimal CantiadPorPagar { get { return mCantiadPorPagar; } set { mCantiadPorPagar = value; } }
            private String mNota = String.Empty;
            public String Nota { get { return mNota; } set { mNota = value; } }
            private int mIdPago = 0;
            public int IdPago { get { return mIdPago; } set { mIdPago = value; } }
            
        }

        public enum TipoArchivo { Factura = 1, Xml = 2, Comprobante = 3 }
    
}
