namespace cpplib
{
    /// <summary>
    /// Clase General de Instanciación de procesos CRUD
    /// </summary>
    public class Comun 
    {
        public admArchivos admarchivos;
        public admArchivosContrato admarchivoscontrato;
        public admArchivosCxc admarchivoscxc;
        public admBitacoraSolicitud admbitacorasolicitud;
        public Conv admcantidadletra;
        public admCatalogos admcatalogos;
        public admCatClientes admcatclientes;
        public admCatCondPago admcatcondpago;
        public admCatEmpresa admcatempresa;
        public admCatFormasPago admcatformaspago;
        public admCatProveedor admcatproveedor;
        public admCatProyectos admcatproyectos;
        public admCatRechazos admcatrechazos;
        public admCatServicios admcatservicios;
        public admCatUnidadNegocio admcatunidadnegocio;
        public admEmpresasClientes admempresasclientes;
        public admEmpresasProyectos admempresasproyectos;
        public admEmpresasUnidadNegocio admempresasunidadnegocio;
        public admCorreo admcorreo;
        public admCredencial admcredencial;
        public admCuenta admcuenta;
        public admCxcBitacora admcxcbitacora;
        public admCxcProyeccionCobranza admcxcproyeccioncobranza;
        public admcxpBitacoraEventos admcxpbitacoraeventos;
        public admCxpConciliarPago admcxpconciliarpago;
        public admCxpNotaCredito admcxpnotacredito;
        public admDirectorio admdirectorio;
        public admFondos admfondos;
        public admCatMonedas admcatmonedas;
        public admOrdenFactura admordenfactura;
        public admOrdenServicio admordenservicio;
        public admPartidasFactura admpartidasfactura;
        public admSolicitud admsolicitud;

        public clsSIPAC clssipac;
        public clsSIPAC_Security clssipacsecurity;
        public csConsultas csconsultas;
        public LeerXML leerxml;

        public Modelos modelos;

        public Comun()
        {
            admarchivos = new admArchivos();
            admarchivoscontrato = new admArchivosContrato();
            admarchivoscxc = new admArchivosCxc();
            admbitacorasolicitud = new admBitacoraSolicitud();
            admcantidadletra = new Conv();
            admcatalogos = new admCatalogos();
            admcatclientes = new admCatClientes();
            admcatcondpago = new admCatCondPago();
            admcatempresa = new admCatEmpresa();
            admcatformaspago = new admCatFormasPago();
            admcatproveedor = new admCatProveedor();
            admcatproyectos = new admCatProyectos();
            admcatrechazos = new admCatRechazos();
            admcatservicios = new admCatServicios();
            admcatunidadnegocio = new admCatUnidadNegocio();
            admempresasclientes = new admEmpresasClientes();
            admempresasproyectos = new admEmpresasProyectos();
            admempresasunidadnegocio = new admEmpresasUnidadNegocio();
            admcorreo = new admCorreo();
            admcredencial = new admCredencial();
            admcuenta = new admCuenta();
            admcxcbitacora = new admCxcBitacora();
            admcxcproyeccioncobranza = new admCxcProyeccionCobranza();
            admcxpbitacoraeventos = new admcxpBitacoraEventos();
            admcxpconciliarpago = new admCxpConciliarPago();
            admcxpnotacredito = new admCxpNotaCredito();
            admdirectorio = new admDirectorio();
            admfondos = new admFondos();
            admcatmonedas = new admCatMonedas();
            admordenfactura = new admOrdenFactura();
            admordenservicio = new admOrdenServicio();
            admpartidasfactura = new admPartidasFactura();
            admsolicitud = new  admSolicitud();

            clssipac = new clsSIPAC();
            clssipacsecurity = new clsSIPAC_Security();
            csconsultas = new csConsultas();
            leerxml = new LeerXML();

            modelos = new Modelos();
            
        }


    }
}
