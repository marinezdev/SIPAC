using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class esperaV1 : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.clsSIPAC negocio = null;
                List<cpplib._usuarioPerfiles> _perfiles = null;

                negocio = new cpplib.clsSIPAC(); //quitar
                cpplib.credencial _usuarioCredencial = (cpplib.credencial)Session["credencial"];

                lbMstNombreUsuario.Text = _usuarioCredencial.Nombre.ToString();
                _perfiles = comun.clssipac.usuarios_GetPerfiles(_usuarioCredencial.Nombre.ToString());
                if (_perfiles.Count > 2)
                {
                    List<cpplib._usuarioRegistro> _userAll = null;
                    _userAll = comun.clssipac.usuarios_GetRegistros(_usuarioCredencial.Nombre.ToString());
                    rptRegistros.DataSource = _userAll;
                    rptRegistros.DataBind();
                    _userAll = null;
                }
                else
                {
                    Response.Redirect("espera.aspx");
                }

                negocio = null;
                _perfiles = null;
            }
        }

        protected void rptRegistros_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Registrar")
            {
                int IdUsuario = int.Parse(e.CommandArgument.ToString());
                redo_login(IdUsuario);
            }
        }

        protected void rptRegistros_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        private void redo_login(int IdUsaurio)
        {
            // Declaración e inicialización de Varibales
            cpplib.clsSIPAC negocio = null;
            List<cpplib._usuarioRegistro> _userData = null;
            cpplib.credencial _usuarioCredencial = (cpplib.credencial)Session["credencial"];
            negocio = new cpplib.clsSIPAC();

            // Obtenemos los datos para el login en SIPAC V0
            _userData = comun.clssipac.usuarios_GetRegistro(IdUsaurio);

            if (_userData.Count == 1)
            {
                cpplib.admCredencial oAdmCredencial = new cpplib.admCredencial();
                if (comun.admcredencial.usuarioClaveCorrecto(_userData[0].Usuario.ToString(), _userData[0].Clave.ToString()))
                {
                    cpplib.credencial oCredencial = comun.admcredencial.carga(_userData[0].Usuario.ToString(), _userData[0].Clave.ToString());
                    if (oCredencial.Estado == cpplib.credencial.usrEstado.Activo)
                    {
                        if (!comun.admcredencial.estaConectado(_userData[0].Usuario.ToString()))
                        {
                            comun.admcredencial.registraIdSession(oCredencial.IdUsr, Session.SessionID);
                            Session["credencial"] = oCredencial;

                            System.Web.Security.FormsAuthentication.SetAuthCookie(oCredencial.Usuario, false);
                            Response.Redirect("espera.aspx");
                        }
                        else 
                        {
                            lblInformacion.Text = "USUARIO YA CONECTADO EN OTRA SESION"; 
                        }
                    }
                    else 
                    {
                        lblInformacion.Text = "USUARIO TEMPORALMENTE SUSPENDIDO"; 
                    }
                }
                else 
                {
                    lblInformacion.Text = "USUARIO INCORRECTO"; 
                }
            }
            else
            {
                lblInformacion.Text = "x2007010005. No se pudó determinar el usuario a utilizar para entrar al sistema.";
                lblInformacion.Visible = true;
            }

            // Liberación de Recursos
            negocio = null;
            _usuarioCredencial = null;
            _userData = null;
        }
    }
}