using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class cxpcxcSeleccionaEmpresa : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) 
        {
            if (Session["credencial"] == null) 
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                this.llenaEmpresas(); 
            }
        }

        private void llenaEmpresas()
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            //DataTable Lista = comun.admcredencial.DalistaEmpresaAsignadasUsuario(oCrd.IdUsr);
            //rptSelEmpresa.DataSource = Lista;
            //rptSelEmpresa.DataBind();
            LlenarControles.LLenarRepeaterDataTable(ref rptSelEmpresa, comun.admcredencial.DalistaEmpresaAsignadasUsuario(oCrd.IdUsr));
        }

        protected void rptSelEmpresa_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            cpplib.credencial oCred = (cpplib.credencial)Session["credencial"];

            // debemos actualizar la unidad de negocio en base a la empresa seleccionada.
            //cpplib.clsSIPAC _SIPAC = new cpplib.clsSIPAC();
            int _UnidadNegocio = comun.clssipac.UnidadNegocio(oCred.IdUsr, Convert.ToInt32(e.CommandArgument.ToString()));

            oCred.UnidadNegocio = _UnidadNegocio;
            oCred.IdEmpresaTrabajo = Convert.ToInt32(e.CommandArgument.ToString ());
            Session["credencial"] = oCred;
            Session.Remove("csConsultas");
            Response.Redirect("espera.aspx");
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            Response.Redirect("espera.aspx"); 
        }
    }
}