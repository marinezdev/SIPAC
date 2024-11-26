using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_SolSinFactura : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.txFhFactura.Attributes.Add("readonly", "true");
                int Idpv = Convert.ToInt32(Request.Params["Id"]);
                String Cuenta = Request.Params["ct"];
                this.llenaProveedor(Idpv);
                this.llenaDatosCuenta(Idpv, Cuenta);
                this.llenaCatalogos(); 
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesRegistro.aspx"); }

        private void llenaDatosCuenta(int Idpvd, String Cuenta)
        {
            cpplib.admCuenta admCta = new cpplib.admCuenta();
            cpplib.Cuenta oCta = admCta.carga(Idpvd, Cuenta);
            lbBanco.Text = oCta.Banco;
            lbCuenta.Text = oCta.NoCuenta;
            lbClabe.Text = oCta.CtaClabe;
            lbSucursal.Text = oCta.Sucursal;
        }
        private void llenaProveedor(int Idpvd)
        {
            cpplib.CatProveedor opvd = (new cpplib.admCatProveedor()).carga(Idpvd);
            hdIdProveedor.Value = opvd.Id.ToString() ;
            lbProveedor.Text = opvd.Nombre;
            lbRfc.Text = opvd.Rfc;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSol = recuperaDatos(oCredencial);
            int IdSol = admSol.nueva(oSol);
            if (IdSol > 0)
            {
                this.RegistraBitacora(oCredencial, IdSol);
                this.EnviarCorreoXSolicitud(oCredencial, oSol);
                Response.Redirect("trf_SolicitudesRegistro.aspx");
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
            oSol.FechaFactura = Convert.ToDateTime(txFhFactura.Text);
            oSol.Importe = Convert.ToDecimal(txImporte.Text);
            if (txConcepto.Text.Length > 255) { oSol.Concepto = txConcepto.Text.Substring(0, 254); } else { oSol.Concepto = txConcepto.Text; }
            oSol.CondicionPago = dpCondPago.SelectedValue;
            oSol.Proyecto = dpProyecto.SelectedValue;
            if (txDecProyecto.Text.Length > 128) { oSol.DescProyecto = txDecProyecto.Text.Substring(0, 127); } else { oSol.DescProyecto = txDecProyecto.Text; }
            oSol.Moneda = dpTpMoneda.SelectedValue;
            oSol.Estado = cpplib.Solicitud.solEstado.Solicitud;
            oSol.ConFactura = cpplib.Solicitud.enConFactura.NO;
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

        private void EnviarCorreoXSolicitud(cpplib.credencial oCrd,cpplib.Solicitud oSol ) {
            csGeneral Gral = new csGeneral();
            Gral.EnviaCorreoDireccionXSolicitudAutorizacion(oCrd.IdEmpresa.ToString(), oCrd.Nombre, oSol);
        }
    }
}