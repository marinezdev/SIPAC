using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
    public class admEmpresasUnidadNegocio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        public List<EmpresasUnidadNegocio> Seleccionar(string idempresa)
        {
            List<EmpresasUnidadNegocio> respuesta = new List<EmpresasUnidadNegocio>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasUnidadNegocio_Seleccionar_PorEmpresa " + idempresa);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(Armar(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool SeleccionarEstadoActual(string idempresa, string idudn)
        {
            bool respuesta = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasUnidadNegocio_Seleccionar_EstadoActual " + idempresa + "," + idudn);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull("activo"))
                    respuesta = bool.Parse(datos.Rows[0]["activo"].ToString());
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private EmpresasUnidadNegocio Armar(DataRow pRegistro)
        {
            EmpresasUnidadNegocio respuesta = new EmpresasUnidadNegocio();
            if (!pRegistro.IsNull("IdEmpresa"))
                respuesta.IdEmpresa = int.Parse(pRegistro["idempresa"].ToString());
            if (!pRegistro.IsNull("IdUDN"))
                respuesta.IdUDN = int.Parse(pRegistro["idudn"].ToString());
            if (!pRegistro.IsNull("Activo"))
                respuesta.Activo = bool.Parse(pRegistro["activo"].ToString());
            if (!pRegistro.IsNull("empresa"))
                respuesta.NombreEmpresa = pRegistro["empresa"].ToString();
            if (!pRegistro.IsNull("titulo"))
                respuesta.TituloUDN = pRegistro["titulo"].ToString();
            return respuesta;
        }


        public bool Agregar(EmpresasUnidadNegocio items)
        {
            bool resultado = false;
            string consulta = "exec EmpresasUnidadNegocio_Agregar " + items.IdEmpresa + "," + items.IdUDN + "," + items.Activo;
            mbd.BD BD = new mbd.BD();
            try
            {
                BD.EjecutaCmd(consulta);
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                BD.CierraBD();
            }
            return resultado;
        }

        public bool Modificar(EmpresasUnidadNegocio items)
        {
            bool resultado = false;
            int valor = items.Activo == true ? 1 : 0;
            string consulta = "exec EmpresasunidadNegocio_Modificar " + items.IdEmpresa + "," + items.IdUDN + "," + valor;
            mbd.BD BD = new mbd.BD();
            try
            {
                BD.EjecutaCmd(consulta);
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                BD.CierraBD();
            }
            return resultado;
        }
    }

    public class EmpresasUnidadNegocio
    {
        public int IdEmpresa { get; set; }
        public int IdUDN { get; set; }
        public bool Activo { get; set; }

        public string NombreEmpresa { get; set; }
        public string TituloUDN { get; set; }
    }
}
