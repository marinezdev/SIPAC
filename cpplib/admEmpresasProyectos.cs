using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
    public class admEmpresasProyectos
    {
        public List<EmpresasProyectos> Seleccionar(string idempresa)
        {
            List<EmpresasProyectos> respuesta = new List<EmpresasProyectos>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasProyectos_Seleccionar_PorEmpresa " + idempresa);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(Armar(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool SeleccionarEstadoActual(string idempresa, string idproyecto)
        {
            bool respuesta = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasProyectos_Seleccionar_EstadoActual " + idempresa + "," + idproyecto);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull("activo"))
                    respuesta = bool.Parse(datos.Rows[0]["activo"].ToString());
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private EmpresasProyectos Armar(DataRow pRegistro)
        {
            EmpresasProyectos respuesta = new EmpresasProyectos();
            if (!pRegistro.IsNull("IdEmpresa"))
                respuesta.IdEmpresa = int.Parse(pRegistro["idempresa"].ToString());
            if (!pRegistro.IsNull("IdProyecto"))
                respuesta.IdProyecto = int.Parse(pRegistro["idproyecto"].ToString());
            if (!pRegistro.IsNull("Activo"))
                respuesta.Activo = bool.Parse(pRegistro["activo"].ToString());
            if (!pRegistro.IsNull("empresa"))
                respuesta.NombreEmpresa = pRegistro["empresa"].ToString();
            if (!pRegistro.IsNull("titulo"))
                respuesta.TituloProyecto = pRegistro["titulo"].ToString();
            return respuesta;
        }


        public bool Agregar(EmpresasProyectos items)
        {
            bool resultado = false;
            string consulta = "exec EmpresasProyectos_Agregar " + items.IdEmpresa + "," + items.IdProyecto + "," + items.Activo;
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

        public bool Modificar(EmpresasProyectos items)
        {
            bool resultado = false;
            int valor = items.Activo == true ? 1 : 0;
            string consulta = "exec EmpresasProyectos_Modificar " + items.IdEmpresa + "," + items.IdProyecto + "," + valor;
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

    public class EmpresasProyectos
    {
        public int IdEmpresa { get; set; }
        public int IdProyecto { get; set; }
        public bool Activo { get; set; }

        public string NombreEmpresa { get; set; }
        public string TituloProyecto { get; set; }
    }
}
