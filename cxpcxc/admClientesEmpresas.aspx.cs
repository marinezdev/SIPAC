using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admClientesEmpresas : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        { 
            if (Session["credencial"] == null) 
                Response.Redirect("Default.aspx"); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];                
                this.llenaCombos();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            this.Response.Redirect("espera.aspx"); 
        }

        protected void rptEmpresasClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                trCliente.Visible = true;
                dpEmpresa.Enabled = false;
                dpCliente.Enabled = false;
                string[] commandArgs = ((ImageButton)e.CommandSource).CommandArgument.Split(new char[] { ',' });

                dpCliente.SelectedValue = commandArgs[1];

                bool valor = comun.admempresasclientes.SeleccionarEstadoActual(commandArgs[0], commandArgs[1]);
                chkActivo.Checked = valor ? true : false;

                btnGuardar.Visible = false;
                trEstado.Visible = true;
                btnGuardarModificado.Visible = true;
            }
        }

        private void llenaCombos()
        {
            //List<cpplib.Empresa> Lista = (new cpplib.admCatEmpresa()).ListaEmpresas();
            LlenarControles.LlenarDropDownList(ref dpEmpresa, comun.admcatempresa.ListaEmpresas(), "Nombre", "Id");
            //dpEmpresa.DataSource = comun.admcatempresa.ListaEmpresas();
            //dpEmpresa.DataTextField = "Nombre";
            //dpEmpresa.DataValueField = "Id";
            //dpEmpresa.DataBind();
            //dpEmpresa.Items.Insert(0, (new ListItem("Seleccionar", "0")));

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            cpplib.EmpresasClientes emcli = new cpplib.EmpresasClientes();
            emcli.IdCliente = int.Parse(dpCliente.SelectedValue);
            emcli.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            emcli.Activo = true;
            if (admcliemp.Agregar(emcli))
            {
                LlenarEmpresasClientes();
            }
            else 
            { 
                ltMsg.Text = "El cliente ya está asignado a la empresa seleccionada."; 
            }
        }

        private void LlenarEmpresasClientes()
        {
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            //rptEmpresasClientes.DataSource = comun.admempresasclientes.Seleccionar(dpEmpresa.SelectedValue);
            //rptEmpresasClientes.DataBind();
            LlenarControles.LlenarRepeater(ref rptEmpresasClientes, comun.admempresasclientes.Seleccionar(dpEmpresa.SelectedValue));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Obtener los clientes que pertenecen a la empresa seleccionada
            dpEmpresa.Enabled = true;
            dpCliente.Enabled = true;
            //List<cpplib.CatClientes> lstClte = (new cpplib.admCatClientes()).ListaClientesXEmpresa(dpEmpresa.SelectedValue);
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(dpEmpresa.SelectedValue), "Nombre", "Id");
            //dpCliente.DataSource = comun.admcatclientes.ListaClientesXEmpresa(dpEmpresa.SelectedValue);
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));
            trCliente.Visible = true;
            btnGuardar.Visible = true;
            trEstado.Visible = false;
            btnGuardarModificado.Visible = false;

            LlenarEmpresasClientes();
        }

        protected void btnGuardarModificado_Click(object sender, EventArgs e)
        {
            //Guardar la modificación hecha por el usuario
            ltMsg.Text = "";
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            cpplib.EmpresasClientes emcli = new cpplib.EmpresasClientes();
            emcli.IdCliente = int.Parse(dpCliente.SelectedValue);
            emcli.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            emcli.Activo = chkActivo.Checked == true ? true : false;
            if (comun.admempresasclientes.Modificar(emcli))
            {
                dpEmpresa.Enabled = true;
                dpCliente.Enabled = true;
                trCliente.Visible = false;
                trEstado.Visible = false;
                btnGuardarModificado.Visible = false;
                LlenarEmpresasClientes();                
            }
            else
            {
                ltMsg.Text = "No se pudo cambiar el estado del registro.";
            }
        }
    }
}