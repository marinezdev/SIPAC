using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admEmpresasProyectos : Utilerias.Comun
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

        protected void rptEmpresasProyectos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                trCliente.Visible = true;
                dpEmpresa.Enabled = false;
                dpProyecto.Enabled = false;
                string[] commandArgs = ((ImageButton)e.CommandSource).CommandArgument.Split(new char[] { ',' });

                dpProyecto.SelectedValue = commandArgs[1];

                bool valor = comun.admempresasproyectos.SeleccionarEstadoActual(commandArgs[0], commandArgs[1]);
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
            //cpplib.admEmpresasProyectos admempproy = new cpplib.admEmpresasProyectos();
            cpplib.EmpresasProyectos emproy = new cpplib.EmpresasProyectos();
            emproy.IdProyecto = int.Parse(dpProyecto.SelectedValue);
            emproy.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            emproy.Activo = true;
            if (comun.admempresasproyectos.Agregar(emproy))
            {
                LlenarEmpresasProyectos();
            }
            else
            {
                ltMsg.Text = "El proyecto ya está asignado a la empresa seleccionada.";
            }
        }

        private void LlenarEmpresasProyectos()
        {
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            //rptEmpresasProyectos.DataSource = comun.admempresasproyectos.Seleccionar(dpEmpresa.SelectedValue);
            //rptEmpresasProyectos.DataBind();
            LlenarControles.LlenarRepeater(ref rptEmpresasProyectos, comun.admempresasproyectos.Seleccionar(dpEmpresa.SelectedValue));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Obtener los proyectos que pertenecen a la empresa seleccionada
            dpEmpresa.Enabled = true;
            dpProyecto.Enabled = true;
            //List<cpplib.CatClientes> lstClte = (new cpplib.admCatClientes()).ListaClientesXEmpresa(dpEmpresa.SelectedValue);
            LlenarControles.LlenarDropDownList(ref dpProyecto, comun.admcatproyectos.ListarTodos(), "Titulo", "Id");
            //dpProyecto.DataSource = comun.admcatproyectos.ListarTodos();
            //dpProyecto.DataTextField = "Titulo";
            //dpProyecto.DataValueField = "Id";
            //dpProyecto.DataBind();
            //dpProyecto.Items.Insert(0, (new ListItem("Seleccionar", "0")));
            trCliente.Visible = true;
            btnGuardar.Visible = true;
            trEstado.Visible = false;
            btnGuardarModificado.Visible = false;

            LlenarEmpresasProyectos();
        }

        protected void btnGuardarModificado_Click(object sender, EventArgs e)
        {
            //Guardar la modificación hecha por el usuario
            ltMsg.Text = "";
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            cpplib.EmpresasProyectos emproy = new cpplib.EmpresasProyectos();
            emproy.IdProyecto = int.Parse(dpProyecto.SelectedValue);
            emproy.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            emproy.Activo = chkActivo.Checked == true ? true : false;
            if (comun.admempresasproyectos.Modificar(emproy))
            {
                dpEmpresa.Enabled = true;
                dpProyecto.Enabled = true;
                trCliente.Visible = false;
                trEstado.Visible = false;
                btnGuardarModificado.Visible = false;
                LlenarEmpresasProyectos();
            }
            else
            {
                ltMsg.Text = "No se pudo cambiar el estado del registro.";
            }
        }


    }
}