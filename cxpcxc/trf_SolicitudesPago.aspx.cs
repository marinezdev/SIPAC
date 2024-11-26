using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_SolicitudesPago : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["Consulta"] != null) { Session.Remove("Consulta"); Session["Consulta"] = "N"; } else { Session["Consulta"] = "N"; }
                hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
                llenaCatalogos(hdIdEmpresa.Value);
                this.DaSolicitudes(); 
            }
        }

        private void llenaCatalogos(string IdEmpresa)
        {
            List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            dpProveedor.DataSource = lstPvd;
            dpProveedor.DataValueField = "Rfc";
            dpProveedor.DataTextField = "Nombre";
            dpProveedor.DataBind();
            dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(IdEmpresa);

            dpProyecto.DataSource = lstProyectos;
            dpProyecto.DataValueField = "Valor";
            dpProyecto.DataTextField = "Texto";
            dpProyecto.DataBind();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void DaSolicitudes()
        {
            ltMsg.Text = "";
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            List<cpplib.Solicitud> Lista = admSol.ListaSolicitudesXPagar(hdIdEmpresa.Value,dpProveedor.SelectedValue,dpProyecto .SelectedValue);
            if (Lista.Count > 0)
            {
                rptSolicitud.DataSource = Lista;
                rptSolicitud.DataBind();
                lbTotPesos.Text =Lista.Where( sol=> sol.Moneda == "Pesos").Sum(sol => sol.Importe).ToString ("C2") ;
                lbTotDlls.Text = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.Importe).ToString("C2");
                pnTotales.Visible = true;
            }
            else
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                pnTotales.Visible = false;
                ltMsg.Text = "No hay Solicitudes Disponibles";
            }
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) Response.Redirect("trf_PagoSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolicitudesPago");
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { DaSolicitudes(); }

    }
}