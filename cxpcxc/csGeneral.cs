using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cxpcxc
{
    /// <summary>
    /// Envío de Correo
    /// </summary>
    public class csGeneral
    {
        public void  EnviaCorreoDireccionXSolicitudAutorizacion( string IdEmpresa, string Solicitante, cpplib.Solicitud osol){
            List<cpplib.credencial> lstUsuarios = (new cpplib.admCredencial()).DaUsuariosEnvioCorreoXSolicitudAutorizacion(IdEmpresa);
            if (lstUsuarios.Count > 0)
            {
                cpplib.Empresa oEmp = (new cpplib.admCatEmpresa()).carga(Convert.ToInt32(IdEmpresa));
                foreach (cpplib.credencial dst in lstUsuarios) {
                    System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + dst.Nombre.ToUpper() + " <br/><br/>");
                    mensaje.AppendLine("Te informamos que has recibido una solicitud de autorización de factura realizada por " + Solicitante + ", la cual ya está disponible en el sistema SIPAC <br/><br/>"); ;

                    mensaje.Append("<Table style='width :90%;margin:0 auto'>");
                    mensaje.AppendLine("<tr><td style='width :20%'>EMPRESA: </td><td>" + oEmp.Nombre + "</td></tr>");
                    mensaje.AppendLine("<tr><td style='height :40px'></td><td></td></tr>");
                    mensaje.AppendLine("<tr><td>Factura No: </td><td>" + osol.Factura + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Proveedor: </td><td>" + osol.Proveedor + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Unidad de Negocio: </td><td>" + osol.Proyecto + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Fecha Factura: </td><td>" + osol.FechaFactura.ToString ("dd/MM/yyyy") + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Descripcion: </td><td>" + osol.Concepto + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Importe: </td><td>" + osol.Importe.ToString ("c2") + "</td></tr>");
                    mensaje.AppendLine("<tr><td>Tipo de Moneda: </td><td>" + osol.Moneda + "</td></tr>");
                    mensaje.Append("</Table><br/><br/>");

                    mensaje.AppendLine("Seguimos trabajando para atenderte mejor <br/><br/>");
                    mensaje.AppendLine("<b>ATTE. <br/>");
                    mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

                    cpplib.CorreoMsg msg = new cpplib.CorreoMsg();
                    msg.Asunto = "Autorización de Solicitud  " + DateTime.Now.ToString ("dd/MM/yyyy") ;
                    msg.Mensaje = mensaje.ToString();
                    msg.Destinatarios.Add(dst.Correo );

                    this.EnviaCorreo(msg);
                }
            }
        }

        public void EnviaCorreoSolicitudFondosLote(string Nombre ,cpplib.LoteFondos osol)
        {
            List<cpplib.credencial> lstUsuarios = (new cpplib.admCredencial()).DaUsuariosPresidencia();
            
            foreach (cpplib.credencial cd in lstUsuarios)
            { 
                System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + cd.Nombre.ToUpper() + " <br/><br/>");
                mensaje.AppendLine("Te informamos que has recibido una solicitud de fondos realizada por : " + Nombre);
                mensaje.AppendLine(" la cual ya está disponible en el sistema SIPAC <br/><br/>");

                mensaje.Append("<Table style='width :90%;margin:0 auto'>");
                mensaje.AppendLine("<tr><td style='width :28%'>EMPRESA: </td><td>" + osol.Empresa + "</td></tr>");
                mensaje.AppendLine("<tr><td style='height :40px'></td><td></td></tr>");
                mensaje.AppendLine("<tr><td>Lote No: </td><td>" + osol.IdFondeo + "</td></tr>");
                mensaje.AppendLine("<tr><td>Fecha de Autorización: </td><td>" + DateTime.Now.ToString ("dd/MM/yyyy") + "</td></tr>");
                mensaje.AppendLine("<tr><td>No. de facturas: </td><td>" + osol.NoSolicitudes + "</td></tr>");
                mensaje.AppendLine("<tr><td>Total de fondos solicitados M.N.: </td><td>" + osol.Total.ToString("c2") + "</td></tr>");
                mensaje.Append("</Table><br/><br/>");

                mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
                mensaje.AppendLine("<b>ATTE. <br/>");
                mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

                cpplib.CorreoMsg msg = new cpplib.CorreoMsg();
                msg.Asunto = "Solicitud de fondos  " + DateTime.Now.ToString("dd/MM/yyyy");
                msg.Mensaje = mensaje.ToString();

                if (!string.IsNullOrEmpty(cd.Correo)) { msg.Destinatarios.Add(cd.Correo); }

                this.EnviaCorreo(msg);
            }
        }

        public void EnviaCorreoNotificacionLoteSinDeposito(string Nombre, cpplib.LoteFondos osol)
        {
            List<cpplib.credencial> lstUsuarios = (new cpplib.admCredencial()).DaUsuariosPresidencia();

            foreach (cpplib.credencial cd in lstUsuarios)
            {
                System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + cd.Nombre.ToUpper() + "<br/><br/>");
                mensaje.AppendLine("Le informamos que se ha generado un lote por : " + Nombre + " <br/><br/>");

                mensaje.AppendLine("<div style='font-size:14px; color :red'><b>EL LOTE NO REQUIERE DE UN DEPOSIDO,  ESTE SERA PAGADO ATRAVEZ DE LA CUENTA DE DOLARES</b></div><br/><br/>");

                mensaje.Append("<Table style='width :90%;margin:0 auto'>");
                mensaje.AppendLine("<tr><td style='width :28%'>EMPRESA: </td><td>" + osol.Empresa + "</td></tr>");
                mensaje.AppendLine("<tr><td style='height :20px'></td><td></td></tr>");
                mensaje.AppendLine("<tr><td>Lote No: </td><td>" + osol.IdFondeo + "</td></tr>");
                mensaje.AppendLine("<tr><td>Fecha de Autorización: </td><td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr>");
                mensaje.AppendLine("<tr><td>No. de facturas: </td><td>" + osol.NoSolicitudes + "</td></tr>");
                mensaje.AppendLine("<tr><td>Total: </td><td>" + osol.Total.ToString("c2") + "</td></tr>");
                mensaje.Append("</Table><br/><br/>");

                mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
                mensaje.AppendLine("<b>ATTE. <br/>");
                mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

                cpplib.CorreoMsg msg = new cpplib.CorreoMsg();
                msg.Asunto = "NOTIFICACION LOTE " + osol.IdFondeo + " (NO REQUIERE DEPOSITO)" ;
                msg.Mensaje = mensaje.ToString();

                if (!string.IsNullOrEmpty(cd.Correo)) { msg.Destinatarios.Add(cd.Correo); }

                this.EnviaCorreo(msg);
            }
        }

        public void EnviaCorreoAutorizacionFondos(string idFondeo)
        {
            cpplib.admFondos admf = new cpplib.admFondos();
            cpplib.LoteFondos solfd = admf.carga(idFondeo);

            List<cpplib.credencial> lstUsuarios = (new cpplib.admCredencial()).DaUsuariosAplicacionPago(solfd.IdEmpresa.ToString());
            List<cpplib.credencial> lstUsrPresidencia = (new cpplib.admCredencial()).DaUsuariosPresidencia();
            cpplib.credencial Autorizador = (new cpplib.admCredencial()).carga (solfd.IdUsr);

            System.Text.StringBuilder mensaje = new System.Text.StringBuilder(solfd.Empresa.ToUpper() + " <br/><br/>");
            mensaje.AppendLine("Estimado(a): " + Autorizador.Nombre.ToUpper() + " <br/><br/>");
            mensaje.AppendLine("Te informamos que los fondos para la liberación de esta solicitud ya fue autorizado,");
            mensaje.AppendLine(" la cual ya puedes consultar en el sistema SIPAC <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td style='width :28%'>Lote No: </td><td>" + solfd.IdFondeo + "</td></tr>");
            mensaje.AppendLine("<tr><td>Fecha de Autorización: </td><td>" + solfd.FechaFondos.ToString("dd/MM/yyyy") + "</td></tr>");
            mensaje.AppendLine("<tr><td>No. de facturas: </td><td>" + solfd.NoSolicitudes + "</td></tr>");
            mensaje.AppendLine("<tr><td>Total de fondos solicitados M.N.: </td><td>" + solfd.Total.ToString("c2") + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td colspan='2'><b>FONDEO</b></td></tr>");
            mensaje.AppendLine("<tr><td style='width :28%'>No. de facturas: </td><td>" + solfd.NoSolicitudesAprob + "</td></tr>");
            mensaje.AppendLine("<tr><td>Total de fondos solicitados M.N.: </td><td>" + solfd.TotalAprob.ToString("c2") + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            cpplib.CorreoMsg msg = new cpplib.CorreoMsg();
            msg.Asunto = "Autorizacion de fondos  " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();

            if (!string.IsNullOrEmpty(Autorizador.Correo)) { msg.Destinatarios.Add(Autorizador.Correo); } 
            foreach (cpplib.credencial dst in lstUsuarios) { if (!string.IsNullOrEmpty(dst.Correo)) { msg.Destinatarios.Add(dst.Correo); } }
            foreach (cpplib.credencial UsrPsd in lstUsrPresidencia) { if (!string.IsNullOrEmpty(UsrPsd.Correo)) { msg.Destinatarios.Add(UsrPsd.Correo); } }
            
            this.EnviaCorreo(msg);
        }

        public void EnviaCorreoOrdenFacturacion(cpplib.OrdenFactura oOrdenFact) {

            cpplib.CorreoMsg msg = new cpplib.CorreoMsg();
            
            List<cpplib.credencial> lstUsuarios = (new cpplib.admCredencial()).DaUsuariosFacturacion();
            cpplib.credencial Interesado = (new cpplib.admCredencial()).carga(oOrdenFact.IdUsr);
            if (!string.IsNullOrEmpty(Interesado.Correo)) { msg.Destinatarios.Add(Interesado.Correo); }
            
            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): ");
            foreach (cpplib.credencial dst in lstUsuarios) { 
                if (!string.IsNullOrEmpty(dst.Correo)) { 
                    msg.Destinatarios.Add(dst.Correo);
                    mensaje.AppendLine( dst.Nombre + "; ");
                } 
            }
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que hay una solicitud de facturación,");
            mensaje.AppendLine(" la cual puedes consultar en el sistema SIPAC <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden de Servicio </td><td>" + oOrdenFact.IdServicio.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden Factura </td><td>" + oOrdenFact.IdOrdenFactura.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td>Cliente: </td><td>" + oOrdenFact.Cliente + "</td></tr>");
            
            mensaje.AppendLine("<tr><td>Unidad de Negocio: </td><td>" + "" + "</td></tr>");
            mensaje.AppendLine("<tr><td>Fecha Solicitud: </td><td>" + DateTime. Now.ToString ("dd/MM/yyyy") + "</td></tr>");
            mensaje.AppendLine("<tr><td>Anotaciones: </td><td>" + oOrdenFact.Anotaciones + "</td></tr>");
            mensaje.AppendLine("<tr><td>Importe: </td><td>" + oOrdenFact.Importe.ToString ("c2") + "</td></tr>");
            mensaje.AppendLine("<tr><td>Tipo de moneda: </td><td>" + oOrdenFact.TipoMoneda + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            
            msg.Asunto = "Solicitud de Facturación  " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();

           this.EnviaCorreo(msg);
        }

        public void EnviaCorreofacturaAgregada(string pIdOrdenFact, int idusr)
        {
            cpplib.OrdenFactura oOrdFac = (new cpplib.admOrdenFactura()).carga(Convert.ToInt32(pIdOrdenFact));
            cpplib.CorreoMsg msg = new cpplib.CorreoMsg();

            cpplib.credencial Interesado = (new cpplib.admCredencial()).carga(oOrdFac.IdUsr);
            if (idusr != Interesado.IdUsr)
            {
                if (!string.IsNullOrEmpty(Interesado.Correo)) { msg.Destinatarios.Add(Interesado.Correo); }
            }
            
            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + Interesado.Nombre);
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que tu solicitud de facturación ya fue atendida, ");
            mensaje.AppendLine(" la puedes consultar en el sistema SIPAC <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden venta </td><td>" + oOrdFac.IdServicio.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden Factura </td><td>" + oOrdFac.IdOrdenFactura.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td>Empresa: </td><td>" + oOrdFac.Empresa  + "</td></tr>");
            mensaje.AppendLine("<tr><td>Factura: </td><td>" + oOrdFac.NumFactura  + "</td></tr>");
            mensaje.AppendLine("<tr><td>Fecha Factura: </td><td>" + oOrdFac.FechaFactura.ToString ("dd/MM/yyyy")  + "</td></tr>");
            mensaje.AppendLine("<tr><td>Cliente: </td><td>" + oOrdFac.Cliente + "</td></tr>");
            mensaje.AppendLine("<tr><td>Importe: </td><td>" + oOrdFac.Importe.ToString("c2") + "</td></tr>");
            mensaje.AppendLine("<tr><td>Tipo de moneda: </td><td>" + oOrdFac.TipoMoneda + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");
            
            msg.Asunto = "Factura Anexada " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();
                        
            this.EnviaCorreo(msg);
        }

        public void EnviaCorreoSolDatosOrdenFactura(string pIdOrdenFact)
        {
            cpplib.OrdenFactura oOrdFac = (new cpplib.admOrdenFactura()).carga(Convert.ToInt32 (pIdOrdenFact));
            cpplib.CorreoMsg msg = new cpplib.CorreoMsg();

            cpplib.credencial Interesado = (new cpplib.admCredencial()).carga(oOrdFac.IdUsr);
            if (!string.IsNullOrEmpty(Interesado.Correo)) { msg.Destinatarios.Add(Interesado.Correo); }

            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + Interesado.Nombre);
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que la orden de servicio abajo descrita requiere que ingreses ");
            mensaje.AppendLine("algunos datos para poder ser facturada. <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden de Servicio </td><td>" + oOrdFac.IdServicio.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td style='width :28%'>Orden Factura </td><td>" + oOrdFac.IdOrdenFactura.ToString() + "</td></tr>");
            mensaje.AppendLine("<tr><td>Empresa: </td><td>" + oOrdFac.Empresa + "</td></tr>");
            mensaje.AppendLine("<tr><td>Cliente: </td><td>" + oOrdFac.Cliente + "</td></tr>");
           mensaje.AppendLine("<tr><td>Tipo de moneda: </td><td>" + oOrdFac.TipoMoneda + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            msg.Asunto = "Factura Anexada " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();
                        
            this.EnviaCorreo(msg);
        }

        public void EnviaCorreoConFacturaCliente(string pIdOrdenFact,List <string> plstArchivos)
        {
            cpplib.OrdenFactura oOrdFac = (new cpplib.admOrdenFactura()).carga(Convert.ToInt32(pIdOrdenFact));
            
            cpplib.credencial Interesado = (new cpplib.admCredencial()).carga(oOrdFac.IdUsr);
            string CorreoCliente = (new cpplib.admCatClientes()).DaCorreoCliente(oOrdFac.IdCliente);

            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado CLiente: " + oOrdFac.Cliente);
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que has recibido una factura emitida por: " + oOrdFac.Empresa);
            mensaje.AppendLine("que a continución se detalla. <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto'>");
            mensaje.AppendLine("<tr><td style='width :28%'>No. de factura: </td><td>" + oOrdFac.NumFactura + "</td></tr>");
            mensaje.AppendLine("<tr><td style='width :28%'>Fecha Factura: </td><td>" + oOrdFac.FechaFactura.ToString("dd/MM/yyyy") + "</td></tr>");
            mensaje.AppendLine("<tr><td>Importe: </td><td>" + oOrdFac.Importe.ToString("c2") + "</td></tr>");
            mensaje.AppendLine("<tr><td>Tipo de moneda: </td><td>" + oOrdFac.TipoMoneda + "</td></tr>");
            mensaje.Append("</Table><br/><br/>");

            cpplib.CorreoMsg msg = new cpplib.CorreoMsg();

            msg.Asunto = "Factura " + oOrdFac.Empresa;
            msg.Mensaje = mensaje.ToString();

            if (!string.IsNullOrEmpty(Interesado.Correo)) { msg.Destinatarios.Add(Interesado.Correo); }
            if (!string.IsNullOrEmpty(CorreoCliente)) { msg.Destinatarios.Add(CorreoCliente); }

            msg.ArhivosAdjuntos = plstArchivos;

            this.EnviaCorreo(msg);
        }

        private void EnviaCorreo(cpplib.CorreoMsg msg)
        {
            
            if (msg.Destinatarios.Count > 0)
            {
                cpplib.CorreoCfg cfg = (new csGeneral()).daConfiguracionCorreo();
                cpplib.admCorreo admCorreo = new cpplib.admCorreo();
                admCorreo.enviar(cfg, msg);
            }
        
        }

        private  cpplib.CorreoCfg daConfiguracionCorreo()
        {
            cpplib.CorreoCfg resultado = new cpplib.CorreoCfg();
            resultado.Nombre = Properties.Settings.Default.correoNombre;
            resultado.Servidor = Properties.Settings.Default.correoServidor;
            resultado.Cuenta = Properties.Settings.Default.correoUsuario;
            resultado.Clave = Properties.Settings.Default.correoClave;
            resultado.Puerto = Properties.Settings.Default.correoPuerto;
            return resultado;
        }

    }
}