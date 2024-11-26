using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cxc_Archivos
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected int Seleccionar_NumeroComprobante(int idordenfactura, int tipo)
        {
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_NumeroComprobante");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            return int.Parse(b.SelectString());
        }

        protected List<mod.cxc_Archivos> Seleccionar_ComprobantesPorIdOrdenFactura(int idordenfactura, int tipo)
        {
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_ComprobantePorIdOrdenFactura");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            List<mod.cxc_Archivos> resultado = new List<mod.cxc_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cxc_Archivos item = new mod.cxc_Archivos();
                item.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.Tipo = (mod.Enumeradores.cxcTipoArchivo)reader["tipo"];
                item.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                item.ArchvioOrigen = reader["archivoorigen"].ToString();
                item.ArchivoDestino = reader["archivodestino"].ToString();
                item.Nota = reader["nota"].ToString();

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cxc_Archivos Seleccionar_Factura(int idordenfactura, int tipo)
        {            
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_Factura");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            mod.cxc_Archivos resultado = new mod.cxc_Archivos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Tipo = (mod.Enumeradores.cxcTipoArchivo)reader["tipo"];
                resultado.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                resultado.ArchvioOrigen = reader["archivoorigen"].ToString();
                resultado.ArchivoDestino = reader["archivodestino"].ToString();
                resultado.Nota = reader["nota"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cxc_Archivos Seleccionar_DocumentoPago(int idordenfactura, int tipo, int pagina)
        {
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_DocumentoPago");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            b.AddParameter("@iddocumento", pagina, SqlDbType.Int);
            mod.cxc_Archivos resultado = new mod.cxc_Archivos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Tipo = (mod.Enumeradores.cxcTipoArchivo)reader["tipo"];
                resultado.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                resultado.ArchvioOrigen = reader["archivoorigen"].ToString();
                resultado.ArchivoDestino = reader["archivodestino"].ToString();
                resultado.Nota = reader["nota"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cxc_Archivos Seleccionar_Comprobante(int idordenfactura, int tipo,  int iddocumento)
        {
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_Comprobante");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@tipo", tipo, SqlDbType.Int);
            b.AddParameter("@iddocumento", iddocumento, SqlDbType.Int);
            mod.cxc_Archivos resultado = new mod.cxc_Archivos();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Tipo = (mod.Enumeradores.cxcTipoArchivo)reader["tipo"];
                resultado.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                resultado.ArchvioOrigen = reader["archivoorigen"].ToString();
                resultado.ArchivoDestino = reader["archivodestino"].ToString();
                resultado.Nota = reader["nota"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cxc_Archivos> Seleccionar_ArchivosSolicitud(int idordenfactura)
        {
            b.ExecuteCommandSP("cxc_Archivos_Seleccionar_ArchivosSolicitud");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            List<mod.cxc_Archivos> resultado = new List<mod.cxc_Archivos>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cxc_Archivos item = new mod.cxc_Archivos();
                item.IdOrdenFactura = int.Parse(reader["idordenfactura"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.Tipo = (mod.Enumeradores.cxcTipoArchivo)reader["tipo"];
                item.IdDocumento = int.Parse(reader["iddocumento"].ToString());
                item.ArchvioOrigen = reader["archivoorigen"].ToString();
                item.ArchivoDestino = reader["archivodestino"].ToString();
                item.Nota = reader["nota"].ToString();

                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(mod.cxc_Archivos items)
        {
            b.ExecuteCommandSP("cxc_Archivos_Agregar");
            b.AddParameter("@idordenfactura", items.IdOrdenFactura, SqlDbType.Int);
            b.AddParameter("@tipo", items.Tipo, SqlDbType.Int);
            b.AddParameter("@iddocumento", items.IdDocumento, SqlDbType.Int);
            b.AddParameter("@archivoorigen", items.ArchvioOrigen, SqlDbType.VarChar, 65);
            b.AddParameter("@archivodestino", items.ArchivoDestino, SqlDbType.VarChar, 65);
            b.AddParameter("@nota", items.IdOrdenFactura, SqlDbType.VarChar, 255);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
        protected bool Eliminar(int idordenfactura)
        {
            b.ExecuteCommandSP("cxc_Archivos_Eliminar");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
