using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class EmpresasProyectos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        protected bool Seleccionar_EstadoActual(string idempresa, string idproyecto)
        {
            b.ExecuteCommandSP("EmpresasProyectos_Seleccionar_EstadoActual");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idproyecto", idproyecto, SqlDbType.Int);
            return bool.Parse(b.SelectString());
        }

        protected List<mod.EmpresasProyectos> Seleccionar_PorEmpresa(int idempresa)
        {
            b.ExecuteCommandSP("EmpresasUnidadNegocio_Seleccionar_PorEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.EmpresasProyectos> resultado = new List<mod.EmpresasProyectos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.EmpresasProyectos item = new mod.EmpresasProyectos();
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.IdProyecto = int.Parse(reader["idproyecto"].ToString());
                item.Activo = bool.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(mod.EmpresasProyectos items)
        {
            b.ExecuteCommandSP("EmpresasProyectos_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idproyecto", items.IdProyecto, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar(mod.EmpresasProyectos items)
        {
            b.ExecuteCommandSP("EmpresasProyectos_Modificar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idproyecto", items.IdProyecto, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
