using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_AlterarEstadoPartida : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Mes = Convert.ToInt32(DateTime.Now.Month);
                string Año = DateTime.Now.Year.ToString();
                dpMes.SelectedIndex = Mes;
                dpAño.Text = Año;
                this.llenaCombos();
                this.ListaOrdenfactura();
            }
        }

        private void llenaCombos()
        {
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            /* Llena clientes*/
            //List<cpplib.CatClientes> lstClte = (new cpplib.admCatClientes()).ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString());  //Anterior: .ListaTodosClientes();
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //dpCliente.DataSource = comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString());
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            /* Llena estado*/
            dpEstado.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (int value in Enum.GetValues(typeof(cpplib.OrdenFactura.EstadoOrdFac)))
            {
                var name = Enum.GetName(typeof(cpplib.OrdenFactura.EstadoOrdFac), value);
                dpEstado.Items.Add(new ListItem(name, value.ToString()));
            }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta); //admOrd.ConsultaFacturas(Consulta);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
            }
            else
            {
                ltMsg.Text = "No hay información";
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
            }
        }
        private string Daconsulta()
        {
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            string resultado = string.Empty;
            resultado = ArmaConsulta(resultado, (" IdEmpresa='" + crd.IdEmpresaTrabajo.ToString() + "'"));
            resultado = ArmaConsulta(resultado, (" UnidadNegocio='" + crd.UnidadNegocio.ToString() + "'"));
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpEstado.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" Estado='" + dpEstado.SelectedValue + "'")); }
            if (!dpMes.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(MONTH,FECHAINICIO)=" + dpMes.SelectedValue)); }
            if (!dpAño.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(YEAR,FECHAINICIO)=" + dpAño.SelectedValue)); }
            return resultado;
        }

        private string ArmaConsulta(string Cadena, string Dato)
        {
            if (string.IsNullOrEmpty(Cadena)) { Cadena = " where " + Dato; }
            else { Cadena += " and " + Dato; }
            return Cadena;
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.ListaOrdenfactura(); }

        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                string datos = ordFac.IdServicio.ToString() + "|" + ordFac.IdOrdenFactura.ToString();

                ImageButton imgFc = (ImageButton)e.Item.FindControl("imgbtnFacturar");
                ImageButton imgCc = (ImageButton)e.Item.FindControl("imgbtnCancelar");
                ImageButton imgEl = (ImageButton)e.Item.FindControl("imgbtnEliminar");
                
                imgFc.CommandArgument = datos;
                imgCc.CommandArgument = datos;
                imgEl.CommandArgument = datos;

                if ((ordFac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Solicitud)) || (ordFac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura ))) { imgFc.Visible = false; }
                if (ordFac.Estado.Equals(cpplib.OrdenFactura.EstadoOrdFac.Cancelado)) { imgCc.Visible = false; }
                
            }
        }

        protected void rptsol_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string [] Datos=e.CommandArgument.ToString().Split('|'); 
            if(Datos.Count() == 2){

                if (e.CommandName.Equals("Cancelar")) { this.CancelaOrden(Convert.ToInt32(Datos[0]), Convert.ToInt32(Datos[1])); }
                if (e.CommandName.Equals("Facturar")) { this.RegresaFacturar(Convert.ToInt32(Datos[1]));}
                if (e.CommandName.Equals ( "Eliminar")) { this.EliminarOrden(Convert.ToInt32(Datos[0]), Convert.ToInt32(Datos[1]));}
                
                this.ListaOrdenfactura();
            }
        }
        
        private void EliminarOrden(int IdServicio, int idOrdFactura)
        {
            //cpplib.admOrdenFactura admOrdFc = new cpplib.admOrdenFactura();

            if (comun.admordenfactura.Eliminar(idOrdFactura))
            {
                bool resultado = (new cpplib.admCxcBitacora()).Eliminar(idOrdFactura);
                resultado = (new cpplib.admArchivosCxc()).Eliminar(idOrdFactura);

                //cpplib.admOrdenServicio admOrdSrv = new cpplib.admOrdenServicio();
                if ((comun.admordenservicio.carga(IdServicio).Periodos == 1))
                    comun.admordenservicio.Eliminar(IdServicio);
                else
                    comun.admordenservicio.ActualizaDatosOrden(IdServicio); 
            }
        }

        private void RegresaFacturar(int idOrdFactura)
        {
            cpplib.admOrdenFactura admOrdFc = new cpplib.admOrdenFactura();
            cpplib.OrdenFactura ordFac = admOrdFc.carga(idOrdFactura);
            
            bool resultado = (new cpplib.admArchivosCxc()).Eliminar(idOrdFactura);
            resultado = admOrdFc.EliminarLlaveCFD(idOrdFactura);
            admOrdFc.ActualizaDatosFacturacion(idOrdFactura.ToString(), "", ordFac.FechaInicio.ToString ("dd/MM/yyyy"), ordFac.Importe, cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura, 0);
        }

        private void CancelaOrden(int IdServicio, int idOrdFactura)
        {
            //cpplib.admOrdenFactura admOrdFc = new cpplib.admOrdenFactura();
            comun.admordenfactura.CambiaEstadoOrdenFactura(idOrdFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.Cancelado);

            //cpplib.admOrdenServicio admOrdSrv = new cpplib.admOrdenServicio();
            if ((comun.admordenservicio.carga(IdServicio).Periodos == 1))
            {
                comun.admordenservicio.CambiaEstadoOrden(IdServicio, cpplib.OrdenServicio.EstadoOrdSvc.Cancelado);
            }
        }
    }
}