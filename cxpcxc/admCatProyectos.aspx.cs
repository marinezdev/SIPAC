using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatProyectos : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null){hdIdEmpresa.Value = Convert.ToString(Request.Params["Id"]);}
                else{hdIdEmpresa.Value= ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString () ;}
                this.PintaEmpresa();
                this.llenaGridDatos();
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
            //cpplib.admCatProyectos admcat = new cpplib.admCatProyectos();
            //List<cpplib.CatProyectos > lista = admcat.ListaCatProyectos (hdIdEmpresa.Value);
            //rptProy.DataSource = comun.admcatproyectos.ListaCatProyectos(hdIdEmpresa.Value);
            //rptProy.DataBind();
            LlenarControles.LlenarRepeater(ref rptProy, comun.admcatproyectos.ListaCatProyectos(hdIdEmpresa.Value));
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatProyectos admProy = new cpplib.admCatProyectos();
            cpplib.CatProyectos  oProy = RecuperaDatos();
            oProy.Id = Convert.ToInt32(hdIdCat.Value);
            if (!comun.admcatproyectos.Existe(oProy))
            {
                comun.admcatproyectos.modifica(oProy);
                this.Limpiar();
                this.llenaGridDatos();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "El Proyecto ya existe"; }
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
            //cpplib.admCatProyectos admCat = new cpplib.admCatProyectos();
            cpplib.CatProyectos oCat = RecuperaDatos();
            if (!comun.admcatproyectos.Existe(oCat))
            {
                if (comun.admcatproyectos.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "El Proyecto ya existe"; }
        }

        private cpplib.CatProyectos RecuperaDatos()
        {
            cpplib.CatProyectos oCat = new cpplib.CatProyectos();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txProyecto.Text;
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txProyecto.Text = String.Empty;
        }

        protected void rptProy_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                //hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatProyectos admProy = new cpplib.admCatProyectos();
                cpplib.CatProyectos oCodpag = comun.admcatproyectos.carga(int.Parse(e.CommandArgument.ToString())); //admProy.carga(Convert.ToInt32(hdIdCat.Value));
                txProyecto.Text = oCodpag.Titulo;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

    }
}