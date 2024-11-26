using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_ConsultaNotaCrcedito : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                txF_Inicio.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_txF_Inicio.EndDate = DateTime.Now;
                ce_txF_Fin.EndDate = DateTime.Now;
                this.llenacatalogos(oCd.IdEmpresaTrabajo.ToString());
                this.ValidaConsultaPrevia();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_ConsultaNotaCredito"))
                {
                    hdConsulta.Value = csConsulta.Datos;
                    this.EjecutaConsulta(csConsulta.Datos);
                }
            }
            else { this.ConsultaNormal(); }
        }

        private void llenacatalogos(string IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");
            //List<cpplib.CatProveedor> LstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = LstPvd;
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.ConsultaNormal(); }

        private void ConsultaNormal()
        {
            hdConsulta.Value = this.DaFiltradoConsulta();
            this.EjecutaConsulta(hdConsulta.Value);
        }

        private void EjecutaConsulta(string Consulta)
        {
            ltMsg.Text = "";
            cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
            //cpplib.admCxpNotaCredito adm = new cpplib.admCxpNotaCredito();

            if (!string.IsNullOrEmpty(Consulta))
            {
                List<cpplib.cxpNotaCredito> Lista = comun.admcxpnotacredito.Lista(ocd.IdEmpresa, Consulta);
                if (Lista.Count > 0)
                {
                    //rptNotaCredito .DataSource = Lista;
                    //rptNotaCredito.DataBind();
                    LlenarControles.LlenarRepeater(ref rptNotaCredito, Lista);
                    rptNotaCredito.Visible = true;
                    pnNotaCredito.Visible = true; 
                    this.AgregaConsultaSesion(hdConsulta.Value);
                }
                else 
                { 
                    rptNotaCredito.DataSource = null; 
                    rptNotaCredito.DataBind(); 
                    ltMsg.Text = "No hay datos para mostrar"; 
                    pnNotaCredito.Visible = false; 
                }
            }
            else 
            { 
                ltMsg.Text = "Seleccione los datos de consulta"; 
                pnNotaCredito.Visible = false; 
            }
        }

        private String DaFiltradoConsulta()
        {
            string Consulta = string.Empty;
            if (dpProveedor.SelectedValue != "0") 
                Consulta += " And Rfc='" + dpProveedor.SelectedValue + "'";
            if (!String.IsNullOrEmpty(txF_Inicio.Text) && !String.IsNullOrEmpty(txF_Fin.Text))
            {
                Consulta += " And (FechaRegistro >='" + txF_Inicio.Text + "' and FechaRegistro < DATEADD(dd,1,'" + txF_Fin.Text + "'))";
            }

            return Consulta;
        }

        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "trf_ConsultaNotaCredito";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        protected void lkLimpiar_Click(object sender, EventArgs e)
        {
            dpProveedor.SelectedIndex = 0;
            txF_Inicio.Text = "";
            txF_Fin.Text = "";
        }

        protected void rptNotaCredito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                Response.Redirect("trf_VerNotaCredito.aspx?id=" + e.CommandArgument.ToString());
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("trf_AltaNotaCredito.aspx");
        }
        
    }
}