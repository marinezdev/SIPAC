using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class cxc_VerOrdenFactura : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                llenadatos();
                this.txFhFactura.Attributes.Add("readonly", "true");
                this.txFhCompromisoPago.Attributes.Add("readonly", "true");
            }
        }

        
        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (Request.Params["bk"] != null)
            {
                string regreso = Request.Params["bk"] + ".aspx";
                Response.Redirect(regreso);
            }
            else{Response.Redirect("espera.aspx");}
        }

        private void llenadatos()
        {
            ltMsg.Text = "";
            int IdOrden = Convert.ToInt32(Request.Params["ord"].ToString());
            cpplib.OrdenFactura orfac = comun.admordenfactura.carga(IdOrden);
            lbOrdServicio.Text = orfac.IdServicio.ToString();
            lbOrdFactura.Text = orfac.IdOrdenFactura.ToString();
            lbCliente.Text = orfac.Cliente;
            lbEmpresa.Text = orfac.Empresa;
            lbFhFactura.Text = orfac.FechaFactura.ToString("dd/MM/yyyy");
            txFhFactura.Text = orfac.FechaFactura.ToString("dd/MM/yyyy");
            lbNoFactura .Text = orfac.NumFactura;
            lbTpSolicitud.Text = orfac.TipoSolicitud.ToString();
            lbImporte.Text = orfac.Importe.ToString("C2");
            txMonto.Text = orfac.Importe.ToString();
            lbCodPago.Text = orfac.CondicionPago;
            lbMoneda.Text = orfac.TipoMoneda;
            lbDescripcion.Text = orfac.Descripcion; 
            lbAnotaciones.Text = orfac.Anotaciones;
            chkEspecial.Checked = Convert.ToBoolean(orfac.Especial);
            lbCompPago.Text = Convert.ToDateTime(orfac.FhCompromisoPago).ToString("dd/MM/yyyy");
            ce_FhCompromisoPago.StartDate = orfac.FechaFactura;

            if (orfac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Solicitud))
            {
                pnbtnModificacion.Visible = true;
            }
            else { pnbtnModificacion.Visible = false; }

            if (orfac.Factura == 0) { imgBtDocumento.Visible = false; }
            llenaListaComprobantes(IdOrden);
        }

        protected void btnGuardarModif_Click(object sender, EventArgs e)
        {
             ltMsg.Text = ""; 
            decimal Monto = Convert.ToDecimal(txMonto.Text);
            if (Monto > 0)
            {
                //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
                cpplib.admPartidasFactura admPartidas = new cpplib.admPartidasFactura();
                comun.admordenfactura.ActualizaDatosFacturacion(lbOrdFactura.Text, "", txFhFactura.Text, Monto, cpplib.OrdenFactura.EstadoOrdFac.Solicitud, 0);
                llenadatos();
            }
            else { ltMsg.Text = "El monto no es valido"; }
            
         }

        protected void btnEnviarFacturacion_Click(object sender, EventArgs e)
        {
            //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
            cpplib.OrdenFactura orfac = comun.admordenfactura.carga(Convert.ToInt32(lbOrdFactura.Text));

            if (orfac.Importe != 0)
            {
                if (orfac.Especial == 1)
                {
                    comun.admordenfactura.CambiaEstadoOrdenFactura(orfac.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro);
                    RegistraBitacora(orfac.IdServicio, orfac.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.En_Cobro);
                }
                else
                {
                    comun.admordenfactura.CambiaEstadoOrdenFactura(orfac.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura);
                    RegistraBitacora(orfac.IdServicio, orfac.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura);
                    csGeneral admG = new csGeneral();
                    admG.EnviaCorreoOrdenFacturacion(orfac);
                }
                llenadatos();
            }
            else { ltMsg.Text = "El monto de la factura esta en ceros, no se puede enviar a facturar!"; }
         }

        protected void btnCierraDocumento_Click(object sender, EventArgs e){mtvContenedor.ActiveViewIndex = 0;}

        protected void imgBtDocumento_Click(object sender, ImageClickEventArgs e)
        {
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaFactura(Convert.ToInt32(lbOrdFactura.Text));
            PintaDocumento(oArchivo);
            mtvContenedor.ActiveViewIndex = 1;
        }

        protected void rptPagos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("verpago"))
            {
               cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaComprobante(Convert.ToInt32(lbOrdFactura.Text), Convert.ToInt32(e.CommandArgument.ToString()));
                PintaDocumento(oArchivo);
                FacturasAsociadas(Convert.ToInt32(lbOrdFactura.Text));
                mtvContenedor.ActiveViewIndex = 1;
            }
        }
        
        private void PintaDocumento(cpplib.cxcArchivo oArchivo)
        {
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(lbFhFactura.Text));
            if (!String.IsNullOrEmpty(oArchivo.ArchivoDestino))
            {
                String Archivo = Carpeta + oArchivo.ArchivoDestino;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxc_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
            }else {
                if (oArchivo.Tipo==cpplib.cxcTipoArchivo.Comprobante) { ltDocumento.Text = "<embed src='\\img\\SinComprobante.pdf' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />"; }
            }
        }

        // Muestra los comprobantes de pago
        private void llenaListaComprobantes(int IdOrdenFactura)
        {
            List<cpplib.cxcArchivo> Lista = comun.admarchivoscxc.ListaComprobantes(IdOrdenFactura);
            if (Lista.Count > 0)
            {
                //rptPagos.DataSource =  Lista;
                //rptPagos.DataBind();
                LlenarControles.LlenarRepeater(ref rptPagos, Lista);
            }
            else
                pnPagos.Visible = false;
        }

        // Muestra las facturas que comparten el comprobante de pago
        private void FacturasAsociadas(int IdOrdFactura)
        {
            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            int Idgrupo = comun.admordenfactura.ExisteGrupo(IdOrdFactura);
            if (Idgrupo > 0)
            {
                List<cpplib.OrdenFactura> lista = comun.admordenfactura.DaGrupoFacturasPagadas(Idgrupo, IdOrdFactura);
                //rpFacAsociadas.DataSource = lista;
                //rpFacAsociadas.DataBind();
                LlenarControles.LlenarRepeater(ref rpFacAsociadas, lista);
                pnFacAsociadas.Visible = true;
            }
            else
            {
                rpFacAsociadas.DataSource = null;
                rpFacAsociadas.DataBind();
                pnFacAsociadas.Visible = false;
            }
        }

        private void RegistraBitacora(int IdServicio, int IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac Estado)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.cxcBitacora oBitacora = new cpplib.cxcBitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = oCrd.IdUsr;
            oBitacora.Nombre = oCrd.Nombre;
            oBitacora.Estado = Estado;

            bool Resultado = comun.admcxcbitacora.Registrar(oBitacora);
        }
                
        protected void btnAtzFhCompromisoPago_Click(object sender, EventArgs e)
        {
            cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
            adm.ActualizaFechaCompromisoPago(lbOrdServicio.Text, lbOrdFactura.Text , txFhCompromisoPago.Text);
            lbCompPago.Text = txFhCompromisoPago.Text;
        }
                            
    }
}