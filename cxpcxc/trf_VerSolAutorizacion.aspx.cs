using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_VerSolAutorizacion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdSol.Value = Request.Params["Id"].ToString();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
                this.llenaCatRechazo();
            }
        }

        private void llenaSolicitud(int Idpv)
        {
            cpplib.Solicitud osol =  comun.admsolicitud.carga(Idpv);
            hdIdEmpresa.Value = osol.IdEmpresa.ToString();
            lbBeneficiario.Text = osol.Proveedor ;
            lbBanco.Text = osol.Banco;
            lbCuenta.Text = osol.Cuenta;
            lbClabe.Text = osol.CtaClabe;
            lbSucursal.Text = osol.Sucursal;
            lbFactura.Text = osol.Factura;
            lbFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbImporte.Text = osol.Importe.ToString("C2");
            lbCodPago.Text = osol.CondicionPago;
            lbConcepto.Text = osol.Concepto;
            lbProyecto.Text = osol.Proyecto;
            lbDecProyecto.Text = osol.DescProyecto;
            lbMoneda.Text = osol.Moneda;
            
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.SI) 
            VerFactura(osol.IdSolicitud); 
            else 
                ltDocumento.Text = "LA FACTURA NO HA SIDO AGREGADA"; 
            btnFactura.Visible = osol.ConFactura.Equals(cpplib.Solicitud.enConFactura.SI);
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { 
            if (Request.Params["bk"] != null) 
                Response.Redirect(Request.Params["bk"] + ".aspx");
            else 
                Response.Redirect("espera.aspx");
        }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e){VerFactura(Convert.ToInt32(hdIdSol.Value));}

        private void VerFactura(int IdSolicitud)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaFactura(IdSolicitud);
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
        private void RegistraBitacora(int IdSolicitud, cpplib.Solicitud.solEstado pEstadoSol)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSolicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Estado = pEstadoSol;
            oBitacora.Importe = 0;
            bool Resultado = comun.admbitacorasolicitud.Registrar(oBitacora);
                      
        }

        private void RegistraBitacoraRechazo(int IdSolicitud, cpplib.Solicitud.solEstado pEstadoSol)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSolicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Estado = pEstadoSol;
            oBitacora.Importe = 0;
            bool Resultado = comun.admbitacorasolicitud.Registrar(oBitacora);

        }

        private void llenaCatRechazo()
        {
            String IdEmpresa = ((cpplib.credencial)(Session["credencial"])).IdEmpresa.ToString();
            //cpplib.admCatRechazos  admCat = new cpplib.admCatRechazos ();
            //List<cpplib.valorTexto> lstRechazo = comun.admcatrechazos.DaComboRechazos(IdEmpresa);
            //dpRechazo.DataSource = lstRechazo;
            //dpRechazo.DataValueField = "Valor";
            //dpRechazo.DataTextField = "Texto";
            //dpRechazo.DataBind();
            LlenarControles.LlenarDropDownList(ref dpRechazo, comun.admcatrechazos.DaComboRechazos(IdEmpresa), "Texto", "Valor");
        }
            


        protected void btnAceptaRechazo_Click(object sender, EventArgs e)
        {
            //cpplib.admSolicitud oSol = new cpplib.admSolicitud();
            if (comun.admsolicitud.AgregaRechazo (hdIdSol.Value,dpRechazo .SelectedValue))
            {
                comun.admsolicitud.CambiaEstadoSolicitud(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Rechazada);
                this.RegistraBitacoraRechazo(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Rechazada);
                
                if (Request.Params["bk"] != null) 
                    Response.Redirect(Request.Params["bk"] + ".aspx");
                else 
                    Response.Redirect("espera.aspx");
            }
            else 
                ltMsg.Text = "No se guardo la informacion, Intente nuevamente";
        }

    }
}