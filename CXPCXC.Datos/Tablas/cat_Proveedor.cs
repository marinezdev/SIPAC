using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Proveedor
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.cat_Proveedor> Seleccionar_PorIdEmpresa(string IdEmpresa)
        {
            b.ExecuteCommandSP("cat_Proveedor_Seleccionar_PorIdEmpresa");
            List<mod.cat_Proveedor> resultado = new List<mod.cat_Proveedor>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Proveedor item = new mod.cat_Proveedor();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Nombre = reader["titulo"].ToString();
                item.Rfc = reader["rfc"].ToString();
                item.Contacto = reader["contacto"].ToString();
                item.Correo = reader["correo"].ToString();
                item.Telefono = reader["telefono"].ToString();
                item.Extencion = reader["extencion"].ToString();
                item.SinFactura = (mod.Enumeradores.enSinFactura)reader["titulo"];
                item.Direccion = reader["direccion"].ToString();
                item.Ciudad = reader["ciudad"].ToString();
                item.Estado = reader["estado"].ToString();
                item.Cp = reader["cp"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Proveedor Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Empresas_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Proveedor resultado = new mod.cat_Proveedor();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Nombre = reader["titulo"].ToString();
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Contacto = reader["contacto"].ToString();
                resultado.Correo = reader["correo"].ToString();
                resultado.Telefono = reader["telefono"].ToString();
                resultado.Extencion = reader["extencion"].ToString();
                resultado.SinFactura = (mod.Enumeradores.enSinFactura)reader["titulo"];
                resultado.Direccion = reader["direccion"].ToString();
                resultado.Ciudad = reader["ciudad"].ToString();
                resultado.Estado = reader["estado"].ToString();
                resultado.Cp = reader["cp"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Proveedor Seleccionar_PorIdEmpresa_RFC(string idempresa, string rfc)
        {
            b.ExecuteCommandSP("cat_Proveedor_Seleccionar_PorEmpresaRFC");
            b.AddParameter("@id", idempresa, SqlDbType.Int);
            b.AddParameter("@rfc", rfc, SqlDbType.VarChar, 16);
            mod.cat_Proveedor resultado = new mod.cat_Proveedor();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Nombre = reader["titulo"].ToString();
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Contacto = reader["contacto"].ToString();
                resultado.Correo = reader["correo"].ToString();
                resultado.Telefono = reader["telefono"].ToString();
                resultado.Extencion = reader["extencion"].ToString();
                resultado.SinFactura = (mod.Enumeradores.enSinFactura)reader["titulo"];
                resultado.Direccion = reader["direccion"].ToString();
                resultado.Ciudad = reader["ciudad"].ToString();
                resultado.Estado = reader["estado"].ToString();
                resultado.Cp = reader["cp"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cat_Proveedor> Seleccionar_Activos_Inactivos_PorIdEmpresa(string idempresa, string activo)
        {
            b.ExecuteCommandSP("cat_Proveedor_Seleccionar_ActivosInactivos_PorEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@activo", activo, SqlDbType.Int);
            List<mod.cat_Proveedor> resultado = new List<mod.cat_Proveedor>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Proveedor item = new mod.cat_Proveedor();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Nombre = reader["titulo"].ToString();
                item.Rfc = reader["rfc"].ToString();
                item.Contacto = reader["contacto"].ToString();
                item.Correo = reader["correo"].ToString();
                item.Telefono = reader["telefono"].ToString();
                item.Extencion = reader["extencion"].ToString();
                item.SinFactura = (mod.Enumeradores.enSinFactura)reader["titulo"];
                item.Direccion = reader["direccion"].ToString();
                item.Ciudad = reader["ciudad"].ToString();
                item.Estado = reader["estado"].ToString();
                item.Cp = reader["cp"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cat_Proveedor> Seleccionar_ConSinFactura_PorIdEmpresa(string idempresa, int confactura)
        {
            b.ExecuteCommandSP("cat_Proveedor_Seleccionar_ConSinFactura_PorEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@confactura", confactura, SqlDbType.Int);
            List<mod.cat_Proveedor> resultado = new List<mod.cat_Proveedor>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Proveedor item = new mod.cat_Proveedor();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Nombre = reader["titulo"].ToString();
                item.Rfc = reader["rfc"].ToString();
                item.Contacto = reader["contacto"].ToString();
                item.Correo = reader["correo"].ToString();
                item.Telefono = reader["telefono"].ToString();
                item.Extencion = reader["extencion"].ToString();
                item.SinFactura = (mod.Enumeradores.enSinFactura)reader["titulo"];
                item.Direccion = reader["direccion"].ToString();
                item.Ciudad = reader["ciudad"].ToString();
                item.Estado = reader["estado"].ToString();
                item.Cp = reader["cp"].ToString();
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool ValidarSiExiste(string idempresa, string rfc)
        {
            b.ExecuteCommandSP("cat_Proveedor_Seleccionar_SiExiste");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@rfc", rfc, SqlDbType.VarChar, 16);
            if (b.SelectString() == "1")
                return true;
            else
                return false;
        }

        protected bool Agregar(mod.cat_Proveedor items)
        {
            b.ExecuteCommandSP("cat_Proveedor_Agregar");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.NChar,10);
            b.AddParameter("@nombre" , items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@direccion",items.Direccion, SqlDbType.VarChar, 255);
            b.AddParameter("@ciudad" , items.Ciudad, SqlDbType.VarChar, 128);
            b.AddParameter("@estado", items.Estado, SqlDbType.VarChar, 32);
            b.AddParameter("@cp", items.Cp, SqlDbType.VarChar, 5);
            b.AddParameter("@contacto", items.Contacto, SqlDbType.VarChar, 80);
            b.AddParameter("@correo", items.Correo, SqlDbType.VarChar,64);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.VarChar,32);
            b.AddParameter("@extencion", items.Extencion, SqlDbType.VarChar,16);
            b.AddParameter("@sinfactura", items.SinFactura, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Modificar(mod.cat_Proveedor items)
        {
            b.ExecuteCommandSP("cat_Proveedor_Agregar");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.NChar, 10);
            b.AddParameter("@nombre", items.Nombre, SqlDbType.VarChar, 80);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@direccion", items.Direccion, SqlDbType.VarChar, 255);
            b.AddParameter("@ciudad", items.Ciudad, SqlDbType.VarChar, 128);
            b.AddParameter("@estado", items.Estado, SqlDbType.VarChar, 32);
            b.AddParameter("@cp", items.Cp, SqlDbType.VarChar, 5);
            b.AddParameter("@contacto", items.Contacto, SqlDbType.VarChar, 80);
            b.AddParameter("@correo", items.Correo, SqlDbType.VarChar, 64);
            b.AddParameter("@telefono", items.Telefono, SqlDbType.VarChar, 32);
            b.AddParameter("@extencion", items.Extencion, SqlDbType.VarChar, 16);
            b.AddParameter("@sinfactura", items.SinFactura, SqlDbType.Int);
            b.AddParameter("@activo", items.Activo, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }
    }
}
