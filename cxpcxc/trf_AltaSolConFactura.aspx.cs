using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class trf_AltaSolConFactura : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresaTrabajo.ToString();
                this.llenaCatalogos(); 
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaCatalogos()
        {
            LlenarControles.LlenarDropDownList(ref dpCondPago, comun.admcatcondpago.DaComboCondicionPago(hdIdEmpresa.Value), "Texto", "Valor");
            LlenarControles.LlenarDropDownList(ref dpProyecto, comun.admcatproyectos.DaComboProyectos(hdIdEmpresa.Value), "Texto", "Valor");

            //List<cpplib.valorTexto> lstCodPago = (new cpplib.admCatCondPago()).DaComboCondicionPago(hdIdEmpresa.Value);
            //List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(hdIdEmpresa.Value);

            //dpCondPago.DataSource = lstCodPago;
            //dpCondPago.DataValueField = "Valor";
            //dpCondPago.DataTextField = "Texto";
            //dpCondPago.DataBind();

            //dpProyecto.DataSource = lstProyectos;
            //dpProyecto.DataValueField = "Valor";
            //dpProyecto.DataTextField = "Texto";
            //dpProyecto.DataBind();
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String ArhPdf = RutaDestino + oCredencial.IdUsr.ToString() + "_F.PDF";
            String ArhXml = RutaDestino + oCredencial.IdUsr.ToString() + "_X.XML";
            //string VerArchivo = "\\cxp_doc\\" + oCredencial.IdUsr.ToString() + "_F.PDF";
            string VerArchivo = "cxp_doc\\" + oCredencial.IdUsr.ToString() + "_F.PDF";
            if (CargaArchivos(ArhPdf, ArhXml)) 
            {
                if (this.ExtraerDatosXML(ArhXml)) 
                {
                    pnSelecionArchivo.Visible = false;
                    PintaFactura(VerArchivo);
                    
                    if (ValidaInformacion()) 
                    {
                        if (llenacatalogoCuentas())
                        {
                            btnGuardar.Visible =true;
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
                    if (Path.GetFileNameWithoutExtension(fulFactura.FileName) == Path.GetFileNameWithoutExtension(fulXml.FileName))
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

        private bool ValidaInformacion()
        {
           bool Continuar = true;
           string Mensaje = string.Empty;
           if (String.IsNullOrEmpty(lbRfc.Text)) { Mensaje = "El RFC no se encuentra, sin el no se puede asociar al catalogo de proveedores. ";}
           if (!(new cpplib.admCatProveedor()).Existe(hdIdEmpresa.Value, lbRfc.Text)) { Mensaje += " El proveedor no esta registrado en el catalogo de proveedores de la empresa."; }

            cpplib.Empresa Emp = (new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            if (lbReceptorRfc.Text.Trim() != Emp.Rfc.Trim()) { Mensaje += " La empresa a quien esta dirigida la factura no corresponde con la seleccionada para hacer el registro. "; }

            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            if (admSol.Existe(hdLLaveSol.Value.ToString())) { Mensaje += "  La factura ya esta registrada "; }

            if (Mensaje.Trim().Length > 0) { Continuar = false; ltMsg.Text = Mensaje; }
            return Continuar;
        }
              
        private void PintaFactura(String pArchivo)
        {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        private bool llenacatalogoCuentas()
        {
            bool resultado = false;
            cpplib.CatProveedor opvd = comun.admcatproveedor.DaProvedorXRfc(hdIdEmpresa.Value, lbRfc.Text);
            hdIdpvd.Value = opvd.Id.ToString();
            List<cpplib.Cuenta> Lista = comun.admcuenta.ListaCuentas(opvd.Id.ToString());
            if (Lista.Count > 0)
            {
                if (Lista.Count == 1)
                {
                    llenaCuenta(Lista[0]);
                    pnComplementario.Visible = true;
                }
                if (Lista.Count > 1) 
                { 
                    //rptCuentas.DataSource = Lista; 
                    //DataBind();
                    LlenarControles.LlenarRepeater(ref rptCuentas, Lista);
                }
                resultado = true;
            }
            else 
                ltMsg.Text = "Las cuentas bancarias no estan registradas para el proveedor"; 

            return resultado;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSol = recuperaDatos(oCredencial);
            int IdSol = comun.admsolicitud.nueva(oSol);
            if (IdSol > 0) 
            {
                oSol.IdSolicitud = IdSol;
                if (!String.IsNullOrEmpty(hdLLaveSol.Value)) 
                { 
                    comun.admsolicitud.RegistrarLlave(IdSol.ToString(), hdLLaveSol.Value); 
                }
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
            string RutaFactura = comun.admdirectorio.DaYCreaDirectorioArchivo(DirRaiz,Convert.ToDateTime(lbFhFactura.Text));
            
            String ArhPdf = DirRaiz + IdUsr.ToString() + "_F.PDF";
            String ArhXml = DirRaiz + IdUsr.ToString() + "_X.XML";
                      
            cpplib.admArchivos admArh = new cpplib.admArchivos();
            cpplib.Archivo oArchivo = new cpplib.Archivo();
            oArchivo.IdSolicitud = oSol.IdSolicitud;
            oArchivo.IdDocumento = 1;
            oArchivo.Tipo = cpplib.TipoArchivo.Factura;

            oArchivo.ArchvioOrigen = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_F" + "_" + oSol.Factura.PadLeft(6, '0') + ".PDF";
            oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_F" + "_" + oSol.Factura.PadLeft(6, '0') + ".PDF";
            
            //Guarda PDF
            String Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
            System.IO.File.Copy(ArhPdf, Destino);
            if (System.IO.File.Exists(Destino))
            {
                if (admArh.Agrega(oArchivo)) 
                {
                    //Guarda XML
                    oArchivo.Tipo = cpplib.TipoArchivo.Xml;
                    oArchivo.ArchvioOrigen = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_X" + "_" + oSol.Factura.PadLeft(6, '0') + ".XML";
                    oArchivo.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_X" + "_" + oSol.Factura.PadLeft(6, '0') + ".XML";
                    Destino = DirRaiz + RutaFactura + oArchivo.ArchivoDestino;
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

        private void RegistraBitacora(cpplib.credencial oCredencial, int Idtrf_Solicitud)
        {
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = Idtrf_Solicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Importe = Convert.ToDecimal(lbImporte.Text);
            oBitacora.Estado = cpplib.Solicitud.solEstado.Solicitud;

            bool Resultado = comun.admbitacorasolicitud.Registrar(oBitacora);
        }

        
        private void EnviarCorreoXSolicitud(cpplib.credencial oCrd,cpplib.Solicitud oSol ) {
            csGeneral Gral = new csGeneral();
            Gral.EnviaCorreoDireccionXSolicitudAutorizacion(oCrd.IdEmpresa.ToString(), oCrd.Nombre, oSol);
        }

        protected void rptCuentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Seleccionar"))
            {
                cpplib.Cuenta oCta = comun.admcuenta.carga(Convert.ToInt32(hdIdpvd.Value), e.CommandArgument.ToString());
                this.llenaCuenta(oCta);
                pnSelCuenta.Visible = false;
                pnComplementario.Visible = true;
            }
         }

        private void llenaCuenta(cpplib.Cuenta oCta)
        {
            lbBanco.Text = oCta.Banco;
            lbCuenta.Text = oCta.NoCuenta;
            lbClabe.Text = oCta.CtaClabe;
            lbSucursal.Text = oCta.Sucursal;
        }

    }
}