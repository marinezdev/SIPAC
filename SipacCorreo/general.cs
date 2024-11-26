using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SipacCorreo
{
    public class general
    {
        public void EnviaCorreoDireccionXSolicitudAutorizacion(string IdEmpresa)
        {
            admDatos AdmDatos = new admDatos();
            List<credencial> lstUsuarios = AdmDatos.DaUsuariosEnvioCorreoXBloqueAutorizacion(IdEmpresa);
            DataTable dtsSolicitudes = AdmDatos.DaSolicitudesPorAutorizar(IdEmpresa);

            string Empresa = (new admCatEmpresa()).carga(Convert.ToInt32 (IdEmpresa)).Nombre;

            if ((lstUsuarios.Count > 0) && (dtsSolicitudes.Rows.Count>0))
            {
                string TotalPesos = dtsSolicitudes.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(dtsSolicitudes.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                string TotalDlls = dtsSolicitudes.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(dtsSolicitudes.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2");
                
                foreach (credencial dst in lstUsuarios)
                {
                    if (!string.IsNullOrEmpty(dst.Correo))
                    {
                        System.Text.StringBuilder mensaje = new System.Text.StringBuilder(Empresa.ToUpper() + " <br/><br/>");
                        mensaje.AppendLine("Estimado(a): " + dst.Nombre.ToUpper() + " <br/><br/>");
                        mensaje.AppendLine("Te informamos que tienes pendiente por autorizar " + dtsSolicitudes.Rows.Count.ToString());
                        mensaje.AppendLine(" facturas por un importe total de: <br/><br/>");
                        mensaje.AppendLine("   Pesos: " + TotalPesos + "<br/>");
                        mensaje.AppendLine(" Dolares: " + TotalDlls + "<br/><br/>");

                        mensaje.Append("Las cuales ya están disponibles en el sistema SIPAC<br/><br/>");

                        mensaje.AppendLine("Seguimos trabajando para atenderte mejor <br/><br/>");
                        mensaje.AppendLine("<b>ATTE. <br/>");
                        mensaje.AppendLine("Equipo SIPAC</b><br/><br/>");

                        CorreoCfg cfg = (new general()).daConfiguracionCorreo();
                        CorreoMsg msg = new CorreoMsg();
                        msg.Asunto = "Autorización de Solicitudes";
                        msg.Mensaje = mensaje.ToString();
                        msg.Destinatarios.Add(dst.Correo);

                        admCorreo admCorreo = new admCorreo();
                        admCorreo.enviar(cfg, msg);
                    }
                }
            }
        }
                
        public void EnviaCorreoSolDatosparaFacturar( int Idusr,List <Pendiente> Lista)
        {
            CorreoCfg cfg = (new general()).daConfiguracionCorreo();
            CorreoMsg msg = new CorreoMsg();

            credencial Interesado = (new admCredencial()).carga(Idusr);
            if (!string.IsNullOrEmpty(Interesado.Correo)) { msg.Destinatarios.Add(Interesado.Correo); }

            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): " + Interesado.Nombre);
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que (" + Lista.Count.ToString() + ") solicitudes requieren que ingreses ");
            mensaje.AppendLine("los montos para que puedan ser facturadas. <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto;font-size:10px'>");
            mensaje.AppendLine("<tr><td>ORDEN: </td><td>FECHA: </td><td>EMPRESA:</td><td>CLIENTE:</td><td>SERVICIO:</td><td>DESCRIPCION:</td></tr>");
            foreach (Pendiente obj in Lista) { 
                mensaje.AppendLine("<tr>");
                mensaje.AppendLine("<td>" + obj.IdOrden.ToString() + "</td>");
                mensaje.AppendLine("<td>" + obj.FechaInicio.ToString("dd/MM/yyyy") + "</td>");
                mensaje.AppendLine("<td>" + obj.Empresa + "</td>");
                mensaje.AppendLine("<td>" + obj.Cliente + "</td>");
                mensaje.AppendLine("<td>" + obj.Servicio+ "</td>");
                mensaje.AppendLine("<td>" + obj.Descripcion + "</td>");
                mensaje.AppendLine("</tr>");
            }
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            msg.Asunto = "SIPAC - Cuentas por cobrar " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();

            if (msg.Destinatarios.Count > 0)
            {
                admCorreo admCorreo = new admCorreo();
                admCorreo.enviar(cfg, msg);
            }
        }

        public void EnviaCorreoAFacturacion(List<Pendiente> Lista)
        {
            CorreoCfg cfg = (new general()).daConfiguracionCorreo();
            CorreoMsg msg = new CorreoMsg();

            List<credencial> lstUsuarios = (new admCredencial()).DaUsuariosFacturacion();
            
            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): ");
            foreach (credencial dst in lstUsuarios)
            {
                if (!string.IsNullOrEmpty(dst.Correo))
                {
                    msg.Destinatarios.Add(dst.Correo);
                    mensaje.AppendLine(dst.Nombre + "; ");
                }
            }
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que hay (" + Lista.Count.ToString () +  ") solicitudes para facturación,");
            mensaje.AppendLine(" las cuales puedes consultar en el sistema SIPAC <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto;font-size:10px'>");
            mensaje.AppendLine("<tr><td>ORDEN: </td><td>FECHA: </td><td>EMPRESA:</td><td>CLIENTE:</td><td>SERVICIO:</td><td>DESCRIPCION:</td></tr>");
            foreach (Pendiente obj in Lista)
            {
                mensaje.AppendLine("<tr>");
                mensaje.AppendLine("<td>" + obj.IdOrden.ToString() + "</td>");
                mensaje.AppendLine("<td>" + obj.FechaInicio.ToString("dd/MM/yyyy") + "</td>");
                mensaje.AppendLine("<td>" + obj.Empresa + "</td>");
                mensaje.AppendLine("<td>" + obj.Cliente + "</td>");
                mensaje.AppendLine("<td>" + obj.Servicio + "</td>");
                mensaje.AppendLine("<td>" + obj.Descripcion  + "</td>");
                mensaje.AppendLine("</tr>");
            }
            mensaje.Append("</Table><br/><br/>");
            
            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            msg.Asunto = "SIPAC - Solicitud de Facturación  " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();

            if (msg.Destinatarios.Count > 0)
            {
                admCorreo admCorreo = new admCorreo();
                admCorreo.enviar(cfg, msg);
            }
        }

        public void EnviaCorreoACobranza(int IdEmpresa,List<Pendiente> Lista)
        {
            CorreoCfg cfg = (new general()).daConfiguracionCorreo();
            CorreoMsg msg = new CorreoMsg();

            List<credencial> lstUsuarios = (new admCredencial()).DaUsuariosAplicacionPago(IdEmpresa.ToString ());

            System.Text.StringBuilder mensaje = new System.Text.StringBuilder("Estimado(a): ");
            foreach (credencial dst in lstUsuarios)
            {
                if (!string.IsNullOrEmpty(dst.Correo))
                {
                    msg.Destinatarios.Add(dst.Correo);
                    mensaje.AppendLine(dst.Nombre + "; ");
                }
            }
            mensaje.AppendLine(" <br/><br/>");
            mensaje.AppendLine("Te informamos que hay (" + Lista.Count.ToString() + ") solicitudes por cobrar,");
            mensaje.AppendLine(" las cuales puedes consultar en el sistema SIPAC <br/><br/>");

            mensaje.Append("<Table style='width :90%;margin:0 auto;font-size:10px'>");
            mensaje.AppendLine("<tr><td>ORDEN: </td><td>FECHA:</td><td>EMPRESA:</td><td>CLIENTE:</td><td>SERVICIO:</td><td>DESCRIPCION:</td></tr>");
            foreach (Pendiente obj in Lista)
            {
                mensaje.AppendLine("<tr>");
                mensaje.AppendLine("<td>" + obj.IdOrden.ToString() + "</td>");
                mensaje.AppendLine("<td>" + obj.FechaInicio.ToString("dd/MM/yyyy") + "</td>");
                mensaje.AppendLine("<td>" + obj.Empresa + "</td>");
                mensaje.AppendLine("<td>" + obj.Cliente + "</td>");
                mensaje.AppendLine("<td>" + obj.Servicio + "</td>");
                mensaje.AppendLine("<td>" + obj.Descripcion + "</td>");
                mensaje.AppendLine("</tr>");
            }
            mensaje.Append("</Table><br/><br/>");

            mensaje.AppendLine("Seguimos trabajando para servirte mejor <br/><br/>");
            mensaje.AppendLine("<b>ATTE. <br/>");
            mensaje.AppendLine("Equipo SIPAC</b><br/><br/> ");

            msg.Asunto = "SIPAC - Cuentas por cobrar " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.Mensaje = mensaje.ToString();

            if (msg.Destinatarios.Count > 0)
            {
                admCorreo admCorreo = new admCorreo();
                admCorreo.enviar(cfg, msg);
            }
        }

        public CorreoCfg daConfiguracionCorreo()
        {
            CorreoCfg resultado = new CorreoCfg();
            resultado.Servidor = Properties.Settings.Default.correoServidor;
            resultado.Nombre  = Properties.Settings.Default.correoNombre;
            resultado.Cuenta = Properties.Settings.Default.correoUsuario;
            resultado.Clave = Properties.Settings.Default.correoClave;
            resultado.Puerto = Properties.Settings.Default.correoPuerto;
            return resultado;
        }
    }
}
