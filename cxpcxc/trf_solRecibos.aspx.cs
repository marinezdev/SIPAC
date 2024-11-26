using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace cxpcxc
{
    public partial class trf_solRecibos : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
                this.txFhRecibo.Attributes.Add("readonly", "true");
                this.llenaProveedor();
                this.llenaCatalogos(); 
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaProveedor()
        {
            int Idpv = Convert.ToInt32(Request.Params["Id"]);
            String Cuenta = Request.Params["ct"];
            cpplib.CatProveedor opvd = (new cpplib.admCatProveedor()).carga(Idpv);
            hdIdProveedor.Value = opvd.Id.ToString();
            lbProveedor.Text = opvd.Nombre;
            lbRfc.Text = opvd.Rfc;

            cpplib.admCuenta admCta = new cpplib.admCuenta();
            cpplib.Cuenta oCta = admCta.carga(Idpv, Cuenta);
            lbBanco.Text = oCta.Banco;
            lbCuenta.Text = oCta.NoCuenta;
            lbClabe.Text = oCta.CtaClabe;
            lbSucursal.Text = oCta.Sucursal;
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

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"documentos\");
            String ArhPdf = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            string VerArchivo = "\\documentos\\" + oCredencial.IdUsr.ToString() + "_F.PDF";

            if (CargaArchivo(ArhPdf))
                {
                PintaRecibo(VerArchivo);
                pnRegistro.Visible = true;
                pnSelRecibo.Visible = false;
            }
            
        }

        private bool CargaArchivo(String ArhPdf)
        {
            bool resultado = false;
            try
            {
                if (fulRecibo.HasFile )
                {
                    if (Path.GetExtension(fulRecibo.FileName).ToUpper().Equals(".PDF") )
                    {
                        hdNomArchivo.Value = Path.GetFileNameWithoutExtension(fulRecibo.FileName);
                        this.EliminaArchivosAnteriores(ArhPdf);

                        fulRecibo.SaveAs(ArhPdf);
                        
                        if (File.Exists(ArhPdf)){
                            resultado = true;
                        }
                        else { ltMsg.Text = "El archivo no se cargo, intente nuevamente"; }
                    }
                    else { ltMsg.Text = "El archivo debe ser PDF"; }
                }
                else { ltMsg.Text = "Agregue el archivo"; }
            }
            catch (Exception ex) { ltMsg.Text = ex.Message.ToString(); }
            return resultado;
        }

        private void EliminaArchivosAnteriores(String ArhPdf){if (File.Exists(ArhPdf)) { File.Delete(ArhPdf); }}

        private void PintaRecibo(String pArchivo)
        {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSol = recuperaDatos(oCredencial);
            int IdSol = admSol.nueva(oSol);
            if (IdSol > 0)
            {
                oSol.IdSolicitud = IdSol;
                if (GuardaDocumentos(oSol, oCredencial.IdUsr))
                {
                    this.RegistraBitacora(oCredencial, IdSol);
                    this.EnviarCorreoXSolicitud(oCredencial, oSol);
                    Response.Redirect("trf_SolicitudesRegistro.aspx");
                }
            }
        }

        private cpplib.Solicitud recuperaDatos(cpplib.credencial oCredencial)
        {
            cpplib.Solicitud oSol = new cpplib.Solicitud();
            oSol.IdProveedor = Convert.ToInt32(hdIdProveedor.Value);
            oSol.Proveedor = lbProveedor.Text;
            oSol.Rfc = lbRfc.Text;
            oSol.Banco = lbBanco.Text;
            oSol.Cuenta = lbCuenta.Text;
            oSol.CtaClabe = lbClabe.Text;
            oSol.Sucursal = lbSucursal.Text;
            oSol.Factura = txFactura.Text;
            oSol.FechaFactura = Convert.ToDateTime(txFhRecibo.Text);
            oSol.Importe = Convert.ToDecimal(txImporte.Text);
            if (txConcepto.Text.Length > 255) { oSol.Concepto = txConcepto.Text.Substring(0, 254); } else { oSol.Concepto = txConcepto.Text; }
            oSol.CondicionPago = dpCondPago.SelectedValue;
            oSol.Proyecto = dpProyecto.SelectedValue;
            if (txDecProyecto.Text.Length > 128) { oSol.DescProyecto = txDecProyecto.Text.Substring(0, 127); } else { oSol.DescProyecto = txDecProyecto.Text; }
            oSol.Moneda = dpTpMoneda.SelectedValue;
            oSol.Estado = cpplib.Solicitud.solEstado.Solicitud;
            oSol.ConFactura = cpplib.Solicitud.enConFactura.SI;
            oSol.IdUsr = oCredencial.IdUsr;
            oSol.IdEmpresa = oCredencial.IdEmpresa;
            oSol.UnidadNegocio = oCredencial.UnidadNegocio;
            return oSol;
        }

        private void RegistraBitacora(cpplib.credencial oCredencial, int Idtrf_Solicitud)
        {
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = Idtrf_Solicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Estado = cpplib.Solicitud.solEstado.Solicitud;
            oBitacora.Importe = Convert.ToDecimal(txImporte.Text); 
            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);
        }


        private bool GuardaDocumentos(cpplib.Solicitud oSol, int IdUsr)
        {

            bool Resultado = false;
            String DirRaiz = Server.MapPath(@"documentos\");
            string RutaFactura = (new cpplib.admDirectorio()).DaYCreaDirectorioArchivo(DirRaiz, Convert.ToDateTime(txFhRecibo.Text));

            String ArhPdf = DirRaiz + IdUsr.ToString() + "_F.PDF";
            
            cpplib.admArchivos admArh = new cpplib.admArchivos();
            cpplib.Archivo oArchivo = new cpplib.Archivo();
            oArchivo.IdSolicitud = oSol.IdSolicitud;
            oArchivo.IdDocumento = 1;
            oArchivo.Tipo = cpplib.TipoArchivo.Factura;

            oArchivo.ArchvioOrigen = hdNomArchivo.Value + ".PDF";
            oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_R" + "_" + oSol.Factura.PadLeft(6, '0') + ".PDF";

            //Guarda PDF
            String Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
            System.IO.File.Copy(ArhPdf, Destino);
            if (System.IO.File.Exists(Destino))
            {
                if (admArh.Agrega(oArchivo))
                {
                    Resultado = true;
                }
            }
            return Resultado;
        }

        private void EnviarCorreoXSolicitud(cpplib.credencial oCrd,cpplib.Solicitud oSol ) {
            csGeneral Gral = new csGeneral();
            Gral.EnviaCorreoDireccionXSolicitudAutorizacion(oCrd.IdEmpresa.ToString(), oCrd.Nombre, oSol); 
        }

    }
}