using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Modelos
{
    public class Modelos : IModelos
    {
        cat_Clientes _cat_Clientes;
        cat_CondicionPago _cat_CondicionPago;
        cat_Empresas _cat_Empresas;
        cat_Moneda _cat_Moneda;
        cat_Proveedor _cat_Proveedor;
        cat_Proyectos _cat_Proyectos;
        cat_Rechazos _cat_Rechazos;
        cat_Servicios _cat_Servicios;
        cat_UnidadNegocio _cat_UnidadNegocio;
        CuentasProveedor _CuentasProveedor;
        cxc_ArchivoContrato _cxc_ArchivoContrato;
        cxc_Archivos _cxc_Archivos;
        cxc_Bitacora _cxc_Bitacora;
        cxc_OrdenFactura _cxc_OrdenFactura;
        cxc_OrdenServicio _cxc_OrdenServicio;
        EmpresasClientes _EmpresasClientes;
        EmpresasProyectos _EmpresasProyectos;
        EmpresasUnidadNegocio _EmpresasUnidadNegocio;
        trf_Archivos _trf_Archivos;
        trf_BitacoraEventos _trf_BitacoraEventos;
        trf_ConciliarPago _trf_ConciliarPago;
        trf_NotaCredito _trf_NotaCredito;
        trf_NotaCredito_trf_NotaCreditoAsignacion _trf_NotaCredito_trf_NotaCreditoAsignacion;
        trf_NotaCreditoArchivos _trf_NotaCreditoArchivos;
        trf_NotaCreditoAsignacion _trf_NotaCreditoAsignacion;
        Usuario _Usuario;

        public Modelos()
        { 
        
        }

        public cat_Clientes cat_Clientes
        {
            get { return _cat_Clientes; }
            set { _cat_Clientes = value; }

        }

        public cat_CondicionPago cat_CondicionPago
        {
            get { return _cat_CondicionPago; }
            set { _cat_CondicionPago = value; }
        }

        public cat_Empresas cat_Empresas 
        {
            get { return _cat_Empresas; }
            set { _cat_Empresas = value; }
        }
        public cat_Moneda cat_Moneda 
        {
            get { return _cat_Moneda; }
            set { _cat_Moneda = value; }
        }
        public cat_Proveedor cat_Proveedor 
        {
            get { return _cat_Proveedor; }
            set { _cat_Proveedor = value; }
        }
        public cat_Proyectos cat_Proyectos 
        {
            get { return _cat_Proyectos; }
            set { _cat_Proyectos = value; }
        }
        public cat_Rechazos cat_Rechazos 
        {
            get { return _cat_Rechazos; }
            set { _cat_Rechazos = value; }
        }
        public cat_Servicios cat_Servicios 
        {
            get { return _cat_Servicios; }
            set { _cat_Servicios = value; }
        }
        public cat_UnidadNegocio cat_UnidadNegocio 
        {
            get { return _cat_UnidadNegocio; }
            set { _cat_UnidadNegocio = value; }
        }
        public CuentasProveedor CuentasProveedor 
        {
            get { return _CuentasProveedor; }
            set { _CuentasProveedor = value; }
        }
        public cxc_ArchivoContrato cxc_ArchivoContrato 
        {
            get { return _cxc_ArchivoContrato; }
            set { _cxc_ArchivoContrato = value; }
        }
        public cxc_Archivos cxc_Archivos 
        {
            get { return _cxc_Archivos; }
            set { _cxc_Archivos = value; }
        }
        public cxc_Bitacora cxc_Bitacora 
        {
            get { return _cxc_Bitacora; }
            set { _cxc_Bitacora = value; }
        }
        public cxc_OrdenFactura cxc_OrdenFactura 
        {
            get { return _cxc_OrdenFactura; }
            set { _cxc_OrdenFactura = value; }
        }
        public cxc_OrdenServicio cxc_OrdenServicio 
        {
            get { return _cxc_OrdenServicio; }
            set { _cxc_OrdenServicio = value; }
        }
        public EmpresasClientes EmpresasClientes 
        {
            get { return _EmpresasClientes; }
            set { _EmpresasClientes = value; }
        }
        public EmpresasProyectos EmpresasProyectos 
        {
            get { return _EmpresasProyectos; }
            set { _EmpresasProyectos = value; }
        }
        public EmpresasUnidadNegocio EmpresasUnidadNegocio 
        {
            get { return _EmpresasUnidadNegocio; }
            set { _EmpresasUnidadNegocio = value; }
        }
        public trf_Archivos trf_Archivos 
        {
            get { return _trf_Archivos; }
            set { _trf_Archivos = value; }
        }
        public trf_BitacoraEventos trf_BitacoraEventos 
        {
            get { return _trf_BitacoraEventos; }
            set { _trf_BitacoraEventos = value; }
        }
        public trf_ConciliarPago trf_ConciliarPago 
        {
            get { return _trf_ConciliarPago; }
            set { _trf_ConciliarPago = value; }
        }
        public trf_NotaCredito trf_NotaCredito 
        {
            get { return _trf_NotaCredito; }
            set { _trf_NotaCredito = value; }
        }
        public trf_NotaCredito_trf_NotaCreditoAsignacion trf_NotaCredito_trf_NotaCreditoAsignacion 
        {
            get { return _trf_NotaCredito_trf_NotaCreditoAsignacion; }
            set { _trf_NotaCredito_trf_NotaCreditoAsignacion = value; }
        }
        public trf_NotaCreditoArchivos trf_NotaCreditoArchivos 
        {
            get { return _trf_NotaCreditoArchivos; }
            set { _trf_NotaCreditoArchivos = value; }
        }
        public trf_NotaCreditoAsignacion trf_NotaCreditoAsignacion 
        {
            get { return _trf_NotaCreditoAsignacion; }
            set { _trf_NotaCreditoAsignacion = value; }
        }

        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public virtual void Inicializar()
        {
            _cat_Clientes = new cat_Clientes();
            _cat_CondicionPago = new cat_CondicionPago();
            _cat_Empresas = new cat_Empresas();
            _cat_Moneda = new cat_Moneda();
            _cat_Proveedor = new cat_Proveedor();
            _cat_Proyectos = new cat_Proyectos();
            _cat_Rechazos = new cat_Rechazos();
            _cat_Servicios = new cat_Servicios();
            _cat_UnidadNegocio = new cat_UnidadNegocio();
            _CuentasProveedor = new CuentasProveedor();
            _cxc_ArchivoContrato = new cxc_ArchivoContrato();
            _cxc_Archivos = new cxc_Archivos();
            _cxc_Bitacora = new cxc_Bitacora();
            _cxc_OrdenFactura = new cxc_OrdenFactura();
            _cxc_OrdenServicio = new cxc_OrdenServicio();
            _EmpresasClientes = new EmpresasClientes();
            _EmpresasProyectos = new EmpresasProyectos();
            _EmpresasUnidadNegocio = new EmpresasUnidadNegocio();
            _trf_Archivos = new trf_Archivos();
            _trf_BitacoraEventos = new trf_BitacoraEventos();
            _trf_ConciliarPago = new trf_ConciliarPago();
            _trf_NotaCredito = new trf_NotaCredito();
            _trf_NotaCredito_trf_NotaCreditoAsignacion = new trf_NotaCredito_trf_NotaCreditoAsignacion();
            _trf_NotaCreditoArchivos = new trf_NotaCreditoArchivos();
            _trf_NotaCreditoAsignacion = new trf_NotaCreditoAsignacion();
            _Usuario = new Usuario();
        }


    }
}
