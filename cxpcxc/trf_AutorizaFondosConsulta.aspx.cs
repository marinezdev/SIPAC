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
    public partial class trf_AutorizaFondosConsulta : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
                
                hdIdEmpresa.Value = oCdr.IdEmpresaTrabajo.ToString();
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
                this.ValidaConsultaPrevia();
            }
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_AutorizaFondosConsulta"))
                {
                    string[] valores = csConsulta.Datos.Split('|');
                    dpEstado.SelectedValue = valores[0];
                    txF_Inicio.Text = valores[1];
                    txF_Fin.Text = valores[2];
                }
            }
            this.CargaSolicitudes();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void CargaSolicitudes()
        {
            ltMsg.Text = "";
            //cpplib.admFondos admfd = new cpplib.admFondos();
            List<cpplib.LoteFondos> lista = comun.admfondos.DasolicitudesFondosXEmpresa(hdIdEmpresa.Value, txF_Inicio.Text, txF_Fin.Text, dpEstado.SelectedValue);
            if (lista.Count > 0)
            {
                //rptSolFondeo.DataSource = lista;
                //rptSolFondeo.DataBind();
                LlenarControles.LlenarRepeater(ref rptSolFondeo, lista);

                this.AgregaConsultaSesion(dpEstado.SelectedValue + "|" + txF_Inicio.Text + "|" + txF_Fin.Text );
            }
            else
            {
                ltMsg.Text = "No hay Solicitudes disponibles";
                rptSolFondeo.DataSource = null;
                rptSolFondeo.DataBind();
            }
        }

        protected void rptSolFondeo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.LoteFondos oSol = (cpplib.LoteFondos)(e.Item.DataItem);
                if (oSol.Estado.Equals(cpplib.LoteFondos.SolEdoFondos.Con_Fondos)) 
                    ((ImageButton)e.Item.FindControl("imgBtnComp")).Visible = true; 
                if (oSol.Estado == cpplib.LoteFondos.SolEdoFondos.Autorizado ) 
                    ((Label)e.Item.FindControl("lbFechafd")).Text = ""; 
            }
        }

        protected void rptSolFondeo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Comprobante")) 
                Response.Redirect("trf_VerComprobanteFondos.aspx?idfd=" + e.CommandArgument.ToString() + "&bk=trf_AutorizaFondosConsulta"); 
            else if(e.CommandName.Equals("Detalle"))
                Response.Redirect("trf_AutorizaFondosConsultaDet.aspx?idfd=" + e.CommandArgument.ToString());
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.CargaSolicitudes(); }

        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "trf_AutorizaFondosConsulta";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

    }
}