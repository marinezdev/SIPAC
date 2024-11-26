using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admOrdenFactura
    {
        public int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cxc_OrdenFacturaCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdOrdenFactura");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("IdOrdenFactura")) { Id = Convert.ToInt32(Datos.Rows[0]["IdOrdenFactura"]); }
                }
            }
            BD.CierraBD();
            BD = null; 
            return Id;
        }

        public bool  nueva(OrdenFactura pDatos)
        {
            bool resultado = false;
          
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_OrdenFactura(");
            SqlCmd.Append("IdServicio");
            SqlCmd.Append(",IdOrdenFactura");
            SqlCmd.Append(",FechaInicio");
            SqlCmd.Append(",FechaFactura");
            SqlCmd.Append(",IdCliente");
            SqlCmd.Append(",Rfc");
            SqlCmd.Append(",Cliente");
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Empresa");
            SqlCmd.Append(",TipoSolicitud");
            SqlCmd.Append(",Importe");
            SqlCmd.Append(",CondicionPago");
            SqlCmd.Append(",CondicionPagoDias");
            SqlCmd.Append(",Proyecto");
            SqlCmd.Append(",TipoMoneda");
            SqlCmd.Append(",IdCatServicio");
            SqlCmd.Append(",Servicio");
            SqlCmd.Append(",Descripcion");
            SqlCmd.Append(",Anotaciones");
            SqlCmd.Append(",IdUsr");
            SqlCmd.Append(",UnidadNegocio");
            SqlCmd.Append(",Estado");
            SqlCmd.Append(",Factura");
            SqlCmd.Append(",EnviaCorreoClte");
            SqlCmd.Append(",Especial");
            SqlCmd.Append(",Marcado");
            SqlCmd.Append(",FechaCompromisoPago");
            
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdServicio );
            SqlCmd.Append("," + pDatos.IdOrdenFactura.ToString());
            SqlCmd.Append(",'" + pDatos.FechaInicio.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append(",'" + pDatos.FechaFactura.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append("," + pDatos.IdCliente.ToString());
            SqlCmd.Append(",'" + pDatos.Rfc + "'");
            SqlCmd.Append(",'" + pDatos.Cliente + "'");
            SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
            SqlCmd.Append(",'" + pDatos.Empresa + "'");
            SqlCmd.Append("," + pDatos.TipoSolicitud.ToString("d"));
            SqlCmd.Append("," + pDatos.Importe );
            SqlCmd.Append(",'" + pDatos.CondicionPago + "'");
            SqlCmd.Append("," + pDatos.CondicionPagoDias );
            SqlCmd.Append(",'" + pDatos.Proyecto + "'");
            SqlCmd.Append(",'" + pDatos.TipoMoneda + "'");
            SqlCmd.Append("," + pDatos.IdCatServicio);
            SqlCmd.Append(",'" + pDatos.Servicio + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append(",'" + pDatos.Anotaciones + "'");
            SqlCmd.Append("," + pDatos.IdUsr);
            SqlCmd.Append("," + pDatos.UnidadNegocio);
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.Factura);
            SqlCmd.Append("," + pDatos.EnviaCorreoClte);
            SqlCmd.Append("," + pDatos.Especial);
            SqlCmd.Append("," + pDatos.Marcado);
            SqlCmd.Append(",'" + pDatos.FhCompromisoPago + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
         
            return resultado;
        }

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
            if (!pRegistro.IsNull("IdServicio")) respuesta.IdServicio = Convert.ToInt32(pRegistro["IdServicio"]);
            if (!pRegistro.IsNull("IdOrdenFactura")) respuesta.IdOrdenFactura = Convert.ToInt32(pRegistro["IdOrdenFactura"]);
            if (!pRegistro.IsNull("FechaInicio")) respuesta.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("IdCliente")) respuesta.IdCliente = Convert.ToInt32(pRegistro["IdCliente"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Cliente")) respuesta.Cliente = Convert.ToString(pRegistro["Cliente"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Empresa")) respuesta.Empresa = Convert.ToString(pRegistro["Empresa"]);
            if (!pRegistro.IsNull("TipoSolicitud")) respuesta.TipoSolicitud = (OrdenServicio.enTipoSolicitud)(pRegistro["TipoSolicitud"]);
            if (!pRegistro.IsNull("NumFactura")) respuesta.NumFactura = Convert.ToString(pRegistro["NumFactura"]);
            if (!pRegistro.IsNull("FechaFactura")) respuesta.FechaFactura = Convert.ToDateTime(pRegistro["FechaFactura"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("IdCatServicio")) respuesta.IdCatServicio = Convert.ToInt32(pRegistro["IdCatServicio"]);
            if (!pRegistro.IsNull("Servicio")) respuesta.Servicio = Convert.ToString(pRegistro["Servicio"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Anotaciones")) respuesta.Anotaciones = Convert.ToString(pRegistro["Anotaciones"]);
            if (!pRegistro.IsNull("CondicionPago")) respuesta.CondicionPago = Convert.ToString(pRegistro["CondicionPago"]);
            if (!pRegistro.IsNull("CondicionPagoDias")) respuesta.CondicionPagoDias = Convert.ToInt32(pRegistro["CondicionPagoDias"]);
            if (!pRegistro.IsNull("Proyecto")) respuesta.Proyecto = Convert.ToString(pRegistro["Proyecto"]);
            if (!pRegistro.IsNull("TipoMoneda")) respuesta.TipoMoneda = Convert.ToString(pRegistro["TipoMoneda"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("UnidadNegocio")) respuesta.UnidadNegocio = Convert.ToInt32(pRegistro["UnidadNegocio"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (OrdenFactura.EstadoOrdFac)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Factura")) respuesta.Factura = Convert.ToInt32(pRegistro["Factura"]);
            if (!pRegistro.IsNull("EnviaCorreoClte")) respuesta.EnviaCorreoClte = Convert.ToInt32(pRegistro["EnviaCorreoClte"]);
            if (!pRegistro.IsNull("Especial")) respuesta.Especial = Convert.ToInt32(pRegistro["Especial"]);
            if (!pRegistro.IsNull("Marcado")) respuesta.Marcado = Convert.ToInt32(pRegistro["Marcado"]);
            if (!pRegistro.IsNull("FechaCompromisoPago")) respuesta.FhCompromisoPago = Convert.ToString(pRegistro["FechaCompromisoPago"]);
            
            
            return respuesta;
        }

        public List<OrdenFactura> Facturas_ConsultarGeneral(int IdEmpresa, int IdCliente)
        {
            // RMF
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "SELECT * FROM cxc_OrdenFactura ";
            cmdSql += " WHERE IdCliente = " + IdCliente.ToString();
            cmdSql += " ORDER BY Cliente,FechaInicio";
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) 
            { 
                respuesta.Add(arma(reg)); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<OrdenFactura> ConsultaFacturas(string Consulta)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "SELECT * FROM cxc_OrdenFactura ";
            if (!string.IsNullOrEmpty(Consulta)) { cmdSql += Consulta; }
            cmdSql += " order by Cliente,FechaInicio"; 
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }
        
        public List<OrdenFactura> ConsultaFacturasXIdServicio(int IdServico)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "SELECT * FROM cxc_OrdenFactura  where  IdServicio=" + IdServico.ToString ();
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<OrdenFactura> DaListaOrdenesEnFacturacion( string IdEmpresa,string IdCliente)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql="SELECT * FROM cxc_OrdenFactura where estado=" + OrdenFactura .EstadoOrdFac.Generacion_Factura.ToString ("d");
            cmdSql += " and IdEmpresa=" + IdEmpresa;
            if (IdCliente!="0") { cmdSql += " and IdCliente=" + IdCliente; }
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows){respuesta.Add(arma(reg));}
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<OrdenFactura> DaRegistrosDeOrdenXFacturar( string IdServicio)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "SELECT * FROM cxc_OrdenFactura where IdServicio=" + IdServicio ;
            cmdSql += " and estado=" + OrdenFactura.EstadoOrdFac.Solicitud.ToString("d");
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }
        public bool ExisteCFD(string pLlave)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT IdOrdenFactura FROM cxc_LlaveCFD WHERE CFD= '" + pLlave + "'");
            if (datos.Rows.Count > 0)
            {
                String Id = datos.Rows[0]["IdOrdenFactura"].ToString();
                datos = BD.LeeDatos("SELECT Estado FROM cxc_OrdenFactura WHERE IdOrdenFactura=" + Id + " and Estado=" + OrdenFactura.EstadoOrdFac.Cancelado.ToString("d"));
                if (datos.Rows.Count == 0) { resultado = true; }
            };
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public bool RegistrarCDF(String IdOrdenFactura, String Llave)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_LlaveCFD (");
            SqlCmd.Append("IdOrdenFactura,");
            SqlCmd.Append("FechaRegistro,");
            SqlCmd.Append("CFD");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(IdOrdenFactura);
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + Llave + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public void ActualizaDatosFacturacion(string IdOrdenFactura,string NumFactura, string FechaFactura, decimal Importe,OrdenFactura.EstadoOrdFac pEstado,int ConFactura)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET ";
            SqlCmd += " Estado=" + pEstado.ToString("d");
            SqlCmd += " ,NumFactura='" + NumFactura + "'";
            SqlCmd += " ,FechaFactura='" + FechaFactura + "'";
            SqlCmd += " ,Importe='" + Importe + "'";
            SqlCmd += " ,Factura=" + ConFactura.ToString () ;
            SqlCmd += " WHERE IdOrdenFactura=" + IdOrdenFactura;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void CambiaEstadoOrdenFactura(string IdOrdenFactura, OrdenFactura.EstadoOrdFac pEstado)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Estado=" + pEstado.ToString("d") + " WHERE IdOrdenFactura=" + IdOrdenFactura;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void CambiaEstadoGrupoOrdenFactura(string Lista, OrdenFactura.EstadoOrdFac pEstado)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Estado=" + pEstado.ToString("d") + " WHERE IdOrdenFactura in (" + Lista + ")";
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }
        
        public OrdenFactura DaUltimaFacturaOrdenServicio(int pIdServicio)
        {
            OrdenFactura respuesta = new OrdenFactura();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("select top 1 * from cxc_OrdenFactura where IdServicio=" + pIdServicio.ToString () + " order by IdOrdenFactura desc");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool ActulizaMontoDescGrupoFacturas(string  GrupoId, string Monto, string Descripcion)
        {
            bool respuesta= false ;
            mbd.BD BD = new mbd.BD();
            string CmdSql = " update cxc_OrdenFactura set Importe='" + Monto + "',";
            CmdSql += " Descripcion='" + Descripcion + "'";
            CmdSql +=" where IdOrdenFactura in ("+ GrupoId + ")";
            respuesta = BD.EjecutaCmd(CmdSql);
            BD.CierraBD();
            return respuesta;
        }

        public bool ActualizaFechaCompromisoPago(string idServicio, string IdOrdenFactura, string FhCompromisoPago)
        {
            bool respuesta = false;
            mbd.BD BD = new mbd.BD();
            string CmdSql = " update cxc_OrdenFactura set FechaCompromisoPago='" + FhCompromisoPago + "'";
            CmdSql += " where IdServicio=" + idServicio + " and IdOrdenFactura='" + IdOrdenFactura + "'";
            respuesta = BD.EjecutaCmd(CmdSql);
            BD.CierraBD();
            return respuesta;
        }


        // CONSULTAS UTILIZADAS PARA CONTABILIDAD//////

        public DataTable ListaFacturasPagadas(String IdEmpresa, String pAño, String pMes)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT F.IDORDENFACTURA, NG.TITULO AS UNIDADNEGOCIO, U.NOMBRE AS SOLICITANTE, F.NUMFACTURA,F.FECHAFACTURA,F.CONDICIONPAGO,");
            SqlCmd.Append(" F.ANOTACIONES,F.IMPORTE,F.TIPOMONEDA, F.CLIENTE,'' AS NOMBRE_ARCHIVO");
            SqlCmd.Append(" FROM CXC_BITACORA AS B INNER JOIN CXC_ORDENFACTURA  AS F");
            SqlCmd.Append(" ON F.IDORDENFACTURA =B.IDORDENFACTURA");
            SqlCmd.Append(" INNER JOIN [DBO].[USUARIO] AS U ");
            SqlCmd.Append(" ON U.IDUSR =F.IDUSR ");
            SqlCmd.Append(" LEFT JOIN [DBO].[CAT_UNIDADNEGOCIO] AS NG");
            SqlCmd.Append(" ON NG.ID =F.UNIDADNEGOCIO");
            SqlCmd.Append(" WHERE F.IDEMPRESA=" + IdEmpresa + " AND B.ESTADO=" + OrdenFactura .EstadoOrdFac .Pagado.ToString("d"));
            SqlCmd.Append(" AND ((DATEPART(MONTH,B.FECHAREGISTRO)=" + pMes + ") AND (DATEPART(YEAR,B.FECHAREGISTRO)=" + pAño + "))");
            SqlCmd.Append(" ORDER BY F.CLIENTE,F.FECHAFACTURA");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

    #region CONSULTAS PARA DIRECCION
        
        public DataTable DaConsultaXCobrar(string Consulta)
        {
            mbd.BD BD = new mbd.BD();
            string cmdSql = "select UN.Titulo as UnidadNegocio,F.IdOrdenFactura,F.FechaInicio , F.Cliente,F.FechaFactura ,F.NumFactura,F.Servicio, F.Descripcion,";
            cmdSql += " CASE F.Importe WHEN  0 THEN '----' ELSE convert(varchar, cast(F.importe AS money), 1) END  AS ImporteVista,";
            cmdSql += " F.Importe,F.CondicionPago,F.TipoMoneda, DATEADD(dd,F.CondicionPagoDias,F.FechaInicio) as Vencimiento,F.Factura,";
            cmdSql += " CASE F.Estado WHEN  10 THEN 'Solicitud' WHEN  20 THEN 'Generacion_Factura' WHEN  30 THEN 'En_Cobro' WHEN  40 THEN 'Pagado' WHEN  100 THEN 'Cancelado' END  AS Estado";
            cmdSql += " from cxc_OrdenFactura as F  left join cat_UnidadNegocio as UN";
            cmdSql += " on UN.Id=F.UnidadNegocio";
            cmdSql += " where FechaInicio < DATEADD(dd,1,GETDATE()) and F.Estado<" + OrdenFactura.EstadoOrdFac.Pagado.ToString("d");
            if (!string.IsNullOrEmpty(Consulta)) { cmdSql += Consulta; }
            cmdSql += " order by F.Cliente, F.FechaInicio";
            DataTable datos = BD.LeeDatos(cmdSql);
            BD.CierraBD();
            return datos;
        }

    #endregion

    #region SECCION  PARA AGREGAR UN CONTROL A EL PAGO DE FACTURAS POR GRUPO
        
        public void QuitarTodasMarcasGrupoPago(string IdUsr)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Marcado=0";
            SqlCmd += " WHERE Marcado=" + IdUsr ;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void QuitarMarcarGrupoPago(string IdFactura, string IdUsr)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Marcado=0";
            SqlCmd += " WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ") and IdOrdenFactura=" + IdFactura;
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public bool  PonerMarcarGrupoPago(string IdFactura, string IdUsr)
        {
            String SqlCmd = "UPDATE cxc_OrdenFactura SET Marcado=" + IdUsr;
            SqlCmd += " WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ") and IdOrdenFactura=" + IdFactura;
            SqlCmd += " and Estado<>" + OrdenFactura .EstadoOrdFac.Pagado.ToString("d") ;
            mbd.BD BD = new mbd.BD();
            bool resultado= BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
            return resultado;
        }

        public List<OrdenFactura> DaGrupoFacturasMarcadasPago(String IdEmpresa, string IdUsrs)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = " Select * from cxc_OrdenFactura where IdEmpresa=" + IdEmpresa + " and Marcado=" + IdUsrs ;
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public int daSiguienteIdgrupoPago()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cxc_GrupoPagoCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as Id");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("Id")) { Id = Convert.ToInt32(Datos.Rows[0]["Id"]); }
                }
            }
            return Id;
        }

        public bool AgregaOrdenalGrupo(int Idgrupo,  int IdOrdenFactura)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_GrupoPago (");
            SqlCmd.Append("Idgrupo");
            SqlCmd.Append(",IdOrdenFactura");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(Idgrupo);
            SqlCmd.Append("," +IdOrdenFactura);
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        public int ExisteGrupo(int IdOrdenFactura)
        {
            int Idgrupo = 0;
            mbd.BD BD = new mbd.BD();
            string cmdSql = " Select top 1 Idgrupo from cxc_GrupoPago";
            cmdSql += " where IdOrdenFactura=" + IdOrdenFactura.ToString();
            DataTable datos = BD.LeeDatos(cmdSql);
            if (datos.Rows.Count > 0) { Idgrupo = Convert.ToInt32(datos.Rows[0]["Idgrupo"]); }
            datos.Dispose();
            BD.CierraBD();
            return Idgrupo;
        }

        public List<OrdenFactura> DaGrupoFacturasPagadas(int Idgrupo,int IdOrdenFactura)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = " select F.* from cxc_GrupoPago as G inner join cxc_OrdenFactura as F";
            cmdSql +=" on f.IdOrdenFactura = G.IdOrdenFactura";
            cmdSql +=" where G.Idgrupo=" + Idgrupo.ToString () ;
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public DataTable DaListaFactursMarcadasPago(string IdUsrs) { 
            mbd.BD BD = new mbd.BD();
            string cmdSql = " select TipoMoneda,Importe from cxc_OrdenFactura ";
            cmdSql += " where Marcado=" + IdUsrs;
            DataTable resultado = BD.LeeDatos(cmdSql);
            BD.CierraBD();
        return resultado ;
        }
    #endregion

        public List<OrdenFactura> DaOrdenesVariblesParaActualizarMontos(string Consulta)
        {
            List<OrdenFactura> respuesta = new List<OrdenFactura>();
            mbd.BD BD = new mbd.BD();
            string cmdSql = "select * from cxc_OrdenFactura";
            cmdSql += " where estado=" + OrdenFactura.EstadoOrdFac.Solicitud.ToString("d");
            cmdSql += " and TipoSolicitud ="+ OrdenServicio .enTipoSolicitud.Variable.ToString ("d") ;
            if (!string.IsNullOrEmpty(Consulta)) { cmdSql +=  Consulta; }
            DataTable datos = BD.LeeDatos(cmdSql);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Eliminar(int IdOrdenFactura)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Delete cxc_OrdenFactura where IdOrdenFactura=" + IdOrdenFactura);
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public bool EliminarLlaveCFD(int IdOrdenFactura)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Delete cxc_LlaveCFD where IdOrdenFactura=" + IdOrdenFactura);
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }
    }

    public class OrdenFactura
    {
        private int mIdServicio = 0;
        public int IdServicio { get { return mIdServicio; } set { mIdServicio= value; } }
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
        public string ImporteVista { get { if (mImporte == 0) { mImporteVista = "----"; } else { mImporteVista = mImporte.ToString("C2");} return mImporteVista; } }

        private int mIdCatServicio = 0;
        public int  IdCatServicio { get { return mIdCatServicio; } set { mIdCatServicio = value; } }
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
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private int mUnidadNegocio = 0;
        public int UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
        private EstadoOrdFac mEstado = EstadoOrdFac.Solicitud;
        public EstadoOrdFac Estado { get { return mEstado; } set { mEstado = value; } }
        private int mFactura = 0;
        public int Factura { get { return mFactura; } set { mFactura = value; } }
        private int mEnviaCorreoClte = 0;
        public int EnviaCorreoClte { get { return mEnviaCorreoClte; } set { mEnviaCorreoClte = value; } }
        private int mEspecial = 0;
        public int Especial { get { return mEspecial; } set { mEspecial = value; } }
        private int mMarcado = 0;
        public int Marcado { get { return mMarcado; } set { mMarcado = value; } }
        private string mFhCompromisoPago = "";
        public string FhCompromisoPago { get { return mFhCompromisoPago; } set { mFhCompromisoPago = value; } }

        public enum EstadoOrdFac { Solicitud = 10, Generacion_Factura = 20, En_Cobro = 30, Pagado = 40, Cancelado = 100 }
        
    }

}
