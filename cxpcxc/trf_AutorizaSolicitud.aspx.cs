using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_AutorizaSolicitud : System.Web.UI.Page
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
            cpplib.Solicitud osol = (new cpplib.admSolicitud()).carga(Idpv);
            hdIdEmpresa.Value = osol.IdEmpresa.ToString();
            lbBeneficiario.Text = osol.Proveedor ;
            lbBanco.Text = osol.Banco;
            lbCuenta.Text = osol.Cuenta;
            lbClabe.Text = osol.CtaClabe;
            lbSucursal.Text = osol.Sucursal;
            lbFactura.Text = osol.Factura;
            lbFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbImporte.Text = osol.Importe.ToString("C2");
            lbImpLetra.Text = osol.ImporteLetra;
            lbCodPago.Text = osol.CondicionPago;
            lbConcepto.Text = osol.Concepto;
            lbProyecto.Text = osol.Proyecto;
            lbDecProyecto.Text = osol.DescProyecto;
            lbMoneda.Text = osol.Moneda;
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.NO) { btnFactura.Visible = false; }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + hdIdEmpresa.Value); }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("trf_VerFactura.aspx?Id=" + hdIdSol.Value +"&bk=trf_AutorizaSolicitud");
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {            
            cpplib.admSolicitud oSol = new cpplib.admSolicitud();
            oSol.CambiaEstadoSolicitud(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Captura);
            this.RegistraBitacora(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Autorizacion);
            Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + hdIdEmpresa.Value);

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
            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);
                      
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
            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);

        }

        private void llenaCatRechazo()
        {
            String IdEmpresa = ((cpplib.credencial)(Session["credencial"])).IdEmpresa.ToString(); 
            cpplib.admCatRechazos  admCat = new cpplib.admCatRechazos ();
            List<cpplib.valorTexto> lstRechazo = admCat.DaComboRechazos(IdEmpresa);
            dpRechazo.DataSource = lstRechazo;
            dpRechazo.DataValueField = "Valor";
            dpRechazo.DataTextField = "Texto";
            dpRechazo.DataBind();
            }


        protected void btnAceptaRechazo_Click(object sender, EventArgs e)
        {
            cpplib.admSolicitud oSol = new cpplib.admSolicitud();
            if (oSol.AgregaRechazo (hdIdSol.Value,dpRechazo .SelectedValue))
            {
                oSol.CambiaEstadoSolicitud(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Rechazada);
                this.RegistraBitacoraRechazo(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Rechazada);
                Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + hdIdEmpresa.Value);
            }
            else { ltMsg.Text = "No se guardo la informacion, Intente nuevamente"; }
        }

        
    }
}