using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admFondos
    {
        public int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO trf_SolicitudFondosCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdFondeo");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("IdFondeo")) { Id = Convert.ToInt32(Datos.Rows[0]["IdFondeo"]); }
                }
            }
            return Id;
        }

        public Boolean RegistraLoteFondeo(LoteFondos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_SolicitudFondos(");
            SqlCmd.Append("IdFondeo");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Empresa");
            SqlCmd.Append(",NoSolicitudes");
            SqlCmd.Append(",ImporteMx");
            SqlCmd.Append(",ImporteDlls");
            SqlCmd.Append(",TipoCambio");
            SqlCmd.Append(",Total");
            SqlCmd.Append(",Idusr");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdFondeo.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
            SqlCmd.Append(",'" + pDatos.Empresa  + "'");
            SqlCmd.Append("," + pDatos.NoSolicitudes);
            SqlCmd.Append("," + pDatos.ImporteMx);
            SqlCmd.Append("," + pDatos.ImporteDlls );
            SqlCmd.Append("," + pDatos.TipoCambio );
            SqlCmd.Append("," + pDatos.Total );
            SqlCmd.Append("," + pDatos.IdUsr );
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
                
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public Boolean RegistraLoteSinFondeo(LoteFondos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_SolicitudFondos(");
            SqlCmd.Append("IdFondeo");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Empresa");
            SqlCmd.Append(",NoSolicitudes");
            SqlCmd.Append(",ImporteMx");
            SqlCmd.Append(",ImporteDlls");
            SqlCmd.Append(",TipoCambio");
            SqlCmd.Append(",Total");
            SqlCmd.Append(",FechaFondos");
            SqlCmd.Append(",NoSolicitudesAprob");
            SqlCmd.Append(",TotalAprob");
            SqlCmd.Append(",Idusr");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdFondeo.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
            SqlCmd.Append(",'" + pDatos.Empresa + "'");
            SqlCmd.Append("," + pDatos.NoSolicitudes);
            SqlCmd.Append("," + pDatos.ImporteMx);
            SqlCmd.Append("," + pDatos.ImporteDlls);
            SqlCmd.Append("," + pDatos.TipoCambio);
            SqlCmd.Append("," + pDatos.Total);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.NoSolicitudes);
            SqlCmd.Append("," + pDatos.Total);
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append("," + LoteFondos.SolEdoFondos.Autorizado_sin_deposito.ToString("d"));
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public bool RegistraFactura(int IdFondo, int IdSolicitud, Decimal Importe, SolicitudFondos.enEstado Estado)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_SolicitudFondosDetalle(");
            SqlCmd.Append("IdFondeo");
            SqlCmd.Append(",IdSolicitud");
            SqlCmd.Append(",ImporteAutorizado");
            SqlCmd.Append(",ImporteFondos");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(",Marcado");
            SqlCmd.Append(",Idpago");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(IdFondo.ToString());
            SqlCmd.Append("," + IdSolicitud);
            SqlCmd.Append("," + Importe);
            SqlCmd.Append("," + Importe);
            SqlCmd.Append("," + Estado.ToString("d"));
            SqlCmd.Append(",0");
            SqlCmd.Append(",0");
            
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            
            return resultado;
        }

        public LoteFondos  carga(String pIdFondo)
        {
            LoteFondos resultado = null;
            String SqlCmd = "SELECT * FROM trf_SolicitudFondos WHERE IdFondeo = " + pIdFondo;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        private LoteFondos arma(DataRow pRegistro)
        {
            LoteFondos resultado = new LoteFondos();
            if (!pRegistro.IsNull("IdFondeo")) { resultado.IdFondeo = Convert.ToInt32(pRegistro["IdFondeo"]); }
            if (!pRegistro.IsNull("FechaRegistro")) { resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]); }
            if (!pRegistro.IsNull("IdEmpresa")) { resultado.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]); }
            if (!pRegistro.IsNull("Empresa")) { resultado.Empresa = (Convert.ToString(pRegistro["Empresa"])).Trim(); }
            if (!pRegistro.IsNull("NoSolicitudes")) { resultado.NoSolicitudes = Convert.ToInt32(pRegistro["NoSolicitudes"]); }
            if (!pRegistro.IsNull("ImporteMx")) { resultado.ImporteMx = Convert.ToDecimal(pRegistro["ImporteMx"]); }
            if (!pRegistro.IsNull("ImporteDlls")) { resultado.ImporteDlls = Convert.ToDecimal(pRegistro["ImporteDlls"]); }
            if (!pRegistro.IsNull("TipoCambio")) { resultado.TipoCambio = Convert.ToDecimal(pRegistro["TipoCambio"]); }
            if (!pRegistro.IsNull("FechaFondos")) { resultado.FechaFondos = Convert.ToDateTime(pRegistro["FechaFondos"]); }
            if (!pRegistro.IsNull("NoSolicitudesAprob")) { resultado.NoSolicitudesAprob  = Convert.ToInt32(pRegistro["NoSolicitudesAprob"]); }
            if (!pRegistro.IsNull("TotalAprob")) { resultado.TotalAprob = Convert.ToDecimal(pRegistro["TotalAprob"]); }
            if (!pRegistro.IsNull("IdUsr")) { resultado.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]); }
            if (!pRegistro.IsNull("Estado")) { resultado.Estado = (LoteFondos.SolEdoFondos)pRegistro["Estado"]; }
            return resultado;
        }

        public List<LoteFondos> DasolicitudesFondos(string IdEmpresa)
        {
            List<LoteFondos> resultado = new List<LoteFondos>();
            String SqlCmd = "SELECT * FROM trf_SolicitudFondos ";
            SqlCmd += " where  Estado =" + LoteFondos.SolEdoFondos.Autorizado.ToString ("d") + " And IdEmpresa=" + IdEmpresa; ;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<LoteFondos> DasolicitudesFondosXEmpresa(String IdEmpresa, String FechaIni, string FechaFin, string Estado)
        {
            List<LoteFondos> resultado = new List<LoteFondos>();
            String SqlCmd = "SELECT * FROM trf_SolicitudFondos ";
            SqlCmd += " where IdEmpresa=" + IdEmpresa ;
            SqlCmd += " AND (FechaRegistro >='" + FechaIni + "' and FechaRegistro < DATEADD(dd,1,'" + FechaFin + "'))";
            if (Estado != "0") { SqlCmd += "  And Estado=" + Estado; }
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }
        
        public DataTable DaDetalleSolicitudesXFondos(string IdFondeo)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.FechaRegistro,S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.IdSolicitud,S.Moneda,");
            SqlCmd.Append(" CASE WHEN S.ConFactura = 0 THEN 'NO' WHEN  S.ConFactura = 1 THEN 'SI' END  AS ConFactura,SF.ImporteFondos,SF.ImporteAutorizado,SF.Estado,S.DescProyecto,S.Estado as EdoSol ");
            SqlCmd.Append(" FROM trf_SolicitudFondosDetalle as SF inner join trf_Solicitud  as S");
            SqlCmd.Append(" on S.IdSolicitud =SF.IdSolicitud");
            SqlCmd.Append(" left  join [dbo].[usuario] as U");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng ");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio");
            SqlCmd.Append(" Where sf.IdFondeo =" + IdFondeo) ;
            SqlCmd.Append(" order by S.Proveedor,S.FechaFactura");
          
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public void ActualizaSolicitudFondos(string pIdFondeo,int NoSolAprobadas, decimal TotalAProb,int IdUsr)
        {
            String SqlCmd = "UPDATE trf_SolicitudFondos SET ";
            SqlCmd += "FechaFondos=getdate()";
            SqlCmd += ",NoSolicitudesAprob=" + NoSolAprobadas;
            SqlCmd += ",TotalAprob=" + TotalAProb;
            SqlCmd += ",Estado=20";
            SqlCmd += ",IdUsrFondos=" + IdUsr;

            SqlCmd += "WHERE IdFondeo=" + pIdFondeo;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void Actualizafactura(string IdFondeo,int pIdSol, decimal TotalAProb, SolicitudFondos.enEstado Estado )
        {
            String SqlCmd = "UPDATE trf_SolicitudFondosDetalle SET ";
            SqlCmd += " ImporteFondos=" + TotalAProb;
            SqlCmd += ",Estado=" + Estado.ToString ("d");
            SqlCmd += " WHERE IdFondeo=" + IdFondeo;
            SqlCmd += " and IdSolicitud=" + pIdSol;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }
        public void ActualizaEstadofactura(string IdFondeo, int pIdSol, SolicitudFondos.enEstado Estado)
        {
            String SqlCmd = "UPDATE trf_SolicitudFondosDetalle SET ";
            SqlCmd += " Estado=" + Estado.ToString ("d");
            SqlCmd += " WHERE IdFondeo=" + IdFondeo;
            SqlCmd += " and IdSolicitud=" + pIdSol;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }
                
        
        // SECCION PARA REGISTRO DEL ARCHIVO DE FONDOS.

        public int daNumeroComprobante(int pIdFondeo)
        {
            int Id = 1;
            String SqlCmd = "Select Max(IdDocumento) AS Id from trf_ArchivoFondos  where IdFondeo=" + pIdFondeo.ToString();
            mbd.BD BD = new mbd.BD();
            DataTable Datos = BD.LeeDatos(SqlCmd);
            if (Datos.Rows.Count > 0)
            {
                if (!Datos.Rows[0].IsNull("Id")) { Id = (Convert.ToInt32(Datos.Rows[0]["Id"]) + 1); }
            }
            return Id;
        }

        public bool AgregaArchivo(ArchivoFodos pDatos)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_ArchivoFondos (");
            SqlCmd.Append("IdFondeo");
            SqlCmd.Append(",IdDocumento");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",ArchivoOrigen");
            SqlCmd.Append(",ArchivoDestino");
            SqlCmd.Append(",Nota");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdFondeo.ToString()); 
            SqlCmd.Append("," +pDatos.IdDocumento.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + pDatos.ArchivoOrigen  + "'");
            SqlCmd.Append(",'" + pDatos.ArchivoDestino + "'");
            SqlCmd.Append(",'" + pDatos.Nota.ToString() + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        private ArchivoFodos  armaArchivo(DataRow pRegistro)
        {
            ArchivoFodos respuesta = new ArchivoFodos();
            if (!pRegistro.IsNull("IdFondeo")) respuesta.IdFondeo = Convert.ToInt32(pRegistro["IdFondeo"]);
            if (!pRegistro.IsNull("IdDocumento")) respuesta.IdDocumento = Convert.ToInt32(pRegistro["IdDocumento"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("ArchivoOrigen")) respuesta.ArchivoOrigen = Convert.ToString(pRegistro["ArchivoOrigen"]);
            if (!pRegistro.IsNull("ArchivoDestino")) respuesta.ArchivoDestino = Convert.ToString(pRegistro["ArchivoDestino"]);
            if (!pRegistro.IsNull("Nota")) respuesta.Nota = pRegistro["Nota"].ToString();
            return respuesta;
        }

        public ArchivoFodos  cargaArchivo(string pIdFondeo,string IdDocumento)
        {
            ArchivoFodos respuesta = new ArchivoFodos();
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_ArchivoFondos  WHERE IdFondeo=" + pIdFondeo + "and Iddocumento=" + IdDocumento; 
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = armaArchivo(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<ArchivoFodos> DaLista(string pIdFondeo)
        {
            List<ArchivoFodos> Lista = new List<ArchivoFodos>(); 
            mbd.BD BD = new mbd.BD();
            String SqlCmd = "SELECT * FROM trf_ArchivoFondos  WHERE IdFondeo=" + pIdFondeo;
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { Lista.Add(armaArchivo(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return Lista;
        }

        
       public DataTable ConsultaDeSeguimientoLotes(int IdEmpresa, string FhInicio, string FhTermino, string Tipo)
        {
            string SqlCmd = "select * from  ConsultaSeguimientoLotes(" + IdEmpresa + ",'" + FhInicio + "',' " + FhTermino + "')";
            if (Tipo == "1") { SqlCmd += " where NoPagadas =0 "; }
            if (Tipo == "2") { SqlCmd += " where NoPagadas >0 "; } 
            SqlCmd+= " order by IdFondeo ";

            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

       public void ActualizaDatosPagoFactura( int IdFondeo, int pIdSol,int IdPago, SolicitudFondos.enEstado Estado)
       {
           String SqlCmd = "UPDATE trf_SolicitudFondosDetalle SET ";
           SqlCmd += " Estado=" + Estado.ToString ("d");
           SqlCmd += " ,Marcado=0";
           SqlCmd += " ,IdPago=" + IdPago.ToString();
           SqlCmd += " WHERE IdFondeo=" + IdFondeo;
           SqlCmd += " and IdSolicitud=" + pIdSol;
           mbd.BD BD = new mbd.BD();
           BD.EjecutaCmd(SqlCmd);
           BD.CierraBD();
       }

        // SECCION PARA SELECIONAR SOLICITUDES A PAGAR
        
       public DataTable ListaSolicitudesXPagar(String IdEmpresa, string RFC, string Proyecto)
       {
           StringBuilder SqlCmd = new StringBuilder("select F.IdFondeo,F.Marcado,S.IdSolicitud,S.FechaRegistro ,S.Factura,s.FechaFactura,s.Proveedor,S.Proyecto,S.Moneda,S.Importe,s.CantidadPagar ");
           SqlCmd.Append(" from trf_SolicitudFondosDetalle  as F inner join trf_Solicitud as S  on S.IdSolicitud =F.IdSolicitud ");
           SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and F.Estado=" + SolicitudFondos.enEstado.Con_fondos.ToString("d"));
           if (RFC != "0") { SqlCmd.Append(" and S.rfc='" + RFC + "'"); }
           if (Proyecto != "0") { SqlCmd.Append(" and S.Proyecto='" + Proyecto + "'"); }
           SqlCmd.Append(" order by  F.IdFondeo, S.Proveedor,S.FechaFactura");
           mbd.BD BD = new mbd.BD();
           DataTable datos = BD.LeeDatos(SqlCmd.ToString());
           BD.CierraBD();
           return datos;
       }


       public void QuitarMarcarGrupoPago(int IdFondeo, int IdSolicitud, string IdUsr)
       {
           String SqlCmd = "UPDATE trf_SolicitudFondosDetalle SET Marcado=0";
           SqlCmd += " WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ") and IdFondeo=" + IdFondeo.ToString() + " and IdSolicitud=" + IdSolicitud.ToString();
           mbd.BD BD = new mbd.BD();
           BD.EjecutaCmd(SqlCmd);
           BD.CierraBD();
       }

       public bool PonerMarcarGrupoPago(int IdFondeo, int IdSolicitud, string IdUsr)
       {
           String SqlCmd = "UPDATE trf_SolicitudFondosDetalle SET Marcado=" + IdUsr;
           SqlCmd += " WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ") and IdFondeo=" + IdFondeo.ToString() + " and IdSolicitud=" + IdSolicitud.ToString() ;
           SqlCmd += " and Estado=" + SolicitudFondos.enEstado.Con_fondos.ToString("d");
           mbd.BD BD = new mbd.BD();
           bool resultado = BD.EjecutaCmd(SqlCmd);
           BD.CierraBD();
           return resultado;
       }

       public DataTable DasolMarcadasPago(string IdEmpresa,String IdUSr)
       {
           StringBuilder SqlCmd = new StringBuilder("select F.IdFondeo,S.IdSolicitud,S.FechaRegistro ,S.Factura,s.FechaFactura,s.Proveedor,S.Proyecto,S.Moneda,S.Importe,s.CantidadPagar ");
           SqlCmd.Append(" from trf_SolicitudFondosDetalle  as F inner join trf_Solicitud as S  on S.IdSolicitud =F.IdSolicitud ");
           SqlCmd.Append(" Where F.Marcado=" + IdUSr + " and F.Estado=" + SolicitudFondos.enEstado.Con_fondos.ToString("d"));
           SqlCmd.Append(" and S.IdEmpresa=" + IdEmpresa) ;
           SqlCmd.Append(" order by  F.IdFondeo, S.Proveedor,S.FechaFactura");
           mbd.BD BD = new mbd.BD();
           DataTable datos = BD.LeeDatos(SqlCmd.ToString());
           BD.CierraBD();
           return datos;
       }
        
    }

    public class LoteFondos
    {
        private int mIdFondeo = 0;
        public int IdFondeo { get { return mIdFondeo; } set { mIdFondeo = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private string mEmpresa = String.Empty;
        public string Empresa { get { return mEmpresa; } set { mEmpresa = value; } }
        private int mNoSolicitudes = 0;
        public int NoSolicitudes { get { return mNoSolicitudes; } set { mNoSolicitudes = value; } }
        private decimal mImporteMx = 0;
        public decimal ImporteMx { get { return mImporteMx; } set { mImporteMx = value; } }
        private decimal mImporteDlls = 0;
        public decimal ImporteDlls { get { return mImporteDlls; } set { mImporteDlls = value; } }
        private decimal mTipoCambio = 0;
        public decimal TipoCambio { get { return mTipoCambio; } set { mTipoCambio = value; } }
        public decimal Total { get { return (mImporteMx + (ImporteDlls * mTipoCambio)); } }
        private DateTime mFechaFondos = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFondos { get { return mFechaFondos; } set { mFechaFondos = value; } }
        private int mNoSolicitudesAprob = 0;
        public int NoSolicitudesAprob { get { return mNoSolicitudesAprob; } set { mNoSolicitudesAprob = value; } }
        private decimal mTotalAprob = 0;
        public decimal TotalAprob { get { return mTotalAprob; } set { mTotalAprob = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private SolEdoFondos mEstado = SolEdoFondos.Autorizado;
        public SolEdoFondos Estado { get { return mEstado; } set { mEstado = value; } }

        public enum SolEdoFondos { Autorizado = 10, Con_Fondos = 20, Autorizado_sin_deposito = 30 ,Rechazado=100}
    }

    public class ArchivoFodos {
        private int mIdFondeo = 0;
        public int IdFondeo { get { return mIdFondeo; } set { mIdFondeo = value; } }
        private int mIdDocumento = 0;
        public int IdDocumento { get { return mIdDocumento; } set { mIdDocumento = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mArchivoOrigen = string.Empty;
        public string  ArchivoOrigen { get { return mArchivoOrigen; } set { mArchivoOrigen = value; } }
        private string mArchivoDestino = String.Empty;
        public string ArchivoDestino { get { return mArchivoDestino; } set { mArchivoDestino = value; } }
        private string mNota = String.Empty;
        public string Nota { get { return mNota; } set { mNota = value; } }    
    }

    public class SolicitudFondos
    {
        private int mIdFondeo = 0;
        public int IdFondeo { get { return mIdFondeo; } set { mIdFondeo = value; } }
        private int mIdSolicitud = 0;
        public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
        private Decimal  mImporteAutorizado = 0;
        public Decimal ImporteAutorizado { get { return mImporteAutorizado; } set { mImporteAutorizado = value; } }
        private Decimal mImporteFondos = 0;
        public Decimal ImporteFondos { get { return mImporteFondos; } set { mImporteFondos = value; } }
        private int mEstado = 0;
        public int Estado { get { return mEstado; } set { mEstado = value; } }
        private String  mMoneda = String.Empty;
        public String Moneda { get { return mMoneda; } set { mMoneda = value; } }

        public enum enEstado {Rechazado=0, Autorizado = 10, Con_fondos = 20, Pagado = 30}
    }

}
