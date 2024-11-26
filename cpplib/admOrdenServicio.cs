using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admOrdenServicio
    {
        public int daSiguienteIdServicio()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cxc_OrdenServicioCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdServicio");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("IdServicio")) { Id = Convert.ToInt32(Datos.Rows[0]["IdServicio"]); }
                }
            }
            return Id;
        }

        public bool nueva(OrdenServicio pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_OrdenServicio (");
            SqlCmd.Append("IdServicio");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",IdCliente");
            SqlCmd.Append(",Rfc");
            SqlCmd.Append(",Cliente");    
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Empresa");
            SqlCmd.Append(",TipoSolicitud");
            SqlCmd.Append(",FechaInicio");
            SqlCmd.Append(",FechaTermino");
            SqlCmd.Append(",Importe");
            SqlCmd.Append(",Periodos");
            SqlCmd.Append(",TipoPeriodo");
            SqlCmd.Append(",CondicionPago");
            SqlCmd.Append(",CondicionPagoDias");
            SqlCmd.Append(",Proyecto");
            SqlCmd.Append(",TipoMoneda");
            SqlCmd.Append(",IdCatServicio");
            SqlCmd.Append(",Servicio");
            SqlCmd.Append(",Descripcion");
            SqlCmd.Append(",IdUsr");
            SqlCmd.Append(",UnidadNegocio");
            SqlCmd.Append(",Contrato");
            SqlCmd.Append(",EnviaCorreoClte");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(",Especial");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdServicio.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pDatos.IdCliente.ToString());
            SqlCmd.Append(",'" + pDatos.Rfc + "'");
            SqlCmd.Append(",'" + pDatos.Cliente + "'"); 
            SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
            SqlCmd.Append(",'" + pDatos.Empresa + "'");
            SqlCmd.Append("," + pDatos.TipoSolicitud.ToString ("d"));
            SqlCmd.Append(",'" + pDatos.FechaInicio.ToString ("dd/MM/yyyy") + "'");
            SqlCmd.Append(",'" + pDatos.FechaTermino.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append("," + pDatos.Importe);
            SqlCmd.Append("," + pDatos.Periodos);
            SqlCmd.Append("," + pDatos.TipoPeriodo);
            SqlCmd.Append(",'" + pDatos.CondicionPago + "'");
            SqlCmd.Append("," + pDatos.CondicionPagoDias);
            SqlCmd.Append(",'" + pDatos.Proyecto  + "'");
            SqlCmd.Append(",'" + pDatos.TipoMoneda + "'");
            SqlCmd.Append("," + pDatos.IdCatServicio);
            SqlCmd.Append(",'" + pDatos.Servicio + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append("," + pDatos.UnidadNegocio);
            SqlCmd.Append("," + pDatos.Contrato);
            SqlCmd.Append("," + pDatos.EnviaCorreoClte);
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.Especial);
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public OrdenServicio carga(int pId)
        {
            OrdenServicio respuesta = new OrdenServicio();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cxc_OrdenServicio WHERE IdServicio=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private OrdenServicio arma(DataRow pRegistro)
        {
            OrdenServicio respuesta = new OrdenServicio();
            if (!pRegistro.IsNull("IdServicio")) respuesta.IdServicio = Convert.ToInt32(pRegistro["IdServicio"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdCliente")) respuesta.IdCliente = Convert.ToInt32(pRegistro["IdCliente"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Cliente")) respuesta.Cliente = Convert.ToString(pRegistro["Cliente"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Empresa")) respuesta.Empresa = Convert.ToString(pRegistro["Empresa"]);
            if (!pRegistro.IsNull("TipoSolicitud")) respuesta.TipoSolicitud = (OrdenServicio.enTipoSolicitud)(pRegistro["TipoSolicitud"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("FechaInicio")) respuesta.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("FechaTermino")) respuesta.FechaTermino = Convert.ToDateTime(pRegistro["FechaTermino"]);
            if (!pRegistro.IsNull("Periodos")) respuesta.Periodos = Convert.ToInt32(pRegistro["Periodos"]);
            if (!pRegistro.IsNull("TipoPeriodo")) respuesta.TipoPeriodo = Convert.ToInt32(pRegistro["TipoPeriodo"]);
            if (!pRegistro.IsNull("CondicionPago")) respuesta.CondicionPago = Convert.ToString(pRegistro["CondicionPago"]);
            if (!pRegistro.IsNull("CondicionPagoDias")) respuesta.CondicionPagoDias = Convert.ToInt32(pRegistro["CondicionPagoDias"]);
            if (!pRegistro.IsNull("Proyecto")) respuesta.Proyecto = Convert.ToString(pRegistro["Proyecto"]);
            if (!pRegistro.IsNull("TipoMoneda")) respuesta.TipoMoneda = Convert.ToString(pRegistro["TipoMoneda"]);
            if (!pRegistro.IsNull("IdCatServicio")) respuesta.IdCatServicio = Convert.ToInt32(pRegistro["IdCatServicio"]);
            if (!pRegistro.IsNull("Servicio")) respuesta.Servicio = Convert.ToString(pRegistro["Servicio"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("UnidadNegocio")) respuesta.UnidadNegocio = Convert.ToInt32(pRegistro["UnidadNegocio"]);
            if (!pRegistro.IsNull("Contrato")) respuesta.Contrato = Convert.ToInt32(pRegistro["Contrato"]);
            if (!pRegistro.IsNull("EnviaCorreoClte")) respuesta.EnviaCorreoClte = Convert.ToInt32(pRegistro["EnviaCorreoClte"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (OrdenServicio.EstadoOrdSvc)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Especial")) respuesta.Especial = Convert.ToInt32(pRegistro["Especial"]);
            return respuesta;
        }

        public List<OrdenServicio> ConsultaOrdenesServicio(string Consulta)
        {
            List<OrdenServicio> respuesta = new List<OrdenServicio>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "SELECT * FROM cxc_OrdenServicio where  periodos > 1";
            if (!string.IsNullOrEmpty(Consulta)) { cmdSql += Consulta; }
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public void ValidaCierreOrden(string Id) {
            string cmdSql = "Select * from cxc_OrdenFactura where IdServicio =" + Id + " and Estado< " + OrdenFactura.EstadoOrdFac .Pagado.ToString("d") ;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(cmdSql);
            if (datos.Rows.Count == 0)
            {
                cmdSql = "Update cxc_OrdenServicio set Estado=" + OrdenServicio .EstadoOrdSvc.Cerrado.ToString("d") ;
                cmdSql += " where IdServicio =" + Id;
                bool resultado = BD.EjecutaCmd(cmdSql);
            }
            datos.Dispose();
            BD.CierraBD();
        }


        public void ActualizaDatosOrden( int IdServicio)
        {
            string cmdSql = "update cxc_OrdenServicio";
            cmdSql += " set Periodos =(select count(*) from cxc_OrdenFactura where IdServicio =" + IdServicio + "),";
            cmdSql += " FechaTermino =(select max(fechaInicio) from cxc_OrdenFactura where IdServicio= " + IdServicio + ")";
            cmdSql += " where IdServicio =" + IdServicio;
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(cmdSql);
            BD.CierraBD();
        }

        public void ActualizaDatosOrden(string Id,DateTime FechaTermino,int Periodos)
        {
            string cmdSql = "Update cxc_OrdenServicio  set FechaTermino='" + FechaTermino.ToString("dd/MM/yyyy") + "'";
            cmdSql += " ,Periodos=" + Periodos.ToString ();
            cmdSql += " where IdServicio =" + Id;
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(cmdSql);
            BD.CierraBD();
        }

        public bool  ActualizaMontoOrden(string Id,string Monto, string Descripcion)
        {
            string cmdSql = "Update cxc_OrdenServicio  set Importe=" + Monto;
            cmdSql += ", Descripcion='" + Descripcion + "'";
            cmdSql += " where IdServicio =" + Id;
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(cmdSql);
            BD.CierraBD();
            return resultado;
        }

        public void ActualizaEstadoContrato(string idServicio, string Estado)
        {
            String SqlCmd = "UPDATE cxc_OrdenServicio SET Contrato=" + Estado + " WHERE IdServicio=" + idServicio;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public bool Eliminar(int IdServicio)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Delete cxc_OrdenServicio where IdServicio=" + IdServicio);
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public void CambiaEstadoOrden(int IdServicio , OrdenServicio.EstadoOrdSvc pEstado)
        {
            String SqlCmd = "UPDATE cxc_OrdenServicio SET Estado=" + pEstado.ToString("d") + " WHERE IdServicio=" + IdServicio.ToString ();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        

        
    }

    public class OrdenServicio {
        private int mIdServicio = 0;
        public int IdServicio { get { return mIdServicio; } set { mIdServicio = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdCliente = 0;
        public int IdCliente { get { return mIdCliente; } set { mIdCliente = value; } }
        private string mRfc = string.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mCliente = string .Empty;
        public string Cliente { get { return mCliente; } set { mCliente = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private string mEmpresa = String.Empty;
        public string Empresa { get { return mEmpresa; } set { mEmpresa = value; } }
        private enTipoSolicitud mTipoSolicitud = enTipoSolicitud.Fijo;
        public enTipoSolicitud TipoSolicitud { get { return mTipoSolicitud; } set { mTipoSolicitud = value; } }
        private decimal  mImporte= 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private string mImporteVista = "";
        public string ImporteVista { get { if (mImporte == 0) { mImporteVista = "----"; } else { mImporteVista = mImporte.ToString("C2"); } return mImporteVista; } }
        private DateTime mFechaInicio =new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return mFechaInicio; } set { mFechaInicio = value; } }
        private DateTime mFechaTermino = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaTermino { get { return mFechaTermino; } set { mFechaTermino = value; } }
        private int mPeriodos = 0;
        public int Periodos { get { return mPeriodos; } set { mPeriodos = value; } }
        private int mTipoPeriodo = 0;
        public int TipoPeriodo { get { return mTipoPeriodo; } set { mTipoPeriodo = value; } }
        private string mCondicionPago = String.Empty;
        public string CondicionPago { get { return mCondicionPago; } set { mCondicionPago = value; } }
        private int mCondicionPagoDias = 0;
        public int CondicionPagoDias { get { return mCondicionPagoDias; } set { mCondicionPagoDias = value; } }
        private string mProyecto = String.Empty;
        public string Proyecto { get { return mProyecto; } set { mProyecto = value; } }
        private string mTipoMoneda = String.Empty;
        public string TipoMoneda { get { return mTipoMoneda; } set { mTipoMoneda = value; } }
        private int mIdCatServicio = 0;
        public int IdCatServicio { get { return mIdCatServicio; } set { mIdCatServicio = value; } }
        private string mServicio = String.Empty;
        public string Servicio { get { return mServicio; } set { mServicio = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private int mUnidadNegocio = 0;
        public int UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
        private int mContrato = 0;
        public int Contrato { get { return mContrato; } set { mContrato = value; } }
        private int mEnviaCorreoClte = 0;
        public int EnviaCorreoClte { get { return mEnviaCorreoClte; } set { mEnviaCorreoClte = value; } }
        private EstadoOrdSvc mEstado = EstadoOrdSvc.Abierto;
        public EstadoOrdSvc Estado { get { return mEstado; } set { mEstado = value; } }
        private int mEspecial = 0;
        public int Especial { get { return mEspecial; } set { mEspecial = value; } }

        public enum EstadoOrdSvc { Abierto = 10, Cerrado = 20, Cancelado = 30}
        public enum enTipoSolicitud { Fijo = 1, Variable = 2}
    }

}
