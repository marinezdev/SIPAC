using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_SolRequierenFactura : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltMsg.Text = "";
                cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
                cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                List<cpplib.Solicitud> Lista = admSol.DaTodasSolicitudesSinfactura(ocd.IdEmpresa, ocd.UnidadNegocio);
                if (Lista.Count > 0)
                {
                    rptSolicitud.DataSource = Lista;
                    rptSolicitud.DataBind();
                }
                else 
                { 
                    ltMsg.Text = "No hay solicitudes pendientes sin factura"; 
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

            
        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                Response.Redirect("trf_VerSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolRequierenFactura"); 
            }
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image oimg = (Image)e.Item.FindControl("imgConfactura");
                cpplib.Solicitud oSol = (cpplib.Solicitud)(e.Item.DataItem);

                if (oSol.ConFactura.Equals(cpplib.Solicitud.enConFactura.NO)) { oimg.ImageUrl = "~/img/sem_R.png"; }
                if (oSol.ConFactura.Equals(cpplib.Solicitud.enConFactura.SI)) { oimg.ImageUrl = "~/img/Sem_V.png"; }
            }
        }

                
    }
}