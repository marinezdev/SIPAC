using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_VerSolSinFactura : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdSol.Value = Request.Params["Id"].ToString();
                this.llenaCatalogos();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
                this.llenaListaComprobantes(Convert.ToInt32(hdIdSol.Value));
            }
        }

        private void llenaCatalogos()
        {
            String IdEmpresa = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
            List<cpplib.valorTexto> lstCodPago = (new cpplib.admCatCondPago()).DaComboCondicionPago(IdEmpresa);
            List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(IdEmpresa);

            dpCondPago.DataSource = lstCodPago;
            dpCondPago.DataValueField = "Valor";
            dpCondPago.DataTextField = "Texto";
            dpCondPago.DataBind();

            dpProyecto.DataSource = lstProyectos;
            dpProyecto.DataValueField = "Valor";
            dpProyecto.DataTextField = "Texto";
            dpProyecto.DataBind();

        }

        private void llenaSolicitud(int IdSol)
        {
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud osol = admSol.carga(IdSol);
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

            txFactura.Text = osol.Factura;
            txFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            txImporte.Text = osol.Importe.ToString();
            txConcepto.Text = osol.Concepto;
            dpCondPago.Text = osol.CondicionPago;
            dpProyecto.Text = osol.Proyecto;
            txDecProyecto.Text = osol.DescProyecto;
            dpTpMoneda.SelectedValue = osol.Moneda;

            if (osol.Estado.Equals(cpplib.Solicitud.solEstado.Rechazada)) { lbMotivoRechazo.Text = admSol.DaRechazoSolicitud(IdSol); pnRechazo.Visible = true; btnAregarFactura.Visible = false; }
            if (!osol.Estado.Equals(cpplib.Solicitud.solEstado.Solicitud)) { btnActualizar.Visible = false; pnPopActlz.Visible = false; }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) {
            if (Request.Params["bk"] != null)
            {
                string regreso = Request.Params["bk"] + ".aspx";
                Response.Redirect(regreso);
            }
            else { Response.Redirect("espera.aspx"); }
        }

        private void llenaListaComprobantes(int IdSol)
        {
            List<cpplib.Archivo> Lista = (new cpplib.admArchivos()).ListaComprobantes(IdSol);
            if (Lista.Count > 0){
                rptComprobantes.DataSource = Lista;
                rptComprobantes.DataBind();
                foreach (cpplib.Archivo reg in Lista) { if (!string.IsNullOrEmpty(reg.Nota)) { lbNotaPago.Text = "\n * " + reg.Nota; } }
            }else{
                rptComprobantes.DataSource = null;
                rptComprobantes.DataBind();
                pnPagos.Visible = false;
            }
        }

        protected void rptComprobantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                //Response.Redirect("trf_VerComprobante.aspx?id=" + hdIdSol.Value + "&IdDoc=" + e.CommandArgument.ToString() + "&bk=trf_VerSolSinFactura");
                cpplib.Archivo oArchivo = (new cpplib.admArchivos()).cargaComprobante(Convert.ToInt32(hdIdSol.Value), Convert.ToInt32(e.CommandArgument.ToString()));
                cpplib.Solicitud oSol = (new cpplib.admSolicitud()).carga(Convert.ToInt32(hdIdSol.Value));
                String Carpeta = (new cpplib.admDirectorio()).DadirectorioArchivo(oSol.FechaFactura);
                String Archivo = Carpeta + oArchivo.ArchivoDestino;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxp_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
                pnlDocumento.Visible = true;
            }
        }

        protected void btnAceptaCambio_Click(object sender, EventArgs e)
        {
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSolicitud = recuperaDatos();
            admSol.ModificaSolSinFactura(oSolicitud);
            llenaSolicitud(oSolicitud.IdSolicitud);
        }

        private cpplib.Solicitud recuperaDatos()
        {
            cpplib.Solicitud oSol = new cpplib.Solicitud();
            oSol.IdSolicitud = Convert.ToInt32(hdIdSol.Value);

            oSol.Factura = txFactura.Text;
            oSol.FechaFactura = Convert.ToDateTime(txFhFactura.Text);
            oSol.Importe = Convert.ToDecimal(txImporte.Text);
            oSol.CantidadPagar = Convert.ToDecimal(txImporte.Text);
            oSol.Concepto = txConcepto.Text;

            oSol.CondicionPago = dpCondPago.SelectedValue;
            oSol.Proyecto = dpProyecto.SelectedValue;
            if (txDecProyecto.Text.Length > 128) { oSol.DescProyecto = txDecProyecto.Text.Substring(0, 127); }
            else { oSol.DescProyecto = txDecProyecto.Text; }
            oSol.Moneda = dpTpMoneda.SelectedValue;
            return oSol;
        }

        protected void btnAregarFactura_Click(object sender, EventArgs e)
        {
            Response.Redirect("trf_AgregaArchivosSinFactura.aspx?Id=" + hdIdSol.Value);
        }

    }
}