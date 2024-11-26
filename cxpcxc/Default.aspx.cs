using cpplib;
using System;

namespace cxpcxc
{
    public partial class Default : Utilerias.Comun
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txUsuario.Text.Trim()) && !string.IsNullOrEmpty(txClave.Text.Trim()))
            {
                //cpplib.admCredencial oAdmCredencial = new cpplib.admCredencial();
                if (comun.admcredencial.usuarioClaveCorrecto(txUsuario.Text.Trim(), txClave.Text.Trim()))
                {
                    cpplib.credencial oCredencial = comun.admcredencial.carga(txUsuario.Text.Trim(), txClave.Text.Trim());
                    if (oCredencial.Estado == cpplib.credencial.usrEstado.Activo)
                    {
                        if (!comun.admcredencial.estaConectado(txUsuario.Text.Trim())) //Este método no hace nada, siempre devuelve false
                        { 
                            comun.admcredencial.registraIdSession(oCredencial.IdUsr, Session.SessionID);
                            Session["credencial"] = oCredencial;
                            
                            System.Web.Security.FormsAuthentication.SetAuthCookie(oCredencial.Usuario, false);
                            Response.Redirect("espera.aspx");
                        }
                        else { ltMsg.Text = "USUARIO YA CONECTADO EN OTRA SESION"; }
                    }
                    else { ltMsg.Text = "USUARIO TEMPORALMENTE SUSPENDIDO"; }
                }
                else { ltMsg.Text = "USUARIO INCORRECTO"; }
            }
        }

    }
}