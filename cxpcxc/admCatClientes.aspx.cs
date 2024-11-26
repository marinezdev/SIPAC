using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatClientes : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Automatización JLVR
                cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
                this.LlenaGridClientes(Crd.IdEmpresaTrabajo.ToString());
            }
        }
        
        protected void BtnCerrar_Click(object sender, EventArgs e) { this.Response.Redirect("espera.aspx"); }

        protected void rptClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdCte.Value = e.CommandArgument.ToString();
                //cpplib.admCatClientes admCte = new cpplib.admCatClientes ();
                cpplib.CatClientes  oCte = comun.admcatclientes.carga(Convert.ToInt32(hdIdCte.Value));
                txNombre.Text = oCte.Nombre;
                txRfc.Text = oCte.Rfc;
                txDireccion.Text = oCte.Direccion;
                txCiudad.Text = oCte.Ciudad;
                dpEstado.SelectedValue = oCte.Estado;
                txCp.Text = oCte.Cp;
                txContactoProy.Text = oCte.ContactoProyecto ;
                txContactoFact.Text = oCte.ContactoFacturacion;
                txCorreo.Text = oCte.Correo;
                txTelefono.Text = oCte.Telefono;
                txExtencion.Text = oCte.Extencion;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
            ltMsg.Text = "";
            //cpplib.admCatClientes admCte = new cpplib.admCatClientes();
            cpplib.CatClientes oCte = RecuperaDatos();
            if (!comun.admcatclientes.Existe(oCte.Rfc))
            {
                int Id = comun.admcatclientes.nuevo(oCte);
                this.Limpiar();
                this.LlenaGridClientes(Crd.IdEmpresaTrabajo.ToString());

            }
            else { ltMsg.Text = "El cliente ya existe"; }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
            //cpplib.admCatClientes admCte= new cpplib.admCatClientes();
            cpplib.CatClientes  oCte = RecuperaDatos();
            oCte.Id = Convert.ToInt32(hdIdCte.Value);
            comun.admcatclientes.modifica(oCte);
            this.Limpiar();
            this.LlenaGridClientes(Crd.IdEmpresaTrabajo.ToString());
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        private void Limpiar()
        {
            txNombre.Text = String.Empty;
            txRfc.Text = String.Empty;
            txDireccion.Text = String.Empty;
            txCiudad.Text = String.Empty;
            dpEstado.SelectedIndex= 0;
            txCp.Text = String.Empty;
            txContactoProy .Text = String.Empty;
            txContactoFact .Text = String.Empty;
            txCorreo.Text = String.Empty;
            txTelefono.Text = String.Empty;
            txExtencion.Text = String.Empty;
        }

        private cpplib.CatClientes RecuperaDatos()
        {
            cpplib.CatClientes oCte = new cpplib.CatClientes();
            oCte.Nombre = txNombre.Text.ToUpper();
            oCte.Rfc = txRfc.Text.ToUpper();
            oCte.Direccion = txDireccion.Text;
            oCte.Ciudad = txCiudad.Text;
            oCte.Estado = dpEstado.SelectedValue;
            oCte.Cp = txCp.Text;
            oCte.ContactoProyecto = txContactoProy.Text;
            oCte.ContactoFacturacion  = txContactoFact .Text;
            oCte.Correo = txCorreo.Text;
            oCte.Telefono = txTelefono.Text;
            oCte.Extencion = txExtencion.Text;
            return oCte;
        }

        private void LlenaGridClientes(string idempresa)
        {
            //cpplib.admCatClientes admCte = new cpplib.admCatClientes();
            //List<cpplib.CatClientes> lista = admCte.ListaTodosClientes();
            //List<cpplib.CatClientes> lista = admCte.ListaClientesXEmpresa(idempresa);
            LlenarControles.LlenarRepeater(ref rptClientes, comun.admcatclientes.ListaClientesXEmpresa(idempresa));
            //rptClientes.DataSource = comun.admcatclientes.ListaClientesXEmpresa(idempresa);
            //rptClientes.DataBind();
        }
    }
}