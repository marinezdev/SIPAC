using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SipacCorreo
{
    public class admCredencial
    {
        public int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO usrctrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as IdUsr");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("IdUsr")) { Id = Convert.ToInt32(Datos.Rows[0]["IdUsr"]); }
                }
            }
            return Id;
        }

        public int nuevo(credencial pCredencial)
        {
            int nuevoId = 0;
            nuevoId = daSiguienteIdentificador();
            if (nuevoId > 0)
            {
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO usuario (");
                SqlCmd.Append("IdUsr,");
                SqlCmd.Append("Usuario,");
                SqlCmd.Append("Clave,");
                SqlCmd.Append("Nombre,");
                SqlCmd.Append("UnidadNegocio,");
                SqlCmd.Append("Grupo,");
                SqlCmd.Append("Estado,");
                SqlCmd.Append("Conectado,");
                SqlCmd.Append("Fecha,");
                SqlCmd.Append("IdEmpresa,");
                SqlCmd.Append("Correo,");
                SqlCmd.Append("TipoRecCorreo");
                
                SqlCmd.Append(")");

                SqlCmd.Append(" Values(");
                SqlCmd.Append(nuevoId.ToString());
                SqlCmd.Append(",'" + pCredencial.Usuario + "'");
                SqlCmd.Append(",'" + pCredencial.Clave + "'");
                SqlCmd.Append(",'" + pCredencial.Nombre.ToString() + "'");
                SqlCmd.Append("," + pCredencial.UnidadNegocio.ToString());
                SqlCmd.Append("," + pCredencial.Grupo.ToString("d"));
                SqlCmd.Append("," + pCredencial.Estado.ToString("d"));
                SqlCmd.Append(",'N',NULL");
                SqlCmd.Append("," + pCredencial.IdEmpresa.ToString());
                SqlCmd.Append(",'" + pCredencial.Correo + "'");
                SqlCmd.Append("," + pCredencial.TipoRecCorreo );
                SqlCmd.Append(")");
                mbd.BD BD = new mbd.BD();
                BD.EjecutaCmd(SqlCmd.ToString());
                BD.CierraBD();
            }
            return nuevoId;
        }

        public Boolean usuarioClaveCorrecto(String pUsuario, String pClave)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT Clave FROM usuario WHERE Usuario = '" + pUsuario + "'";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { if (!datos.Rows[0].IsNull(0)) { resultado = ((String)datos.Rows[0][0]).Equals(pClave); } }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public Boolean estaConectado(String pUsuario)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT Conectado FROM usuario WHERE Usuario = '" + pUsuario + "'";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { if (!datos.Rows[0].IsNull(0)) { resultado = !((String)datos.Rows[0][0]).Equals("N"); } }
            datos.Dispose();
            BD.CierraBD();
            //return resultado;
            return false;
        }

        public Boolean existe(String pUsuario)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT IdUsr FROM usuario WHERE Usuario = '" + pUsuario + "'";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public Boolean existe(String pUsuario, int pId)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT IdUsr FROM usuario WHERE Usuario = '" + pUsuario + "' AND IdUsr != " + pId.ToString();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        private credencial arma(DataRow pRegistro)
        {
            credencial resultado = new credencial();
            if (!pRegistro.IsNull("IdUsr")) { resultado.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]); }
            if (!pRegistro.IsNull("Usuario")) { resultado.Usuario = (Convert.ToString(pRegistro["Usuario"])).Trim(); }
            if (!pRegistro.IsNull("Clave")) { resultado.Clave = (Convert.ToString(pRegistro["Clave"])).Trim(); }
            if (!pRegistro.IsNull("Grupo")) { resultado.Grupo = (credencial.usrGrupo)(pRegistro["Grupo"]); }
            if (!pRegistro.IsNull("UnidadNegocio")) { resultado.UnidadNegocio= Convert.ToInt32(pRegistro["UnidadNegocio"]); }
            if (!pRegistro.IsNull("Estado")) { resultado.Estado = (credencial.usrEstado)pRegistro["Estado"]; }
            if (!pRegistro.IsNull("Nombre")) { resultado.Nombre = Convert.ToString (pRegistro["Nombre"]); }
            if (!pRegistro.IsNull("IdEmpresa")) { resultado.IdEmpresa= Convert.ToInt32(pRegistro["IdEmpresa"]); }
            if (!pRegistro.IsNull("Correo")) { resultado.Correo = (Convert.ToString(pRegistro["Correo"])).Trim(); }
            if (!pRegistro.IsNull("TipoRecCorreo")) { resultado.TipoRecCorreo = Convert.ToInt32(pRegistro["TipoRecCorreo"]); }
            return resultado;
        }
                
        public credencial carga(String pUsuario)
        {
            credencial resultado = null;
            String SqlCmd = "SELECT * FROM usuario WHERE Usuario = '" + pUsuario + "'";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public credencial carga(int pId)
        {
            credencial resultado = null;
            String SqlCmd = "SELECT * FROM usuario WHERE IdUsr = " + pId.ToString();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public string daUsuarioDeId(string pId)
        {
            string resultado = "";
            String SqlCmd = "SELECT Usuario FROM usuario WHERE IdUsr = " + pId;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = Convert.ToString(datos.Rows[0][0]).Trim(); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void modifica(credencial pCredencial)
        {
            String SqlCmd = "UPDATE usuario SET ";
            SqlCmd +="Clave='" +pCredencial.Clave  + "'";
            SqlCmd +=",Nombre='" +pCredencial.Nombre  + "'";
            SqlCmd +=",Grupo='" +pCredencial.Grupo.ToString ("d")   + "'";
            SqlCmd += ",Estado=" + pCredencial.Estado.ToString("d");
            SqlCmd += ",UnidadNegocio=" + pCredencial.UnidadNegocio.ToString();
            SqlCmd += ",Correo='" + pCredencial.Correo + "'" ;
            //SqlCmd += ",TipoRecCorreo=" + pCredencial.TipoRecCorreo;

            SqlCmd +="WHERE IdUsr=" + pCredencial.IdUsr.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

        public void modificaClave(int pIdUsr, String pUsuario, String pClave)
        {
            String SqlCmd = "UPDATE usuario SET ";
            SqlCmd += "Usuario='" + pUsuario + "'";
            SqlCmd += ",Clave='" + pClave + "'";
            SqlCmd += "WHERE IdUsr=" + pIdUsr.ToString ();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }
        
        public Boolean desconecta(int pIdUsr)
        {
            Boolean resultado = false;
            String SqlCmd = "UPDATE usuario SET Conectado = 'N', Fecha = NULL WHERE IdUsr=" + pIdUsr.ToString();
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
            return resultado;
        }

        public Boolean registraIdSession(int pIdUsr, String pIdSession)
        {
            Boolean resultado = false;
            String SqlCmd = "UPDATE usuario SET Conectado = '" + pIdSession + "', Fecha = getDate() WHERE IdUsr=" + pIdUsr.ToString();
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
            return resultado;
        }

        public  List<credencial> ListaUsuariosxEmpresa(String IdEmpresa) {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where idEmpresa=" + IdEmpresa + " and IdUsr>0";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) {resultado.Add(arma(reg));}
            datos.Dispose();
            BD.CierraBD();
            return resultado;
       }

        public List<credencial> DaUsuariosxEmpresaxSolicitante(String IdEmpresa)
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Grupo=" + credencial .usrGrupo.Solicitante.ToString ("d")  + " and idEmpresa=" + IdEmpresa;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosSolicitantexEmpresaxUnidNegocio(String IdEmpresa, string pUnidadNegocio)
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Grupo=" + credencial.usrGrupo.Solicitante.ToString("d") + " and idEmpresa=" + IdEmpresa + " and unidadnegocio=" + pUnidadNegocio;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosEnvioCorreoXSolicitudAutorizacion(string IdEmpresa) {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and IdEmpresa=" + IdEmpresa + " and Grupo=" + credencial.usrGrupo.Direccion.ToString("d") + " and TipoRecCorreo=1";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosPresidencia(string IdEmpresa)
        {
            List<credencial> resultado = new List<credencial>();
            //String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and IdEmpresa=" + IdEmpresa + " and Grupo=" + credencial.usrGrupo.Presidencia.ToString("d");
            String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and Grupo=" + credencial.usrGrupo.Presidencia.ToString("d");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosEnvioCorreoXBloqueAutorizacion( string IdEmpresa)
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and IdEmpresa=" + IdEmpresa + " and Grupo=" + credencial.usrGrupo.Direccion.ToString("d") + " and TipoRecCorreo=2";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosAplicacionPago(string IdEmpresa)
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and IdEmpresa=" + IdEmpresa + " and Grupo=" + credencial.usrGrupo.AplicacionPago.ToString("d");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<credencial> DaUsuariosFacturacion()
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and Estado=1 and Grupo=" + credencial.usrGrupo.Facturacion.ToString("d");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void ActualizaTipoRecebcionCorreo( int pIdUsr,int Tipo) {
            String SqlCmd = "UPDATE usuario SET ";
            SqlCmd += "TipoRecCorreo=" + Tipo ;
            SqlCmd += "WHERE IdUsr=" + pIdUsr.ToString();
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd);
            BD.CierraBD();
        }

    }

    [Serializable]
    public class credencial
    {
        public int IdUsr { get; set; }
        public int IdEmpresa { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public usrGrupo Grupo { get; set; }
        public usrEstado Estado { get; set; }
        public string Conectado { get; set; }
        public DateTime Fecha { get; set; }
        private String mNombre = String.Empty;
        public String Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mUnidadNegocio = 0;
        public int UnidadNegocio { get { return mUnidadNegocio; } set { mUnidadNegocio = value; } }
        public string Correo { get; set;}
        private int mTipoRecCorreo = 0;
        public int TipoRecCorreo { get { return mTipoRecCorreo; } set { mTipoRecCorreo=value; } } 
        public enum usrEstado { Desactivado = 0, Activo = 1 }
        public enum usrGrupo { Admsys = 100, Administrador = 70, Presidencia=65, Direccion = 60, Coordinador=55, Contabilidad = 50, AplicacionPago = 40, Captura = 30, Facturacion=25, Autorizacion = 20, Solicitante = 10 }
    }
}
