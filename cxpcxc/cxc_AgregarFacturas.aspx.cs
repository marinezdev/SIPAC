using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace cxpcxc
{
    public partial class cxc_AgregarFacturas : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                string IdOrdenFact= Request.Params["Ord"].ToString ();
                llenaDatosOrden(IdOrdenFact);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("cxc_SolicitudesFacturacion.aspx"); }

        private void llenaDatosOrden(string IdOrdenFact)
        {
            cpplib.OrdenFactura oOrdFact = (new cpplib.admOrdenFactura()).carga(Convert.ToInt32(IdOrdenFact));
            lbOrdServicio.Text = oOrdFact.IdServicio.ToString();
            lbOrdenFactura.Text = oOrdFact.IdOrdenFactura.ToString();
            lbOrdEmpresa.Text = oOrdFact.Empresa;
            lbFechaInicio.Text = oOrdFact.FechaInicio.ToString("dd/MM/yyyy");
            lbOrdCliente.Text = oOrdFact.Cliente;
            lbOrdRfc.Text = oOrdFact.Rfc;
            lbOrdImporte.Text = oOrdFact.Importe.ToString("C2");
            lbOrdCodPago.Text = oOrdFact.CondicionPago;
            lbOrdTpSolicitud.Text = oOrdFact.TipoSolicitud.ToString ();
            lbOrdMoneda.Text = oOrdFact.TipoMoneda;
            lbOrdAnotaciones.Text = oOrdFact.Anotaciones;
            if (oOrdFact.EnviaCorreoClte == 1) { chkEvioCliente.Checked = true;}
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxc_doc\");
            hdRutaArchPdf.Value = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            hdRutaArchXML.Value = RutaDestino + oCredencial.IdUsr.ToString() + "_X.XML";
            string VerArchivo = "\\cxc_doc\\" + oCredencial.IdUsr.ToString() + "_F.PDF";
            
            if (CargaArchivos(hdRutaArchPdf.Value, hdRutaArchXML.Value))
            {
                if (this.ExtraerDatosXML(hdRutaArchXML.Value))
                {
                    PintaFactura(VerArchivo); 
                    if (!ExisteCFD())
                    {
                        if (ValidaRFC())
                        {
                            pnCargaArchivo.Visible = false;
                            btnRegistrar.Visible = true;
                        }
                    }
                }
            }
        }

        private bool CargaArchivos(String ArhPdf, String ArhXml)
        {
            bool resultado = false;
            try
            {
                if (fulFactura.HasFile && fulXml.HasFile)
                {
                    hdArchivoOrg.Value = Path.GetFileNameWithoutExtension(fulXml.FileName);
                    if (Path.GetFileNameWithoutExtension(fulFactura.FileName) == Path.GetFileNameWithoutExtension(fulXml.FileName))
                    {
                        if (fulFactura.FileName.Length <= 64)
                        {
                            if (Path.GetExtension(fulFactura.FileName).ToUpper().Equals(".PDF") && Path.GetExtension(fulXml.FileName).ToUpper().Equals(".XML"))
                            {
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
                        else { ltMsg.Text = "EL nombre del archivo es demasiado grande."; }
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
            cpplib.DatosXML Datos = comun.leerxml.ExtraerDatos(ArhXml); //(new cpplib.LeerXML()).ExtraerDatos(ArhXml);
            if (!string.IsNullOrEmpty(Datos.Rfc))
            {
                lbEmpresa.Text = Datos.Nombre;
                lbRfc.Text = Datos.Rfc;
                lbFactura.Text = Datos.Folio;
                lbFhFactura.Text = Datos.Fecha.ToString("dd/MM/yyyy");
                lbImporte.Text = Datos.Total.ToString();
                lbCliente.Text = Datos.Receptor.Nombre;
                lbRfcCliente.Text = Datos.Receptor.Rfc;
                hdCFD.Value = Datos.Sello.ToString();
                lbConcepto.Text = Datos.Concepto;
                pnDatosXml.Visible = true;
                pnFactura.Visible = true;
                Resultado = true;
            }
            else { ltMsg.Text = "El archivo XML no es correcto o esta dañado"; }
            return Resultado;
        }

        private bool ValidaRFC()
        {
            bool Continuar = false;
            if (lbRfcCliente.Text.Substring(0,8)=="XEXX0101"){
                Continuar = true;
                ltMsg.Text = "EL RFC ES GENERICO, ASEGURESE DE ANEXAR LA FACTURA A LA ORDEN CORRECTA."; 
            }
            else
            {
                if (lbOrdRfc.Text == lbRfcCliente.Text) { Continuar = true;}
                else { ltMsg.Text = "El cliente en la factura no corresponde con la solicitud"; }
            }
            return Continuar;
        }

        private void PintaFactura(String pArchivo)
        {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        private bool ExisteCFD()
        {
            bool resultado = false;
            //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
            resultado = comun.admordenfactura.ExisteCFD(hdCFD.Value.ToString()); //adm.ExisteCFD(hdCFD.Value.ToString());
            if (resultado) { ltMsg.Text = "La factura ya fue registrada en otra order de servicio "; }
            return resultado;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            //cpplib .admOrdenFactura adm= new cpplib.admOrdenFactura ();
            List<string> LstArchivos = new List<string>();

            if (lbOrdRfc.Text.Equals(lbOrdRfc.Text))
            {
                if (GuardaDocumentos(oCredencial.IdUsr, LstArchivos))
                {
                    if (!String.IsNullOrEmpty(hdCFD.Value)) 
                        comun.admordenfactura.RegistrarCDF(lbOrdenFactura.Text, hdCFD.Value); // adm.RegistrarCDF(lbOrdenFactura.Text, hdCFD.Value); }
                    comun.admordenfactura.ActualizaDatosFacturacion(lbOrdenFactura.Text, lbFactura.Text, lbFhFactura.Text, Convert.ToDecimal(lbImporte.Text), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro, 1); // adm.ActualizaDatosFacturacion(lbOrdenFactura.Text, lbFactura.Text, lbFhFactura.Text, Convert.ToDecimal(lbImporte.Text), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro, 1);
                    csGeneral admg = new csGeneral();
                    RegistraBitacora(Convert.ToInt32(lbOrdServicio.Text), Convert.ToInt32(lbOrdenFactura.Text), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro);

                    admg.EnviaCorreofacturaAgregada(lbOrdenFactura.Text, oCredencial.IdUsr);
                    
                    if (chkEvioCliente.Checked) 
                        admg.EnviaCorreoConFacturaCliente(lbOrdenFactura.Text, LstArchivos);
                    Response.Redirect("cxc_SolicitudesFacturacion.aspx");
                }
            }
            else 
                ltMsg.Text = "EL RFC de la factura no corresponde con el cliente. ";
        }

        private bool GuardaDocumentos(int IdUsr, List<string> lstArchivos )
        {
            bool Resultado = false;
            String DirRaiz = Server.MapPath(@"cxc_doc\");
            string RutaFactura = comun.admdirectorio.DaYCreaDirectorioArchivo(DirRaiz, Convert.ToDateTime(lbFechaInicio.Text)); //(new cpplib.admDirectorio()).DaYCreaDirectorioArchivo(DirRaiz, Convert.ToDateTime(lbFechaInicio.Text));

            //cpplib.admArchivosCxc admArh = new cpplib.admArchivosCxc();
            cpplib.cxcArchivo oArchivo = new cpplib.cxcArchivo();
            oArchivo.IdOrdenFactura = Convert.ToInt32(lbOrdenFactura.Text);
            oArchivo.IdDocumento = 1;
            oArchivo.Tipo = cpplib.cxcTipoArchivo.Factura;

            oArchivo.ArchvioOrigen = hdArchivoOrg.Value + ".PDF";
            oArchivo.ArchivoDestino = lbOrdenFactura.Text.ToString().PadLeft(6, '0') + "_F" + "_" + lbFactura.Text + ".PDF";

            //Guarda PDF
            
            String Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
            lstArchivos.Add(Destino);

            System.IO.File.Copy (hdRutaArchPdf.Value, Destino,true);
            if (System.IO.File.Exists(Destino))
            {
                if (comun.admarchivoscxc.Agrega(oArchivo))
                {
                    //Guarda XML
                    oArchivo.Tipo = cpplib.cxcTipoArchivo.Xml;
                    oArchivo.ArchvioOrigen = hdArchivoOrg.Value + ".XML";
                    oArchivo.ArchivoDestino = lbOrdenFactura.Text.ToString().PadLeft(6, '0') + "_X" + "_" + lbFactura.Text + ".XML";
                    Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
                    lstArchivos.Add(Destino);
                    System.IO.File.Copy(hdRutaArchXML.Value, Destino,true);
                    if (System.IO.File.Exists(Destino))
                    {
                        if (comun.admarchivoscxc.Agrega(oArchivo))
                        {
                            Resultado = true;
                        }
                    }
                }
            }
            return Resultado;
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

            bool Resultado = comun.admcxcbitacora.Registrar(oBitacora);  //(new cpplib.admCxcBitacora()).Registrar(oBitacora);
        }

    }
}