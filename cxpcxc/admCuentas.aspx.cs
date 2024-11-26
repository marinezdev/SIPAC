using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCuentas : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                if (Request.Params["Id"] != null)
                {
                    hdIdPvd.Value = Convert.ToString(Request.Params["Id"]);
                    this.LlenaCuentas(hdIdPvd.Value);
                    this.PintaProveedor(hdIdPvd.Value);
                }    
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admproveedor.aspx?Id=" + hdIdEmpresa.Value); }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCuenta admCta = new cpplib.admCuenta();
            cpplib.Cuenta oCuenta = RecuperaDatos();
            if (!comun.admcuenta.Existe(hdIdPvd.Value,oCuenta.NoCuenta))
            {
                comun.admcuenta.nuevo(oCuenta);
                this.Limpiar();
                this.LlenaCuentas(hdIdPvd.Value);
            }
        }

        private void Limpiar()
        {
            txBanco.Text = String.Empty;
            txCuenta.Text = String.Empty;
            txClabe.Text = String.Empty;
            txSucursal.Text = String.Empty;
            dpMoneda.SelectedIndex =0;
        }

        private cpplib.Cuenta RecuperaDatos()
        {
            cpplib.Cuenta oPvd = new cpplib.Cuenta();
            oPvd.Id = Convert.ToInt32(hdIdPvd.Value);
            oPvd.Banco= txBanco.Text;
            oPvd.NoCuenta = txCuenta.Text;
            oPvd.CtaClabe = txClabe.Text;
            oPvd.Sucursal = txSucursal.Text;
            oPvd.Moneda = dpMoneda.SelectedValue;
            return oPvd;
        }

        private void LlenaCuentas(String IdPvd)
        {
            //cpplib.admCuenta admCta = new cpplib.admCuenta();
            //List<cpplib.Cuenta> lista = admCta.ListaCuentas(IdPvd);
            //rptCuentas.DataSource = comun.admcuenta.ListaCuentas(IdPvd);
            //rptCuentas.DataBind();
            LlenarControles.LlenarRepeater(ref rptCuentas, comun.admcuenta.ListaCuentas(IdPvd));
        }

        private void PintaProveedor(String IdPvd)
        {
            //cpplib.admCatProveedor admPvd = new cpplib.admCatProveedor();
            cpplib.CatProveedor oPvd = comun.admcatproveedor.carga(Convert.ToInt32(IdPvd));
            hdIdEmpresa.Value = oPvd.IdEmpresa.ToString(); 
            lbNombre.Text = oPvd.Nombre;
            lbRfc.Text = oPvd.Rfc;
            lbContacto.Text = oPvd.Contacto;
            lbCorreo.Text = oPvd.Correo;
            lbTelefono.Text = oPvd.Telefono;
            lbExtencion.Text = oPvd.Extencion;
        }

        protected void rptProveedor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Quitar"))
            {
                //cpplib.admCuenta admCta = new cpplib.admCuenta();
                comun.admcuenta.Eliminar(hdIdPvd.Value, e.CommandArgument.ToString());
                this.LlenaCuentas(hdIdPvd.Value);
            }
        }
    }
}