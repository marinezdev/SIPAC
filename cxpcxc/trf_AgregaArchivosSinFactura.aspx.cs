using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace cxpcxc
{
    public partial class trf_AgregaArchivosSinFactura : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
                hdIdSol.Value = Request.Params["Id"].ToString();
        }
        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("trf_VerSolicitud.aspx?Id=" + hdIdSol.Value); }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";

            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String ArhPdf = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            String ArhXml = RutaDestino + oCredencial.IdUsr.ToString() + "_X.XML";
            string VerArchivo = "\\cxp_doc\\" + oCredencial.IdUsr.ToString() + "_F.PDF";

            if (CargaArchivos(ArhPdf, ArhXml))
            {
                if (this.ExtraerDatosXML(ArhXml))
                {
                    LlenaDatosSolicitud();
                    if (validaInformacion())  
                    {
                        pnCargaArchivo.Visible = false;
                        btnAceptar.Visible = true;
                    }
                    PintaFactura(VerArchivo);
                }
            }
        }

        private void LlenaDatosSolicitud() {
            //cpplib.admSolicitud adm = new cpplib.admSolicitud();
            cpplib.Solicitud osol = comun.admsolicitud.carga(Convert.ToInt32(hdIdSol.Value));
            lbOrgProveedor.Text = osol.Proveedor;
            lbOrgRfc.Text = osol.Rfc;
            bOrgFactura.Text = osol.Factura;
            lbOrgFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbOrgImporte.Text = Convert.ToInt32 (osol.Importe).ToString();
            lbOrgConcepto.Text = osol.Concepto;
        }

        private bool validaInformacion()
        {
            bool resultado = true;
            
            resultado = resultado && lbOrgRfc.Text.Equals(lbRfc.Text);
            //resultado = resultado && lbOrgImporte.Text.Equals(lbImporte.Text);
            if (!resultado) { ltMsg.Text = "Los datos de la factura no corresponden con la solicitud (RFC,IMPORTE)"; }
            if (resultado) {
                //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                if (comun.admsolicitud.Existe(hdLLaveSol.Value.ToString())) 
                { 
                    ltMsg.Text = "La factura ya esta registrada "; 
                    resultado = false; 
                }
            }
            return resultado;
        }

        private bool CargaArchivos(String ArhPdf, String ArhXml)
        {
            bool resultado = false;
            try
            {
                if (fulFactura.HasFile && fulXml.HasFile)
                {
                    if (Path.GetFileNameWithoutExtension(fulFactura.FileName) == Path.GetFileNameWithoutExtension(fulXml.FileName))
                    {
                        if (Path.GetExtension(fulFactura.FileName).ToUpper().Equals(".PDF") && Path.GetExtension(fulXml.FileName).ToUpper().Equals(".XML"))
                        {
                            hdNomArchivo.Value = Path.GetFileNameWithoutExtension(fulFactura.FileName);
                            this.EliminaArchivosAnteriores(ArhPdf, ArhXml);

                            fulFactura.SaveAs(ArhPdf);
                            fulXml.SaveAs(ArhXml);
                            if (File.Exists(ArhPdf) && File.Exists(ArhXml))
                            {
                                resultado = true;
                            }
                            else { ltMsg.Text = "Los archivos no se cargaron, intente nuevamente"; }
                        }
                        else { ltMsg.Text = "Los archivos deben ser PDF y XML"; }
                    }
                    else { ltMsg.Text = "El nombre de los archivos debe ser igual"; }
                }
                else { ltMsg.Text = "Agregue los archivos"; }
            }
            catch (Exception ex) { ltMsg.Text = ex.Message.ToString(); }
            return resultado;
        }

        private void EliminaArchivosAnteriores(String ArhPdf, String ArhXml)
        {
            if (File.Exists(ArhPdf)) { File.Delete(ArhPdf); }
            if (File.Exists(ArhXml)) { File.Delete(ArhXml); }
        }

        private bool ExtraerDatosXML(String ArhXml)
        {
            bool Resultado = false;
            
            cpplib.DatosXML Datos = (new cpplib.LeerXML()).ExtraerDatos(ArhXml);
            if (!string.IsNullOrEmpty(Datos.Rfc))
            {
                lbProveedor.Text = Datos.Nombre;
                lbRfc.Text = Datos.Rfc;
                lbFactura.Text = Datos.Folio;
                lbFhFactura.Text = Datos.Fecha.ToString("dd/MM/yyyy");
                lbImporte.Text = Convert.ToInt32(Datos.Total).ToString();
                lbConcepto.Text = Datos.Concepto;
                hdLLaveSol.Value = Datos.Sello.ToString();
                pnDatosXml.Visible = true;
                Resultado = true;
            }
            else { ltMsg.Text = "El archivo XML no es correcto o esta dañado"; }
            
            return Resultado;
        }

        
        private void PintaFactura(String pArchivo)
        {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            //cpplib.admSolicitud adm = new cpplib.admSolicitud();
            cpplib.Solicitud osol = comun.admsolicitud.carga(Convert.ToInt32(hdIdSol.Value));
            osol.Factura = lbFactura.Text;
            osol.Concepto = lbConcepto.Text;
            if (osol.Concepto.Length > 254) { osol.Concepto = osol.Concepto.Substring(0, 254); }
            osol.FechaFactura = vdFecha(osol.Estado, Convert.ToDateTime(lbOrgFhFactura.Text), Convert.ToDateTime(lbFhFactura.Text));
            
            if (GuardaDocumentos(osol, oCredencial.IdUsr))
            {
                comun.admsolicitud.ActualizaDatosDeSinFactura(osol);
                comun.admsolicitud.RegistrarLlave(osol.IdSolicitud.ToString(), hdLLaveSol.Value);
                Response.Redirect("trf_SolicitudesRegistro.aspx");
            }
        }

        private DateTime  vdFecha(cpplib.Solicitud.solEstado Estado, DateTime FechaSol, DateTime  FechaFactura) {
            DateTime resultado = FechaSol;

            if (Estado == cpplib.Solicitud.solEstado.Solicitud){resultado=FechaFactura ;}
            else {            
                if(FechaSol.Month.Equals(FechaFactura.Month) && FechaSol.Year.Equals(FechaFactura.Year)){resultado =FechaFactura ;}
            }
            return resultado;
        }
        
        private bool GuardaDocumentos(cpplib.Solicitud oSol, int IdUsr)
        {
            bool Resultado = false;
            String DirRaiz = Server.MapPath(@"cxp_doc\");
            string RutaFactura = (new cpplib.admDirectorio()).DaYCreaDirectorioArchivo(DirRaiz, oSol.FechaFactura);

            String ArhPdf = DirRaiz + IdUsr.ToString() + "_F.PDF";
            String ArhXml = DirRaiz + IdUsr.ToString() + "_X.XML";

            cpplib.admArchivos admArh = new cpplib.admArchivos();
            cpplib.Archivo oArchivo = new cpplib.Archivo();
            oArchivo.IdSolicitud = oSol.IdSolicitud;
            oArchivo.IdDocumento = 1;
            oArchivo.Tipo = cpplib.TipoArchivo.Factura;

            oArchivo.ArchvioOrigen = hdNomArchivo.Value  + ".PDF";
            oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_F" + "_" + oSol.Factura.PadLeft(6, '0') + ".PDF";

            //Guarda PDF
            String Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
            if (System.IO.File.Exists(Destino)) { System.IO.File.Delete(Destino); }
            System.IO.File.Copy(ArhPdf, Destino);
            if (System.IO.File.Exists(Destino))
            {
                if (admArh.Agrega(oArchivo))
                {
                    //Guarda XML
                    oArchivo.Tipo = cpplib.TipoArchivo.Xml;
                    oArchivo.ArchvioOrigen = hdNomArchivo.Value + ".XML";
                    oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_X" + "_" + oSol.Factura.PadLeft(6, '0') + ".XML";
                    Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
                    if (System.IO.File.Exists(Destino)) { System.IO.File.Delete(Destino); }
                    System.IO.File.Copy(ArhXml, Destino);
                    if (System.IO.File.Exists(Destino))
                    {
                        if (admArh.Agrega(oArchivo))
                        {
                            Resultado = true;
                        }
                    }
                }
            }
            return Resultado;
        }

    }
}