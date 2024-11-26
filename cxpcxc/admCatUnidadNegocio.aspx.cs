using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatUnidadNegocio : Utilerias.Comun
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
            //cpplib.admCatUnidadNegocio admcat = new cpplib.admCatUnidadNegocio();
            //List<cpplib.CatUnidadNegocio > lista = admcat.ListaUnidadNegocio(hdIdEmpresa.Value);
            //rptUndNegocio.DataSource = comun.admcatunidadnegocio.ListaUnidadNegocio(hdIdEmpresa.Value);
            //rptUndNegocio.DataBind();
            LlenarControles.LlenarRepeater(ref rptUndNegocio, comun.admcatunidadnegocio.ListaUnidadNegocio(hdIdEmpresa.Value));
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatUnidadNegocio admoUNeg = new cpplib.admCatUnidadNegocio();
            cpplib.CatUnidadNegocio  oUNeg = RecuperaDatos();
            oUNeg.Id = Convert.ToInt32(hdIdCat.Value);
            if (!comun.admcatunidadnegocio.Existe(oUNeg))
            {
                comun.admcatunidadnegocio.modifica(oUNeg);
                this.Limpiar();
                this.llenaGridDatos();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "La Unidad de Negocio ya existe"; }
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
            btnGuardar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatUnidadNegocio admCat = new cpplib.admCatUnidadNegocio();
            cpplib.CatUnidadNegocio oCat = RecuperaDatos();
            if (!comun.admcatunidadnegocio.Existe(oCat))
            {
                if (comun.admcatunidadnegocio.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "La Unidad de Negocio ya existe"; }
        }

        private cpplib.CatUnidadNegocio RecuperaDatos()
        {
            cpplib.CatUnidadNegocio oCat = new cpplib.CatUnidadNegocio();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txUndNeg.Text;
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txUndNeg.Text = String.Empty;
        }

        protected void rptUndNegocio_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatUnidadNegocio admProy = new cpplib.admCatUnidadNegocio();
                cpplib.CatUnidadNegocio oCodpag = comun.admcatunidadnegocio.carga(int.Parse(hdIdCat.Value));  //admProy.carga(Convert.ToInt32(hdIdCat.Value));
                txUndNeg.Text = oCodpag.Titulo;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }
    }
}