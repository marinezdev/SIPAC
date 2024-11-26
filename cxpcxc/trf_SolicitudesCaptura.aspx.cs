using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace cxpcxc
{
    public partial class trf_SolicitudesCaptura : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){this.DaSolicitudes();}
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect ("espera.aspx");}
        
        private void DaSolicitudes()
        {
            String IdEmpresa = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            List<cpplib.Solicitud> Lista = admSol.ListaSolicitudesXCapturar(IdEmpresa);
            if (Lista.Count > 0)
            {
                rptSolicitud.DataSource = Lista;
                rptSolicitud.DataBind();
                BtnExportar.Visible = true;
            }
            else
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                BtnExportar.Visible = false;
                ltMsg.Text = "No hay Solicitudes para Captura";
            }
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            String IdEmpresa = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
            string Archivo = "SolicitudesCaptura_" + DateTime .Now.ToString("ddMMyy") +".xls";
            DataTable Datos = (new cpplib.admSolicitud()).ListaSolXCapturarParaExportar(IdEmpresa);

            if (Datos.Rows.Count > 0){
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Archivo);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView grdDatos = new GridView();
                    grdDatos.DataSource = Datos;
                    grdDatos.DataBind();
                    grdDatos.AllowPaging = false;
                    grdDatos.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Ver")) Response.Redirect("trf_CapturaSolicitud.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_SolicitudesCaptura");

            if (e.CommandName.Equals("Aceptar"))
            {
             cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
                cpplib.admSolicitud oSol = new cpplib.admSolicitud();
                oSol.CambiaEstadoSolicitud(Convert.ToInt32(e.CommandArgument.ToString()), cpplib.Solicitud.solEstado.Captura);
                RegistraBitacora(oCredencial, Convert.ToInt32(e.CommandArgument.ToString()));
                this.DaSolicitudes();
            }
        }

        private void RegistraBitacora(cpplib.credencial oCredencial, int IdSolicitud)
        {
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSolicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Estado = cpplib.Solicitud.solEstado.Captura;
            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);
        }

    }
}