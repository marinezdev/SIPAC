using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cxc_Bitacora
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.cxc_Bitacora> Seleccionar_PorIdOrdenFactura(string id)
        {
            b.ExecuteCommandSP("cat_cxc_Bitacora_Seleccionar_PorIdOrdenFactura");
            b.AddParameter("@idsolicitud", id, SqlDbType.Int);
            List<mod.cxc_Bitacora> resultado = new List<mod.cxc_Bitacora>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cxc_Bitacora item = new mod.cxc_Bitacora();
                item.IdServicio = int.Parse(reader["idservicio"].ToString());
                item.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.Estado = (mod.Enumeradores.EstadoOrdFac)int.Parse(reader["estado"].ToString());
                item.IdUsr = int.Parse(reader["titulo"].ToString());
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;            
        }

        protected bool Agregar(mod.cxc_Bitacora items)
        {
            b.ExecuteCommandQuery("cxc_Bitacora_Agregar");
            b.AddParameter("@idservicio", items.IdServicio, SqlDbType.Int);
            b.AddParameter("@idordenfactura", items.IdOrdenFactura, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool AgregarRegistroPago(mod.cxc_Bitacora items)
        {
            b.ExecuteCommandQuery("cxc_Bitacora_Agregar_RegistroPago");
            b.AddParameter("@idservicio", items.IdServicio, SqlDbType.Int);
            b.AddParameter("@idordenfactura", items.IdOrdenFactura, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Eliminar(string idordenfactura)
        {
            b.ExecuteCommandQuery("cxc_Bitacora_Eliminar");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
