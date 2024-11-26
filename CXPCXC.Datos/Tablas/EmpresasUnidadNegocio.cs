using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class EmpresasUnidadNegocio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        protected List<mod.EmpresasUnidadNegocio> Seleccionar(string idempresa)
        {
            b.ExecuteCommandSP("EmpresasUnidadNegocio_Seleccionar_PorEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.EmpresasUnidadNegocio> resultado = new List<mod.EmpresasUnidadNegocio>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.EmpresasUnidadNegocio item = new mod.EmpresasUnidadNegocio();
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.IdUDN = int.Parse(reader["idudn"].ToString());
                item.Activo = bool.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool SeleccionarEstadoActual(string idempresa, string idudn)
        {
            b.ExecuteCommandSP("EmpresasUnidadNegocio_Seleccionar_EstadoActual");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idudn", idudn, SqlDbType.Int);
            return bool.Parse(b.SelectString());
        }

        protected bool Agregar(mod.EmpresasUnidadNegocio items)
        {
            b.ExecuteCommandSP("EmpresasUnidadNegocio_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idudn", items.IdUDN, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar(mod.EmpresasUnidadNegocio items)
        {
            b.ExecuteCommandSP("EmpresasunidadNegocio_Modificar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idudn", items.IdUDN, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
