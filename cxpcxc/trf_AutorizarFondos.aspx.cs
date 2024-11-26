using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_AutorizarFondos : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CargaSolicitudes();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        
        private void CargaSolicitudes()
        {
            ltMsg.Text = "";
            cpplib.credencial oCrd=(cpplib.credencial)Session["credencial"];
            //cpplib.admFondos admfd = new cpplib.admFondos();
            List<cpplib.LoteFondos> lista = comun.admfondos.DasolicitudesFondos(oCrd.IdEmpresaTrabajo.ToString());
            if (lista.Count > 0)
            {
                //rptSolFondeo.DataSource = lista;
                //rptSolFondeo.DataBind();
                LlenarControles.LlenarRepeater(ref rptSolFondeo, lista);
            }
            else {
                ltMsg .Text ="No hay Solicitudes disponibles";
                rptSolFondeo.DataSource = null;
                rptSolFondeo.DataBind();
            }
        }

        protected void rptSolFondeo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.LoteFondos oSol = (cpplib.LoteFondos)(e.Item.DataItem);
                if (oSol.Estado == cpplib.LoteFondos.SolEdoFondos.Autorizado ) { ((Label)e.Item.FindControl("lbFechafd")).Text = ""; }
            }
        }

        protected void rptSolFondeo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Detalle"))
            {
                Response.Redirect("trf_AutorizarFondosDetalle.aspx?idfd=" + e.CommandArgument.ToString());
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.CargaSolicitudes(); }

    }
}