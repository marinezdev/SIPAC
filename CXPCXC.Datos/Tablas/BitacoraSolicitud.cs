using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class BitacoraSolicitud
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.BitacoraSolicitud> Seleccionar_SeguimientoBitacora(int idsolicitud)
        {
            b.ExecuteCommandSP("BitacoraSolicitud_Seleccionar_PorIdSolicitud");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            List<mod.BitacoraSolicitud> resultado = new List<mod.BitacoraSolicitud>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.BitacoraSolicitud item = new mod.BitacoraSolicitud();
                item.IdSolicitud = int.Parse(reader["idsolicitud"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.Estado = (mod.Enumeradores.solEstado)reader["estado"];
                item.IdUsr = int.Parse(reader["idusr"].ToString());
                item.Nombre = reader["nombre"].ToString();
                item.Importe = decimal.Parse(reader["importe"].ToString());

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;

        }

        protected bool Agregar(mod.BitacoraSolicitud items)
        {
            b.ExecuteCommandSP("BitacoraSolicitud_Registrar");
            b.AddParameter("@idsolicitud", items.IdSolicitud, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
