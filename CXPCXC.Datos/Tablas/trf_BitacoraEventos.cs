using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class trf_BitacoraEventos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        public bool Agregar_Registrar(mod.trf_BitacoraEventos items)
        {
            b.ExecuteCommandQuery("trf_BitacoraEventos_Agregar");
            b.AddParameter("@idsolicitud", items.IdSolicitud, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.VarChar, 128);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
