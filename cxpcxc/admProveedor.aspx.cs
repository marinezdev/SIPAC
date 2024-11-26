using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admProveedor : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //if (Request.Params["Id"] != null)
                //{
                    //Automatizacion JLVR 
                    cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
                    hdIdEmpresa.Value = Crd.IdEmpresaTrabajo.ToString();  //Comentado, el valor se toma de la variable de sesión Convert.ToString(Request.Params["Id"]);
                    this.PintaEmpresa();
                    this.LlenaGridProveedores();
                //}
            }
        }

        private void PintaEmpresa()
        {
            cpplib.Empresa oEmpresa = comun.admcatempresa.carga(int.Parse(hdIdEmpresa.Value)); //(new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            ltEmpresa.Text = oEmpresa.Nombre;
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){this.Response.Redirect("espera.aspx");}

        protected void rptProveedor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cuentas")) Response.Redirect("admCuentas.aspx?id=" + e.CommandArgument.ToString() );

            if (e.CommandName.Equals("Editar")) {
                hdIdProveedor.Value = e.CommandArgument.ToString();
                //cpplib.admCatProveedor admPv = new cpplib.admCatProveedor();
                cpplib.CatProveedor oPvd = comun.admcatproveedor.carga(int.Parse(hdIdProveedor.Value)); //admPv.carga(Convert.ToInt32(hdIdProveedor.Value));
                txNombre.Text = oPvd.Nombre.ToUpper();
                txRfc.Text = oPvd.Rfc.ToUpper ();
                txDireccion.Text = oPvd.Direccion;
                txCiudad.Text = oPvd.Ciudad;
                dpEstado.SelectedValue = oPvd.Estado;
                txCp.Text = oPvd.Cp;
                txContacto.Text = oPvd.Contacto;
                txCorreo.Text = oPvd.Correo;
                txTelefono.Text = oPvd.Telefono;
                txExtencion.Text = oPvd.Extencion;
                if (oPvd.Activo.Equals(1)) { chkActivo.Checked = true;} else { chkActivo.Checked = false;}
                chkSinFactura.Checked = Convert.ToBoolean(oPvd.SinFactura);

                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatProveedor admPv = new cpplib.admCatProveedor();
            cpplib.CatProveedor oPvd = RecuperaDatos();
            if (!comun.admcatproveedor.Existe(hdIdEmpresa.Value, oPvd.Rfc))
            {
                int Id= comun.admcatproveedor.nuevo(oPvd);
                this.Limpiar();
                this.LlenaGridProveedores();

            }
            else { ltMsg.Text ="Ya existe un proveedor registrado registrado con esos datos."; }
        }
        
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatProveedor admPv = new cpplib.admCatProveedor();
            cpplib.CatProveedor oPvd = RecuperaDatos();
            oPvd.Id = Convert.ToInt32(hdIdProveedor.Value);
            comun.admcatproveedor.modifica(oPvd);
            this.Limpiar();
            this.LlenaGridProveedores();
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
                
        private void Limpiar() {
            txNombre.Text =String.Empty;
            txRfc.Text = String.Empty;
            txDireccion.Text = String.Empty;
            txCiudad.Text = String.Empty;
            dpEstado.SelectedIndex = 0;
            txCp.Text = String.Empty;
            txContacto.Text = String.Empty;
            txCorreo.Text = String.Empty;
            txTelefono.Text = String.Empty;
            txExtencion.Text = String.Empty;
            chkSinFactura.Checked = false;
            ltMsg.Text = "";
        }

        private cpplib.CatProveedor RecuperaDatos()
        {
            cpplib.CatProveedor oPvd = new cpplib.CatProveedor();
            oPvd.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oPvd.Nombre=txNombre.Text.ToUpper () ;
            oPvd.Rfc = txRfc.Text.ToUpper();
            oPvd.Direccion = txDireccion.Text;
            oPvd.Ciudad = txCiudad.Text;
            oPvd.Estado = dpEstado.SelectedValue;
            oPvd.Cp = txCp.Text;
            oPvd.Contacto=txContacto.Text ;
            oPvd.Correo=txCorreo.Text;
            oPvd.Telefono = txTelefono.Text;
            oPvd.Extencion = txExtencion.Text;
            if (chkActivo.Checked) { oPvd.Activo = 1; } else { oPvd.Activo = 0; }
            if (chkSinFactura.Checked) { oPvd.SinFactura = cpplib.CatProveedor.enSinFactura.SI; }
            return oPvd;
        }

        private void LlenaGridProveedores() {
            //cpplib.admCatProveedor admPvd = new cpplib.admCatProveedor();
            //List<cpplib.CatProveedor> lista = admPvd.ListaTodosProveedores(hdIdEmpresa.Value);
            //rptProveedor.DataSource = comun.admcatproveedor.ListaTodosProveedores(hdIdEmpresa.Value);
            //rptProveedor.DataBind();
            LlenarControles.LlenarRepeater(ref rptProveedor, comun.admcatproveedor.ListaTodosProveedores(hdIdEmpresa.Value));
        }
    }
}