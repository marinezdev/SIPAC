using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxpcxc : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                if (Session["credencial"] != null)
                {
                    cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];                                   
                    String txtArchivoMenu = Server.MapPath(".") + "\\menushtml\\" + oCredencial.Grupo.ToString() + ".html";     //--+
                    System.IO.StreamReader objReader = System.IO.File.OpenText(txtArchivoMenu);                                 //  +--Comentar para agrgar un menu dinámico
                    ltStrMenu.Text = objReader.ReadToEnd();                                                                     //  |
                    objReader.Close();                                                                                          //--+
                    //if (oCredencial.Grupo == cpplib.credencial.usrGrupo.Admsys) 
                    lbMstNombreUsuario.Text = oCredencial.Nombre;
                    cpplib.Empresa oEmp = (new cpplib.admCatEmpresa()).carga(oCredencial.IdEmpresaTrabajo);
                    lbMstEmpresa.Text =oEmp.Nombre ;
                    if (!string.IsNullOrEmpty(oEmp.Logo))
                    {
                        ImgLogo.Src = "~/img/" + oEmp.Logo;
                    }
                    else 
                    { 
                        ImgLogo.Src = "~/img/logo_asae.png"; 
                    }
                }
           }
        }
    }
}