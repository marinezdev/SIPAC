using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admCxpNotaCredito
    {
        public int nueva(cxpNotaCredito  pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_NotaCredito (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Fecha");
            SqlCmd.Append(",RFC");
            SqlCmd.Append(",Proveedor");
            SqlCmd.Append(",Descripcion");
            SqlCmd.Append(",Importe");
            SqlCmd.Append(",Moneda");
            SqlCmd.Append(",ImportePendiente");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(",IdUsr");
            SqlCmd.Append(",IdSolicitudOrigen");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append("," + pDatos.IdEmpresa );
            SqlCmd.Append(",'" + pDatos.Fecha.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append(",'" + pDatos.Rfc.ToString() + "'");
            SqlCmd.Append(",'" + pDatos.Proveedor + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append("," + pDatos.Importe.ToString ());
            SqlCmd.Append(",'" + pDatos.Moneda + "'");
            SqlCmd.Append("," + pDatos.ImportePendiente.ToString());
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append("," + pDatos.IdSolicitudOrigen);
            SqlCmd.Append(")");
            
            mbd.BD BD = new mbd.BD();
            int Id = 0;
            bool resultado =  BD.EjecutaCmd(SqlCmd.ToString());
            if (resultado){
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdNotaCredito");
                if (Datos.Rows.Count > 0) { if (!Datos.Rows[0].IsNull("IdNotaCredito")) { Id = Convert.ToInt32(Datos.Rows[0]["IdNotaCredito"]); } }
            }            
            BD.CierraBD();
            return Id;
        }
        
        private cxpNotaCredito arma(DataRow pRegistro)
        {
            cxpNotaCredito respuesta = new cxpNotaCredito();
            if (!pRegistro.IsNull("IdNotaCredito")) respuesta.IdNotaCredito = Convert.ToInt32(pRegistro["IdNotaCredito"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Fecha")) respuesta.Fecha = Convert.ToDateTime(pRegistro["Fecha"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Proveedor")) respuesta.Proveedor = Convert.ToString(pRegistro["Proveedor"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = Convert.ToString(pRegistro["Moneda"]);
            if (!pRegistro.IsNull("ImportePendiente")) respuesta.ImportePendiente = Convert.ToDecimal(pRegistro["ImportePendiente"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (cxpNotaCredito.enEstado)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("IdSolicitudOrigen")) respuesta.IdSolicitudOrigen = Convert.ToInt32(pRegistro["IdSolicitudOrigen"]);
            return respuesta;
        }

        public cxpNotaCredito carga(int IdNotaCredito)
        {
            cxpNotaCredito respuesta = new cxpNotaCredito();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM trf_NotaCredito WHERE IdNotaCredito=" + IdNotaCredito.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();        
            BD.CierraBD();
            return respuesta;
        }

        public bool Eliminar(int IdNotaCredito)
        {
            bool Resultado = false;

            mbd.BD BD = new mbd.BD();
            string SqlCmd = "Delete trf_NotaCredito where IdNotaCredito=" + IdNotaCredito + " and Estado=" + cxpNotaCredito.enEstado.Activa.ToString("d");
            Resultado = BD.EjecutaCmd(SqlCmd.ToString());
            if (Resultado)
            {
                SqlCmd = "Delete trf_NotaCreditoArchivos where IdNotaCredito=" + IdNotaCredito;
                Resultado = BD.EjecutaCmd(SqlCmd.ToString());
            }
            BD.CierraBD();

            return Resultado;
        }

        public void CambiaEstadoYMonto(int Id, cxpNotaCredito.enEstado  pEstado,Decimal Importe )
        {
            String SqlCmd = "UPDATE trf_NotaCredito SET Estado=" + pEstado.ToString("d") + ", ImportePendiente=" + Importe .ToString ();
            SqlCmd += " WHERE IdNotaCredito=" + Id.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public List<cxpNotaCredito > Lista(int IdEmpresa, string Consulta)
        {
            List<cxpNotaCredito> respuesta = new List<cxpNotaCredito>();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_NotaCredito where IdEmpresa=" + IdEmpresa.ToString() +  Consulta);
            SqlCmd.Append(" order by IdNotaCredito");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<cxpNotaCredito> listaNotasCreditoProveedor(int IdEmpresa, string Rfc)
        {
            List<cxpNotaCredito> respuesta = new List<cxpNotaCredito>();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_NotaCredito where IdEmpresa=" + IdEmpresa.ToString() + " and RFC='" + Rfc + "'");
            SqlCmd.Append(" and estado=" + cxpNotaCredito .enEstado.Activa.ToString ("d") );
            SqlCmd.Append(" order by IdNotaCredito");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool  AsignarNotaCredito(int IdNotaCredito,int IdSolicitud, decimal Monto,int IdUsr){
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_NotaCreditoAsignacion (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",IdNotaCredito");
            SqlCmd.Append(",IdSolicitud");
            SqlCmd.Append(",Monto");
            SqlCmd.Append(",IdUsr");
            SqlCmd.Append(")");
            SqlCmd.Append("VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append("," + IdNotaCredito.ToString ());
            SqlCmd.Append("," + IdSolicitud.ToString () );
            SqlCmd.Append("," + Monto.ToString ());
            SqlCmd.Append("," + IdUsr.ToString ());
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            bool Resultado = BD.EjecutaCmd (SqlCmd.ToString());
            BD.CierraBD();
            return Resultado;
        }



        /// <summary>
        /// SECCION  MANEJO DE ARCHIVOS.
        /// </summary>

        public bool  AgregarArchivo(cxpNotaCreditoArchivo  pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_NotaCreditoArchivos (");
            SqlCmd.Append("IdNotaCredito");
            SqlCmd.Append(",Tipo");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdNotaCredito.ToString()  );
            SqlCmd.Append("," + pDatos.Tipo.ToString("d"));
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            
            SqlCmd.Append(")");

            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd.ToString());
            return resultado;
        }

        public cxpNotaCreditoArchivo CargaArchivo(int IdNotaCredito)
        {
            cxpNotaCreditoArchivo resultado = new cxpNotaCreditoArchivo();
            StringBuilder SqlCmd = new StringBuilder("Select * from trf_NotaCreditoArchivos  where IdNotaCredito="  + IdNotaCredito.ToString () );
            SqlCmd.Append(" and Tipo=" + cxpNotaCreditoArchivo .TipoArchivo .PDF.ToString ("d") );
            mbd.BD BD = new mbd.BD();
            DataTable  Datos = BD.LeeDatos (SqlCmd.ToString());
            if (Datos.Rows.Count > 0) { resultado = armaArchivo(Datos.Rows[0]);}
            return resultado;
        }

        public string  DaNombreImageNota(int IdNotaCredito)
        {
            string resultado = string.Empty; 
            StringBuilder SqlCmd = new StringBuilder("Select Nombre from trf_NotaCreditoArchivos  where IdNotaCredito=" + IdNotaCredito.ToString());
            SqlCmd.Append(" and  Tipo=1");
            mbd.BD BD = new mbd.BD();
            DataTable Datos = BD.LeeDatos(SqlCmd.ToString());
            if (Datos.Rows.Count > 0) { resultado = Datos.Rows[0]["Nombre"].ToString (); }
            return resultado;
        }

        private cxpNotaCreditoArchivo  armaArchivo(DataRow pRegistro)
        {
            cxpNotaCreditoArchivo respuesta = new cxpNotaCreditoArchivo();
            if (!pRegistro.IsNull("IdNotaCredito")) respuesta.IdNotaCredito = Convert.ToInt32(pRegistro["IdNotaCredito"]);
            if (!pRegistro.IsNull("Tipo")) respuesta.Tipo = (cxpNotaCreditoArchivo.TipoArchivo)(pRegistro["Tipo"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre= Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public DataTable DaSolicitudesRelacionadasNotaCredito(int IdNotaCredito) {
            StringBuilder SqlCmd = new StringBuilder("select S.* from trf_NotaCreditoAsignacion as NC inner join trf_Solicitud  as S");
            SqlCmd.Append(" on S.IdSolicitud = NC.IdSolicitud ");
            SqlCmd.Append(" where IdNotaCredito=" + IdNotaCredito.ToString());
            mbd.BD BD = new mbd.BD();
            DataTable resultado = BD.LeeDatos(SqlCmd.ToString());
            return resultado;
        }

        public DataTable  DaNotaCreditoAsignadasSolicitud(int IdSolicitud)
        {
            StringBuilder SqlCmd = new StringBuilder(" select NA.*,NC.Fecha , NC.Descripcion, NC.Importe  from trf_NotaCreditoAsignacion NA inner join trf_NotaCredito NC");
            SqlCmd.Append(" on NC.IdNotaCredito =NA.IdNotaCredito ");
            SqlCmd.Append(" where NA.IdSolicitud=" + IdSolicitud.ToString());
            mbd.BD BD = new mbd.BD();
            DataTable Resultado = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return Resultado;
        }

    }
    
    public class cxpNotaCredito
    {
        private int mIdNotaCredito = 0;
        public int IdNotaCredito { get { return mIdNotaCredito; } set { mIdNotaCredito = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private DateTime mFecha = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime Fecha { get { return mFecha; } set { mFecha = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mProveedor = String.Empty;
        public string Proveedor { get { return mProveedor; } set { mProveedor = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private string mMoneda = String.Empty;
        public string Moneda { get { return mMoneda; } set { mMoneda = value; } }
        private decimal mImportePendiente = 0;
        public decimal ImportePendiente { get { return mImportePendiente; } set { mImportePendiente = value; } }
        private enEstado mEstado = enEstado.Activa;
        public enEstado Estado { get { return mEstado; } set { mEstado = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private int mIdSolicitudOrigen = 0;
        public int IdSolicitudOrigen { get { return mIdSolicitudOrigen; } set { mIdSolicitudOrigen = value; } }
        private int mIdSolicitudAsignada = 0;
        public int IdSolicitudAsignada { get { return mIdSolicitudAsignada; } set { mIdSolicitudAsignada = value; } }

        public enum enEstado { Activa = 10, Parcial = 20, Asignada = 30 }
    }

    public class cxpNotaCreditoArchivo
    {
        private int mIdNotaCredito = 0;
        public int IdNotaCredito { get { return mIdNotaCredito; } set { mIdNotaCredito = value; } }
        private TipoArchivo mTipo = TipoArchivo.PDF ;
        public TipoArchivo Tipo { get { return mTipo; } set { mTipo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        
        public enum TipoArchivo { PDF = 1, XML = 2}    
    }

    
}
