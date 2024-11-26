using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cxc_ArchivoContrato
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.cxc_ArchivoContrato Seleccionar_PorIdServicio(int idservicio)
        {
            b.ExecuteCommandSP("cxc_ArchivoContrato_Seleccionar_PorIdServicio");
            b.AddParameter("@idservicio", idservicio, SqlDbType.Int);
            mod.cxc_ArchivoContrato resultado = new mod.cxc_ArchivoContrato();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdServicio = int.Parse(reader["idservicio"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.ArchivoDestino = reader["archivodestino"].ToString();
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(mod.cxc_ArchivoContrato items)
        {
            b.ExecuteCommandSP("cxc_ArchivoContrato_Agregar");
            b.AddParameter("@idservicio", items.IdServicio, SqlDbType.Int);
            b.AddParameter("@archivodestino", items.ArchivoDestino, SqlDbType.VarChar, 65);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Actualizar(mod.cxc_ArchivoContrato items)
        {

            b.ExecuteCommandSP("cxc_ArchivoContrato_Modificar");
            b.AddParameter("@idservicio", items.IdServicio, SqlDbType.Int);
            b.AddParameter("@archivodestino", items.ArchivoDestino, SqlDbType.VarChar, 65);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
