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
    public partial class trf_VerSolicitud : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdSol.Value =Request.Params["Id"].ToString ();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
                this.llenaListaComprobantes(Convert.ToInt32(hdIdSol.Value));
                this.llenaNotasCreditoAsignadas(Convert.ToInt32(hdIdSol.Value));
            }
        }

        private void llenaSolicitud(int IdSol)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            //cpplib.admSolicitud admSol =new cpplib.admSolicitud();
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

            if (osol.Estado.Equals(cpplib.Solicitud.solEstado.Rechazada)) 
            { 
                lbMotivoRechazo.Text = comun.admsolicitud.DaRechazoSolicitud(IdSol); 
                pnRechazo.Visible = true; 
            }
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.SI) 
                VerFactura(osol.IdSolicitud); 
            else 
                ltDocumento.Text = "LA FACTURA NO HA SIDO AGREGADA"; 
            btnFactura.Visible = osol.ConFactura.Equals(cpplib.Solicitud.enConFactura.SI);
            btnAregarFactura.Visible = osol.ConFactura.Equals(cpplib.Solicitud.enConFactura.NO) && (osol.UnidadNegocio == oCrd.UnidadNegocio || osol.IdUsr == oCrd.IdUsr);  
            brnImgEliminar.Visible = osol.Estado.Equals(cpplib.Solicitud.solEstado.Solicitud)  && (osol.UnidadNegocio == oCrd.UnidadNegocio || osol.IdUsr == oCrd.IdUsr);
            btnImgModificar.Visible = osol.Estado.Equals(cpplib.Solicitud.solEstado.Solicitud) && osol.ConFactura.Equals(cpplib.Solicitud.enConFactura.NO) && (osol.UnidadNegocio == oCrd.UnidadNegocio || osol.IdUsr == oCrd.IdUsr);
            imgbtnNotacredito.Visible = (osol.Estado.Equals(cpplib.Solicitud.solEstado.Solicitud) || osol.Estado.Equals(cpplib.Solicitud.solEstado.PagoParcial)) && (osol.UnidadNegocio == oCrd.UnidadNegocio || osol.IdUsr == oCrd.IdUsr);

            if (imgbtnNotacredito.Visible) 
                this.llenaNotasCreditoDisponibles(osol.IdEmpresa, osol.Rfc);
        }
                       
        protected void BtnCerrar_Click(object sender, EventArgs e) { this.Regresar(); }

        private void Regresar() {

            if (Request.Params["bk"] != null){Response.Redirect(Request.Params["bk"] + ".aspx");}
            else { Response.Redirect("espera.aspx"); }
        }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e)
        {
            VerFactura(Convert.ToInt32(hdIdSol.Value));
        }

        private void llenaListaComprobantes(int IdSol)
        {
            List<cpplib.Archivo> Lista = comun.admarchivos.ListaComprobantes(IdSol);
            if (Lista.Count > 0)
            {
                //rptComprobantes.DataSource = Lista;
                //rptComprobantes.DataBind();
                LlenarControles.LlenarRepeater(ref rptComprobantes, Lista);
                foreach (cpplib.Archivo reg in Lista) 
                { 
                    if (!string.IsNullOrEmpty(reg.Nota)) 
                    lbNotaPago.Text = "\n -" + reg.Nota;
                }
            }
            else
            {
                rptComprobantes.DataSource = null;
                rptComprobantes.DataBind();
                pnPagos.Visible = false;
            }
        }

        protected void rptComprobantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
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

        private void PintaImagen(String Archivo) {
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void btnImgModificar_Click(object sender, ImageClickEventArgs e) { Response.Redirect("trf_MoficarSolicitud.aspx?Id=" + hdIdSol.Value); }
        
        protected void brnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if(comun.admsolicitud.EliminarSolicitud(Convert.ToInt32 (hdIdSol .Value)))
                this.Regresar();
        }

        protected void btnAregarFactura_Click(object sender, EventArgs e)
        {
            Response.Redirect("trf_AgregaArchivosSinFactura.aspx?Id=" + hdIdSol.Value);
        }

        private void llenaNotasCreditoAsignadas(int IdSolicitud)
        {
            
            DataTable  Lista = comun.admcxpnotacredito.DaNotaCreditoAsignadasSolicitud(IdSolicitud);            
            if (Lista.Rows.Count > 0)
            {
                //rpNotaCreditoAsignada.DataSource = Lista;
                //rpNotaCreditoAsignada.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rpNotaCreditoAsignada, Lista);
                pnNotaCreditoAsignada.Visible  = true;
            }
        }

        private void llenaNotasCreditoDisponibles(int IdEmpresa, string Rfc)
        {
            List<cpplib.cxpNotaCredito> Lista = comun.admcxpnotacredito.listaNotasCreditoProveedor (IdEmpresa,Rfc);
            if (Lista.Count > 0)
            {
                //rpNotaCreditoDisponibles.DataSource = Lista;
                //rpNotaCreditoDisponibles.DataBind();
                LlenarControles.LlenarRepeater(ref rpNotaCreditoDisponibles, Lista);
            }
            else imgbtnNotacredito.Visible = false;
        }

        protected void rpNotaCreditoDisponibles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Asignar"))
            {
                cpplib .credencial  oCrd=(cpplib .credencial )Session ["credencial"]; 
                //cpplib.admCxpNotaCredito admNc=  new cpplib.admCxpNotaCredito();
                //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                
                cpplib.cxpNotaCredito oNC = comun.admcxpnotacredito.carga(Convert.ToInt32(e.CommandArgument.ToString()));
                cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(hdIdSol.Value));

                if (oSol.CantidadPagar > 0) {
                    if (oSol.Moneda.Equals(oNC.Moneda))
                    {
                        decimal Total = oSol.CantidadPagar - oNC.ImportePendiente;
                        if (Total > 0)
                        {
                            comun.admcxpnotacredito.AsignarNotaCredito(oNC.IdNotaCredito, oSol.IdSolicitud, oNC.Importe, oCrd.IdUsr);
                            comun.admcxpnotacredito.CambiaEstadoYMonto(oNC.IdNotaCredito, cpplib.cxpNotaCredito.enEstado.Asignada, 0);
                            comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, oSol.Estado, Total);
                        }
                        else
                        {
                            Total = (Total * -1);
                            comun.admcxpnotacredito.AsignarNotaCredito(oNC.IdNotaCredito, oSol.IdSolicitud, (oNC.Importe - Total), oCrd.IdUsr);
                            comun.admcxpnotacredito.CambiaEstadoYMonto(oNC.IdNotaCredito, cpplib.cxpNotaCredito.enEstado.Parcial, Total);
                            comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, cpplib.Solicitud.solEstado.Pagado, 0);
                        }
                        this.llenaNotasCreditoAsignadas(oSol.IdSolicitud);
                    }
                    else 
                        ltMsg.Text = "Hay una diferencia en el tipo de moneda para aplicar la nota de credito.";
                }
            }
        }

        protected void rpNotaCreditoAsignada_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("verNota"))
            {
                int IdNotaCredito = Convert .ToInt32 (e.CommandArgument .ToString());
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