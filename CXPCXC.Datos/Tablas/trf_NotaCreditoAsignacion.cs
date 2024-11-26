using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class trf_NotaCreditoAsignacion
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.trf_NotaCredito_trf_NotaCreditoAsignacion Seleccionar_Solicitud(int idsolicitud)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Seleccionar_PorIdNotaCredito");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            mod.trf_NotaCredito_trf_NotaCreditoAsignacion resultado = new mod.trf_NotaCredito_trf_NotaCreditoAsignacion();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.trf_NotaCreditoAsignacion.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.trf_NotaCreditoAsignacion.IdNotaCredito = int.Parse(reader["idnotacredito"].ToString());
                resultado.trf_NotaCreditoAsignacion.IdSolicitud = int.Parse(reader["idsolicitud"].ToString());
                resultado.trf_NotaCreditoAsignacion.Monto = decimal.Parse(reader["idmonto"].ToString());
                resultado.trf_NotaCreditoAsignacion.IdUsr = int.Parse(reader["idusr"].ToString());
                resultado.trf_NotaCredito.Fecha = DateTime.Parse(reader["fecha"].ToString());
                resultado.trf_NotaCredito.Descripcion = reader["descripcion"].ToString();
                resultado.trf_NotaCredito.Importe = decimal.Parse(reader["importe"].ToString());                
            }
            reader = null;
            b.CloseConnection();
            return resultado;
        }



    }
}
