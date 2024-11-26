using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public interface IModelos
    {
        cat_Clientes cat_Clientes { get; set; }
        cat_CondicionPago cat_CondicionPago { get; set; }
        cat_Empresas cat_Empresas { get; set; }
        cat_Moneda cat_Moneda { get; set; }
        cat_Proveedor cat_Proveedor { get; set; }
        cat_Proyectos cat_Proyectos { get; set; }
        cat_Rechazos cat_Rechazos { get; set; }
        cat_Servicios cat_Servicios { get; set; }
        cat_UnidadNegocio cat_UnidadNegocio { get; set; }
        CuentasProveedor CuentasProveedor { get; set; }
        cxc_ArchivoContrato cxc_ArchivoContrato { get; set; }
        cxc_Archivos cxc_Archivos { get; set; }
        cxc_Bitacora cxc_Bitacora { get; set; }
        cxc_OrdenFactura cxc_OrdenFactura { get; set; }
        cxc_OrdenServicio cxc_OrdenServicio { get; set; }
        EmpresasClientes EmpresasClientes { get; set; }
        EmpresasProyectos EmpresasProyectos { get; set; }
        EmpresasUnidadNegocio EmpresasUnidadNegocio { get; set; }
        trf_Archivos trf_Archivos { get; set; }
        trf_BitacoraEventos trf_BitacoraEventos { get; set; }
        trf_ConciliarPago trf_ConciliarPago { get; set; }
        trf_NotaCredito trf_NotaCredito { get; set; }
        trf_NotaCredito_trf_NotaCreditoAsignacion trf_NotaCredito_trf_NotaCreditoAsignacion { get; set; }
        trf_NotaCreditoArchivos trf_NotaCreditoArchivos { get; set; }
        trf_NotaCreditoAsignacion trf_NotaCreditoAsignacion { get; set; }

        void Inicializar();
    }
}
