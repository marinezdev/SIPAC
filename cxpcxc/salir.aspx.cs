using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class salir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["credencial"] != null)
            //{
            //    iiclib.credencial oCredencial = (iiclib.credencial)Session["credencial"];
            //    iiclib.admCredencial oAdmCred = new iiclib.admCredencial();
            //    oAdmCred.desconecta(oCredencial.IdUsr);
            //}
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }
    }
}