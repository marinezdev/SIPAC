using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_CapturaSolicitud : System.Web.UI.Page
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

        private void llenaSolicitud(int Idpv)
        {
            cpplib.Solicitud osol = (new cpplib.admSolicitud()).carga(Idpv);
            lbBeneficiario.Text = osol.Proveedor;
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
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.NO) { btnFactura.Visible = false; }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesCaptura.aspx"); }

        protected void btnFactura_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("trf_VerFactura.aspx?Id=" + hdIdSol.Value + "&bk=trf_CapturaSolicitud");
        }

        protected void btnCapturado_Click(object sender, EventArgs e)
        {
           cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
           cpplib.admSolicitud oSol = new cpplib.admSolicitud();
           oSol.CambiaEstadoSolicitud(Convert.ToInt32(hdIdSol.Value), cpplib.Solicitud.solEstado.Captura);
           this.RegistraBitacora(oCredencial,Convert.ToInt32 (hdIdSol.Value));
            Response.Redirect("trf_SolicitudesCaptura.aspx");
        }

        private void RegistraBitacora(cpplib.credencial oCredencial, int IdSolicitud)
        {
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSolicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Estado = cpplib.Solicitud.solEstado.Captura;

            bool Resultado = (new cpplib.admBitacoraSolicitud()).Registrar(oBitacora);
        }
    }
}