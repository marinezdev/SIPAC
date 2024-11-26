using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using cpplib;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class trf_NotaCredito : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int IdNotaCredito = Convert.ToInt32(Request.Params["Id"]);
                this.llenaDatos(IdNotaCredito);
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("trf_ConsultaNotaCredito.aspx");}
        
        private void llenaDatos(int IdNotaCredito)
        {
            //cpplib.admCxpNotaCredito adm = new cpplib.admCxpNotaCredito();
            cpplib.cxpNotaCredito oResultado = comun.admcxpnotacredito.carga(IdNotaCredito);
            lbProveeedor.Text = oResultado.Proveedor;
            lbFolio.Text = oResultado.IdNotaCredito.ToString();
            lbFecha.Text = oResultado.Fecha.ToString ("dd/MM/yyyy");
            lbImporte.Text = oResultado.Importe.ToString("C2");
            lbdescripcion.Text = oResultado.Descripcion;
            lbMoneda.Text = oResultado.Moneda;
            imgbtnVerFactOrg.CommandArgument = oResultado.IdSolicitudOrigen.ToString ();
            
            cpplib.cxpNotaCreditoArchivo oArchivo = comun.admcxpnotacredito.CargaArchivo(IdNotaCredito);
            ImgVerNotaCredito.CommandArgument = oArchivo .Nombre;

            this.MuestraImagenNota(oArchivo.Nombre);

            this.MuestraSolicitudOrigen(oResultado.IdSolicitudOrigen);

            DataTable LstAsignacion = comun.admcxpnotacredito.DaSolicitudesRelacionadasNotaCredito(IdNotaCredito);
            if (LstAsignacion.Rows.Count > 0) 
            {
                //rptSolAsig.DataSource = LstAsignacion;
                //rptSolAsig.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptSolAsig, LstAsignacion);
                pnSolAsignadas.Visible = true;
            }
            
            brnImgEliminar .Visible = oResultado.Estado.Equals (cpplib.cxpNotaCredito.enEstado.Activa);
        }

        private void MuestraImagenNota(String Archivo)
        {
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\NotasCredito\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        private  void MuestraSolicitudOrigen(int Idsolicitud)
        {
            cpplib.Solicitud oResultado = comun.admsolicitud.carga(Idsolicitud);
            ltSolOrigen.Text = "<table runat ='server' style ='width:98%;margin :0 auto; font-size :13px;background-color:#F3F9FE'>";
            ltSolOrigen.Text += "<tr><td colspan ='2' class ='Titulos'><b>" + oResultado.Proveedor.ToString() + "</b></td></tr>";
            ltSolOrigen.Text += "<tr><td  style='width :15%'><b>Fecha:</b></td><td>" + oResultado.FechaFactura.ToString("dd/MM/yyyy") + "</td></tr>";
            ltSolOrigen.Text += "<tr><td><b>Factura:</b></td><td>" + oResultado.Factura.ToString() + "</td></tr>";
            ltSolOrigen.Text += "<tr><td><b>Importe:</b></td><td>" + oResultado.Importe.ToString("C2") + "</td></tr>";
            ltSolOrigen.Text += "<tr><td><b>Concepto:</b></td><td>" + oResultado.Concepto + "</td></tr>";
            ltSolOrigen.Text += "</table>";
        }

       
        protected void brnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if(comun.admcxpnotacredito.Eliminar (Convert.ToInt32 (lbFolio .Text)))
                Response .Redirect ("trf_COnsultaNotaCredito.aspx");
            else
                ltMsg.Text ="No es posible eliminar la nota de Credito.";
        }


        private void VerFactura(int IdSolicitud)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaFactura(IdSolicitud);
            cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSolicitud));
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
            String Archivo = Carpeta + oArchivo.ArchivoDestino;
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void rptSolAsig_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) { lbTituloDocto.Text = "SOLICITUD DE ASIGNACION"; this.VerFactura(Convert.ToInt32(e.CommandArgument)); }
        }

        protected void ImgVerNotaCredito_Click(object sender, ImageClickEventArgs e) 
        { 
            lbTituloDocto.Text ="NOTA DE CREDITO"; 
            this.MuestraImagenNota(ImgVerNotaCredito.CommandArgument); 
        }

        protected void imgbtnVerFactOrg_Click(object sender, ImageClickEventArgs e) 
        { 
            lbTituloDocto.Text = "SOLICITUD QUE LA ORIGINO"; 
            this.VerFactura(Convert.ToInt32(imgbtnVerFactOrg.CommandArgument)); 
        }

    }

}