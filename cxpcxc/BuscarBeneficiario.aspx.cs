using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class BuscarBeneficiario : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LlenaComboProveedores();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesRegistro.aspx"); }

        protected void dpProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpProveedor.SelectedValue != "0")
            {
                hdIdPvd.Value = dpProveedor.SelectedValue;
                this.PintaProveedor(dpProveedor.SelectedValue);
                this.llenaCuentas(dpProveedor.SelectedValue);
                this.pnCuentas.Visible = true;
            }
            else { this.pnCuentas.Visible = false; }
        }


        private void PintaProveedor(String IdPvd)
        {
            //cpplib.admCatProveedor admPvd = new cpplib.admCatProveedor();
            cpplib.CatProveedor oPvd = comun.admcatproveedor.carga(int.Parse(IdPvd)); //admPvd.carga(Convert.ToInt32(IdPvd));
            lbNombre.Text = oPvd.Nombre;
            lbRfc.Text = oPvd.Rfc;
            lbContacto.Text = oPvd.Contacto;
            lbCorreo.Text = oPvd.Correo;
            lbTelefono.Text = oPvd.Telefono;
            lbExtencion.Text = oPvd.Extencion;
        }

        private void llenaCuentas(String IdPvd)
        {
            //cpplib.admCuenta admCta = new cpplib.admCuenta();
            //List<cpplib.Cuenta> lista = admCta.ListaCuentas(IdPvd);
            //rptCuentas.DataSource = comun.admcuenta.ListaCuentas(IdPvd);
            //rptCuentas.DataBind();
            LlenarControles.LlenarRepeater(ref rptCuentas, comun.admcuenta.ListaCuentas(IdPvd));
        }

        private void LlenaComboProveedores() {
            cpplib.credencial ocredencial = (cpplib.credencial)Session["credencial"];
            //cpplib.admCatProveedor admPvd = new cpplib.admCatProveedor();
            List<cpplib.CatProveedor> Lista = new List<cpplib.CatProveedor>();

            string pagina = Request.Params["pg"].ToString();
            if (pagina.Equals("sf"))
            {
                Lista = comun.admcatproveedor.LstProveedoresSinFactura(ocredencial.IdEmpresaTrabajo.ToString()); //Antes: IdEmpresa
            }
            else
            {
                Lista = comun.admcatproveedor.ListaTodosProveedoresActivos(ocredencial.IdEmpresaTrabajo.ToString()); //Antes: IdEmpresa
            }
            LlenarControles.LlenarDropDownList(ref dpProveedor, Lista, "Nombre", "Id");
            //dpProveedor.DataSource = Lista;
            //dpProveedor.DataValueField = "Id";
            //dpProveedor.DataTextField = "NOMBRE";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        
        protected void rptCuentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Registrar")){
                string pagina = Request.Params["pg"].ToString();
                String IdPvd = dpProveedor.SelectedValue;
                if (pagina.Equals("sf")) {
                    Response.Redirect("trf_AltaSolSinFactura.aspx?id=" + IdPvd + "&ct=" + e.CommandArgument.ToString());
                }
                else if (pagina.Equals("rb"))
                {
                    Response.Redirect("trf_AltaSolRecibo.aspx?id=" + IdPvd + "&ct=" + e.CommandArgument.ToString());
                }
            }
        }
    }
}