using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class EmpresasClientes
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected bool Seleccionar_EstadoActual(string idempresa, string idcliente)
        {
            b.ExecuteCommandSP("EmpresasClientes_Seleccionar_EstadoActual");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@idcliente", idcliente, SqlDbType.Int);
            return bool.Parse(b.SelectString());
        }

        protected List<mod.EmpresasClientes> Seleccionar_PorEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("EmpresasClientes_Seleccionar_PorEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.EmpresasClientes> resultado = new List<mod.EmpresasClientes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.EmpresasClientes item = new mod.EmpresasClientes();
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.IdCliente = int.Parse(reader["idcliente"].ToString());
                item.Activo = bool.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(mod.EmpresasClientes items)
        {
            b.ExecuteCommandSP("EmpresasClientes_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idcliente", items.IdCliente, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar(mod.EmpresasClientes items)
        {
            b.ExecuteCommandSP("EmpresasClientes_Modificar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@idcliente", items.IdCliente, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Bit);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
