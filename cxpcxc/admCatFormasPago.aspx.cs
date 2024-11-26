using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatFormasPago : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["IdEmp"] != null)
                {
                    hdIdEmpresa.Value = Convert.ToString(Request.Params["IdEmp"]);
                    this.PintaEmpresa();
                    this.llenaGridDatos();
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { this.Response.Redirect("espera.aspx"); }

        private void PintaEmpresa()
        {
            cpplib.Empresa oEmpresa = comun.admcatempresa.carga(int.Parse(hdIdEmpresa.Value));  //(new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            ltEmpresa.Text = oEmpresa.Nombre;
        }

        private void llenaGridDatos()
        {
            //cpplib.admCatFormasPago admcat = new cpplib.admCatFormasPago();
            //List<cpplib.CatFormaPago> lista = admcat.ListaFormasPago(hdIdEmpresa.Value);
            //rptfrmsPago.DataSource = comun.admcatformaspago.ListaFormasPago(hdIdEmpresa.Value);
            //rptfrmsPago.DataBind();
            LlenarControles.LlenarRepeater(ref rptfrmsPago, comun.admcatformaspago.ListaFormasPago(hdIdEmpresa.Value));
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatFormasPago admCat = new cpplib.admCatFormasPago();
            cpplib.CatFormaPago oCat = RecuperaDatos();
            if (!comun.admcatformaspago.Existe(oCat))
            {
                if (comun.admcatformaspago.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "La condicion de pago ya esta registrada"; }
        }

        private cpplib.CatFormaPago RecuperaDatos()
        {
            cpplib.CatFormaPago oCat = new cpplib.CatFormaPago();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txTitulo.Text;
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txTitulo.Text = String.Empty;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatFormasPago admCodPag = new cpplib.admCatFormasPago();
            cpplib.CatFormaPago oFrmPag = RecuperaDatos();
            oFrmPag.Id = Convert.ToInt32(hdIdCat.Value);
            if (!comun.admcatformaspago.Existe(oFrmPag))
            {
                comun.admcatformaspago.modifica(oFrmPag);
                this.Limpiar();
                this.llenaGridDatos();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "La condicion de pago ya esta registrada"; }
        }

        protected void rptCodPago_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatFormasPago  admCodPag = new cpplib.admCatFormasPago();
                cpplib.CatFormaPago oFrmPag = comun.admcatformaspago.carga(Convert.ToInt32(hdIdCat.Value));
                txTitulo.Text = oFrmPag.Titulo;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }
    }
}