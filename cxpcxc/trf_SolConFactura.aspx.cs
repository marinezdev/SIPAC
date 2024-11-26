using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_SolConFactura : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString(); 
                this.llenaCatalogos(); 
                this.CargaDatos();
            }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaCatalogos(){
            List<cpplib.valorTexto> lstCodPago = (new cpplib.admCatCondPago()).DaComboCondicionPago(hdIdEmpresa.Value);
            List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(hdIdEmpresa.Value);

            dpCondPago.DataSource = lstCodPago;
            dpCondPago.DataValueField = "Valor";
            dpCondPago.DataTextField = "Texto";
            dpCondPago.DataBind();

            dpProyecto.DataSource = lstProyectos;
            dpProyecto.DataValueField = "Valor";
            dpProyecto.DataTextField = "Texto";
            dpProyecto.DataBind();
        }

        private void CargaDatos(){
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String ArhPdf = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            String ArhXml = RutaDestino + oCredencial.IdUsr.ToString() + "_X.XML";
            if (this.ExtraerDatosXML(ArhXml))
            {
                this.llenaBeneficiario();
            }
        }
        
        private bool ExtraerDatosXML(String ArhXml) {
            bool Resultado = false;
            
            cpplib.DatosXML Datos = (new cpplib.LeerXML()).ExtraerDatos(ArhXml);
            if (!string.IsNullOrEmpty(Datos.Rfc))
            {
                lbProveedor.Text = Datos.Nombre;
                lbRfc.Text = Datos.Rfc;
                lbFactura.Text = Datos.Folio;
                lbFhFactura.Text = Datos.Fecha.ToString("dd/MM/yyyy");
                lbImporte.Text = Datos.Total.ToString();
                hdLLaveSol.Value = Datos.Sello.ToString();
                lbConcepto.Text = Datos.Concepto;
                pnDatosXml.Visible = true;
                Resultado = true;
            }
            else { ltMsg.Text = "El archivo XML no es correcto o esta dañado"; }
            return Resultado;
        }

        private void llenaBeneficiario()
        {
          String Rfc = lbRfc.Text;
            if (!String.IsNullOrEmpty(Rfc))
            {
                cpplib.CatProveedor opvd = (new cpplib.admCatProveedor()).DaProvedorXRfc(hdIdEmpresa.Value, Rfc);
                if (opvd.Id != 0){
                    lbProveedor.Text = opvd.Nombre;
                    hdIdpvd.Value = opvd.Id.ToString();
                    cpplib.admCuenta admCta = new cpplib.admCuenta();
                    List<cpplib.Cuenta> Lista = admCta.ListaCuentas(opvd.Id.ToString ());
                    if (Lista.Count == 1) {
                        llenaCuenta(Lista[0]);
                        pnCuenta.Visible = true; 
                    }else {
                        rptCuentas.DataSource = Lista;
                        DataBind ();
                        pnSelCuenta.Visible = true;
                    }
                }
                else { ltMsg.Text = "El RFC: " + Rfc + " No esta en el catalogo de proveedores, consulte con el administrador"; }
            }
        }
        
        private void  llenaCuenta(cpplib.Cuenta oCta)
        {
            lbBanco.Text = oCta.Banco;
            lbCuenta.Text = oCta.NoCuenta;
            lbClabe.Text = oCta.CtaClabe;
            lbSucursal.Text = oCta.Sucursal;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSol = recuperaDatos(oCredencial);
            int IdSol = admSol.nueva(oSol);
            if (IdSol > 0) {
                oSol.IdSolicitud = IdSol;
                if (!String.IsNullOrEmpty(hdLLaveSol.Value)) { admSol.RegistrarLlave(IdSol.ToString(), hdLLaveSol.Value); }
                if (GuardaDocumentos(oSol, oCredencial.IdUsr))
                {
                    this.RegistraBitacora(oCredencial, IdSol);
                    this.EnviarCorreoXSolicitud(oCredencial, oSol);
                    Response.Redirect("trf_SolicitudesRegistro.aspx");
                }
            }
        }

        private bool GuardaDocumentos(cpplib.Solicitud oSol, int IdUsr) {
            
            bool Resultado=false;
            String DirRaiz = Server.MapPath(@"cxp_doc\");
            string RutaFactura = (new cpplib.admDirectorio()).DaYCreaDirectorioArchivo(DirRaiz,Convert.ToDateTime(lbFhFactura.Text));
            
            String ArhPdf = DirRaiz + IdUsr.ToString() + "_F.PDF";
            String ArhXml = DirRaiz + IdUsr.ToString() + "_X.XML";
                      
            cpplib.admArchivos admArh = new cpplib.admArchivos();
            cpplib.Archivo oArchivo = new cpplib.Archivo();
            oArchivo.IdSolicitud = oSol.IdSolicitud;
            oArchivo.IdDocumento = 1;
            oArchivo.Tipo = cpplib.TipoArchivo.Factura; 

            oArchivo.ArchvioOrigen = Request.Params["arch"].ToString() + ".PDF";
            oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_F" + "_" + oSol.Factura.PadLeft(6, '0') + ".PDF";
            
            //Guarda PDF
            String Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
            System.IO.File.Copy(ArhPdf, Destino);
            if (System.IO.File.Exists(Destino)){
                if (admArh.Agrega(oArchivo)) {
                    //Guarda XML
                    oArchivo.Tipo = cpplib.TipoArchivo.Xml; 
                    oArchivo.ArchvioOrigen = Request.Params["arch"].ToString() + ".XML";
                    oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_X" + "_" + oSol.Factura.PadLeft(6, '0') + ".XML";
                    Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
                    System.IO.File.Copy(ArhXml, Destino);
                    if (System.IO.File.Exists(Destino))
                    {
                        if (admArh.Agrega(oArchivo)) {
                            Resultado = true;
                        }
                    }
                }
            }
            return Resultado;
        }

        private cpplib.Solicitud recuperaDatos(cpplib.credencial oCredencial)
        {
            cpplib.Solicitud oSol = new cpplib.Solicitud();
            oSol.IdEmpresa = oCredencial.IdEmpresa;
            oSol.IdProveedor = Convert.ToInt32 (hdIdpvd.Value);
            oSol.Proveedor = lbProveedor.Text;
            oSol.Rfc = lbRfc.Text;
            oSol.Banco = lbBanco.Text;
            oSol.Cuenta = lbCuenta.Text;
            oSol.CtaClabe = lbClabe.Text;
            oSol.Sucursal = lbSucursal.Text;
            oSol.Factura =lbFactura.Text;
            oSol.FechaFactura=Convert.ToDateTime(lbFhFactura.Text);
            oSol.Importe=Convert.ToDecimal(lbImporte.Text);
            if (lbConcepto.Text.Length > 254) { oSol.Concepto = lbConcepto.Text.Substring(0, 254); }
            else { oSol.Concepto = lbConcepto.Text; }
            oSol.CondicionPago = dpCondPago.SelectedValue;
            oSol.Proyecto = dpProyecto.SelectedValue;
            if (txDecProyecto.Text.Length > 128) { oSol.DescProyecto = txDecProyecto.Text.Substring(0, 127); }
            else {oSol.DescProyecto = txDecProyecto.Text; }
            oSol.Moneda = dpTpMoneda.SelectedValue;
            oSol.Estado = cpplib.Solicitud.solEstado.Solicitud;
            oSol.ConFactura = cpplib.Solicitud.enConFactura.SI;
            oSol.IdUsr = oCredencial.IdUsr;
            oSol.UnidadNegocio = oCredencial.UnidadNegocio; 
            return oSol;
        }

        private void RegistraBitacora(cpplib.credencial oCredencial, int Idtrf_Solicitud){
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = Idtrf_Solicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Importe = Convert.ToDecimal(lbImporte.Text);
            oBitacora.Estado = cpplib.Solicitud.solEstado.Solicitud;

            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);
        }

        protected void rptProveedor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Seleccionar"))
            {
                cpplib.Cuenta oCta = (new cpplib.admCuenta()).carga(Convert.ToInt32(hdIdpvd.Value), e.CommandArgument.ToString());
                this.llenaCuenta(oCta);
                pnSelCuenta.Visible = false;
                pnCuenta.Visible = true;
            }
        }

        private void EnviarCorreoXSolicitud(cpplib.credencial oCrd,cpplib.Solicitud oSol ) {
            csGeneral Gral = new csGeneral();
            Gral.EnviaCorreoDireccionXSolicitudAutorizacion(oCrd.IdEmpresa.ToString(), oCrd.Nombre, oSol);
        }
    }
}