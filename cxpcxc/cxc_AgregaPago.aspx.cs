using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_AgregaPago : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){llenadatos();}
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("cxc_SeleccionarSolPago.aspx"); }
        
        private void llenadatos()
        {
            int IdOrden = Convert.ToInt32(Request.Params["ord"].ToString());
            cpplib.OrdenFactura orfac = comun.admordenfactura.carga(IdOrden); //(new cpplib.admOrdenFactura()).carga(IdOrden);
            lbOrdServicio.Text = orfac.IdServicio.ToString();
            lbOrdFactura.Text = orfac.IdOrdenFactura.ToString();
            lbCliente.Text = orfac.Cliente;
            lbEmpresa.Text = orfac.Empresa;
            lbFhFactura.Text = orfac.FechaInicio.ToString("dd/MM/yyyy");
            lbNoFactura.Text = orfac.NumFactura;
            lbTpSolicitud.Text = orfac.TipoSolicitud.ToString();
            lbImporte.Text = orfac.Importe.ToString("C2");
            lbCodPago.Text = orfac.CondicionPago;
            lbMoneda.Text = orfac.TipoMoneda;
            lbDescripcion.Text = orfac.Descripcion;
            lbAnotaciones.Text = orfac.Anotaciones;

            if (orfac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.En_Cobro) || orfac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Pagado))
            { 
                pnRegComprobante.Visible = true;
                if (orfac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { hdAnexarCop.Value = "1"; }
            }
                      
            if (orfac.Factura !=1) { imgBtDocumento.Visible = false; }
            llenaListaComprobantes(IdOrden);

        }

        protected void btnCierraDocumento_Click(object sender, EventArgs e)
        {
            mtvContenedor.ActiveViewIndex = 0;
        }

        protected void imgBtDocumento_Click(object sender, ImageClickEventArgs e)
        {
            mtvContenedor.ActiveViewIndex = 1;
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaFactura(int.Parse(lbOrdFactura.Text)); // (new cpplib.admArchivosCxc()).cargaFactura(Convert.ToInt32(lbOrdFactura.Text));
            PintaDocumento(oArchivo);
        }

        protected void btnRegPago_Click(object sender, EventArgs e)
        {
          if (ValidaArchivos())
            {
                List<cpplib.cxcArchivo> Lista = new List<cpplib.cxcArchivo>();
                if (CopiaArchivosDestinoYExtraedatos(Lista))
                {
                    cpplib.admOrdenFactura admOdFact = new cpplib.admOrdenFactura();
                    InsertaListArchivos(Lista);
                    if(!hdAnexarCop.Value.Equals ("1")){
                        comun.admordenfactura.CambiaEstadoOrdenFactura(lbOrdFactura.Text, cpplib.OrdenFactura.EstadoOrdFac.Pagado); //admOdFact.CambiaEstadoOrdenFactura(lbOrdFactura.Text, cpplib.OrdenFactura.EstadoOrdFac.Pagado);
                        RegistraBitacora(Convert.ToInt32 (lbOrdServicio.Text), Convert.ToInt32 (lbOrdFactura.Text), cpplib.OrdenFactura.EstadoOrdFac.Pagado);
                    }
                    llenadatos();
                }
            }
        }

        private bool CopiaArchivosDestinoYExtraedatos(List<cpplib.cxcArchivo> Lista)
        {
            bool Resultado = true;
            String RutaDestino = Server.MapPath(@"cxc_doc\");
            //cpplib.admDirectorio admDir = new cpplib.admDirectorio();
            cpplib.admArchivosCxc admComp = new cpplib.admArchivosCxc();
            String CarpetaUbicacion = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(lbFhFactura.Text));
            bool ValidaDir = comun.admdirectorio.ValidaDirectorio(RutaDestino + CarpetaUbicacion);

            int IdDocto = admComp.daNumeroComprobante(Convert.ToInt32(lbOrdFactura.Text));

            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                cpplib.cxcArchivo oArv = new cpplib.cxcArchivo();
                String Ext = System.IO.Path.GetExtension(postedFile.FileName);
                oArv.IdOrdenFactura = Convert.ToInt32(lbOrdFactura.Text);
                oArv.Tipo = cpplib.cxcTipoArchivo.Comprobante;
                oArv.IdDocumento = IdDocto;
                oArv.ArchvioOrigen = System.IO.Path.GetFileName(postedFile.FileName); 
                oArv.ArchivoDestino = lbOrdFactura.Text.PadLeft(6, '0') + "_D" + oArv.IdDocumento.ToString() + "_" + lbNoFactura.Text.PadLeft(6, '0') + System.IO.Path.GetExtension(fulComprobante.FileName);

                String Archivo = RutaDestino + CarpetaUbicacion + oArv.ArchivoDestino;
                postedFile.SaveAs(Archivo);
                if (!System.IO.File.Exists(Archivo)) { Resultado = Resultado && false; }

                Lista.Add(oArv);

                IdDocto += 1;
            }

            return Resultado;
        }

        private bool ValidaArchivos()
        {
            bool Resultado = (fulComprobante.PostedFiles.Count > 0);
            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                string ext = System.IO.Path.GetExtension(fulComprobante.FileName);
                if (!((postedFile.FileName.Length <= 64) && (ext.ToUpper().Equals(".PDF"))))
                {
                    ltMsg.Text = "El o los Archivos no cumplen con las especificaciones (nombre menor a 64, y del tipo PDF )";
                    Resultado = Resultado && false;
                }
            }
            return Resultado;
        }


        private void InsertaListArchivos(List<cpplib.cxcArchivo> Lista)
        {
            //cpplib.admArchivosCxc admComp = new cpplib.admArchivosCxc();
            foreach (cpplib.cxcArchivo oComp in Lista)
            {
                comun.admarchivoscxc.Agrega(oComp);
            }
        }

        private void RegistraBitacora(int IdServicio, int IdOrdenFactura,cpplib.OrdenFactura.EstadoOrdFac Estado)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.cxcBitacora oBitacora = new cpplib.cxcBitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = oCrd.IdUsr;
            oBitacora.Nombre = oCrd.Nombre;
            oBitacora.Estado = Estado;

            bool Resultado = comun.admcxcbitacora.Registrar(oBitacora); //(new cpplib.admCxcBitacora()).Registrar(oBitacora);
        }

        protected void rptPagos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                mtvContenedor.ActiveViewIndex = 1;
                cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaComprobante(Convert.ToInt32(lbOrdFactura.Text), Convert.ToInt32(e.CommandArgument.ToString()));
                PintaDocumento(oArchivo);
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
            }
        }

        private void llenaListaComprobantes(int IdOrdenFactura)
        {
            List<cpplib.cxcArchivo> Lista = comun.admarchivoscxc.ListaComprobantes(IdOrdenFactura);
            if (Lista.Count > 0) 
            { 
                //rptPagos.DataSource = Lista; 
                //rptPagos.DataBind();
                LlenarControles.LlenarRepeater(ref rptPagos, Lista);
                pnPagos.Visible = true; 
            }
        }
    }
}