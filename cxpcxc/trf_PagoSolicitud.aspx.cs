using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_PagoSolicitud : Utilerias.Comun
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

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesPagadas.aspx");}

        private void llenaSolicitud(int IdSol)
        {
            cpplib.Solicitud osol = comun.admsolicitud.carga(IdSol);
            lbBeneficiario.Text = osol.Proveedor;
            lbBanco.Text = osol.Banco;
            lbCuenta.Text = osol.Cuenta;
            lbClabe.Text = osol.CtaClabe;
            lbSucursal.Text = osol.Sucursal;
            lbFactura.Text = osol.Factura;
            lbFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbImporte.Text = osol.Importe.ToString();
            lbCodPago.Text = osol.CondicionPago;
            lbConcepto.Text = osol.Concepto;
            lbProyecto.Text = osol.Proyecto;
            lbDecProyecto.Text = osol.DescProyecto;
            lbMoneda.Text = osol.Moneda;
            this.llenaListaComprobantes(IdSol);
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.SI) { VerFactura(osol.IdSolicitud); } else { ltDocumento.Text = "LA FACTURA NO HA SIDO AGREGADA"; }
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.NO) { btnFactura.Visible = false; }

        }

        private void llenaListaComprobantes(int IdSol)
        {
            //cpplib.admArchivos admCp = new cpplib.admArchivos();
            hdTotalPagos.Value= comun.admarchivos.DaImporteTotalComprobantes(IdSol).ToString();
            List<cpplib.Archivo> Lista = comun.admarchivos.ListaComprobantes(IdSol);
            if (Lista.Count > 0)
            {
                //rptComprobantes.DataSource = Lista;
                //rptComprobantes.DataBind();
                LlenarControles.LlenarRepeater(ref rptComprobantes, Lista);
                foreach (cpplib.Archivo reg in Lista) 
                { 
                    if (!string.IsNullOrEmpty(reg.Nota)) 
                    { 
                        lbNotaPago.Text = "\n " + reg.Nota; 
                    } 
                }
            }
            else
            {
                rptComprobantes.DataSource = null;
                rptComprobantes.DataBind();
                pnPagos.Visible = false;
            }
        }

        private void RegistraBitacora(cpplib.Solicitud.solEstado pEstado,decimal Cantidad )
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = Convert.ToInt32(hdIdSol.Value);
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Importe = Cantidad;
            oBitacora.Estado = pEstado;

            bool Resultado = comun.admbitacorasolicitud.Registrar(oBitacora);
        }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e){VerFactura(Convert.ToInt32(hdIdSol.Value));}

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
           String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime (lbFhFactura.Text));
           String Archivo = Carpeta + oArchivo.ArchivoDestino;
           PintaImagen(Archivo);
       }

       private void VerComprobante(int IdSolicitud, int IdDoc)
       {
           cpplib.Archivo oArchivo = comun.admarchivos.cargaComprobante(IdSolicitud, IdDoc);
           String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(lbFhFactura.Text));
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
       private bool ValidaArchivos() {
           bool Resultado = (fulComprobante.PostedFiles.Count > 0);
           foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                string ext = System.IO.Path.GetExtension(postedFile.FileName);
                string NombreArchivo = System.IO.Path.GetFileName(postedFile.FileName);
                if (!((NombreArchivo.Length <= 64) && (ext.ToUpper().Equals(".PDF"))))
                {
                    ltMsg.Text = "El o los Archivos no cumplen con las especificaciones (nombre menor a 64, y del tipo PDF )";
                    Resultado = Resultado && false;
                }
            }
           return Resultado ;
       }

       private bool CopiaArchivosDestinoYExtraedatos(List<cpplib.Archivo> Lista)
       {
            bool Resultado =true ;
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            //cpplib.admDirectorio admDir = new cpplib.admDirectorio();
            String CarpetaUbicacion = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(lbFhFactura.Text));
            bool ValidaDir = comun.admdirectorio.ValidaDirectorio(RutaDestino + CarpetaUbicacion);
           
            int IdDocto = comun.admarchivos.daNumeroComprobante(Convert.ToInt32(hdIdSol.Value));

            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                cpplib.Archivo oComp = new cpplib.Archivo();
                String Ext = System.IO.Path.GetExtension(postedFile.FileName);
                oComp.IdSolicitud = Convert.ToInt32(hdIdSol.Value);
                oComp.Tipo = cpplib.TipoArchivo.Comprobante;
                oComp.IdDocumento = IdDocto;
                oComp.ArchvioOrigen = System.IO.Path.GetFileName(postedFile.FileName);
                oComp.ArchivoDestino = hdIdSol.Value.PadLeft(6, '0') + "_D" + IdDocto.ToString() + "_" + lbFactura.Text.PadLeft(6, '0') + Ext;

                String Archivo = RutaDestino + CarpetaUbicacion + oComp.ArchivoDestino;
                postedFile.SaveAs(Archivo);
                if (!System.IO.File.Exists(Archivo)) { Resultado = Resultado && false; }
                
                Lista.Add(oComp);

                IdDocto += 1;
            }

            return Resultado;
       }

       private void InsertaListArchivos(List<cpplib.Archivo> Lista)
       {
           cpplib.admArchivos admComp = new cpplib.admArchivos();
            foreach(cpplib.Archivo oComp  in Lista) 
            {
                admComp.Agrega(oComp);
            }
       }

       protected void btnAnexaComprobante_Click(object sender, EventArgs e)
       {
            if (ValidaArchivos())
            {
                List<cpplib.Archivo> Lista = new List<cpplib.Archivo>();
                if (CopiaArchivosDestinoYExtraedatos(Lista))
                {
                    InsertaListArchivos(Lista);
                    this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
                }
            }
       }
    }
}