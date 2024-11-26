using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class CuentasProveedor
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.CuentasProveedor Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_CuentasProveedor_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.CuentasProveedor resultado = new mod.CuentasProveedor();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Banco = reader["banco"].ToString();
                resultado.Cuenta = reader["cuenta"].ToString();
                resultado.CtaClabe = reader["ctaclabe"].ToString();
                resultado.Sucursal = reader["sucursal"].ToString();
                resultado.Moneda = reader["moneda"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.CuentasProveedor Seleccionar_PrimeraCuenta(int id)
        {
            b.ExecuteCommandSP("cat_CuentasProveedor_Seleccionar_PrimeraCuenta");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.CuentasProveedor resultado = new mod.CuentasProveedor();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.Banco = reader["banco"].ToString();
                resultado.Cuenta = reader["cuenta"].ToString();
                resultado.CtaClabe = reader["ctaclabe"].ToString();
                resultado.Sucursal = reader["sucursal"].ToString();
                resultado.Moneda = reader["moneda"].ToString();
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Agregar(mod.CuentasProveedor items)
        {
            b.ExecuteCommandQuery("cat_CuentasProveedor_Agregar");
            b.AddParameter("@banco", items.Banco, SqlDbType.VarChar, 32);
            b.AddParameter("@cuenta", items.Cuenta, SqlDbType.VarChar, 32);
            b.AddParameter("@clabe", items.CtaClabe, SqlDbType.VarChar, 32);
            b.AddParameter("@sucursal", items.Sucursal, SqlDbType.VarChar, 32);
            b.AddParameter("@moneda", items.Moneda, SqlDbType.VarChar, 50);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        protected bool Eliminar(mod.CuentasProveedor items)
        {
            b.ExecuteCommandQuery("cat_CuentasProveedor_Eliminar");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@cuenta", items.Cuenta, SqlDbType.VarChar, 32);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

    }
}
