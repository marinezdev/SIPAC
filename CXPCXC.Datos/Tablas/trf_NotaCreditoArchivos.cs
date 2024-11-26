using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class trf_NotaCreditoArchivos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected string SeleccionarNombre(int idnotacredito)
        {
            b.ExecuteCommandSP("trf_NotaCreditoArchivos_Seleccionar_Nombre");
            b.AddParameter("@idnotacredito", idnotacredito, SqlDbType.Int);
            mod.trf_NotaCredito resultado = new mod.trf_NotaCredito();
            return b.SelectString();
        }

        protected List<mod.trf_NotaCredito> Seleccionar_NotasCreditoProveedor(int idempresa, string rfc, int estado)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Seleccionar_NotasCreditoProveedor");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@rfc", rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@empresa", estado, SqlDbType.Int);
            List<mod.trf_NotaCredito> resultado = new List<mod.trf_NotaCredito>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.trf_NotaCredito item = new mod.trf_NotaCredito();
                item.IdNotaCredito = int.Parse(reader["idnotacredito"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Fecha = DateTime.Parse(reader["fecha"].ToString());
                item.Rfc = reader["rfc"].ToString();
                item.Proveedor = reader["proveedor"].ToString();
                item.Descripcion = reader["descripcion"].ToString();
                item.Importe = decimal.Parse(reader["importe"].ToString());
                item.Moneda = reader["moneda"].ToString();
                item.ImportePendiente = decimal.Parse(reader["importependiente"].ToString());
                item.Estado = (mod.Enumeradores.enEstado2)int.Parse(reader["estado"].ToString());
                item.IdUsr = int.Parse(reader["fecharegistro"].ToString());
                item.IdSolicitudOrigen = int.Parse(reader["fecharegistro"].ToString());

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

    }
}
