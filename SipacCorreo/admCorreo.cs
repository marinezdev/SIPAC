using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SipacCorreo
{
    public class admCorreo
    {
        public void enviar(CorreoCfg cfg, CorreoMsg msg)
        {
            MailMessage mensage = new MailMessage();
            
            mensage.To.Clear();
            mensage.CC.Clear();
            mensage.Bcc.Clear();
            mensage.Attachments.Clear();
            mensage.Priority = MailPriority.Normal;

            foreach (string destinatario in msg.Destinatarios) mensage.To.Add(destinatario);
            mensage.From = new MailAddress(cfg.Cuenta,"");
            mensage.Subject = msg.Asunto;
            mensage.SubjectEncoding = System.Text.Encoding.UTF8;
            mensage.Body = msg.Mensaje;
            mensage.BodyEncoding = System.Text.Encoding.UTF8;
            mensage.IsBodyHtml = true;
            foreach (string archivo in msg.ArhivosAdjuntos) if (!string.IsNullOrEmpty(archivo)) mensage.Attachments.Add(new Attachment(archivo));

            SmtpClient clientSmtp = new SmtpClient(cfg.Servidor, cfg.Puerto);
            clientSmtp.Credentials = new NetworkCredential(cfg.Nombre, cfg.Clave );
            clientSmtp.EnableSsl = false;
            clientSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try { clientSmtp.Send(mensage); }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }

    public class CorreoMsg
    {
        public string Asunto = string.Empty;
        public string Mensaje = string.Empty;
        public List<string> Destinatarios = new List<string>();
        public List<string> ArhivosAdjuntos = new List<string>();
        private bool mFormatoHtml = false;
        public bool FormatoHtml { get { return mFormatoHtml; } set { mFormatoHtml = value; } }
    }

    public class CorreoCfg
    {
        public string Nombre = string.Empty;
        public string Servidor = string.Empty;
        public string Cuenta = string.Empty;
        public string Clave = string.Empty;
        public int Puerto = 0;
    }
}
