using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class admAcceso : Utilerias.Comun
    {
        
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.llenaDatosUsuario();
            }
        }

        private void llenaDatosUsuario() {
            int idUsr=((cpplib.credencial)Session["credencial"]).IdUsr;
            cpplib.credencial oCdr = comun.admcredencial.carga (idUsr);

            lbNombre.Text = oCdr.Nombre;
            lbUsrActual.Text = oCdr.Usuario;

            if ((oCdr.Grupo == cpplib.credencial.usrGrupo.Direccion) || (oCdr.Grupo == cpplib.credencial.usrGrupo.Presidencia))
            { 
                pnEnvioCorreo.Visible = true;

                if (oCdr.TipoRecCorreo==0 ) { chkSinNotificar.Checked = true; }
                if (oCdr.TipoRecCorreo == 1) { chkSol.Checked = true; }
                if (oCdr.TipoRecCorreo == 2) { chkBloque.Checked = true; }
            }
        }
        
        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("espera.aspx");}

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
            //cpplib.admCredencial admCd = new cpplib.admCredencial();
            
            if (comun.admcredencial.usuarioClaveCorrecto(lbUsrActual.Text, txClaveActual.Text.Trim()))
            {
                if (!comun.admcredencial.existe(txNvoUsuario.Text,oCdr.IdUsr))
                  {
                      comun.admcredencial.modificaClave(oCdr.IdUsr,txNvoUsuario.Text, txNuevaClave.Text );
                      comun.admcredencial.desconecta(oCdr.IdUsr);
                      Session.Clear();
                      Response.Redirect("admConfirmaAcceso.aspx?Id=" + oCdr.IdUsr.ToString ());
                  }
                else { ltMsg.Text = "El usu" +
                        "0rio ya esta registrado con otra persona";}
            }
            else ltMsg.Text = "La clave actual no es correcta";
        }

        protected void ServicioCorreo_CheckedChanged(object sender, EventArgs e)
        {
            cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
            int Valor = 0;
            if (chkSinNotificar.Checked) { Valor = 0; }
            if (chkSol.Checked) { Valor = 1; }
            if (chkBloque.Checked) { Valor = 2; }
            //cpplib.admCredencial admCdr = new cpplib.admCredencial();
            comun.admcredencial.ActualizaTipoRecebcionCorreo(oCdr.IdUsr, Valor);
        }
    }
}