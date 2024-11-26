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
    public partial class trf_SolictudesCoordinador : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial  oCd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCd.IdEmpresa.ToString();
                hdUndNegocio.Value = oCd.UnidadNegocio.ToString();
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Now.AddDays (-7).ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.llenaCombos();
                this.ValidaConsultaPrevia();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaCombos()
        {
            LlenarControles.LlenarDropDownList(ref dpSolicitante, comun.admcredencial.DaUsuariosSolicitantexEmpresaxUnidNegocio(hdIdEmpresa.Value, hdUndNegocio.Value), "Nombre", "IdUsr");

            /*Unidad Solicitante*/
            //List<cpplib.credencial> LstUsr = (new cpplib.admCredencial()).DaUsuariosSolicitantexEmpresaxUnidNegocio(hdIdEmpresa.Value,hdUndNegocio.Value );
            //dpSolicitante.DataSource = LstUsr;
            //dpSolicitante.DataTextField = "Nombre";
            //dpSolicitante.DataValueField = "IdUsr";
            //dpSolicitante.DataBind();

            //dpSolicitante.Items.Insert(0, new ListItem("Seleccionar", "0"));

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

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e)
        {
            ltMsg.Text = "";
            hdConsulta.Value =  this.DaConsulta();
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            if (!String.IsNullOrEmpty(hdConsulta.Value))
            {
                List<cpplib.SolContabilidad> Lista = comun.admsolicitud.DaSolCoordinadorConsultaAbierta(hdIdEmpresa.Value, hdConsulta.Value);
                if (Lista.Count > 0)
                {
                    //rptSolicitud.DataSource = Lista;
                    //rptSolicitud.DataBind();
                    LlenarControles.LlenarRepeater(ref rptSolicitud, Lista);
                    pnSolicitud.Visible = true;

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
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                Response.Redirect("trf_VerSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolictudesCoordinador"); 
            }
        }

        private String DaConsulta()
        {
            string Consulta = " And S.UnidadNegocio=" + hdUndNegocio.Value;

            if (dpSolicitante.SelectedValue != "0") { Consulta += " And S.IdUsr=" + dpSolicitante.SelectedValue; }
            if (dpEstado.SelectedValue != "0") { Consulta += " And S.Estado=" + dpEstado.SelectedValue; }

            if (!String.IsNullOrEmpty(txF_Inicio.Text) && !String.IsNullOrEmpty(txF_Fin.Text))
            {
                if (string.IsNullOrEmpty(Consulta)) { Consulta = " And (S.FechaRegistro >='" + txF_Inicio.Text + "' and S.FechaRegistro < DATEADD(dd,1,'" + txF_Fin.Text + "'))"; }
                else { Consulta += " And (S.FechaRegistro >='" + txF_Inicio.Text + "' and S.FechaRegistro < DATEADD(dd,1,'" + txF_Fin.Text + "'))"; }
            }
            return Consulta;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image oimg = (Image)e.Item.FindControl("imgEstado");
                string Imagen = String.Empty;
                cpplib.SolContabilidad oSol = (cpplib.SolContabilidad)(e.Item.DataItem);

                switch (oSol.Estado)
                {
                    case cpplib.Solicitud.solEstado.Solicitud:
                        oimg.ImageUrl = "~/img/niv_1.png";
                        break;
                    case cpplib.Solicitud.solEstado.Autorizacion:
                        oimg.ImageUrl = "~/img/niv_2.png";
                        break;
                    case cpplib.Solicitud.solEstado.Captura:
                        oimg.ImageUrl = "~/img/niv_3.png";
                        break;
                    case cpplib.Solicitud.solEstado.PagoParcial:
                        oimg.ImageUrl = "~/img/niv_4.png";
                        break;
                    case cpplib.Solicitud.solEstado.Pagado:
                        oimg.ImageUrl = "~/img/niv_5.png";
                        break;
                    case cpplib.Solicitud.solEstado.Rechazada:
                        oimg.ImageUrl = "~/img/niv_0.png";
                        break;
                }
            }
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            DataTable Lista = comun.admsolicitud.DaSolCoordinadorExportar (hdIdEmpresa.Value, hdConsulta.Value);
            String IdUsr = ((cpplib.credencial)Session["credencial"]).IdUsr.ToString();
            string Archivo = IdUsr.PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy") + ".xls";
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
            csConsulta.Pagina = "trf_SolictudesCoordinador";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_SolictudesCoordinador"))
                {
                    hdConsulta.Value = csConsulta.Datos;
                    //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                    List<cpplib.SolContabilidad> Lista = comun.admsolicitud.DaSolCoordinadorConsultaAbierta(hdIdEmpresa.Value, hdConsulta.Value);
                    if (Lista.Count > 0)
                    {
                        //rptSolicitud.DataSource = Lista;
                        //rptSolicitud.DataBind();
                        LlenarControles.LlenarRepeater(ref rptSolicitud, Lista);
                        pnSolicitud.Visible = true;

                    }
                }
            }
        }


    }

}