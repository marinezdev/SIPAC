using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace cxpcxc
{
    public partial class trf_SolConFacturaValida : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {hdIdEmpresa.Value =((cpplib .credencial)Session["credencial"]).IdEmpresa.ToString();}
        }

        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }
        
        protected void btnContinuar_Click(object sender, EventArgs e){ Response.Redirect("trf_SolConFactura.aspx?arch="+ hdNomArchivo.Value); }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String ArhPdf = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            String ArhXml = RutaDestino + oCredencial.IdUsr.ToString() + "_X.XML";
            string VerArchivo = "\\cxp_doc\\" + oCredencial.IdUsr.ToString() + "_F.PDF";
                                
            if( CargaArchivos(ArhPdf,ArhXml)){
                if (this.ExtraerDatosXML(ArhXml))
                {
                    if (ValidaEmpresaReceptora()) { 
                        if (ValidaRFC())
                        {
                            if (!ExisteFactura())
                            { 
                                PintaFactura(VerArchivo);
                                pnCargaArchivo.Visible = false;
                                btnContinuar.Visible = true;
                            }
                            else { PintaFactura(VerArchivo); }
                        }
                        else { PintaFactura(VerArchivo); }
                    }
                    else { PintaFactura(VerArchivo); }
                }
            }
        }
        
        private bool CargaArchivos(String ArhPdf, String ArhXml)
        {
            bool resultado = false;
            try
            {
                if (fulFactura.HasFile && fulXml.HasFile){
                   if (Path.GetFileNameWithoutExtension(fulFactura.FileName) == Path.GetFileNameWithoutExtension(fulXml.FileName)) {
                        if (fulFactura.FileName.Length <=64){
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
            if (File.Exists(ArhPdf)) {File.Delete(ArhPdf); }
            if (File.Exists(ArhXml)) {File.Delete(ArhXml); }
        }

        private bool ExtraerDatosXML(String ArhXml)
        {
            bool Resultado = false;
            cpplib.DatosXML Datos= (new cpplib.LeerXML()).ExtraerDatos(ArhXml);
            if (!string.IsNullOrEmpty(Datos.Rfc))
            {
                lbProveedor.Text = Datos.Nombre;
                lbRfc.Text = Datos.Rfc;
                lbFactura.Text = Datos.Folio;
                lbFhFactura.Text = Datos.Fecha.ToString("dd/MM/yyyy");
                lbImporte.Text = Datos.Total.ToString();
                hdLLaveSol.Value = Datos.Sello.ToString();
                lbConcepto.Text = Datos.Concepto;
                lbReceptor.Text = Datos.Receptor.Nombre;
                lbReceptorRfc.Text = Datos.Receptor.Rfc;
                pnDatosXml.Visible = true;
                Resultado = true;
            }
            else { ltMsg.Text = "El archivo XML no es correcto o esta dañado"; }
            
            return Resultado;
        }

        private bool ValidaRFC()
        {
            bool Continuar = false;
            if (!String.IsNullOrEmpty(lbRfc.Text))
            {
                if ((new cpplib.admCatProveedor()).Existe(hdIdEmpresa.Value, lbRfc.Text))
                {
                    Continuar = true;
                }
                else { ltMsg.Text = "El proveedor no esta registrado en el catalogo de proveedores de la empresa, consulte con el administrador"; }
            }
            else { ltMsg.Text = "El RFC no se encuentra, sin el no se puede asociar al catalogo de proveedores, consulte con el administrador"; }
            return Continuar;
        }
        
        private void PintaFactura(String pArchivo) {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        private bool ExisteFactura() {
            bool resultado = false;
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            resultado = admSol.Existe(hdLLaveSol .Value.ToString ());
            
            if (resultado ){ ltMsg.Text = "La factura ya esta registrada "; }
            return resultado;
        }

        private bool ValidaEmpresaReceptora() {
            bool resultado = false;
            cpplib.Empresa Emp = (new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            if (lbReceptorRfc.Text.Trim ()==Emp .Rfc.Trim () ){
                resultado=true;
            }else {ltMsg.Text ="La empresa a quien esta dirigida la factura no corresponde con la seleccionada para hacer el registro.";}

            return resultado;
        }

    }
}