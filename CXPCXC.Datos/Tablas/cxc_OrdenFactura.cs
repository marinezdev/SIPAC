using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    /// <summary>
    /// Proyección Factura
    /// </summary>
    public class cxc_OrdenFactura
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected DataTable SeleccionarTotalProyeccionPorFecha(int idempresa, DateTime fechainicio, DateTime fechatermino, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_TotalProyeccionPorFecha");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@fechatermino", fechatermino, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected DataTable SeleccionarGrupoProyeccion(int idempresa, DateTime fechainicio, DateTime fechatermino, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_Seleccionar_GrupoProyeccion");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@fechatermino", fechatermino, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected DataTable SeleccionarFacturasProyeccion(int idempresa, DateTime fechainicio, DateTime fechatermino, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_Seleccionar_FacturasProyeccion");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@fechatermino", fechatermino, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected DataTable SeleccionarPendientesActualizarFechaCompromiso(int idempresa, DateTime fechainicio, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_SeleccionarPendientesActualizarFechaCompromiso");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected DataTable SeleccionarCobranzaRealPorProyecto(int idempresa, DateTime fechainicio, DateTime fechatermino, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_SeleccionarPendientesActualizarFechaCompromiso");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@fechatermino", fechatermino, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected DataTable SeleccionarFacturasCobranzaReal(int idempresa, DateTime fechainicio, DateTime fechatermino, string estadoordenfactura)
        {
            b.ExecuteCommandQuery("cxc_OrdenFactura_Seleccionar_FacturasCobranzaReal");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            b.AddParameter("@fechainicio", fechainicio, SqlDbType.DateTime);
            b.AddParameter("@fechatermino", fechatermino, SqlDbType.DateTime);
            b.AddParameter("@estadoordenfactura", estadoordenfactura, SqlDbType.Int);
            return b.Select();
        }

        protected int Agregar(mod.cxc_OrdenFactura items)
        {
            b.ExecuteCommandSP("cxc_OrdenFactura_Agregar");
            b.AddParameter("@idordenfactura", items.IdOrdenFactura, SqlDbType.Int);
            b.AddParameter("@fechainicio", items.FechaInicio, SqlDbType.DateTime);
            b.AddParameter("@fechafactura", items.FechaFactura, SqlDbType.DateTime);
            b.AddParameter("@idcliente", items.IdCliente, SqlDbType.Int);
            b.AddParameter("@rfc", items.Rfc, SqlDbType.VarChar, 16);
            b.AddParameter("@cliente", items.Cliente, SqlDbType.VarChar, 80);
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@empresa", items.Empresa, SqlDbType.VarChar, 80);
            b.AddParameter("@tiposolicitud", items.TipoSolicitud, SqlDbType.Int);            
            b.AddParameter("@importe", items.Importe, SqlDbType.Decimal);
            b.AddParameter("@condicionpago", items.CondicionPago, SqlDbType.VarChar, 64);
            b.AddParameter("@condicionpagodias", items.CondicionPagoDias, SqlDbType.Int);
            b.AddParameter("@proyecto", items.Proyecto, SqlDbType.VarChar, 100);
            b.AddParameter("@tipomoneda", items.TipoMoneda, SqlDbType.VarChar, 8);
            b.AddParameter("@idcatservicio", items.IdCatServicio, SqlDbType.Int);
            b.AddParameter("@servicio", items.Servicio, SqlDbType.VarChar, 100);
            b.AddParameter("@descripcion", items.Descripcion, SqlDbType.VarChar, 128);
            b.AddParameter("@anotaciones", items.Anotaciones, SqlDbType.VarChar, 255);
            b.AddParameter("@idusr", items.IdUsr, SqlDbType.Int);
            b.AddParameter("@unidadnegocio", items.UnidadNegocio, SqlDbType.Int);
            b.AddParameter("@estado", items.Estado, SqlDbType.Int);
            b.AddParameter("@factura", items.Factura, SqlDbType.Int);
            b.AddParameter("@enviacorreoclte", items.EnviaCorreoClte, SqlDbType.Int);
            b.AddParameter("@especial", items.Especial, SqlDbType.Int);
            b.AddParameter("@marcado", items.Marcado, SqlDbType.Int);
            b.AddParameter("@fechacompromisopago", items.FechaCompromisoPago, SqlDbType.DateTime);
            b.AddParameterWithReturnValue("@idobtenido");
            return b.InsertWithReturnValue();
        }

        protected int ModificarEstado(int idordenfactura, int estado)
        {
            b.ExecuteCommandSP("cxc_OrdenFactura_Modificar_Estado");
            b.AddParameter("@idordenfactura", idordenfactura, SqlDbType.Int);
            b.AddParameter("@estado", estado, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}
