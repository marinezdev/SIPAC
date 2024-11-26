using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_VeSolFacturaFondos : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdSol.Value = Request.Params["Id"].ToString();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            if (Request.Params["bk"] != null)
            {
                string regreso = Request.Params["bk"] + ".aspx";
                regreso = regreso + "?idfd=" + Request.Params["idfd"].ToString();
                Response.Redirect(regreso);
            }
            else{Response.Redirect("espera.aspx");}
        }

        private void llenaSolicitud(int IdSol)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud osol = comun.admsolicitud.carga(IdSol);
            lbProveedor.Text = osol.Proveedor;
            lbRfc.Text = osol.Rfc;
            lbBanco.Text = osol.Banco;
            lbCuenta.Text = osol.Cuenta;
            lbClabe.Text = osol.CtaClabe;
            lbSucursal.Text = osol.Sucursal;
            lbFactura.Text = osol.Factura;
            lbFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbImporte.Text = osol.Importe.ToString("C2");
            lbCodPago.Text = osol.CondicionPago;
            lbConcepto.Text = osol.Concepto;
            lbProyecto.Text = osol.Proyecto;
            lbDecProyecto.Text = osol.DescProyecto;
            lbMoneda.Text = osol.Moneda;

            if (osol.ConFactura.Equals(cpplib.Solicitud.enConFactura.SI)) { CargaFactura(osol.IdSolicitud, osol.FechaFactura); } else { }
            if (osol.Estado.Equals(cpplib.Solicitud.solEstado.Rechazada)) { lbMotivoRechazo.Text = comun.admsolicitud.DaRechazoSolicitud(IdSol); pnRechazo.Visible = true; }
        }

        protected void CargaFactura(int IdSolicitud, DateTime FechaFactura)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaFactura(IdSolicitud);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(FechaFactura);
            String Archivo = Carpeta + oArchivo.ArchivoDestino;
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }
    }
}