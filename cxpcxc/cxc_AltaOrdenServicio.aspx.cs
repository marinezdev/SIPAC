using cpplib;
using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_AltaOrdenServicio : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) 
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void enviaMsgCliente(string pMensaje) 
        {
            ltMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                 dpTpSolicitud.Attributes.Add("onChange","VistaRegPartidas();" );
        
                validaSessionPartidas();
                cpplib.credencial oCdr = (cpplib.credencial)Session["Credencial"];
                lbUnidadNegocio.Text = comun.admcatunidadnegocio.carga(oCdr.UnidadNegocio).Titulo; //(new cpplib.admCatUnidadNegocio()).carga(oCdr.UnidadNegocio).Titulo; 
                this.txFhInicio.Attributes.Add("readonly", "true");
                this.txFhInicio.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                llenacombos();
            }
        }
        private void validaSessionPartidas() 
        {
            if (Session["Partidas"] != null)
            {
                Session.Remove("Partidas");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        { 
            validaSessionPartidas(); 
            Response.Redirect("espera.aspx"); 
        }
               
        private void llenacombos() 
        {
            cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
            //List<cpplib.CatClientes> lstClte = (new cpplib.admCatClientes()).ListaClientesXEmpresa(Crd.IdEmpresaTrabajo.ToString());  //Anterior: ListaTodosClientes();
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(Crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //dpCliente.DataSource = comun.admcatclientes.ListaClientesXEmpresa(Crd.IdEmpresaTrabajo.ToString());
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            cddwEmpresa.Category = "Empresa";
           }

        protected void dpCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpCliente.SelectedIndex > 0)
            {
                cpplib.CatClientes oCte = comun.admcatclientes.carga(Convert.ToInt32(dpCliente.SelectedValue)); //(new cpplib.admCatClientes()).carga(Convert.ToInt32(dpCliente.SelectedValue));
                lbNombre.Text = oCte.Nombre;
                lbRfc.Text = oCte.Rfc;
                lbDireccion.Text = oCte.Direccion;
                lbCiudad.Text = oCte.Ciudad;
                lbEstado.Text = oCte.Estado;
                lbCp.Text = oCte.Cp;
                lbCorreo.Text = oCte.Correo;
                pnCliente.Visible = true;
            }
            else 
            { 
                pnCliente.Visible = false; 
            }
        }

        private cpplib.OrdenServicio CargaDatosOrden()
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.OrdenServicio oOrdSrv = new cpplib.OrdenServicio();
            oOrdSrv.IdCliente = Convert.ToInt32(dpCliente.SelectedValue);
            oOrdSrv.Rfc = lbRfc.Text;
            oOrdSrv.Cliente = lbNombre.Text;
            oOrdSrv.IdEmpresa = Convert.ToInt32(dpEmpresa.SelectedValue);
            oOrdSrv.Empresa =  dpEmpresa.SelectedItem.Text ;
            oOrdSrv.TipoSolicitud=(cpplib.OrdenServicio.enTipoSolicitud)(Convert.ToInt32 (dpTpSolicitud.SelectedValue));
            oOrdSrv.FechaInicio =Convert.ToDateTime(txFhInicio.Text); 
            oOrdSrv.FechaTermino = calculaFechaTermino();
            oOrdSrv.Periodos=Convert.ToInt32 (txPeriodos.Text.ToString () );
            oOrdSrv.TipoPeriodo =DaTipoPeriodo();
            oOrdSrv.CondicionPago =dpCodPago.SelectedItem.Text ;
            oOrdSrv.CondicionPagoDias = Convert.ToInt32(dpCodPago.SelectedValue); 
            oOrdSrv.TipoMoneda =dpMoneda.SelectedValue;
            oOrdSrv.IdCatServicio = Convert.ToInt32(dpServicio.SelectedValue);
            oOrdSrv.Servicio = dpServicio.SelectedItem.Text;
            oOrdSrv.Descripcion = txDescServicio.Text;
            if (oOrdSrv.Descripcion.Length > 128) { oOrdSrv.Descripcion = oOrdSrv.Descripcion.Substring(0, 127); }
            oOrdSrv.Proyecto = dpProyecto.SelectedValue;
            oOrdSrv.IdUsr = oCredencial.IdUsr;
            oOrdSrv.UnidadNegocio = oCredencial.UnidadNegocio;
            if (chkEnvioCorreo.Checked) { oOrdSrv.EnviaCorreoClte =1; }
            if (chkEspecial.Checked) { oOrdSrv.Especial = 1; }

            //Calcula el iva al importe dependiedo si la solicitud es cliente especial o no
            if (oOrdSrv.TipoSolicitud == cpplib.OrdenServicio.enTipoSolicitud.Fijo)
            {
                Decimal Total = Convert.ToDecimal(txMonto.Text.Replace("$", "").Replace(",", ""));
                //if ((Total > 0) && (oOrdSrv.Especial == 0)) { Total = (((Total / 100) * 16) + Total); }
                oOrdSrv.Importe = Convert.ToDecimal(Total);
                oOrdSrv.Importe = Total;
            }
            return oOrdSrv;
        }

        private int DaTipoPeriodo()
        {
            int resultado=0;
            if(rbMes.Checked){resultado=1;}
            if(rdBimestral.Checked){resultado=2;}
            if(rdSemestral.Checked){resultado=6;}
            if(rdAnual.Checked){resultado=12;}
            return resultado;
        }
                
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            cpplib.ArchivoContrato oContrato= new cpplib.ArchivoContrato();
            
            List<cpplib.OrdenFactura> lstOrdenes = new List<cpplib.OrdenFactura>();
            if (ValidaMonto())
            {
                if (ValidaContrato(oContrato))
                {
                    //cpplib.admOrdenServicio admOrdSvc = new cpplib.admOrdenServicio();
                    cpplib.OrdenServicio OrdSvc = CargaDatosOrden();
                    OrdSvc.IdServicio = comun.admordenservicio.daSiguienteIdServicio(); //admOrdSvc.daSiguienteIdServicio();
                    OrdSvc.Contrato = Convert.ToInt32(oContrato.Exite);
                    if (comun.admordenservicio.nueva(OrdSvc))
                    {
                        if (RegistraOrdenFacturas(OrdSvc, lstOrdenes))
                        {
                            RegistraContrato(OrdSvc.IdServicio, oContrato);
                            ValidaEnvioCorreo(lstOrdenes);
                            Response.Redirect("espera.aspx");
                        }
                    }
                }
            }
        }

        private bool ValidaMonto()
        {
            bool resultado = false;
            if (dpTpSolicitud.SelectedValue == "1")
            {
                if (Convert.ToDecimal(txMonto.Text) > 0) { resultado = true; } else { ltMsg.Text = "El monto no es valido"; }
            }
            if (dpTpSolicitud.SelectedValue == "2") { resultado = true; }

            return resultado;
        }

        private bool ValidaContrato(cpplib.ArchivoContrato poContrato)
        {
            bool resultado = false;
            ltMsg.Text = "";
            try
            {
                if (fulContrato.HasFile)
                {
                    cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
                    String RutaContrato = Server.MapPath(@"cxc_doc\");
                    poContrato.UbicacionTmp = RutaContrato + oCredencial.IdUsr.ToString() + "_C" + System.IO.Path.GetExtension(fulContrato.FileName);
                    if (System.IO.File.Exists(poContrato.UbicacionTmp )){System.IO.File.Delete(poContrato.UbicacionTmp);}
                    fulContrato.SaveAs(poContrato.UbicacionTmp );
                    if (System.IO.File.Exists(poContrato.UbicacionTmp ))
                    {
                        resultado = true;
                        poContrato.Exite=true;
                    }
                    else { ltMsg.Text = "El contrato no se cargo, intente nuevamente"; }
                    
                }
                else {resultado = true; }
            }
            catch (Exception ex) 
            { 
                ltMsg.Text = ex.Message.ToString(); 
            }
            return resultado;
        }

        private void  RegistraContrato(int IdServicio, cpplib.ArchivoContrato poContrato){
            poContrato.IdServicio =IdServicio ;
            poContrato.ArchivoDestino=IdServicio.ToString().PadLeft(6, '0') + "_C" + System.IO.Path .GetExtension (poContrato.UbicacionTmp);
            String Destino = Server.MapPath(@"cxc_doc\Contratos\") + poContrato.ArchivoDestino;
            if (System.IO.File.Exists(poContrato.UbicacionTmp )){System.IO.File.Move(poContrato.UbicacionTmp,Destino);}
            if (System.IO.File.Exists(Destino))
            {
                //cpplib .admArchivosContrato adm = new cpplib.admArchivosContrato ();
                comun.admarchivoscontrato.Agrega(poContrato);  //adm.Agrega (poContrato);
            }
        }

        private bool RegistraOrdenFacturas(cpplib.OrdenServicio OrdSvc, List<cpplib.OrdenFactura> lstOrdenes )
        {
            bool Resultado = true;
            int NumPeriodo=0;
            //cpplib.admOrdenFactura admOrdFac =new cpplib.admOrdenFactura();
            cpplib.admPartidasFactura admPartidas = new cpplib.admPartidasFactura();
            try
            {
                while (NumPeriodo< OrdSvc.Periodos) {
                    NumPeriodo+= 1;
                    cpplib.OrdenFactura oOrdFact = new cpplib.OrdenFactura();
                    oOrdFact.IdServicio = OrdSvc.IdServicio;
                    oOrdFact.IdOrdenFactura = comun.admordenfactura.daSiguienteIdentificador(); //admOrdFac.daSiguienteIdentificador();
                    oOrdFact.FechaInicio = CalculaFechaDeOrdenFactura(NumPeriodo, OrdSvc.TipoPeriodo, OrdSvc.FechaInicio);
                    oOrdFact.FechaFactura = CalculaFechaDeOrdenFactura(NumPeriodo, OrdSvc.TipoPeriodo, OrdSvc.FechaInicio);
                    oOrdFact.IdCliente =OrdSvc.IdCliente;
                    oOrdFact.Rfc = OrdSvc.Rfc;
                    oOrdFact.Cliente =OrdSvc.Cliente;
                    oOrdFact.IdEmpresa= OrdSvc.IdEmpresa;
                    oOrdFact.Empresa= OrdSvc.Empresa;
                    oOrdFact.TipoSolicitud = OrdSvc.TipoSolicitud;
                    oOrdFact.Importe = OrdSvc.Importe; 
                    oOrdFact.IdCatServicio = OrdSvc.IdCatServicio;
                    oOrdFact.Servicio = OrdSvc.Servicio; 
                    oOrdFact.Descripcion =OrdSvc.Descripcion ;
                    oOrdFact.Anotaciones = TxAnotacion.Text.Trim();
                    if (oOrdFact.Anotaciones.Length > 255) { oOrdFact.Anotaciones = oOrdFact.Anotaciones.Substring(0, 254); }
                    oOrdFact.CondicionPago = OrdSvc.CondicionPago;
                    oOrdFact.CondicionPagoDias  = OrdSvc.CondicionPagoDias ;
                    oOrdFact.TipoMoneda = OrdSvc.TipoMoneda;
                    oOrdFact.IdUsr = OrdSvc.IdUsr;
                    oOrdFact.UnidadNegocio = OrdSvc.UnidadNegocio;
                    oOrdFact.Proyecto = OrdSvc.Proyecto;
                    oOrdFact.Estado = cpplib.OrdenFactura.EstadoOrdFac.Solicitud;
                    oOrdFact.EnviaCorreoClte = OrdSvc.EnviaCorreoClte;
                    oOrdFact.Especial = OrdSvc.Especial;
                    oOrdFact.FhCompromisoPago = oOrdFact.FechaFactura.AddDays(OrdSvc.CondicionPagoDias).ToString("dd/MM/yyyy");

                    lstOrdenes.Add(oOrdFact);
                    comun.admordenfactura.nueva(oOrdFact);  //admOrdFac.nueva(oOrdFact);
                    RegistraBitacora(oOrdFact.IdServicio, oOrdFact.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.Solicitud);
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        private DateTime CalculaFechaDeOrdenFactura( int NumPeriodo, int TipoPeriodo,DateTime FechaInicio ) { 
            DateTime Resultado = DateTime.Now; 
            if (NumPeriodo ==1){Resultado=FechaInicio ;}
            else{Resultado = FechaInicio.AddMonths((TipoPeriodo * (NumPeriodo-1)));}
            return Resultado;
        }

        private void RegistraBitacora( int IdServicio, int IdOrdenFactura,cpplib.OrdenFactura.EstadoOrdFac Estado)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.cxcBitacora oBitacora = new cpplib.cxcBitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = oCrd.IdUsr;
            oBitacora.Nombre = oCrd.Nombre;
            oBitacora.Estado = Estado;

            bool Resultado = comun.admcxcbitacora.Registrar(oBitacora);  //(new cpplib.admCxcBitacora()).Registrar(oBitacora);
        }
        
        private void ValidaEnvioCorreo(List<cpplib.OrdenFactura> lstOrdenes)
        {
            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            foreach (cpplib.OrdenFactura Orden in lstOrdenes) { 
                if(Orden.FechaInicio <= DateTime.Now){
                    csGeneral admG = new csGeneral();
                    if (Orden.TipoSolicitud == cpplib.OrdenServicio.enTipoSolicitud.Fijo)
                    {
                        if (Orden.Especial == 1){
                            comun.admordenfactura.CambiaEstadoOrdenFactura(Orden.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro );
                            RegistraBitacora(Orden.IdServicio, Orden.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.En_Cobro);
                        }
                        else {
                            comun.admordenfactura.CambiaEstadoOrdenFactura(Orden.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura);
                            RegistraBitacora(Orden.IdServicio, Orden.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura);
                            admG.EnviaCorreoOrdenFacturacion(Orden); 
                        }
                    }
                    else {admG.EnviaCorreoSolDatosOrdenFactura(Orden.IdOrdenFactura.ToString()); } 
                }
            }            
        }

        [System.Web.Services.WebMethod()] 
        [System.Web.Script.Services.ScriptMethod()] 
        public static AjaxControlToolkit.CascadingDropDownNameValue[] xDatos(string knownCategoryValues, string category)
        {
            List<AjaxControlToolkit.CascadingDropDownNameValue> Respuesta = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            if (category.Equals("Empresa"))
            {
               cpplib.admCatCondPago adm = new cpplib.admCatCondPago();
                List<cpplib.Empresa> lstEmpresa = (new cpplib.admCatEmpresa()).ListaEmpresas();
                foreach (cpplib.Empresa oEmp in lstEmpresa)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oEmp.Nombre, oEmp.Id.ToString ()));
                }
            }
            if (category.Equals("CdPago"))
            {
                string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
                cpplib.admCatCondPago adm = new cpplib.admCatCondPago();
                List<cpplib.catCondPago> lstCat = adm.ListaCondiconesPago(ValSel);
                foreach (cpplib.catCondPago oCodP in lstCat)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oCodP.Titulo, oCodP.NumDias.ToString()));
                }
            }

            if (category.Equals("Proyecto"))
            {
                string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
                cpplib.admCatProyectos adm = new cpplib.admCatProyectos();
                List<cpplib.CatProyectos> lstCat = adm.ListaCatProyectos(ValSel);
                foreach (cpplib.CatProyectos oProy in lstCat)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oProy.Titulo, oProy.Titulo));
                }
            }

            if (category.Equals("Servicio"))
            {
                string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
                cpplib.admCatServicios adm = new cpplib.admCatServicios();
                List<cpplib.catServicios> lstCat = adm.ListaServicios(ValSel);
                foreach (cpplib.catServicios oProy in lstCat)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oProy.Titulo, oProy.Id.ToString ()));
                }
            }

            return Respuesta.ToArray();
        }
                      
        private DateTime  calculaFechaTermino() {
            DateTime fh = Convert.ToDateTime(txFhInicio.Text);
            if (!String.IsNullOrEmpty(txPeriodos.Text))
            {
                int NumPeriodo = Convert.ToInt32(txPeriodos.Text);
                int TipoPeriodo = DaTipoPeriodo();
                fh = fh.AddMonths((TipoPeriodo * (NumPeriodo - 1)));    
            }
            return fh; 
        }

    }

}