using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admSolicitud
    {
        public int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO trf_SolicitudCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdSolicitud");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("IdSolicitud")) { Id = Convert.ToInt32(Datos.Rows[0]["IdSolicitud"]); }
                }
            }
            BD.CierraBD();
            return Id;
        }

        public int nueva(Solicitud pDatos)
        {
            int nuevoId = 0;
            nuevoId = daSiguienteIdentificador();
            if (nuevoId > 0)
            {
                bool resultado = false;
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_Solicitud (");
                SqlCmd.Append("IdSolicitud,");
                SqlCmd.Append("FechaRegistro,");
                SqlCmd.Append("IdEmpresa,");
                SqlCmd.Append("Factura,");
                SqlCmd.Append("FechaFactura,");
                SqlCmd.Append("CondicionPago,");
                SqlCmd.Append("Concepto,");
                SqlCmd.Append("Importe,");
                SqlCmd.Append("IdProveedor,");
                SqlCmd.Append("Proveedor,");
                SqlCmd.Append("Rfc,");
                SqlCmd.Append("Banco,");
                SqlCmd.Append("Cuenta,");
                SqlCmd.Append("CtaClabe,");
                SqlCmd.Append("Sucursal,");
                SqlCmd.Append("Proyecto,");
                SqlCmd.Append("DescProyecto,");
                SqlCmd.Append("Moneda,");
                SqlCmd.Append("Estado,");
                SqlCmd.Append("ConFactura,");
                SqlCmd.Append("IdUsr,");
                SqlCmd.Append("UnidadNegocio,");
                SqlCmd.Append("CantidadPagar,");
                SqlCmd.Append("Marcado,");
                SqlCmd.Append("Prioridad");
                SqlCmd.Append(",TipoSolicitud");
                SqlCmd.Append(",ReporteIva");
                SqlCmd.Append(")");
                
                SqlCmd.Append("VALUES (");
                SqlCmd.Append(nuevoId.ToString ());
                SqlCmd.Append(",getdate()");
                SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
                SqlCmd.Append(",'" + pDatos.Factura + "'");
                SqlCmd.Append(",'" + pDatos.FechaFactura.ToString("dd/MM/yyyy") + "'");
                SqlCmd.Append(",'" + pDatos.CondicionPago + "'");
                SqlCmd.Append(",'" + pDatos.Concepto + "'");
                SqlCmd.Append("," + pDatos.Importe );
                SqlCmd.Append("," + pDatos.IdProveedor.ToString());
                SqlCmd.Append(",'" + pDatos.Proveedor + "'");
                SqlCmd.Append(",'" + pDatos.Rfc + "'");
                SqlCmd.Append(",'" + pDatos.Banco + "'");
                SqlCmd.Append(",'" + pDatos.Cuenta + "'");
                SqlCmd.Append(",'" + pDatos.CtaClabe + "'");
                SqlCmd.Append(",'" + pDatos.Sucursal + "'");
                SqlCmd.Append(",'" + pDatos.Proyecto + "'");
                SqlCmd.Append(",'" + pDatos.DescProyecto + "'");
                SqlCmd.Append(",'" + pDatos.Moneda + "'");
                SqlCmd.Append("," + pDatos.Estado.ToString("d"));
                SqlCmd.Append("," + pDatos.ConFactura.ToString("d"));
                SqlCmd.Append("," + pDatos.IdUsr);
                SqlCmd.Append("," + pDatos.UnidadNegocio);
                SqlCmd.Append("," + pDatos.Importe );
                SqlCmd.Append("," + pDatos.Marcado );
                SqlCmd.Append("," + pDatos.Prioridad);
                SqlCmd.Append("," + pDatos.TipoSolicitud.ToString("d"));
                SqlCmd.Append("," + pDatos.ReporteIva.ToString());
                SqlCmd.Append(")");
                mbd.BD BD = new mbd.BD();
                   resultado = BD.EjecutaCmd(SqlCmd.ToString());
                BD.CierraBD();
            }
            return nuevoId;;
        }

        public bool RegistrarLlave(String IdSol, String Llave) {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_SolicitudLlave (");
            SqlCmd.Append("IdSolicitud,");
            SqlCmd.Append("FechaRegistro,");
            SqlCmd.Append("Llave");
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(IdSol.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append(",'" + Llave + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }

        public bool Existe(string pLlave)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT top 1 IdSolicitud FROM trf_SolicitudLlave WHERE llave= '" + pLlave + "' order by IdSolicitud desc ");
            if (datos.Rows.Count > 0) {
                String Id = datos.Rows[0]["IdSolicitud"].ToString();
                datos = BD.LeeDatos("SELECT Estado FROM trf_Solicitud WHERE IdSolicitud=" + Id + " and Estado=" + Solicitud.solEstado.Rechazada.ToString("d"));
                if (datos.Rows.Count == 0) { resultado = true; }
            };
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public Solicitud carga(int pId)
        {
            Solicitud respuesta = new Solicitud();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM trf_Solicitud WHERE IdSolicitud=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool EliminarSolicitud(int IdSolicitud)
        {
            bool Resultado = false;

            mbd.BD BD = new mbd.BD();
            string SqlCmd = "Delete trf_Solicitud where Idsolicitud=" + IdSolicitud + " and estado=" + Solicitud.solEstado.Solicitud.ToString("d");
            Resultado = BD.EjecutaCmd(SqlCmd.ToString());
            if (Resultado)
            {
                SqlCmd = "Delete trf_Archivos where Idsolicitud=" + IdSolicitud;
                Resultado = BD.EjecutaCmd(SqlCmd.ToString());

                SqlCmd = "Delete trf_SolicitudLlave where Idsolicitud=" + IdSolicitud;
                Resultado = BD.EjecutaCmd(SqlCmd.ToString());

                SqlCmd = "Delete BitacoraSolicitud where Idsolicitud=" + IdSolicitud;
                Resultado = BD.EjecutaCmd(SqlCmd.ToString());
            }
            BD.CierraBD();

            return Resultado;
        }
                
        private Solicitud arma(DataRow pRegistro)
        {
            Solicitud respuesta = new Solicitud();
            if (!pRegistro.IsNull("IdSolicitud")) respuesta.IdSolicitud = Convert.ToInt32(pRegistro["IdSolicitud"]);
            if (!pRegistro.IsNull("FechaRegistro"))respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("FechaFactura")) respuesta.FechaFactura = Convert.ToDateTime(pRegistro["FechaFactura"]);
            if (!pRegistro.IsNull("Factura")) respuesta.Factura = Convert.ToString(pRegistro["Factura"]);
            if (!pRegistro.IsNull("CondicionPago")) respuesta.CondicionPago = Convert.ToString(pRegistro["CondicionPago"]);
            if (!pRegistro.IsNull("Concepto")) respuesta.Concepto = Convert.ToString(pRegistro["Concepto"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("IdProveedor")) respuesta.IdProveedor  = Convert.ToInt32 (pRegistro["IdProveedor"]);
            if (!pRegistro.IsNull("Proveedor")) respuesta.Proveedor = Convert.ToString(pRegistro["Proveedor"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Banco")) respuesta.Banco = Convert.ToString(pRegistro["Banco"]);
            if (!pRegistro.IsNull("Cuenta")) respuesta.Cuenta = Convert.ToString(pRegistro["Cuenta"]);
            if (!pRegistro.IsNull("CtaClabe")) respuesta.CtaClabe = Convert.ToString(pRegistro["CtaClabe"]);
            if (!pRegistro.IsNull("Sucursal")) respuesta.Sucursal = Convert.ToString(pRegistro["Sucursal"]);
            if (!pRegistro.IsNull("Proyecto")) respuesta.Proyecto = Convert.ToString(pRegistro["Proyecto"]);
            if (!pRegistro.IsNull("DescProyecto")) respuesta.DescProyecto = Convert.ToString(pRegistro["DescProyecto"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = Convert.ToString(pRegistro["Moneda"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (Solicitud.solEstado )(pRegistro["Estado"]);
            if (!pRegistro.IsNull("ConFactura")) respuesta.ConFactura = (Solicitud.enConFactura)(pRegistro["ConFactura"]);
            if (!pRegistro.IsNull("IdUsr")) respuesta.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]);
            if (!pRegistro.IsNull("UnidadNegocio")) respuesta.UnidadNegocio = Convert.ToInt32(pRegistro["UnidadNegocio"]);
            if (!pRegistro.IsNull("CantidadPagar")) respuesta.CantidadPagar = Convert.ToDecimal(pRegistro["CantidadPagar"]);
            if (!pRegistro.IsNull("Marcado")) respuesta.Marcado = Convert.ToInt32(pRegistro["Marcado"]);
            if (!pRegistro.IsNull("Prioridad")) respuesta.Prioridad = Convert.ToInt32(pRegistro["Prioridad"]);
            if (!pRegistro.IsNull("ReporteIva")) respuesta.ReporteIva = Convert.ToInt32(pRegistro["ReporteIva"]);
            //if (!pRegistro.IsNull("TipoSolicitud")) respuesta.TipoSolicitud = (Solicitud.enTpSolicitud)(pRegistro["TipoSolicitud"]);
            return respuesta;
        }

        public void ActualizaDatosDeSinFactura(Solicitud oSol)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE trf_Solicitud  SET");
            SqlCmd.Append(" Factura='" + oSol.Factura + "'");
            SqlCmd.Append(",FechaFactura='" + oSol.FechaFactura.ToString ("dd/MM/yyyy") + "'");
            SqlCmd.Append(",Concepto='" + oSol.Concepto  + "'");
            SqlCmd.Append(",ConFactura =1");
            SqlCmd.Append(" WHERE IdSolicitud=" + oSol.IdSolicitud.ToString());

            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }

        public void modificaSolicitud(Solicitud oSol)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE trf_Solicitud  SET");
            SqlCmd.Append(" CondicionPago='" + oSol.CondicionPago + "'");
            SqlCmd.Append(" ,Proyecto='" + oSol.Proyecto + "'");
            SqlCmd.Append(" ,DescProyecto='" + oSol.DescProyecto + "'");
            SqlCmd.Append(" ,Moneda='" + oSol.Moneda + "'");
            SqlCmd.Append(" WHERE IdSolicitud=" + oSol.IdSolicitud);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

        }

        public bool  ModificaSolSinFactura(Solicitud oSol)
        {
            bool Resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE trf_Solicitud  SET");
            SqlCmd.Append(" Factura='" + oSol.Factura  + "'");
            SqlCmd.Append(" ,FechaFactura='" + oSol.FechaFactura.ToString("dd/MM/yyyy") + "'");
            SqlCmd.Append(" ,Importe='" + oSol.Importe  + "'");
            SqlCmd.Append(" ,Concepto='" + oSol.Concepto + "'");
            SqlCmd.Append(" ,CondicionPago='" + oSol.CondicionPago + "'");
            SqlCmd.Append(" ,Proyecto='" + oSol.Proyecto + "'");
            SqlCmd.Append(" ,DescProyecto='" + oSol.DescProyecto + "'");
            SqlCmd.Append(" ,Moneda='" + oSol.Moneda + "'");
            SqlCmd.Append(" ,CantidadPagar='" + oSol.CantidadPagar + "'");
            SqlCmd.Append(" WHERE IdSolicitud=" + oSol.IdSolicitud);
            mbd.BD BD = new mbd.BD();
            Resultado =BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            
            return Resultado;
        }

        public void CambiaEstadoSolicitud(int IdSolicitud, Solicitud.solEstado pEstado)
        {
            String SqlCmd = "UPDATE trf_Solicitud SET Estado=" + pEstado.ToString("d") + " WHERE IdSolicitud=" + IdSolicitud.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void CambiaEstadoYCantPagoSolicitud(int IdSolicitud, Solicitud.solEstado pEstado,Decimal CantidadPagar)
        {
            String SqlCmd = "UPDATE trf_Solicitud SET Estado=" + pEstado.ToString("d") + ",CantidadPagar=" + CantidadPagar + ", Marcado=0 WHERE IdSolicitud=" + IdSolicitud.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void CambiaSolicitudFondeoAcaptura(string IdFondeo)
        {
            String SqlCmd = "UPDATE trf_Solicitud set estado=" + Solicitud.solEstado.Captura.ToString("d") + " where IdSolicitud in(";
            SqlCmd += " select IdSolicitud  from trf_SolicitudFondosDetalle where Estado=20 and  IdFondeo =" + IdFondeo + ")" ;

            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }
        
        // CONSULTAS UTILIZADAS PARA EL SOLICITANTE//////
        
        public List<Solicitud> DaSolConsultaAbiertaXUsuario(int IdEmpresa, int IdUnidNeg, String pConsulta)
        {
            List<Solicitud> respuesta = new List<Solicitud>();

            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud where IdEmpresa=" + IdEmpresa.ToString () +  " and UnidadNEgocio=" + IdUnidNeg.ToString ()  + pConsulta);
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }


        public DataTable ExportaSolRegistradasXSolicitante(int IdEmpresa, int IdUnidNeg, String pConsulta)
        {
            String SqlCmd = "SELECT ";
            if (string.IsNullOrEmpty(pConsulta)) { SqlCmd = "SELECT TOP 20"; }
            SqlCmd += " FECHAREGISTRO,FACTURA, FECHAFACTURA,CONCEPTO,IMPORTE,PROVEEDOR,RFC,MONEDA,";
            SqlCmd += " CASE ConFactura WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END  AS CONFACTURA,";
            SqlCmd += " CASE Estado WHEN  10 THEN 'Solicitud' WHEN  20 THEN 'Autorizacion' WHEN  30 THEN 'Captura' WHEN  40 THEN 'PagoParcial' WHEN  50 THEN 'Pagado' WHEN  70 THEN 'Rechazada' END  AS ESTADO";
            SqlCmd += " FROM trf_Solicitud where IdEmpresa=" + IdEmpresa.ToString () +  " and UnidadNEgocio=" + IdUnidNeg.ToString () ;
            SqlCmd += pConsulta;
            SqlCmd += " order by PROVEEDOR,FECHAFACTURA";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }
        
        public List<Solicitud> DaSolEnRegistroXUsuario(string IdUsr, String pConsulta)
        {
            List<Solicitud> respuesta = new List<Solicitud>();

            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud where IdUsr=" + IdUsr + " And Estado=" + Solicitud .solEstado .Solicitud.ToString ("d")  + pConsulta);
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public void AsignarMarcaPrioridad(int IdSolicitud, int Prioridad)
        {
            String SqlCmd = "UPDATE trf_Solicitud SET Prioridad=" + Prioridad + " WHERE IdSolicitud=" + IdSolicitud.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public List<Solicitud> DaTodasSolicitudesSinfactura(int IdEmpresa, int IdUnidNeg)
        {
            List<Solicitud> respuesta = new List<Solicitud>();

            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud where ConFactura=0 and estado<70" );
            SqlCmd.Append(" and  IdEmpresa=" + IdEmpresa.ToString() + " and UnidadNEgocio=" + IdUnidNeg.ToString() );
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        
        // CONSULTAS UTILIZADAS PARA PERFIL DE AUTORIZAR//////

      public DataTable ListaSolicitudesXAutorizar(String IdEmpresa, String pConsulta)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.FechaRegistro,S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.IdSolicitud,S.Moneda,");
            SqlCmd.Append(" CASE WHEN S.ConFactura = 0 THEN 'NO' WHEN  S.ConFactura = 1 THEN 'SI' END  AS ConFactura, S.Prioridad,S.CantidadPagar,S.DescProyecto,S.Marcado");
            SqlCmd.Append(" FROM trf_Solicitud as S inner join [dbo].[usuario] as U");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng ");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio ");
            SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and  (S.Estado=" + Solicitud.solEstado.Solicitud.ToString("d") + " or S.Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") +")");
            if (!String.IsNullOrEmpty(pConsulta)) { SqlCmd.Append(" " + pConsulta); }
            SqlCmd.Append(" order by S.Proveedor,S.FechaFactura");
          
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

      public DataTable ListaPreAutorizar(String IdEmpresa,  int IdUsrs)
      {
          StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.FechaRegistro,S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.IdSolicitud,S.Moneda,");
          SqlCmd.Append(" CASE WHEN S.ConFactura = 0 THEN 'NO' WHEN  S.ConFactura = 1 THEN 'SI' END  AS ConFactura, S.Prioridad,S.CantidadPagar,S.DescProyecto");
          SqlCmd.Append(" FROM trf_Solicitud as S inner join [dbo].[usuario] as U");
          SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
          SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng ");
          SqlCmd.Append(" on Ng.Id =S.UnidadNegocio ");
          SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and  (S.Estado=" + Solicitud.solEstado.Solicitud.ToString("d") + " or S.Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + ")");
          SqlCmd.Append(" and Marcado ="+ IdUsrs.ToString ());
          SqlCmd.Append(" order by S.Proveedor,S.FechaFactura");

          mbd.BD BD = new mbd.BD();
          DataTable datos = BD.LeeDatos(SqlCmd.ToString());
          BD.CierraBD();
          return datos;
      }

      public DataTable DaSumaPreautorizacion(String IdEmpresa, string IdUsrs)
      {
          StringBuilder SqlCmd = new StringBuilder("SELECT Moneda, CantidadPagar FROM trf_solicitud ");
          SqlCmd.Append(" where IdEmpresa=" + IdEmpresa + " and Estado in (" + Solicitud.solEstado.Solicitud.ToString("d") + "," + Solicitud.solEstado.PagoParcial.ToString("d") + ") and marcado=" + IdUsrs);
          mbd.BD BD = new mbd.BD();
          DataTable datos = BD.LeeDatos(SqlCmd.ToString());
          BD.CierraBD();
          return datos;
      }


        /// <summary>
        /// PRUEBAS DE AUTORIZACION
        /// </summary>
      public DataTable ListaXAutorizar(String IdEmpresa, string IdUsr ,String pConsulta)
      {
          StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.FechaRegistro,S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.IdSolicitud,S.Moneda,");
          SqlCmd.Append(" CASE WHEN S.ConFactura = 0 THEN 'NO' WHEN  S.ConFactura = 1 THEN 'SI' END  AS ConFactura, S.Prioridad,S.CantidadPagar,S.DescProyecto,S.Marcado");
          SqlCmd.Append(" FROM trf_Solicitud as S inner join [dbo].[usuario] as U");
          SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
          SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng ");
          SqlCmd.Append(" on Ng.Id =S.UnidadNegocio ");
          SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and  (S.Estado=" + Solicitud.solEstado.Solicitud.ToString("d") + " or S.Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + ")");
          SqlCmd.Append(" and S.IdSolicitud not in (select IdSolicitud from trf_AutorizacionTemporal where idusr=" + IdUsr + ")");
          if (!String.IsNullOrEmpty(pConsulta)) { SqlCmd.Append(" " + pConsulta); }
          SqlCmd.Append(" order by S.Proveedor,S.FechaFactura");

          mbd.BD BD = new mbd.BD();
          DataTable datos = BD.LeeDatos(SqlCmd.ToString());
          BD.CierraBD();
          return datos;
      }

      public DataTable ListaAutorizacion(string IdEmpresa, string  IdUsrs)
      {
          StringBuilder SqlCmd = new StringBuilder("SELECT  S.FechaRegistro,S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.IdSolicitud,S.Moneda, ");
          SqlCmd.Append(" CASE WHEN S.ConFactura = 0 THEN 'NO' WHEN  S.ConFactura = 1 THEN 'SI' END  AS ConFactura, S.CantidadPagar,S.DescProyecto, A.ImporteAutorizado");
          SqlCmd.Append(" FROM trf_AutorizacionTemporal as A  left join  trf_Solicitud as S ");
          SqlCmd.Append(" on S.IdSolicitud=A.IdSolicitud ");
          SqlCmd.Append(" Where A.IdEmpresa=" + IdEmpresa + " and A.IdUsr=" + IdUsrs + " order by S.Proveedor,S.FechaFactura");
          
          mbd.BD BD = new mbd.BD();
          DataTable datos = BD.LeeDatos(SqlCmd.ToString());
          BD.CierraBD();
          return datos;
      }

      public bool AsignarMarcaAutorizar(string IdEmpresa, int IdSolicitud, string IdUsr, string ImportePagar)
      {
          bool resultado = false;
          String SqlCmd = "UPDATE trf_Solicitud SET Marcado=" + IdUsr;
          SqlCmd += " WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ")  and IdSolicitud=" + IdSolicitud.ToString();
          SqlCmd += " and (Estado=" + Solicitud.solEstado.Solicitud.ToString("d") + " or Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + ")";
          mbd.BD BD = new mbd.BD();
          if (BD.EjecutaCmd(SqlCmd))
          {
              SqlCmd = "Insert into trf_AutorizacionTemporal Values(" + IdEmpresa + "," + IdUsr + "," + IdSolicitud.ToString() + "," + ImportePagar + ")";
              resultado = BD.EjecutaCmd(SqlCmd);
          }

          BD.CierraBD();
          return resultado;
      }
      
       public void QuitarMarcaAutorizar(int IdSolicitud, string IdUsr)
      {
          String SqlCmd = "UPDATE trf_Solicitud SET Marcado=0 WHERE (Marcado is null or Marcado=0 or Marcado=" + IdUsr + ") and IdSolicitud=" + IdSolicitud.ToString();
          mbd.BD BD = new mbd.BD();
          BD.EjecutaCmd(SqlCmd);
          
          SqlCmd = "Delete trf_AutorizacionTemporal  where IdUsr=" + IdUsr + " and IdSolicitud=" + IdSolicitud.ToString();
          bool resultado = BD.EjecutaCmd(SqlCmd);
          
          BD.CierraBD();
      }

      public bool ActulizaSaldoAutorizacion(string IdEmpresa, string IdUsr, string IdSolicitud, Decimal Monto)
      {
          String SqlCmd = "Update  trf_AutorizacionTemporal  set importeAutorizado=" + Monto.ToString () + "  where IdEmpresa=" + IdEmpresa  + "and  IdUsr=" + IdUsr + " and IdSolicitud=" + IdSolicitud.ToString();
          mbd.BD BD = new mbd.BD();
          bool resultado = BD.EjecutaCmd(SqlCmd);
          BD.CierraBD();
          return resultado;
      }

      public bool EliminaDatosAutoriacion(string IdEmpresa, string IdUsr)
      {
          String SqlCmd = "Delete trf_AutorizacionTemporal where IdEmpresa=" + IdEmpresa  + " and IdUsr=" + IdUsr;
          mbd.BD BD = new mbd.BD();
          bool resultado = BD.EjecutaCmd(SqlCmd);
          BD.CierraBD();
          return resultado;
      }

      
        // CONSULTAS UTILIZADAS PARA PERFIL DE PAGOS//////
        
        public List<Solicitud> ListaSolicitudesXPagar(String IdEmpresa,string RFC,string Proyecto)
        {
            List<Solicitud> respuesta = new List<Solicitud>();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud Where IdEmpresa=" + IdEmpresa + " and Estado=" + Solicitud.solEstado.Captura.ToString("d"));
            if(RFC != "0"){ SqlCmd .Append (" and rfc='" + RFC + "'");}
            if (Proyecto != "0") { SqlCmd.Append(" and Proyecto='" + Proyecto + "'");}
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows){respuesta.Add(arma(reg));}
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<Solicitud> DaSolXEmpresa(int IdEmpresa, String pConsulta)
        {
            List<Solicitud> respuesta = new List<Solicitud>();

            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud where IdEmpresa=" + IdEmpresa.ToString() + pConsulta);
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public DataTable DaSolXEmpresaExportar(int IdEmpresa, String pConsulta)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT fechaRegistro,Factura,FechaFactura,Proveedor,Importe,CantidadPagar,Moneda, ");
            SqlCmd.Append(" CASE Estado WHEN  10 THEN 'Solicitud' WHEN  20 THEN 'Autorizacion' WHEN  30 THEN 'Captura' WHEN  40 THEN 'PagoParcial' WHEN  50 THEN 'Pagado' WHEN  70 THEN 'Rechazada' END  AS ESTADO");
            SqlCmd.Append(" FROM trf_Solicitud where IdEmpresa=" + IdEmpresa.ToString() + pConsulta);
            SqlCmd.Append(" order by Proveedor,FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        // CONSULTAS UTILIZADAS PARA PERFIL DE CONTABILIDAD//////

        public DataTable ListaSolXExportarContabilidad(String IdEmpresa, String pAño, String pMes)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT S.IdSOLICITUD as SOLICITUD, Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante,S.FACTURA,S.FECHAFACTURA,S.CONDICIONPAGO,");
            SqlCmd.Append(" S.CONCEPTO,S.IMPORTE,S.MONEDA, S.PROVEEDOR,S.RFC,S.ConFactura as TIENE_FACTURA,S.BANCO,S.CUENTA,S.CTACLABE,S.SUCURSAL,S.PROYECTO,S.DESCPROYECTO as DESCRIPCION,'' as NOMBRE_ARCHIVO,'' as NOTAS_PAGO");
            SqlCmd.Append(" FROM BitacoraSolicitud  as B inner join trf_Solicitud as S");
            SqlCmd.Append(" on s.IdSolicitud=b.IdSolicitud");
            SqlCmd.Append(" inner join [dbo].[usuario] as U");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio");
            SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and (B.Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + " or B.Estado=" + Solicitud.solEstado.Pagado.ToString("d") + ")");
            SqlCmd.Append(" and ((DATEPART(MONTH,B.FECHAREGISTRO)=" + pMes + ") and (DATEPART(YEAR,B.FECHAREGISTRO)=" + pAño + "))");
            SqlCmd.Append(" order by S.Proveedor,S.FECHAFACTURA");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }
               
        public DataTable ExitenSolSinFacturaContabilidad(String IdEmpresa, String pAño, String pMes)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.FECHAREGISTRO,S.FACTURA,S.FECHAFACTURA,S.PROVEEDOR,S.CONCEPTO,S.IMPORTE,S.MONEDA");
            SqlCmd.Append(" FROM BitacoraSolicitud  as B  inner join trf_Solicitud as S");
            SqlCmd.Append(" on s.IdSolicitud=b.IdSolicitud");
            SqlCmd.Append(" inner join [dbo].[usuario] as U ");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio");
            SqlCmd.Append(" Where S.IdEmpresa=" + IdEmpresa + " and (B.Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + " or B.Estado=" + Solicitud.solEstado.Pagado.ToString("d") + ")");
            SqlCmd.Append(" and ((DATEPART(MONTH,B.FECHAREGISTRO)=" + pMes + ") and (DATEPART(YEAR,B.FECHAREGISTRO)=" + pAño + "))");
            SqlCmd.Append(" and S.ConFActura=0");
            SqlCmd.Append(" order by S.Proveedor,S.FECHAFACTURA ");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        // CONSULTAS UTILIZADAS PARA PERFIL DE DIRECCION//////

        public DataTable DaConsulaDireccion(String IdEmpresa, String Estado, String FechaInicio, String FechaTermino, string subConsulta)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT * from ConsultaDireccion(" + IdEmpresa + "," + Estado + ",'" + FechaInicio + "','" + FechaTermino + "')");
            SqlCmd.Append(subConsulta);
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        private SolContabilidad armaSolContabilidad(DataRow pRegistro)
        {
            SolContabilidad respuesta = new SolContabilidad();
            if (!pRegistro.IsNull("IdSolicitud")) respuesta.IdSolicitud = Convert.ToInt32(pRegistro["IdSolicitud"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Proveedor")) respuesta.Proveedor = Convert.ToString(pRegistro["Proveedor"]);
            if (!pRegistro.IsNull("FechaFactura")) respuesta.FechaFactura = Convert.ToDateTime(pRegistro["FechaFactura"]);
            if (!pRegistro.IsNull("Factura")) respuesta.Factura = Convert.ToString(pRegistro["Factura"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = Convert.ToString(pRegistro["Moneda"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (Solicitud.solEstado)(pRegistro["Estado"]);
            if (!pRegistro.IsNull("ConFactura")) respuesta.ConFactura = (Solicitud.enConFactura )(pRegistro["ConFactura"]);
            if (!pRegistro.IsNull("Solicitante")) respuesta.Solicitante = Convert.ToString(pRegistro["Solicitante"]);
            if (!pRegistro.IsNull("UnidadNegocio")) respuesta.UnidadNegocio = Convert.ToString(pRegistro["UnidadNegocio"]);
                    
            return respuesta;
        }
        
        public bool AgregaRechazo(String IdSolicitud, String Motivo)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO trf_SolicitudRechazo (");
            SqlCmd.Append("IdSolicitud,");
            SqlCmd.Append("Motivo)");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(IdSolicitud);
            SqlCmd.Append(",'" + Motivo + "'");
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        public String DaRechazoSolicitud(int IdSolicitud)
        {
            String resultado = String.Empty ;
            StringBuilder SqlCmd = new StringBuilder("Select Motivo from trf_SolicitudRechazo where IdSolicitud=" + IdSolicitud);
            mbd.BD BD = new mbd.BD();
            DataTable Datos= BD.LeeDatos (SqlCmd.ToString());
            if (Datos.Rows.Count > 0) { if (!Datos.Rows[0].IsNull("Motivo")) { resultado = Datos.Rows[0]["Motivo"].ToString(); }}
            BD.CierraBD();

            return resultado;
        }

        // CONSULTAS UTILIZADAS PARA PERFIL DE COORDINADOR//////

        public List<SolContabilidad> DaSolCoordinadorConsultaAbierta(String IdEmpresa, String pDatosConsulta)
        {
            List<SolContabilidad> respuesta = new List<SolContabilidad>();

            StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante, S.IdSolicitud,S.FechaRegistro,");
            SqlCmd.Append(" S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.Moneda,S.Estado,ConFactura ");
            SqlCmd.Append(" FROM trf_Solicitud as S inner join [dbo].[usuario] as U");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio ");
            SqlCmd.Append(" where S.IdEmpresa=" + IdEmpresa + " " + pDatosConsulta);
            SqlCmd.Append(" order by S.Proveedor,S.FechaFactura ");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(armaSolContabilidad(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public DataTable DaSolCoordinadorExportar(String IdEmpresa, String pDatosConsulta)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT Ng.Titulo as UnidadNegocio, U.Nombre as Solicitante,S.FechaRegistro,");
            SqlCmd.Append(" S.Factura,S.FechaFactura,S.Proveedor,S.Importe,S.Moneda,");
            SqlCmd.Append(" CASE S.ConFactura  WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END  AS ConFactura,");
            SqlCmd.Append(" CASE S.Estado WHEN  10 THEN 'Solicitud' WHEN  20 THEN 'Autorizacion' WHEN  30 THEN 'Captura' WHEN  40 THEN 'PagoParcial' WHEN  50 THEN 'Pagado' WHEN  70 THEN 'Rechazada' END  AS Estado ");
            SqlCmd.Append(" FROM trf_Solicitud as S inner join [dbo].[usuario] as U");
            SqlCmd.Append(" on U.IdUsr =S.IdUsr ");
            SqlCmd.Append(" left join [dbo].[cat_UnidadNegocio] as Ng");
            SqlCmd.Append(" on Ng.Id =S.UnidadNegocio ");
            SqlCmd.Append(" where S.IdEmpresa=" + IdEmpresa + " " + pDatosConsulta);
            SqlCmd.Append(" order by S.Proveedor,S.FechaFactura");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public DataTable ConsultadePagosXProveedor(String IdEmpresa, string RFC, String FechaIni, string FechaFin)
        {
            //StringBuilder SqlCmd = new StringBuilder("select * from ReportePerfilPagos(" + IdEmpresa + "," + Convert.ToDateTime(FechaIni).ToString("yyyyMMdd") + "," + Convert.ToDateTime(FechaFin).ToString("yyyyMMdd") + ")");
            //if (RFC != "0" ){ SqlCmd.Append(" Where RFC='" + RFC + "'") ;}
            //SqlCmd.Append(" order by idSolicitud, FechaRegistro");

            string SqlCmd = "select b.FechaRegistro as FechaPago, s.Factura, s.FechaFactura,s.Proveedor,s.Importe, s.Moneda, sum(b.importe) as ImportePagado,b.IdSolicitud";
            SqlCmd += " from bitacorasolicitud  as b inner join trf_solicitud  as s";
            SqlCmd +=" on  s.idsolicitud=b.idsolicitud ";
            SqlCmd +=" where S.IdEmpresa =" + IdEmpresa.ToString();
            SqlCmd +=" and (B.estado=" + Solicitud .solEstado .PagoParcial .ToString("d") + " or B.estado=" + Solicitud .solEstado .Pagado.ToString("d") + ") " ;
            SqlCmd +=" and ((b.FechaRegistro >= '" +FechaIni + "')  and(b.FechaRegistro < DATEADD(dd,1,'" + FechaFin + "')))";
            if (RFC != "0") { SqlCmd += " and s.Rfc ='" + RFC + "'"; }
            SqlCmd += " group by b.Idsolicitud,b.Fecharegistro ,s.Factura, s.FechaFactura,s.Importe, s.Proveedor,s.Moneda";
            
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            datos.Dispose();
            BD.CierraBD();
            return datos;
        }

        
        // ESTRACCION DE DATOS PARA LOS REPORTES RDLC

        public List<repPendientesGral> ReportePendientesGral(string IdEmpresa,string FhInicio,string FhFin, int Completo)
        {
            List<repPendientesGral> Resultado = new List<repPendientesGral>();
            StringBuilder SqlCmd = new StringBuilder("select Proveedor, Moneda,SUM(importe) as importe from trf_solicitud where estado=" + Solicitud.solEstado.Solicitud.ToString("d"));
            SqlCmd.Append(" and IdEmpresa=" + IdEmpresa);
            if (Completo == 0) { SqlCmd.Append(" and (FechaFactura>'" + FhInicio + "' and FechaFactura< dateadd(dd,1,'" + FhFin + "'))"); }
            SqlCmd.Append(" group by proveedor,Moneda ");
            SqlCmd.Append(" order by Proveedor,Moneda ");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { Resultado.Add(armarepPendientesGral(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return Resultado; 
        }

        public List<Solicitud> ReportePendientesDetallado(string IdEmpresa,string FhInicio,string FhFin, int Completo)
        {

            List<Solicitud> Resultado = new List<Solicitud>();
            StringBuilder SqlCmd = new StringBuilder("select * from trf_solicitud where estado=" + Solicitud .solEstado .Solicitud.ToString ("d"));
            SqlCmd.Append(" and IdEmpresa=" + IdEmpresa);
            if (Completo == 0) { SqlCmd.Append(" and (FechaFactura>'" + FhInicio + "' and FechaFactura< dateadd(dd,1,'" + FhFin + "'))"); }
            SqlCmd.Append(" order by IdEmpresa ,IdProveedor,FechaFactura ");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { Resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return Resultado ;
        }

        private repPendientesGral  armarepPendientesGral(DataRow pRegistro)
        {
            repPendientesGral respuesta = new repPendientesGral();
            if (!pRegistro.IsNull("Proveedor")) respuesta.Proveedor = Convert.ToString(pRegistro["Proveedor"]);
            if (!pRegistro.IsNull("Importe")) respuesta.Importe = Convert.ToDecimal(pRegistro["Importe"]);
            if (!pRegistro.IsNull("Moneda")) respuesta.Moneda = Convert.ToString(pRegistro["Moneda"]);
            return respuesta;
        }
        
        // CONSULTA PARA CAMBIO DE PROVEEDOR
        public List<Solicitud> ConsultaSolCambioProveedor(string IdEmpresa, string IdProveedor, string FhInicio, string FhTermino)
        {
            List<Solicitud> respuesta = new List<Solicitud>();
            string SqlCmd = "SELECT * FROM trf_Solicitud ";
            SqlCmd += " Where IdEmpresa=" + IdEmpresa + " and IdProveedor=" + IdProveedor + " And (FechaRegistro >='" + FhInicio + "' and FechaRegistro < DATEADD(dd,1,'" + FhTermino + "'))";
            SqlCmd += "  and Estado <" + Solicitud .solEstado.Rechazada.ToString ("d"); 
            SqlCmd += " order by Proveedor,FechaFactura";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool ActulizaProveedorSolicitud(int Idsolicitud, CatProveedor oPrv,Cuenta oCta ) {
            string SqlCmd = "update trf_Solicitud ";
            SqlCmd += " set IdProveedor=" + oPrv.Id + ", Proveedor='" + oPrv.Nombre + "', RFC='" + oPrv.Rfc  + "'," ;
            SqlCmd += " Banco='" + oCta.Banco + "', Cuenta='" + oCta.NoCuenta + "', CtaClabe='" + oCta.CtaClabe + "', Sucursal='" + oCta.Sucursal  + "'";
            SqlCmd += " Where Idsolicitud=" + Idsolicitud;
            mbd.BD BD = new mbd.BD();
            bool  resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
            return resultado;
        }
        
        //Reportes graficos de pendientes X dias de atraso
        public DataTable ConsultaPendientesXdiasretraso(int IdEmpresa)
        {
            string SqlCmd = "SELECT * FROM PendientesXDiasIngreso(" + IdEmpresa.ToString () + ")";
            mbd.BD BD = new mbd.BD();
                DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

     
        public List<Solicitud> ListaSolicitudesIngresoNotaCredito(int IdEmpresa, int IdUnidaNegocio, string Rfc, string FechaInicio, string FechaFInal)
        {
            List<Solicitud> respuesta = new List<Solicitud>();

            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud Where  IdEmpresa=" + IdEmpresa.ToString() + " and UnidadNegocio=" + IdUnidaNegocio.ToString());
            SqlCmd.Append(" and Rfc='" + Rfc + "' and (Estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + " or Estado=" + Solicitud.solEstado.Pagado.ToString("d") + ")");
            SqlCmd.Append(" And (FechaRegistro >='" + FechaInicio + "' and FechaRegistro < DATEADD(dd,1,'" + FechaFInal + "'))");
            SqlCmd.Append(" order by IdSolicitud Desc");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        /// <summary>
        /// SECCION PARA OBTENER LOS CALCULOS DE IVA
        /// </summary>
        
        public DataTable ListaSolicitudesParaIVA(String IdEmpresa, String FechaIni, string FechaFin)
        {
            string SqlCmd = "select b.FechaRegistro, s.Factura, s.FechaFactura,s.Proveedor,s.Importe, s.Moneda, sum(b.importe) as ImportePagado,b.IdSolicitud";
            SqlCmd += " from bitacorasolicitud  as b inner join trf_solicitud  as s";
            SqlCmd += " on  s.idsolicitud=b.idsolicitud ";
            SqlCmd += " where S.ReporteIva =1";
            SqlCmd += " and S.IdEmpresa =" + IdEmpresa.ToString();
            SqlCmd += " and (B.estado=" + Solicitud.solEstado.PagoParcial.ToString("d") + " or B.estado=" + Solicitud.solEstado.Pagado.ToString("d") + ") ";
            SqlCmd += " and ((b.FechaRegistro >= '" + FechaIni + "')  and(b.FechaRegistro < DATEADD(dd,1,'" + FechaFin + "')))";
            SqlCmd += " group by b.Idsolicitud,b.Fecharegistro ,s.Factura, s.FechaFactura,s.Importe, s.Proveedor,s.Moneda";

            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            datos.Dispose();
            BD.CierraBD();
            return datos;
        }
        
    }
    
    public class Solicitud
    {
        private int mIdSolicitud = 0;
        public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private DateTime mFechaFactura = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFactura { get { return mFechaFactura; } set { mFechaFactura = value; } }
        private string mFactura = String.Empty;
        public string Factura { get { return mFactura; } set { mFactura = value; } }
        private string mRfc= String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private int mIdProveedor = 0;
        public int IdProveedor { get { return mIdProveedor; } set { mIdProveedor = value; } }
        private string mProveedor = String.Empty;
        public string Proveedor { get { return mProveedor; } set { mProveedor = value; } }
        private string mCondicionPago = String.Empty;
        public string CondicionPago { get { return mCondicionPago; } set { mCondicionPago = value; } }
        private string mConcepto = String.Empty;
        public string Concepto { get { return mConcepto; } set { mConcepto = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private string mBanco = String.Empty;
        public string Banco { get { return mBanco; } set { mBanco = value; } }
        private string mCuenta = String.Empty;
        public string Cuenta { get { return mCuenta; } set { mCuenta = value; } }
        private string mCtaClabe = String.Empty;
        public string CtaClabe { get { return mCtaClabe; } set { mCtaClabe = value; } }
        private string mSucursal = String.Empty;
        public string Sucursal { get { return mSucursal; } set { mSucursal = value; } }
        private string mProyecto = String.Empty;
        public string Proyecto { get { return mProyecto; } set { mProyecto = value; } }
        private string mDescProyecto = String.Empty;
        public string DescProyecto { get { return mDescProyecto; } set { mDescProyecto = value; } }
        private String mMoneda = "Pesos";
        public String Moneda { get { return mMoneda; } set { mMoneda = value; } }
        private solEstado mEstado = solEstado.Solicitud;
        public solEstado Estado { get { return mEstado; } set { mEstado = value; } }
        private enConFactura mConFactura = enConFactura.SI;
        public enConFactura ConFactura { get { return mConFactura; } set { mConFactura = value; } }
        private int mIdUsr = 0;
        public int IdUsr { get { return mIdUsr; } set { mIdUsr = value; } }
        private int mUnidadNegocio = 0;
        public int UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
        private decimal mCantidadPagar = 0;
        public decimal CantidadPagar { get { return mCantidadPagar; } set { mCantidadPagar = value; } }
        private int mMarcado = 0;
        public int Marcado { get { return mMarcado; } set { mMarcado = value; } }
        private int mPrioridad = 0;
        public int Prioridad { get { return mPrioridad; } set { mPrioridad = value; } }
        private enTpSolicitud mTipoSolicitud = enTpSolicitud.Solicitud;
        public enTpSolicitud TipoSolicitud { get { return mTipoSolicitud; } set { mTipoSolicitud = value; } }

        private int mReporteIva = 1;
        public int ReporteIva { get { return mReporteIva; } set { mReporteIva = value; } }

        
        public enum solEstado { Solicitud = 10, Autorizacion = 20, Fondos=25, Captura = 30, PagoParcial = 40, Pagado = 50, Contabilidad = 60, Rechazada = 70 }
        public enum enConFactura { NO = 0, SI = 1 }
        public enum enTpSolicitud { Solicitud = 1, Reembolso= 2 }
    }

    public class SolContabilidad
    {
        private int mIdSolicitud = 0;
        public int IdSolicitud { get { return mIdSolicitud; } set { mIdSolicitud = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mProveedor = String.Empty;
        public string Proveedor { get { return mProveedor; } set { mProveedor = value; } }
        private DateTime mFechaFactura = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFactura { get { return mFechaFactura; } set { mFechaFactura = value; } }
        private string mFactura = String.Empty;
        public string Factura { get { return mFactura; } set { mFactura = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private String mMoneda = "Pesos";
        public String Moneda { get { return mMoneda; } set { mMoneda = value; } }
        private Solicitud.solEstado  mEstado = Solicitud.solEstado.Solicitud ;
        public Solicitud.solEstado Estado { get { return mEstado; } set { mEstado = value; } }
        private Solicitud.enConFactura mConFactura = Solicitud.enConFactura.SI;
        public Solicitud.enConFactura ConFactura { get { return mConFactura; } set { mConFactura = value; } }
        private String mSolicitante =String.Empty;
        public String Solicitante { get { return mSolicitante; } set { mSolicitante = value; } }
        private String mUnidadNegocio = String.Empty;
        public String UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
    }

    public class repPendientesGral
    {
        private string mProveedor = String.Empty;
        public string Proveedor { get { return mProveedor; } set { mProveedor = value; } }
        private decimal mImporte = 0;
        public decimal Importe { get { return mImporte; } set { mImporte = value; } }
        private String mMoneda = "Pesos";
        public String Moneda { get { return mMoneda; } set { mMoneda = value; } }
    }

}
