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
    public partial class trf_VerSolDireccion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdSol.Value = Request.Params["Id"].ToString();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
                this.llenaBitacora(Convert.ToInt32(hdIdSol.Value));
                this.llenaListaComprobantes(Convert.ToInt32(hdIdSol.Value));
                this.llenaNotasCreditoAsignadas(Convert.ToInt32(hdIdSol.Value));
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesDireccion.aspx?Id=" + hdIdEmpresa .Value); }

        private void llenaSolicitud(int IdSol)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud osol = comun.admsolicitud.carga(IdSol);
            hdIdEmpresa.Value = osol.IdEmpresa.ToString();
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

            if (osol.ConFactura == cpplib.Solicitud.enConFactura.SI) { VerFactura(osol.IdSolicitud); } else { btnFactura.Visible = false; ltDocumento.Text = "NO SE HA REGISTRADO LA FACTURA"; }
            if (osol.Estado.Equals(cpplib.Solicitud.solEstado.Rechazada)) { lbMotivoRechazo.Text = comun.admsolicitud.DaRechazoSolicitud(IdSol); pnRechazo.Visible = true; }
        }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e)
        {
            VerFactura(Convert.ToInt32(hdIdSol.Value));
        }

        private void llenaListaComprobantes(int IdSol)
        {
            List<cpplib.Archivo > Lista = comun.admarchivos.ListaComprobantes(IdSol);
            if (Lista.Count > 0)
            {
                //rptComprobantes.DataSource = Lista;
                //rptComprobantes.DataBind();
                LlenarControles.LlenarRepeater(ref rptComprobantes, Lista);
                pnComprobantes.Visible =true;
            }
            else { pnComprobantes.Visible = false; }
        }

        private void llenaBitacora(int IdSol)
        {
            List<cpplib.Bitacora> Lista = comun.admbitacorasolicitud.daSeguimientoBitacora(IdSol);
            if (Lista.Count > 0)
            {
                //rpBitacora .DataSource = Lista;
                //rpBitacora.DataBind();
                LlenarControles.LlenarRepeater(ref rpBitacora, Lista);
            }
        }

        protected void rptComprobantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")){
                VerComprobante(Convert.ToInt32(hdIdSol.Value), Convert.ToInt32(e.CommandArgument.ToString()));
            }
        }

        private void VerFactura(int IdSolicitud)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaFactura(IdSolicitud);
            cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSolicitud));
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
            String Archivo = Carpeta + oArchivo.ArchivoDestino;
            PintaImagen(Archivo);
        }

        private void VerComprobante(int IdSolicitud, int IdDoc)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaComprobante(IdSolicitud, IdDoc);
            cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSolicitud));
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
            String Archivo = Carpeta + oArchivo.ArchivoDestino;
            PintaImagen(Archivo);
        }

        private void PintaImagen(String Archivo)
        {
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        private void llenaNotasCreditoAsignadas(int IdSolicitud)
        {
            DataTable  Lista = comun.admcxpnotacredito.DaNotaCreditoAsignadasSolicitud(IdSolicitud);
            if (Lista.Rows.Count > 0)
            {
                //rpNotaCreditoAsignada.DataSource = Lista;
                //rpNotaCreditoAsignada.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rpNotaCreditoAsignada, Lista);
                pnNotaCreditoAsignada.Visible = true;
            }
        }

        protected void rpNotaCreditoAsignada_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("verNota"))
            {
                int IdNotaCredito = Convert.ToInt32(e.CommandArgument.ToString());
                string Archivo = comun.admcxpnotacredito.DaNombreImageNota(IdNotaCredito);
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxp_doc\\NotasCredito\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
            }
        }
    }
}