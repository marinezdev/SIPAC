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
    public partial class cxc_VerOrdenServicio : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                llenadatos(); 
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("cxc_ConsultaOrdServicio.aspx"); }

        private void llenadatos()
        {
            int IdOrden = Convert.ToInt32(Request.Params["ord"].ToString());
            cpplib.OrdenServicio  orSrv = comun.admordenservicio.carga(IdOrden);
            lbOrdServicio.Text = orSrv.IdServicio.ToString();
            lbCliente.Text = orSrv.Cliente;
            lbEmpresa.Text = orSrv.Empresa;
            lbFhInicio.Text = orSrv.FechaInicio .ToString("dd/MM/yyyy");
            lbFhFin.Text = orSrv.FechaTermino.ToString("dd/MM/yyyy");
            lbPeriodos.Text = orSrv.Periodos.ToString ();
            lbTpSolicitud.Text = orSrv.TipoSolicitud.ToString();
            lbCodPago.Text = orSrv.CondicionPago;
            lbMoneda.Text = orSrv.TipoMoneda;
            lkServicio.Text = orSrv.Servicio;
            lkServicio .CommandArgument =orSrv.IdCatServicio.ToString ();  
            lbDescripcion.Text = orSrv.Descripcion;
            chkEspecial.Checked = Convert.ToBoolean(orSrv.Especial);

            if (orSrv.TipoPeriodo == 1) rbMes.Checked = true;
            if (orSrv.TipoPeriodo == 2) rdBimestral.Checked = true;
            if (orSrv.TipoPeriodo == 6) rdSemestral.Checked = true;
            if (orSrv.TipoPeriodo == 12) rdAnual.Checked = true;
            
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturasXIdServicio(orSrv.IdServicio);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
            }
        }

        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                if (ordFac.Factura == 1)
                {
                    ImageButton imgfact = (ImageButton)e.Item.FindControl("imgbtnVerFac");
                    imgfact.Enabled = true; imgfact.ImageUrl = "~/img/verFac.png";
                }

                //Coloca el semaforo
                DateTime FchActual = DateTime.Now;
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Pagado)
                {
                    ImageButton imgCmpg = (ImageButton)e.Item.FindControl("imgbtnCompPago");
                    imgCmpg.Enabled = true;
                    imgCmpg.ImageUrl = "~/img/pago.png";

                    img.ImageUrl = "~/img/action_check.png";
                }
                else if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado) { img.ImageUrl = "~/img/cancelar.png"; }
                else if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.En_Cobro)
                {
                    DateTime FhVencimiento = ordFac.FechaInicio.AddDays(ordFac.CondicionPagoDias);
                    int Dias = Convert.ToInt32((FhVencimiento - FchActual).TotalDays);
                    if ((Dias <= 1) && (ordFac.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
                else
                {
                    int Dias = Convert.ToInt32((ordFac.FechaFactura - FchActual).TotalDays);
                    if ((Dias <= 1) && (ordFac.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
            }
        }

        
 #region SECCION  PARA MOSTRAR LA IMAGEN DE A FACTURA
        protected void imgbtnVerFac_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            int Id = Convert.ToInt32(wLink.CommandArgument.ToString());
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaFactura(Id);
            MuestraArchivo(Id, oArchivo.ArchivoDestino);
            pnpaginas.Visible = false;
            pnFacAsociadas.Visible = false;            
            mpePopDocumento.Show();
        }

        private void MuestraArchivo(int IdOrdFactura, string ArchivoMostrar)
        {
            cpplib.OrdenFactura oFact = comun.admordenfactura.carga(IdOrdFactura);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(oFact.FechaInicio));
            if (!String.IsNullOrEmpty(ArchivoMostrar))
            {
                String Archivo = Carpeta + ArchivoMostrar;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxc_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
            }
            else { ltDocumento.Text = "<embed src='\\img\\SinComprobante.pdf' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />"; }
        }
        

    #endregion

    #region SECCION  PARA MOSTRAR COMPRABANTE PAGO

        protected void imgbtnCompPago_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            hdIdOrdFactura.Value = wLink.CommandArgument.ToString();
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaDocumentoPago(Convert.ToInt32(hdIdOrdFactura.Value), 1);
            listadePaginas(Convert.ToInt32(hdIdOrdFactura.Value));
            MuestraArchivo(Convert.ToInt32(hdIdOrdFactura.Value), oArchivo.ArchivoDestino);
            FacturasAsociadas(Convert.ToInt32(hdIdOrdFactura.Value));
            mpePopDocumento.Show();
        }

        private void listadePaginas(int IdOrdFactura)
        {
            List<cpplib.cxcArchivo> Lista = comun.admarchivoscxc.ListaComprobantes(IdOrdFactura);
            if (Lista.Count > 1)
            {
                rpPaginas.DataSource = Lista;
                rpPaginas.DataBind();
                pnpaginas.Visible = true;
            }
            else {
                pnpaginas.Visible = false;
            }
        }

        private void FacturasAsociadas(int IdOrdFactura)
        {
            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            int Idgrupo = comun.admordenfactura.ExisteGrupo(IdOrdFactura);
            if (Idgrupo > 0)
            {
                List<cpplib.OrdenFactura> lista = comun.admordenfactura.DaGrupoFacturasPagadas(Idgrupo, IdOrdFactura);
                rpFacAsociadas.DataSource = lista;
                rpFacAsociadas.DataBind();
                lbTotFacAsoc.Text = lista.Sum(sol => sol.Importe).ToString("C2");
                pnFacAsociadas.Visible = true;
            }
            else
            {
                rpFacAsociadas.DataSource = null;
                rpFacAsociadas.DataBind();
                pnFacAsociadas.Visible = false;
            }
        }

        #endregion

        protected void rpPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Docto")
            {
                cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaDocumentoPago(Convert.ToInt32(hdIdOrdFactura.Value), Convert.ToInt32(e.CommandArgument.ToString()));
                MuestraArchivo(Convert.ToInt32(hdIdOrdFactura.Value), oArchivo.ArchivoDestino);
                FacturasAsociadas(Convert.ToInt32(hdIdOrdFactura.Value));
                mpePopDocumento.Show();
            }
        }

        protected void lkServicio_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(lkServicio.CommandArgument.ToString());
            string Archivo = comun.admcatservicios.DaNombreImagen(Id);
            if (!String.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\img\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                pnpaginas.Visible = false;
                pnFacAsociadas.Visible = false;
                mpePopDocumento.Show();
            }
        }
        
    }
}