using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Empresas
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();
        protected List<mod.cat_Empresas> Seleccionar()
        {
            b.ExecuteCommandSP("cat_Empresas_Seleccionar");
            List<mod.cat_Empresas> resultado = new List<mod.cat_Empresas>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Empresas item = new mod.cat_Empresas();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.Rfc = reader["idempresa"].ToString();
                item.Nombre = reader["titulo"].ToString();                
                item.Activo = int.Parse(reader["activo"].ToString());
                item.Logo = reader["logo"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Empresas Selecionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Empresas_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Empresas resultado = new mod.cat_Empresas();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Nombre = reader["nombre"].ToString();                
                resultado.Activo = int.Parse(reader["activo"].ToString());
                resultado.Logo = reader["logo"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Validar(mod.cat_Empresas items)
        {
            b.ExecuteCommandSP("cat_Empresas_Seleccionar_Validar_PorRFCNombre");
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar,16);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 150);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected bool Agregar(mod.cat_Empresas items)
        {
            b.ExecuteCommandSP("cat_Empresas_Agregar");
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 150);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar(mod.cat_Empresas items)
        {
            b.ExecuteCommandSP("cat_Empresas_Modificar");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 150);
            b.AddParameter("@activo", items.Activo, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
