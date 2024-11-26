using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Clientes
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.cat_Clientes Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_Clientes_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_Clientes resultado = new mod.cat_Clientes();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());    
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Nombre = reader["nombre"].ToString();
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Direccion = reader["direccion"].ToString();
                resultado.Ciudad = reader["ciudad"].ToString();
                resultado.Estado = reader["estado"].ToString();
                resultado.Cp = reader["cp"].ToString();
                resultado.ContactoProyecto = reader["contactoproy"].ToString();
                resultado.ContactoFacturacion = reader["contactofact"].ToString();
                resultado.Correo = reader["correo"].ToString();
                resultado.Telefono = reader["telefono"].ToString();
                resultado.Extencion = reader["extencion"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected List<mod.cat_Clientes> Seleccionar_PorIdEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("cat_Clientes_Seleccionar_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_Clientes> resultado = new List<mod.cat_Clientes>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Clientes item = new mod.cat_Clientes();
                item.Id = int.Parse(reader["id"].ToString());
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_Clientes SeleccionarPorIdEmpresaRFC(int idempresa, string rfc)
        {
            b.ExecuteCommandSP("cat_Clientes_Seleccionar_Por_IdEmpresa_RFC");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@rfc", rfc, SqlDbType.VarChar, 16);
            mod.cat_Clientes resultado = new mod.cat_Clientes();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Nombre = reader["nombre"].ToString();
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Direccion = reader["direccion"].ToString();
                resultado.Ciudad = reader["ciudad"].ToString();
                resultado.Estado = reader["estado"].ToString();
                resultado.Cp = reader["cp"].ToString();
                resultado.ContactoProyecto = reader["contactoproy"].ToString();
                resultado.ContactoFacturacion = reader["contactofact"].ToString();
                resultado.Correo = reader["correo"].ToString();
                resultado.Telefono = reader["telefono"].ToString();
                resultado.Extencion = reader["extencion"].ToString();
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }
    }
}
