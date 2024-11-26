using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Rechazos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.cat_Rechazos Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Rechazos_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Rechazos resultado = new mod.cat_Rechazos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Titulo = reader["titulo"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cat_Rechazos> Seleccionar_PorIdEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("cat_Rechazos_Seleccionar_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_Rechazos> resultado = new List<mod.cat_Rechazos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Rechazos item = new mod.cat_Rechazos();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Titulo = reader["titulo"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cat_Rechazos> Seleccionar_Activos_PorIdEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("cat_Rechazos_Seleccionar_Activos_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_Rechazos> resultado = new List<mod.cat_Rechazos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Rechazos item = new mod.cat_Rechazos();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Titulo = reader["titulo"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool ValidarSiExiste(string idempresa, string titulo)
        {
            b.ExecuteCommandSP("cat_Rechazos_Seleccionar_SiExiste");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@titulo", titulo, SqlDbType.VarChar, 16);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected bool Agregar(mod.cat_Rechazos items)
        {
            b.ExecuteCommandQuery("cat_Rechazos_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@titulo", items.Titulo, SqlDbType.VarChar, 16);
            b.AddParameter("@activo", items.Activo, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar_Titulo(mod.cat_Rechazos items)
        {
            b.ExecuteCommandQuery("cat_Rechazos_Modificar_Titulo");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@titulo", items.Titulo, SqlDbType.VarChar, 16);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
