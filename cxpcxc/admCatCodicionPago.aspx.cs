using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatCodicionPago : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    hdIdEmpresa.Value = Convert.ToString(Request.Params["Id"]);
                    this.PintaEmpresa();
                    this.llenaGridDatos();
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { this.Response.Redirect("espera.aspx"); }

        private void PintaEmpresa()
        {
            cpplib.Empresa oEmpresa = comun.admcatempresa.carga(int.Parse(hdIdEmpresa.Value)); //(new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            ltEmpresa.Text = oEmpresa.Nombre;
        }

        private void llenaGridDatos()
        {
            //cpplib.admCatCondPago admcat = new cpplib.admCatCondPago();
            //List<cpplib.catCondPago> lista = admcat.ListaCondiconesPago (hdIdEmpresa.Value);
            //rptCodPago.DataSource = comun.admcatcondpago.ListaCondiconesPago(hdIdEmpresa.Value);
            //rptCodPago.DataBind();
            LlenarControles.LlenarRepeater(ref rptCodPago, comun.admcatcondpago.ListaCondiconesPago(hdIdEmpresa.Value));
        }
        
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatCondPago admCat = new cpplib.admCatCondPago();
            cpplib.catCondPago oCat = RecuperaDatos();
            if (!comun.admcatcondpago.Existe(oCat))
            {
                if (comun.admcatcondpago.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "La condicion de pago ya esta registrada"; }
        }

        private cpplib.catCondPago RecuperaDatos()
        {
            cpplib.catCondPago oCat = new cpplib.catCondPago();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txTitulo.Text;
            oCat.NumDias= Convert.ToInt32 (txDias.Text);
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txTitulo.Text = String.Empty;
            txDias.Text = "0";
        }

        protected void rptCodPago_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatCondPago admCodPag = new cpplib.admCatCondPago();
                cpplib.catCondPago oCodpag = comun.admcatcondpago.carga(Convert.ToInt32(hdIdCat.Value));
                txTitulo.Text = oCodpag.Titulo;
                txDias.Text = oCodpag.NumDias.ToString () ;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatCondPago admCodPag = new cpplib.admCatCondPago ();
            cpplib.catCondPago oCodPag = RecuperaDatos();
            oCodPag.Id = Convert.ToInt32(hdIdCat.Value);
            if (comun.admcatcondpago.Existe(oCodPag))
            {
                comun.admcatcondpago.modifica(oCodPag);
                this.Limpiar();
                this.llenaGridDatos();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "La condicion de pago no Existe"; }
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }
    }
}