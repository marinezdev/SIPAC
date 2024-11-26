using cpplib;
using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_OrdenFactura : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdIdEmpresa.Value = ((cpplib.credencial)(Session["credencial"])).IdEmpresaTrabajo.ToString();
                llenaCombos();
                CargaOrdenesfacturacion();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaCombos() 
        {

            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString());  //Anterior: .ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));
        }

        private void CargaOrdenesfacturacion() {
            ltMsg.Text = "";
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.DaListaOrdenesEnFacturacion(hdIdEmpresa.Value, dpCliente.SelectedValue);
            if (Lista.Count > 0)
            {
                //rptRegistros.DataSource = Lista;
                //rptRegistros.DataBind();
            }
            else
            {
                //rptRegistros.DataSource = null;
                //rptRegistros.DataBind();
                ltMsg.Text = "No hay ordenes de facturación";
            }
            LlenarControles.LlenarRepeater(ref rptRegistros, Lista);
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Registrar") {
                Response.Redirect("cxc_AgregarFacturas.aspx?Ord=" + e.CommandArgument.ToString());
            }

            if (e.CommandName == "Eliminar") { 
                string [] Datos=e.CommandArgument.ToString().Split('|'); 
                if(Datos.Count() == 2){
                    this.ProcesaEliminacion (Convert.ToInt32 (Datos[0]),Convert.ToInt32 (Datos[1]));
                    this.CargaOrdenesfacturacion();
                }
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { CargaOrdenesfacturacion(); }

        private void ProcesaEliminacion(int IdServicio,int idOrdFactura ){
            //cpplib.admOrdenFactura admOrdFc = new cpplib.admOrdenFactura();
            
            if (comun.admordenfactura.Eliminar(idOrdFactura)) {
                bool resultado = comun.admcxcbitacora.Eliminar(idOrdFactura);
                //cpplib.admOrdenServicio admOrdSrv = new cpplib.admOrdenServicio();
                if((comun.admordenservicio.carga(IdServicio).Periodos==1)){
                    comun.admordenservicio.Eliminar(IdServicio);
                } 
            }
        }

        protected void rptRegistros_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                ImageButton imgFac = (ImageButton)e.Item.FindControl("ImgEliminar");
                imgFac.CommandArgument = ordFac.IdServicio.ToString() + "|" + ordFac.IdOrdenFactura.ToString();
            }
        }
    }
}