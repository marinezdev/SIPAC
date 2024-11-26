using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
    public class Modelos
    {
        //Modelos
        public Archivo archivo;
        public ArchivoContrato archivocontrato;
        public Bitacora bitacora;
        public BitacoraEventos bitacoraeventos;
        public cxcArchivo cxcarchivo;
        public CatClientes catclientes;
        public catCondPago catcondpago;
        public CatFormaPago catformapago;
        public CatProveedor catproveedor;
        public CatProyectos catproyectos;
        public catRechazos catrechazos;
        public catServicios catservicios;
        public CatUnidadNegocio catunidadnegocio;
        public CorreoCfg correocfg;
        public CorreoMsg correomsg;
        public credencial credencial;
        public Cuenta cuenta;
        public cxcBitacora cxcbitacora;
        public cxcProyCobranza cscproycobranza;
        public cxpPagos cxppagos;
        public cxpNotaCredito cxpnotacredito;
        public DatosXML datosxml;
        public Empresa empresa;
        public EmpresasClientes empresasclientes;
        public LoteFondos lotefondos;
        public OrdenFactura ordenfactura;
        public OrdenServicio ordenservicio;
        public PartidasFactura partidasfactura;
        public Receptor receptor;
        public Solicitud solicitud;
        public valorTexto valortexto;
        public SIPAC_Clientes sipacclientes;
        public SIPAC_Instalaciones sipacinstalaciones;
        public SIPAC_InstalacionesOcupacion sipacinstalacionesocupacion;
        public _usuarioRegistro usuarioregistro;
        public _usuarioPerfiles usuarioperfiles;
        public _Empresas empresas;

        public Modelos()
        {
            //Modelos
            archivo = new Archivo();
            archivocontrato = new ArchivoContrato();
            bitacora = new Bitacora();
            bitacoraeventos = new BitacoraEventos();
            cxcarchivo = new cxcArchivo();
            catclientes = new CatClientes();
            catcondpago = new catCondPago();
            catformapago = new CatFormaPago();
            catproveedor = new CatProveedor();
            catproyectos = new CatProyectos();
            catrechazos = new catRechazos();
            catservicios = new catServicios();
            catunidadnegocio = new CatUnidadNegocio();
            correocfg = new CorreoCfg();
            correomsg = new CorreoMsg();
            credencial = new credencial();
            cuenta = new Cuenta();
            cxcbitacora = new cxcBitacora();
            cscproycobranza = new cxcProyCobranza();
            cxppagos = new cxpPagos();
            cxpnotacredito = new cxpNotaCredito();
            datosxml = new DatosXML();
            empresa = new Empresa();
            empresasclientes = new EmpresasClientes();
            lotefondos = new LoteFondos();
            ordenfactura = new OrdenFactura();
            ordenservicio = new OrdenServicio();
            partidasfactura = new PartidasFactura();
            receptor = new Receptor();
            solicitud = new Solicitud();
            valortexto = new valorTexto();
            sipacclientes = new SIPAC_Clientes();
            sipacinstalaciones = new SIPAC_Instalaciones();
            sipacinstalacionesocupacion = new SIPAC_InstalacionesOcupacion();
            usuarioregistro = new _usuarioRegistro();
            usuarioperfiles = new _usuarioPerfiles();
            empresas = new _Empresas();
        }
    }
}
