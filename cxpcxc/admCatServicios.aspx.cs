using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatServicios : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["IdEmp"] != null) { hdIdEmpresa.Value = Convert.ToString(Request.Params["IdEmp"]); }
                else { hdIdEmpresa.Value = ((cpplib.credencial)Session["credencial"]).IdEmpresa.ToString(); }
                this.PintaEmpresa();
                this.llenaGridDatos();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){this.Response.Redirect("espera.aspx");}
        
        private void PintaEmpresa()
        {
            cpplib.Empresa oEmpresa = comun.admcatempresa.carga(int.Parse(hdIdEmpresa.Value)); //(new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            ltEmpresa.Text = oEmpresa.Nombre;
        }

        private void llenaGridDatos()
        {
            //cpplib.admCatServicios admcat = new cpplib.admCatServicios ();
            //List<cpplib.catServicios> lista = admcat.ListaServicios(hdIdEmpresa.Value);
            //rptServicios.DataSource = comun.admcatservicios.ListaServicios(hdIdEmpresa.Value);
            //rptServicios.DataBind();
            LlenarControles.LlenarRepeater(ref rptServicios, comun.admcatservicios.ListaServicios(hdIdEmpresa.Value));
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatServicios admSrv = new cpplib.admCatServicios();
            cpplib.catServicios oProy = RecuperaDatos();
            oProy.Id = Convert.ToInt32(hdIdCat.Value);
            if (!comun.admcatservicios.Existe(oProy))
            {
                comun.admcatservicios.modifica(oProy);
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
            //cpplib.admCatServicios admCat = new cpplib.admCatServicios();
            cpplib.catServicios  oCat = RecuperaDatos();
            if (!comun.admcatservicios.Existe(oCat))
            {
                if (comun.admcatservicios.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "El Proyecto ya existe"; }
        }

        private cpplib.catServicios  RecuperaDatos()
        {
            cpplib.catServicios oCat = new cpplib.catServicios();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txServicio.Text;
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txServicio.Text = String.Empty;
        }

        protected void rptServicios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatServicios admSrv = new cpplib.admCatServicios();
                cpplib.catServicios oCodpag = comun.admcatservicios.carga(int.Parse(hdIdCat.Value)); //admSrv.carga(Convert.ToInt32(hdIdCat.Value));
                txServicio.Text = oCodpag.Titulo;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }
    }
}