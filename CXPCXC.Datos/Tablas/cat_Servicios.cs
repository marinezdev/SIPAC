using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Servicios
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.cat_Servicios> Seleccionar_PorIdEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("cat_Servicios_Seleccionar_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_Servicios> resultado = new List<mod.cat_Servicios>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Servicios item = new mod.cat_Servicios();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Titulo = reader["titulo"].ToString();
                item.Imagen = reader["imagen"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Servicios Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Servicios_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Servicios resultado = new mod.cat_Servicios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Titulo = reader["titulo"].ToString();
                resultado.Imagen = reader["imagen"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Servicios Seleccionar_Imagen_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Servicios_Seleccionar_Imagen_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Servicios resultado = new mod.cat_Servicios();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Imagen = reader["imagen"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Seleccionar_SiExiste(mod.cat_Servicios items)
        {
            b.ExecuteCommandSP("cat_Servicios_Seleccionar_SiExiste");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@titulo", items.Titulo, SqlDbType.VarChar, 100);
            if (b.SelectString() != "")
                return true;
            else
                return false;
        }

        protected bool Agregar(mod.cat_Servicios items)
        {
            b.ExecuteCommandSP("cat_Servicios_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@titulo", items.Titulo, SqlDbType.VarChar, 100);
            b.AddParameter("@activo", items.Activo, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar_Titulo(mod.cat_CondicionPago items)
        {
            b.ExecuteCommandSP("cat_Servicios_Modificar_Titulo");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@titulos", items.Titulo, SqlDbType.VarChar, 100);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
