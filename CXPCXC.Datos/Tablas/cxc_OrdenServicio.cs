using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cxc_OrdenServicio
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected mod.cxc_OrdenServicio Seleccionar_PorIdServicio(int idservicio)
        {
            b.ExecuteCommandSP("cat_OrdenServicio_Seleccionar_PorIdServicio");
            b.AddParameter("@idservicio", idservicio, SqlDbType.Int);
            mod.cxc_OrdenServicio resultado = new mod.cxc_OrdenServicio();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.IdServicio = int.Parse(reader["idservicio"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdCliente = int.Parse(reader["idcliente"].ToString());
                resultado.Rfc = reader["rfc"].ToString();
                resultado.Cliente = reader["cliente"].ToString();
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Empresa = reader["empresa"].ToString();
                resultado.TipoSolicitud = int.Parse(reader["tiposolicitud"].ToString());
                resultado.Importe = int.Parse(reader["importe"].ToString());
                resultado.ImporteVista = reader["importevista"].ToString();
                resultado.FechaInicio = DateTime.Parse(reader["fechainicio"].ToString());
                resultado.FechaTermino = DateTime.Parse(reader["fechatermino"].ToString());
                resultado.Periodos = int.Parse(reader["periodos"].ToString());
                resultado.TipoPeriodo = int.Parse(reader["tipoperiodo"].ToString());
                resultado.CondicionPago = reader["condicionpago"].ToString();
                resultado.CondicionPagoDias = int.Parse(reader["condicionpagodias"].ToString());
                resultado.Proyecto = reader["proyecto"].ToString();
                resultado.TipoMoneda = reader["tipomoneda"].ToString();
                resultado.IdCatServicio = int.Parse(reader["idcatservicio"].ToString());
                resultado.Servicio = reader["servicio"].ToString();
                resultado.Descripcion = reader["descripcion"].ToString();
                resultado.IdUsr = int.Parse(reader["idusr"].ToString());
                resultado.UnidadNegocio = int.Parse(reader["unidadnegocio"].ToString());
                resultado.Contrato = int.Parse(reader["contrato"].ToString());
                resultado.EnviaCorreoClte = int.Parse(reader["enviacorreoclte"].ToString());
                resultado.Estado = (mod.Enumeradores.EstadoOrdSvc)int.Parse(reader["estado"].ToString());
                resultado.Especial = int.Parse(reader["especial"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        /// <summary>
        /// Agrega un nuevo registro y devuelve el valor del id de inserción
        /// </summary>
        /// <param name="items"></param>
        /// <returns>id del registro agregado</returns>
        protected int Agregar(mod.cxc_OrdenServicio items)
        {
            b.ExecuteCommandSP("cxc_OrdenServicio_Agregar");
            b.AddParameter("@idservicio", items.IdServicio, SqlDbType.Int);
            b.AddParameter("@idcliente", items.IdCliente, SqlDbType.Int);
            b.AddParameter("@cliente", items.Cliente, SqlDbType.VarChar, 80);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar,16);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@empresa", items.Empresa, SqlDbType.VarChar, 80);
            b.AddParameter("@tiposolicitud", items.TipoSolicitud, SqlDbType.Int);
            b.AddParameter("@fechainicio", items.FechaInicio.ToString("dd/MM/yyyy"), SqlDbType.DateTime);
            b.AddParameter("@fechatermino", items.FechaTermino.ToString("dd/MM/yyyy"), SqlDbType.DateTime);
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            b.AddParameter("@periodos", items.Periodos, SqlDbType.Int);
            b.AddParameter("@tipoperiodo", items.TipoPeriodo, SqlDbType.Int);
            b.AddParameter("@condicionpago", items.CondicionPago, SqlDbType.VarChar, 64);
            b.AddParameter("@condicionpagodias", items.CondicionPagoDias, SqlDbType.Int);
            b.AddParameter("@proyecto", items.Proyecto, SqlDbType.VarChar,100);
            b.AddParameter("@tipomoneda", items.TipoMoneda, SqlDbType.VarChar, 8);
            b.AddParameter("@idcatservicio", items.IdCatServicio, SqlDbType.Int);
            b.AddParameter("@servicio", items.Servicio, SqlDbType.VarChar,100);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@unidadnegocio", items.UnidadNegocio, SqlDbType.Int);
            b.AddParameter("@contrato", items.Contrato, SqlDbType.Int);
            b.AddParameter("@enviacorreoclte", items.EnviaCorreoClte, SqlDbType.Int);
            b.AddParameter("@estado", 0, SqlDbType.Int);
            b.AddParameter("@especial", items.Especial, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        protected int Prueba()
        {
            b.ExecuteCommandSP("Usuario_Contar");
            b.AddParameterWithReturnValue("@usuarioscontados");
            return b.InsertWithReturnValue();
        }


    }
}
