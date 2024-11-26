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
    public partial class trf_SolicitudesPagadas : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["Consulta"] != null) { Session.Remove("Consulta"); Session["Consulta"] = "S"; } else { Session["Consulta"] = "S"; }

                hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresaTrabajo.ToString();

                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.llenaproveedor();
                this.ValidaConsultaPrevia();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("espera.aspx");}

        private void llenaproveedor()
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(hdIdEmpresa.Value), "Nombre", "Rfc");

            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(hdIdEmpresa.Value);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        
        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.DaSolicitudes(); }

        private void DaSolicitudes()
        {                
            DataTable LstSol = comun.admsolicitud.ConsultadePagosXProveedor(hdIdEmpresa.Value, dpProveedor.SelectedValue, txF_Inicio.Text, txF_Fin.Text);
            if (LstSol.Rows.Count > 0)
            {
                //rptSolicitud.DataSource = LstSol;
                //rptSolicitud.DataBind();
                pnSolicitud.Visible = true;
                LlenarControles.LLenarRepeaterDataTable(ref rptSolicitud, LstSol);

                AgregaConsultaSesion(dpProveedor.SelectedValue + "|" + txF_Inicio.Text + "|" + txF_Fin.Text);
            }
            else
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                pnSolicitud.Visible = false;
            }
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) { Response.Redirect("trf_PagoSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolicitudesPagadas"); }
        }
               
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.credencial oCrd= (cpplib.credencial)Session["credencial"];

            DataTable Lista = comun.admsolicitud.ConsultadePagosXProveedor(hdIdEmpresa.Value, dpProveedor.SelectedValue, txF_Inicio.Text, txF_Fin.Text);
            string Archivo = oCrd.IdUsr.ToString ().PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy") + ".xls";
            if (Lista.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Archivo);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView grdDatos = new GridView();
                    grdDatos.DataSource = Lista;
                    grdDatos.DataBind();
                    grdDatos.AllowPaging = false;
                    grdDatos.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }


        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "trf_SolicitudesPagadas";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_SolicitudesPagadas"))
                {
                    string[] valores = csConsulta.Datos.Split('|');
                    dpProveedor.SelectedValue=valores[0];
                    txF_Inicio.Text =valores[1];
                    txF_Fin.Text = valores[2];
                    DataTable LstSol = comun.admsolicitud.ConsultadePagosXProveedor(hdIdEmpresa.Value, valores[0], valores[1], valores[2]);
                    if (LstSol.Rows.Count > 0)
                    {
                        rptSolicitud.DataSource = LstSol;
                        rptSolicitud.DataBind();
                        pnSolicitud.Visible = true;
                    }
                }
            }
        }
                
    }
}