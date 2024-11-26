using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;
using System.Data.SqlTypes;

namespace CXPCXC.Datos.Tablas
{
    public class trf_NotaCredito
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.trf_NotaCredito Seleccionar_PorIdNotaCredito(int idnotacredito)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Seleccionar_PorIdNotaCredito");
            b.AddParameter("@idnotacredito", idnotacredito, SqlDbType.Int);
            mod.trf_NotaCredito resultado = new mod.trf_NotaCredito();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdNotaCredito = int.Parse(reader["idnotacredito"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Fecha = DateTime.Parse(reader["fecha"].ToString());
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Proveedor = reader["proveedor"].ToString();
                resultado.Descripcion = reader["descripcion"].ToString();
                resultado.Importe = decimal.Parse(reader["importe"].ToString());
                resultado.Moneda = reader["moneda"].ToString();
                resultado.ImportePendiente = decimal.Parse(reader["importependiente"].ToString());
                resultado.Estado = (mod.Enumeradores.enEstado2)int.Parse(reader["estado"].ToString());
                resultado.IdUsr = int.Parse(reader["fecharegistro"].ToString());
                resultado.IdSolicitudOrigen = int.Parse(reader["fecharegistro"].ToString());
            }
            reader = null;
            b.CloseConnection();
            return resultado;
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

        protected List<mod.trf_NotaCredito> Seleccionar_SolicitudesRelacionadas(int idsolicitud)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Seleccionar_NotasCreditoProveedor");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
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

        public bool Agregar(mod.trf_NotaCredito items)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@fecha", items.Fecha, SqlDbType.DateTime);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@proveedor", items.Proveedor, SqlDbType.VarChar, 80);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.VarChar, 256);
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            b.AddParameter("@moneda",items.Moneda, SqlDbType.VarChar, 8);
            b.AddParameter("@importependiente", items.ImportePendiente, SqlDbType.Decimal);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@idsolicitudorigen", items.IdSolicitudOrigen, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        public bool ModificarEstadoMonto(mod.trf_NotaCredito items)
        {
            b.ExecuteCommandSP("trf_NotaCredito_ModificarEstadoMonto");
            b.AddParameter("@idnotacredito", items.IdNotaCredito, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        public bool Eliminar(int idnotacredito, int estado)
        {
            b.ExecuteCommandSP("trf_NotaCredito_Eliminar");
            b.AddParameter("@idnotacredito", idnotacredito, SqlDbType.Int);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }


    }
}
