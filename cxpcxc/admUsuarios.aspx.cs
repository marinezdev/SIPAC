using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admUsuarios : Utilerias.Comun
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
                    this.llenagrupo();
                    this.LlenaListaUsuarios();
                    this.llenaUnidadNegocio();
                }
            }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { this.Response.Redirect("espera.aspx"); }

        private void PintaEmpresa() {
            cpplib.Empresa oEmpresa = comun.admcatempresa.carga(int.Parse(hdIdEmpresa.Value)); //(new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value));
            ltEmpresa.Text = oEmpresa.Nombre;
        }

        private void llenagrupo() {
                    
            foreach (int value in Enum.GetValues(typeof(cpplib.credencial.usrGrupo)))
            {
                if (value !=100){
                    var name = Enum.GetName(typeof(cpplib.credencial.usrGrupo), value);
                    dpGrupo.Items.Add(new ListItem(name, value.ToString ()));
                }
            }
        }

        private void llenaUnidadNegocio()
        {
            //List<cpplib.valorTexto> oRsl = (new cpplib.admCatUnidadNegocio()).daComboUnidadNegocio(hdIdEmpresa.Value);
            LlenarControles.LlenarDropDownList(ref dpUdNegocio, comun.admcatunidadnegocio.daComboUnidadNegocio(hdIdEmpresa.Value), "Texto", "Valor");
            //dpUdNegocio.DataSource = comun.admcatunidadnegocio.daComboUnidadNegocio(hdIdEmpresa.Value);
            //dpUdNegocio.DataValueField = "Valor";
            //dpUdNegocio.DataTextField = "Texto";
            //dpUdNegocio.DataBind();
        }
        
        protected void rptProveedor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdUsr.Value = e.CommandArgument.ToString();
                //cpplib.admCredencial admUsr = new cpplib.admCredencial();
                cpplib.credencial oCrd = comun.admcredencial.carga(int.Parse(hdIdUsr.Value)); //admUsr.carga(Convert.ToInt32(hdIdUsr.Value));
                txNombre.Text = oCrd.Nombre;
                txUsuario.Text = oCrd.Usuario;
                txClave.Text = oCrd.Clave;
                dpGrupo.SelectedValue = Convert.ToInt32 (oCrd.Grupo).ToString ();
                dpUdNegocio.SelectedValue = oCrd.UnidadNegocio.ToString();
                txCorreo.Text = oCrd.Correo;
                                
                if (oCrd.Estado.Equals(cpplib.credencial .usrEstado.Activo )) { chkEstado.Checked = true; } else { chkEstado.Checked = false; }
                
                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //cpplib.admCredencial admCrd = new cpplib.admCredencial();
            cpplib.credencial oCrd = RecuperaDatos();
            if (!comun.admcredencial.existe(oCrd.Usuario))
            {
                int Id = comun.admcredencial.nuevo(oCrd);
                this.Limpiar();
                this.LlenaListaUsuarios ();
            }
            else { ltMsg.Text = "El Usuario ya existe"; }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //cpplib.admCredencial admCd = new cpplib.admCredencial();
            cpplib.credencial  oCd = RecuperaDatos();
            oCd.IdUsr = Convert.ToInt32(hdIdUsr.Value);

            if (!comun.admcredencial.existe(txUsuario.Text.Trim(), Convert.ToInt32 (hdIdUsr.Value)))
            {
                comun.admcredencial.modifica(oCd);
                this.Limpiar();
                this.LlenaListaUsuarios();
                btnModificar.Visible = false;
                btnModCancela.Visible = false;
                btnGuardar.Visible = true;
            }
            else { ltMsg.Text = "El usuario ya esta registrado con otra persona";}
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
            ltMsg.Text = "";
            txNombre.Text = String.Empty;
            txUsuario .Text = String.Empty;
            txClave .Text = String.Empty;
            dpUdNegocio.SelectedIndex = 0;
            dpGrupo.SelectedIndex = 0;
            chkEstado.Checked = true;
            txCorreo.Text = "";
        }

        private cpplib.credencial RecuperaDatos()
        {
            cpplib.credencial  oCrd = new cpplib.credencial ();
            oCrd.Nombre = txNombre.Text;
            oCrd.Usuario = txUsuario.Text;
            oCrd.Clave = txClave.Text;
            oCrd.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            oCrd.UnidadNegocio = Convert.ToInt32 (dpUdNegocio.SelectedValue);
            oCrd.Grupo = (cpplib.credencial.usrGrupo)Convert.ToInt32(dpGrupo.SelectedValue);
            oCrd.Estado =(cpplib.credencial.usrEstado)Convert.ToInt32(chkEstado.Checked);
            oCrd.Correo = txCorreo.Text.Trim();
            
            return oCrd;
        }

        private void LlenaListaUsuarios()
        {
            //cpplib.admCredencial  admCd = new cpplib.admCredencial();
            //List<cpplib.credencial> lista = admCd.ListaUsuariosxEmpresa(hdIdEmpresa.Value);
            //rptUsuarios .DataSource = comun.admcredencial.ListaUsuariosxEmpresa(hdIdEmpresa.Value);
            //rptUsuarios.DataBind();
            LlenarControles.LlenarRepeater(ref rptUsuarios, comun.admcredencial.ListaUsuariosxEmpresa(hdIdEmpresa.Value));
        }
    }
}