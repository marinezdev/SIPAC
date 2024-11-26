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
    public partial class trf_SolicitudesRegistro : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cpplib.credencial ocd= (cpplib.credencial)Session["credencial"];
                hdIdUsr.Value = ocd.IdUsr.ToString ();
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                txF_Inicio.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_txF_Inicio.EndDate = DateTime.Now;
                ce_txF_Fin.EndDate=DateTime.Now;
                this.llenaCombos(ocd.IdEmpresa.ToString());
                this.ValidaConsultaPrevia();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("espera.aspx");}

        private void llenaCombos(String IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");

            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            /* Llena estado*/
            foreach (int value in Enum.GetValues(typeof(cpplib.Solicitud.solEstado)))
            {
                if (value != 60)
                {
                    var name = Enum.GetName(typeof(cpplib.Solicitud.solEstado), value);
                    dpEstado.Items.Add(new ListItem(name, value.ToString()));
                }
            }
            dpEstado.Items.Insert(0, new ListItem("Seleccionar", "0"));
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
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();

            if (!string.IsNullOrEmpty(Consulta))
            {

                List<cpplib.Solicitud> Lista = comun.admsolicitud.DaSolConsultaAbiertaXUsuario(ocd.IdEmpresa, ocd.UnidadNegocio, Consulta);
                if (Lista.Count > 0)
                {
                    //rptSolicitud.DataSource = Lista;
                    //rptSolicitud.DataBind();
                    LlenarControles.LlenarRepeater(ref rptSolicitud, Lista);
                    pnSolicitud.Visible = true;

                    lbTotPesos.Text = Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.Importe).ToString("C2");
                    lbTotDlls.Text = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.Importe).ToString("C2");

                    this.AgregaConsultaSesion(hdConsulta.Value);
                }
                else 
                { 
                    rptSolicitud.DataSource = null; 
                    rptSolicitud.DataBind(); 
                    ltMsg.Text = "No hay solicitudes para mostrar"; 
                    pnSolicitud.Visible = false; 
                }
            }
            else 
            { 
                ltMsg.Text = "seleccione los datos de consulta"; 
                pnSolicitud.Visible = false; 
            }
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) { 
                Response.Redirect("trf_VerSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolicitudesRegistro"); 
            }
        }

        private String DaFiltradoConsulta()
        {
            string Consulta = string.Empty;

            if (dpProveedor.SelectedValue!="0"){Consulta += " And Rfc='" + dpProveedor.SelectedValue + "'"; }
            if (dpEstado.SelectedValue != "0") { Consulta += " And Estado=" + dpEstado.SelectedValue; }
            if (!string.IsNullOrEmpty(TxNumFactura.Text.Trim())) {
                if (TxNumFactura.Text.Contains('%')) { Consulta += " And Factura like '" + TxNumFactura.Text.Trim() + "'"; } else { Consulta += " And Factura ='" + TxNumFactura.Text.Trim() + "'"; }
            }
            if (!String.IsNullOrEmpty(txF_Inicio.Text) && !String.IsNullOrEmpty(txF_Fin.Text))
            {
                Consulta += " And (FechaRegistro >='" + txF_Inicio.Text + "' and FechaRegistro < DATEADD(dd,1,'" + txF_Fin.Text + "'))"; 
            }

            return Consulta;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image oimg = (Image)e.Item.FindControl("imgConfactura");
                Image oPrd = (Image)e.Item.FindControl("imgPrioridad");
                cpplib.Solicitud oSol = (cpplib.Solicitud)(e.Item.DataItem);

               
                switch (oSol.ConFactura )
                {
                    case cpplib.Solicitud.enConFactura .NO :
                        oimg.ImageUrl = "~/img/sem_R.png";
                        break;
                    case cpplib.Solicitud.enConFactura.SI :
                        oimg.ImageUrl = "~/img/Sem_V.png";
                        break;
                }
                if (oSol.Prioridad == 1) { oPrd.Visible = true; }
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
            DataTable Lista = comun.admsolicitud.ExportaSolRegistradasXSolicitante(ocd.IdEmpresa, ocd.UnidadNegocio, hdConsulta.Value);
            string Archivo = hdIdUsr.Value.PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy") + ".xls";
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

        protected void btnPrioridad_Click(object sender, EventArgs e) { this.Response.Redirect("trf_AgregaMarcaPrioridad.aspx"); }

        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "trf_SolicitudesRegistro";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_SolicitudesRegistro"))
                {
                    hdConsulta.Value = csConsulta.Datos;
                    this.EjecutaConsulta(csConsulta.Datos);
                }
            }
            else { this.ConsultaNormal(); }
        }

        protected void lkLimpiar_Click(object sender, EventArgs e)
        {
            dpProveedor.SelectedIndex =0;
            dpEstado.SelectedIndex = 0;
            TxNumFactura.Text = "";
            txF_Inicio.Text  = "";
            txF_Fin.Text = "";
            pnSolicitud.Visible = false; 
        }

          
    }
}