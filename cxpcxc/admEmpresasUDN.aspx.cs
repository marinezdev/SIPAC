using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admEmpresasUDN : Utilerias.Comun
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

        protected void rptEmpresasUnidadNegocio_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                trCliente.Visible = true;
                dpEmpresa.Enabled = false;
                dpUDN.Enabled = false;
                string[] commandArgs = ((ImageButton)e.CommandSource).CommandArgument.Split(new char[] { ',' });

                dpUDN.SelectedValue = commandArgs[1];

                bool valor = comun.admempresasunidadnegocio.SeleccionarEstadoActual(commandArgs[0], commandArgs[1]);
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
            cpplib.EmpresasUnidadNegocio empudn = new cpplib.EmpresasUnidadNegocio();
            empudn.IdUDN = int.Parse(dpUDN.SelectedValue);
            empudn.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            empudn.Activo = true;
            if (comun.admempresasunidadnegocio.Agregar(empudn))
            {
                LlenarEmpresasUnidadNegocio();
            }
            else
            {
                ltMsg.Text = "La Unidad de Negocio ya está asignada a la empresa seleccionada.";
            }
        }

        private void LlenarEmpresasUnidadNegocio()
        {
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            //rptEmpresasUnidadNegocio.DataSource = comun.admempresasunidadnegocio.Seleccionar(dpEmpresa.SelectedValue);
            //rptEmpresasUnidadNegocio.DataBind();
            LlenarControles.LlenarRepeater(ref rptEmpresasUnidadNegocio, comun.admempresasunidadnegocio.Seleccionar(dpEmpresa.SelectedValue));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Obtener los proyectos que pertenecen a la empresa seleccionada
            dpEmpresa.Enabled = true;
            dpUDN.Enabled = true;
            //List<cpplib.CatClientes> lstClte = (new cpplib.admCatClientes()).ListaClientesXEmpresa(dpEmpresa.SelectedValue);
            LlenarControles.LlenarDropDownList(ref dpUDN, comun.admcatunidadnegocio.ListaTodosUnidadNegocio(), "Titulo", "Id");
            //dpUDN.DataSource = comun.admcatunidadnegocio.ListaTodosUnidadNegocio();
            //dpUDN.DataTextField = "Titulo";
            //dpUDN.DataValueField = "Id";
            //dpUDN.DataBind();
            //dpUDN.Items.Insert(0, (new ListItem("Seleccionar", "0")));
            trCliente.Visible = true;
            btnGuardar.Visible = true;
            trEstado.Visible = false;
            btnGuardarModificado.Visible = false;

            LlenarEmpresasUnidadNegocio();
        }

        protected void btnGuardarModificado_Click(object sender, EventArgs e)
        {
            //Guardar la modificación hecha por el usuario
            ltMsg.Text = "";
            //cpplib.admEmpresasClientes admcliemp = new cpplib.admEmpresasClientes();
            cpplib.EmpresasUnidadNegocio empudn = new cpplib.EmpresasUnidadNegocio();
            empudn.IdUDN = int.Parse(dpUDN.SelectedValue);
            empudn.IdEmpresa = int.Parse(dpEmpresa.SelectedValue);
            empudn.Activo = chkActivo.Checked == true ? true : false;
            if (comun.admempresasunidadnegocio.Modificar(empudn))
            {
                dpEmpresa.Enabled = true;
                dpUDN.Enabled = true;
                trCliente.Visible = false;
                trEstado.Visible = false;
                btnGuardarModificado.Visible = false;
                LlenarEmpresasUnidadNegocio();
            }
            else
            {
                ltMsg.Text = "No se pudo cambiar el estado del registro.";
            }
        }












    }
}