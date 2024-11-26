using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admCatEmpresas : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { this.llenaEmpresas (); }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect ("espera.aspx");}

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatEmpresa admEp = new cpplib.admCatEmpresa();
            cpplib.Empresa oEmp = RecuperaDatos();
            if (!comun.admcatempresa.Existe(oEmp))
            {
                int Id = comun.admcatempresa.nueva(oEmp);
                this.Limpiar();
                this.llenaEmpresas ();

            }
            else { ltMsg.Text = "El Proveedor ya existe"; }
        }
        
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCatEmpresa admEp = new cpplib.admCatEmpresa();
            cpplib.Empresa oEmp = RecuperaDatos();
            oEmp.Id = Convert.ToInt32(hdIdEmpresa.Value);
            if (!comun.admcatempresa.Existe(oEmp))
            {
                comun.admcatempresa.modifica(oEmp);
                this.Limpiar();
                this.llenaEmpresas();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else  { ltMsg.Text = "La empresa ya existe"; }
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

        }

        private cpplib.Empresa  RecuperaDatos()
        {
            cpplib.Empresa oEmp= new cpplib.Empresa();
            oEmp.Rfc = txRfc.Text;
            oEmp.Nombre = txNombre.Text;
            return oEmp;
        }

        private void llenaEmpresas()
        {
            //cpplib.admCatEmpresa  admPvd = new cpplib.admCatEmpresa ();
            //List<cpplib.Empresa> lista = admPvd.ListaEmpresas ();
            //rptEmpresas.DataSource = comun.admcatempresa.ListaEmpresas();
            //rptEmpresas.DataBind();
            LlenarControles.LlenarRepeater(ref rptEmpresas, comun.admcatempresa.ListaEmpresas());
        }

        protected void rptEmpresas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdEmpresa .Value = e.CommandArgument.ToString();
                //cpplib.admCatEmpresa admEp = new cpplib.admCatEmpresa();
                cpplib.Empresa oEmp = comun.admcatempresa.carga(Convert.ToInt32(hdIdEmpresa.Value));
                txRfc.Text = oEmp.Rfc;
                txNombre.Text = oEmp.Nombre;
                
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

    }
}