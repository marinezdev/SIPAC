using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatRechazos : Utilerias.Comun
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
            //cpplib.admCatRechazos admcat = new cpplib.admCatRechazos();
            //List<cpplib.catRechazos > lista = admcat.ListaRechazos (hdIdEmpresa.Value);
            //rptRechazos .DataSource = comun.admcatrechazos.ListaRechazos(hdIdEmpresa.Value);
            //rptRechazos.DataBind();
            LlenarControles.LlenarRepeater(ref rptRechazos, comun.admcatrechazos.ListaRechazos(hdIdEmpresa.Value));
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatRechazos admRchz = new cpplib.admCatRechazos();
            cpplib.catRechazos oRchz = RecuperaDatos();
            oRchz.Id = Convert.ToInt32(hdIdCat.Value);
            if (!comun.admcatrechazos.Existe(oRchz))
            {
                comun.admcatrechazos.modifica(oRchz);
                this.Limpiar();
                this.llenaGridDatos();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "El Rechazo ya existe"; }
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
            //cpplib.admCatRechazos admCat = new cpplib.admCatRechazos();
            cpplib.catRechazos  oCat = RecuperaDatos();
            if (!comun.admcatrechazos.Existe(oCat))
            {
                if (comun.admcatrechazos.Agrega(oCat))
                {
                    this.Limpiar();
                    this.llenaGridDatos();
                }
            }
            else { ltMsg.Text = "El Rechazo ya existe"; }

        }

        private cpplib.catRechazos  RecuperaDatos()
        {
            cpplib.catRechazos oCat = new cpplib.catRechazos();
            oCat.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCat.Titulo = txRechazo.Text;
            return oCat;
        }

        private void Limpiar()
        {
            ltMsg.Text = "";
            txRechazo.Text = String.Empty;
        }

        protected void rptRechazos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                //hdIdCat.Value = e.CommandArgument.ToString();
                //cpplib.admCatRechazos admRchz = new cpplib.admCatRechazos();
                cpplib.catRechazos oRchz = comun.admcatrechazos.carga(int.Parse(e.CommandArgument.ToString())); //admRchz.carga(Convert.ToInt32(hdIdCat.Value));
                txRechazo.Text = oRchz.Titulo;
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }
    }
}