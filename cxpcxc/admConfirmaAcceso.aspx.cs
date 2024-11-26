using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admConfirmaAcceso : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                if (Request.Params["Id"] != null)
                {
                    //cpplib.admCredencial oAdmCred = new cpplib.admCredencial();
                    lbUsuario.Text = comun.admcredencial.daUsuarioDeId(Request.Params["id"]);
                }
                else { purgaUsuario(); }
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e) { this.purgaUsuario(); }
        private void purgaUsuario()
        {
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }
    }
}