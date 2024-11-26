using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class Usuario
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.Usuario SeleccionarDetallePorId(int idusr)
        {
            b.ExecuteCommandSP("Usuario_Seleccionar_Detalle");
            b.AddParameter("@idusr", idusr, SqlDbType.Int);
            mod.Usuario resultado = new mod.Usuario();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdUsr = int.Parse(reader["idusr"].ToString());
                resultado.Usuario_ = reader["usuario"].ToString();
                resultado.Nombre = reader["nombre"].ToString();
                resultado.UnidadNegocio = reader["unidadnegocio"].ToString();
                resultado.Grupo = int.Parse(reader["grupo"].ToString());
                resultado.Estado = int.Parse(reader["estado"].ToString());
                resultado.Fecha = DateTime.Parse(reader["fecha"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.correo = reader["correo"].ToString();
                resultado.TipoRecCorreo = int.Parse(reader["tiporeccorreo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected int Agregar(mod.Usuario items)
        {
            b.ExecuteCommandSP("Usuario_Agregar");
            b.AddParameter("@usuario", items.Usuario_, SqlDbType.VarChar, 64);
            b.AddParameter("@clave", items.Clave, SqlDbType.VarChar, 64);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@unidadnegocio", items.UnidadNegocio, SqlDbType.VarChar, 50);
            b.AddParameter("@grupo", items.Grupo, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@conectado", items.Conectado, SqlDbType.VarChar, 64);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@correo", items.correo, SqlDbType.VarChar, 64);
            b.AddParameter("@tiporeccorreo", items.TipoRecCorreo, SqlDbType.Int);
            b.AddParameterWithReturnValue("@idobtenido");
            return b.InsertWithReturnValue();
        }

        protected int Modificar(mod.Usuario items)
        {
            b.ExecuteCommandSP("Usuario_Modificar");
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@usuario", items.Usuario_, SqlDbType.VarChar, 64);
            b.AddParameter("@clave", items.Clave, SqlDbType.VarChar, 64);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@unidadnegocio", items.UnidadNegocio, SqlDbType.VarChar, 50);
            b.AddParameter("@grupo", items.Grupo, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@conectado", items.Conectado, SqlDbType.VarChar, 64);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@correo", items.correo, SqlDbType.VarChar, 64);
            b.AddParameter("@tiporeccorreo", items.TipoRecCorreo, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }
    }
}
