using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_RegistroMontosVariables : Utilerias.Comun
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
                ListaOrdenfactura();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenaCombos()
        {
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            /* Llena clientes*/
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString());        //Anterior: .ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));
        }

        private void ListaOrdenfactura()
        {
            ltMsg.Text = "";
            string Consulta = Daconsulta();
            cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.DaOrdenesVariblesParaActualizarMontos(Consulta);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }
            else
            {
                ltMsg.Text = "No hay información";
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }
            LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
        }

        private string Daconsulta()
        {
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            string resultado = string.Empty;
            resultado = " and IdEmpresa='" + crd.IdEmpresaTrabajo.ToString() + "'";
            if (!dpCliente.SelectedValue.Equals("0")) { resultado += " and  IdCliente='" + dpCliente.SelectedValue + "'"; }
            if (!dpMes.SelectedValue.Equals("0")) { resultado += " and DATEPART(MONTH,FECHAINICIO)=" + dpMes.SelectedValue; }
            if (!dpAño.SelectedValue.Equals("0")) { resultado += " and DATEPART(YEAR,FECHAINICIO)=" + dpAño.SelectedValue; }
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
                cpplib.OrdenFactura oFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                //Coloca el semaforo
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                if (oFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Pagado) { img.ImageUrl = "~/img/action_check.png"; }
                else if (oFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado) { img.ImageUrl = ""; }
                else
                {
                    DateTime FchActual = DateTime.Now;
                    int Dias = Convert.ToInt32((oFac.FechaInicio - FchActual).TotalDays);
                    if ((Dias <= 1) && (oFac.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
            }
        }

        protected void rptsol_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDat") { Response.Redirect("cxc_VerOrdenFactura.aspx?ord=" + e.CommandArgument.ToString() + "&bk=cxc_RegistroMontosVariables"); }
        }

        
    }
}