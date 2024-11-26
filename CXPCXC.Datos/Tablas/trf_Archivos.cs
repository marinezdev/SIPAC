using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class trf_Archivos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.trf_Archivos> Seleccionar_ArchivosSolicitud(int idsolicitud)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_ArchivosSolicitud");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            List<mod.trf_Archivos> resultado = new List<mod.trf_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.trf_Archivos items = new mod.trf_Archivos();
                items.IdSolicitud       = int.Parse(reader["idsolicitud"].ToString());
                items.Tipo              = (mod.Enumeradores.TipoArchivo)reader["tipo"];
                items.IdDocumento       = int.Parse(reader["iddocumento"].ToString());
                items.ArchvioOrigen     = reader["archivoorigen"].ToString();
                items.ArchivoDestino    = reader["archivodestino"].ToString();
                items.Cantidad          = decimal.Parse(reader["cantidad"].ToString());
                items.TipoCambio        = decimal.Parse(reader["tipocambio"].ToString());
                items.Pesos             = decimal.Parse(reader["pesos"].ToString());
                items.Nota              = reader["nota"].ToString();
                items.IdPago            = int.Parse(reader["idpago"].ToString());
                resultado.Add(items);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.trf_Archivos> Seleccionar_CargaComprobante(int idsolicitud, int iddocumento, int tipo)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_CargaComprobante");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            List<mod.trf_Archivos> resultado = new List<mod.trf_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.trf_Archivos items = new mod.trf_Archivos();
                items.IdSolicitud    = int.Parse(reader["idsolicitud"].ToString());
                items.Tipo           = (mod.Enumeradores.TipoArchivo)reader["tipo"];
                items.IdDocumento    = int.Parse(reader["iddocumento"].ToString());
                items.ArchvioOrigen  = reader["archivoorigen"].ToString();
                items.ArchivoDestino = reader["archivodestino"].ToString();
                items.Cantidad       = decimal.Parse(reader["cantidad"].ToString());
                items.TipoCambio     = decimal.Parse(reader["tipocambio"].ToString());
                items.Pesos          = decimal.Parse(reader["pesos"].ToString());
                items.Nota           = reader["nota"].ToString();
                items.IdPago         = int.Parse(reader["idpago"].ToString());
                resultado.Add(items);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.trf_Archivos> Seleccionar_CargaFactura(int idsolicitud, int tipo)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_CargaFactura");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            List<mod.trf_Archivos> resultado = new List<mod.trf_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.trf_Archivos items = new mod.trf_Archivos();
                items.IdSolicitud    = int.Parse(reader["idsolicitud"].ToString());
                items.Tipo           = (mod.Enumeradores.TipoArchivo)reader["tipo"];
                items.IdDocumento    = int.Parse(reader["iddocumento"].ToString());
                items.ArchvioOrigen  = reader["archivoorigen"].ToString();
                items.ArchivoDestino = reader["archivodestino"].ToString();
                items.Cantidad       = decimal.Parse(reader["cantidad"].ToString());
                items.TipoCambio     = decimal.Parse(reader["tipocambio"].ToString());
                items.Pesos          = decimal.Parse(reader["pesos"].ToString());
                items.Nota           = reader["nota"].ToString();
                items.IdPago         = int.Parse(reader["idpago"].ToString());
                resultado.Add(items);
            }
            b.CloseConnection();
            return resultado;
        }

        protected decimal Seleccionar_ImporteTotalComprobantes(int idsolicitud)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_ImporteTotalComprobantes");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            return decimal.Parse(b.SelectString());
        }

        protected List<mod.trf_Archivos> Seleccionar_ListaComprobantes(int idsolicitud, int tipo)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_ListaComprobantes");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            List<mod.trf_Archivos> resultado = new List<mod.trf_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.trf_Archivos items = new mod.trf_Archivos();
                items.IdSolicitud       = int.Parse(reader["idsolicitud"].ToString());
                items.Tipo              = (mod.Enumeradores.TipoArchivo)reader["tipo"];
                items.IdDocumento       = int.Parse(reader["iddocumento"].ToString());
                items.ArchvioOrigen     = reader["archivoorigen"].ToString();
                items.ArchivoDestino    = reader["archivodestino"].ToString();
                items.Cantidad          = decimal.Parse(reader["cantidad"].ToString());
                items.TipoCambio        = decimal.Parse(reader["tipocambio"].ToString());
                items.Pesos             = decimal.Parse(reader["pesos"].ToString());
                items.Nota              = reader["nota"].ToString();
                items.IdPago            = int.Parse(reader["idpago"].ToString());
                resultado.Add(items);
            }
            b.CloseConnection();
            return resultado;
        }

        protected decimal Seleccionar_NumeroComprobante(int idsolicitud, int tipo)
        {
            b.ExecuteCommandSP("trf_Archivos_Seleccionar_NumeroComprobante");
            b.AddParameter("@idsolicitud", idsolicitud, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            return decimal.Parse(b.SelectString());
        }

        protected int Agregar(mod.trf_Archivos items)
        {
            b.ExecuteCommandSP("trf_Archivos_Agregar");
            b.AddParameter("@idsolicitud", items.IdSolicitud, SqlDbType.Int);
            b.AddParameter("@tipo", items.Tipo, SqlDbType.Int);
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@archvioorigen", items.ArchvioOrigen, SqlDbType.VarChar, 64);
            b.AddParameter("@archivodestino", items.ArchivoDestino, SqlDbType.VarChar, 64);
            b.AddParameter("@cantidad", items.Cantidad, SqlDbType.Decimal);
            b.AddParameter("@tipocambio", items.TipoCambio, SqlDbType.Decimal);
            b.AddParameter("@pesos", items.Pesos, SqlDbType.Decimal);
            b.AddParameter("@nota", items.Nota, SqlDbType.VarChar, 255);
            b.AddParameter("@idpago", items.IdPago, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }


    }
}
