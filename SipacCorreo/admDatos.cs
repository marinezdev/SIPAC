using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
 
namespace SipacCorreo
{
    public class admDatos
    {
        public List<string> ListaEmpresas()
        {
            List<string> respuesta = new List<string>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Empresas");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(reg["Id"].ToString()); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

       

        public DataTable DaSolicitudesPorAutorizar(string IdEmpresa)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM trf_Solicitud where IdEmpresa=" + IdEmpresa + " and Estado=10");
            SqlCmd.Append(" order by IdSolicitud");
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public List<credencial> DaUsuariosEnvioCorreoXBloqueAutorizacion(string IdEmpresa)
        {
            List<credencial> resultado = new List<credencial>();
            String SqlCmd = "select * from usuario where IdUsr>0 and IdEmpresa=" + IdEmpresa + " and Grupo>=" + credencial.usrGrupo.Direccion.ToString("d") + " and TipoRecCorreo=2";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { resultado.Add(armaCred(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        private credencial armaCred(DataRow pRegistro)
        {
            credencial resultado = new credencial();
            if (!pRegistro.IsNull("IdUsr")) { resultado.IdUsr = Convert.ToInt32(pRegistro["IdUsr"]); }
            if (!pRegistro.IsNull("Usuario")) { resultado.Usuario = (Convert.ToString(pRegistro["Usuario"])).Trim(); }
            if (!pRegistro.IsNull("Clave")) { resultado.Clave = (Convert.ToString(pRegistro["Clave"])).Trim(); }
            if (!pRegistro.IsNull("Grupo")) { resultado.Grupo = (credencial.usrGrupo)(pRegistro["Grupo"]); }
            if (!pRegistro.IsNull("UnidadNegocio")) { resultado.UnidadNegocio = Convert.ToInt32(pRegistro["UnidadNegocio"]); }
            if (!pRegistro.IsNull("Estado")) { resultado.Estado = (credencial.usrEstado)pRegistro["Estado"]; }
            if (!pRegistro.IsNull("Nombre")) { resultado.Nombre = Convert.ToString(pRegistro["Nombre"]); }
            if (!pRegistro.IsNull("IdEmpresa")) { resultado.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]); }
            if (!pRegistro.IsNull("Correo")) { resultado.Correo = (Convert.ToString(pRegistro["Correo"])).Trim(); }
            if (!pRegistro.IsNull("TipoRecCorreo")) { resultado.TipoRecCorreo = Convert.ToInt32(pRegistro["TipoRecCorreo"]); }
            return resultado;
        }

    }
}
