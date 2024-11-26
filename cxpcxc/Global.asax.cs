using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.IO;
using System.Web.Configuration;

namespace cxpcxc
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e){}

        protected void Session_Start(object sender, EventArgs e){}

        protected void Application_BeginRequest(object sender, EventArgs e){}

        protected void Application_AuthenticateRequest(object sender, EventArgs e){}

        protected void Application_Error(object sender, EventArgs e){
            //StringBuilder mensajeErr = new StringBuilder(DateTime.Now.ToString());
            //mensajeErr.AppendLine("Usuario : " + Context.User.Identity.Name.ToString());
            //mensajeErr.AppendLine("Página : " + Request.Url.AbsoluteUri);
            //mensajeErr.AppendLine("Message : " + Server.GetLastError().InnerException.ToString());
            //mensajeErr.AppendLine("InnerException : " + Server.GetLastError().Message.ToString());
            //mensajeErr.AppendLine("Source : " + Server.GetLastError().Source.ToString());
            //mensajeErr.AppendLine("StackTrace : " + Server.GetLastError().StackTrace.ToString());
            //mensajeErr.AppendLine("TargetSite : " + Server.GetLastError().TargetSite.ToString());
            //mensajeErr.AppendLine("-------------------------------------------------------------------------------------");
            //Server.ClearError();

            //string nombreArchivoErr = Properties.Settings.Default.errLog + DateTime.Now.ToString("yyyyMMdd") + ".log";
            //using (StreamWriter archivoErr = new StreamWriter(nombreArchivoErr, true))
            //{
            //    archivoErr.WriteLine(mensajeErr.ToString());
            //}

            //Response.Redirect("errorsistema.html");
        }

        protected void Session_End(object sender, EventArgs e){}

        protected void Application_End(object sender, EventArgs e){}
    }
}