using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SipacCorreo
{
    public class admOrdenFactura
    {

        public OrdenFactura carga(int pId)
        {
            OrdenFactura respuesta = new OrdenFactura();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cxc_OrdenFactura WHERE IdOrdenFactura=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private OrdenFactura arma(DataRow pRegistro)
        {
            OrdenFactura respuesta = new OrdenFactura();
            if (!pRegistro.IsNull("IdOrdenFactura")) respuesta.IdOrdenFactura = Convert.ToInt32(pRegistro["IdOrdenFactura"]);
            if (!pRegistro.IsNull("FechaInicio")) respuesta.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("Cliente")) respuesta.Cliente = Convert.ToString(pRegistro["Cliente"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Empresa")) respuesta.Empresa = Convert.ToString(pRegistro["Empresa"]);
            if (!pRegistro.IsNull("TipoSolicitud")) respuesta.TipoSolicitud = (OrdenServicio.enTipoSolicitud)(pRegistro["TipoSolicitud"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Servicio")) respuesta.Servicio = Convert.ToString(pRegistro["Servicio"]);
            if (!pRegistro.IsNull("CondicionPago")) respuesta.CondicionPago = Convert.ToString(pRegistro["CondicionPago"]);
            if (!pRegistro.IsNull("CondicionPagoDias")) respuesta.CondicionPagoDias = Convert.ToInt32(pRegistro["CondicionPagoDias"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (OrdenFactura.EstadoOrdFac)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Especial")) respuesta.Especial = Convert.ToInt32(pRegistro["Especial"]);
            
            return respuesta;
        }   

        public List<OrdenFactura> DaPendientesDia()
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();

            string cmdSql = "select IdOrdenFactura,FechaInicio,Cliente,IdEmpresa,Empresa,TipoSolicitud,Servicio,Descripcion,CondicionPago,CondicionPagoDias,IdUsr,Estado,Especial";
            cmdSql += " from cxc_OrdenFactura";
            cmdSql += " where Estado=" + OrdenFactura.EstadoOrdFac.Por_Facturar.ToString("d");
            cmdSql += " and (DATEDIFF(day,GETDATE(),DATEADD(dd,CondicionPagoDias,FechaInicio))<5)";
            cmdSql +=" order by IdUsr";
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            
            return respuesta;
        }

        public List<OrdenFactura> DaListaporFacturar()
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();

            string cmdSql = "select IdOrdenFactura,FechaInicio,Cliente,IdEmpresa,Empresa,TipoSolicitud,Servicio,Descripcion,CondicionPago,CondicionPagoDias,IdUsr,Estado,Especial ";
            cmdSql += " from cxc_OrdenFactura";
            cmdSql += " where Estado=" + OrdenFactura.EstadoOrdFac.Emisio_Factura.ToString ("d") ;
            cmdSql += " order by IdUsr";
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();

            return respuesta;
        }


        public List<OrdenFactura> DaListaporCobrar()
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();

            string cmdSql = "select IdOrdenFactura,FechaInicio,Cliente,IdEmpresa,Empresa,TipoSolicitud,Servicio,Descripcion,CondicionPago,CondicionPagoDias,IdUsr,Estado,Especial";
            cmdSql += " from cxc_OrdenFactura";
            cmdSql += " where Especial=0 and Estado=" + OrdenFactura.EstadoOrdFac.En_Cobro.ToString("d");
            cmdSql += " order by IdEmpresa,Cliente";
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();

            return respuesta;
        }

        public void CambiaEstadoOrdenFactura(string IdOrdenFactura, OrdenFactura.EstadoOrdFac pEstado)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Estado=" + pEstado.ToString("d") + " WHERE IdOrdenFactura=" + IdOrdenFactura;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

    }

    public class OrdenFactura
    {
        private int mIdServicio = 0;
        public int IdServicio { get { return mIdServicio; } set { mIdServicio = value; } }
        private int mIdOrdenFactura = 0;
        public int IdOrdenFactura { get { return mIdOrdenFactura; } set { mIdOrdenFactura = value; } }
        private DateTime mFechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return mFechaInicio; } set { mFechaInicio = value; } }
        private int mIdCliente = 0;
        public int IdCliente { get { return mIdCliente; } set { mIdCliente = value; } }
        private string mRfc = string.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mCliente = string.Empty;
        public string Cliente { get { return mCliente; } set { mCliente = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private string mEmpresa = String.Empty;
        public string Empresa { get { return mEmpresa; } set { mEmpresa = value; } }
        private OrdenServicio.enTipoSolicitud mTipoSolicitud = OrdenServicio.enTipoSolicitud.Fijo;
        public OrdenServicio.enTipoSolicitud TipoSolicitud { get { return mTipoSolicitud; } set { mTipoSolicitud = value; } }
        private string mNumFactura = String.Empty;
        public string NumFactura { get { return mNumFactura; } set { mNumFactura = value; } }
        private DateTime mFechaFactura = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFactura { get { return mFechaFactura; } set { mFechaFactura = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private string mImporteVista = "";
        public string ImporteVista { get { if (mImporte == 0) { mImporteVista = "----"; } else { mImporteVista = mImporte.ToString("C2"); } return mImporteVista; } }
        private int mIdCatServicio = 0;
        public int IdCatServicio { get { return mIdCatServicio; } set { mIdCatServicio = value; } }
        private string mServicio = String.Empty;
        public string Servicio { get { return mServicio; } set { mServicio = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        private string mAnotaciones = String.Empty;
        public string Anotaciones { get { return mAnotaciones; } set { mAnotaciones = value; } }
        private string mCondicionPago = String.Empty;
        public string CondicionPago { get { return mCondicionPago; } set { mCondicionPago = value; } }
        private int mCondicionPagoDias = 0;
        public int CondicionPagoDias { get { return mCondicionPagoDias; } set { mCondicionPagoDias = value; } }
        private string mProyecto = String.Empty;
        public string Proyecto { get { return mProyecto; } set { mProyecto = value; } }
        private string mTipoMoneda = String.Empty;
        public string TipoMoneda { get { return mTipoMoneda; } set { mTipoMoneda = value; } }
        private string mCteSolomon = string.Empty;
        public string CteSolomon { get { return mCteSolomon; } set { mCteSolomon = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private int mUnidadNegocio = 0;
        public int UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
        private EstadoOrdFac mEstado = EstadoOrdFac.Por_Facturar;
        public EstadoOrdFac Estado { get { return mEstado; } set { mEstado = value; } }
        private int mContrato = 0;
        public int Contrato { get { return mContrato; } set { mContrato = value; } }
        private int mFactura = 0;
        public int Factura { get { return mFactura; } set { mFactura = value; } }
        private int mEnviaCorreoClte = 0;
        public int EnviaCorreoClte { get { return mEnviaCorreoClte; } set { mEnviaCorreoClte = value; } }
        private int mEspecial = 0;
        public int Especial { get { return mEspecial; } set { mEspecial = value; } }
        private int mMarcado = 0;
        public int Marcado { get { return mMarcado; } set { mMarcado = value; } }

        public enum EstadoOrdFac { Por_Facturar = 10, Emisio_Factura = 20, En_Cobro = 30, Pagado = 40, Cancelado = 100 }

    }

}
