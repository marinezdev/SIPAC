using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_MoficarSolicitud : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                hdIdSol.Value = Request.Params["Id"].ToString();
                this.txFhFactura.Attributes.Add("readonly", "true");
                this.llenaCatalogos();
                this.llenaSolicitud(Convert.ToInt32(hdIdSol.Value));
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("trf_VerSolicitud.aspx?Id=" + hdIdSol.Value);}

        private void llenaCatalogos()
        {           
            String IdEmpresa = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString();
            LlenarControles.LlenarDropDownList(ref dpCondPago, comun.admcatcondpago.DaComboCondicionPago(IdEmpresa), "Texto", "Valor");
            LlenarControles.LlenarDropDownList(ref dpProyecto, comun.admcatproyectos.DaComboProyectos(IdEmpresa), "Texto", "Valor");

            //List<cpplib.valorTexto> lstCodPago = (new cpplib.admCatCondPago()).DaComboCondicionPago(IdEmpresa);
            //List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(IdEmpresa);

            //dpCondPago.DataSource = lstCodPago;
            //dpCondPago.DataValueField = "Valor";
            //dpCondPago.DataTextField = "Texto";
            //dpCondPago.DataBind();

            //dpProyecto.DataSource = lstProyectos;
            //dpProyecto.DataValueField = "Valor";
            //dpProyecto.DataTextField = "Texto";
            //dpProyecto.DataBind();
        }

        private void llenaSolicitud(int IdSol)
        {
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud osol = admSol.carga(IdSol);
            lbProveedor.Text = osol.Proveedor;
            txFactura.Text = osol.Factura;
            txFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            txImporte.Text = osol.Importe.ToString("C2");
            txConcepto.Text = osol.Concepto;

            dpCondPago.Text = osol.CondicionPago;
            dpProyecto.Text = osol.Proyecto;
            txDecProyecto.Text = osol.DescProyecto;
            dpTpMoneda.SelectedValue = osol.Moneda;

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud oSolicitud = recuperaDatos();
            if (admSol.ModificaSolSinFactura(oSolicitud)) 
                Response.Redirect("trf_VerSolicitud.aspx?Id=" + hdIdSol.Value);            
        }
        
        private cpplib.Solicitud recuperaDatos()
        {
            cpplib.Solicitud oSol = new cpplib.Solicitud();
            oSol.IdSolicitud = Convert.ToInt32(hdIdSol.Value);

            oSol.Factura = txFactura.Text;
            oSol.FechaFactura = Convert.ToDateTime(txFhFactura.Text);
            oSol.Importe = Convert.ToDecimal(txImporte.Text);
            oSol.CantidadPagar = Convert.ToDecimal(txImporte.Text);
            oSol.Concepto = txConcepto.Text;

            oSol.CondicionPago = dpCondPago.SelectedValue;
            oSol.Proyecto = dpProyecto.SelectedValue;
            if (txDecProyecto.Text.Length > 128) { oSol.DescProyecto = txDecProyecto.Text.Substring(0, 127); }
            else { oSol.DescProyecto = txDecProyecto.Text; }
            oSol.Moneda = dpTpMoneda.SelectedValue;
            return oSol;
        }
    }
}