using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cxpcxc.Utilerias;
using System.IO;
using System.Xml;
using System.Runtime.CompilerServices;
using cpplib;
using System.Drawing;
using CXPCXC.Modelos;

namespace cxpcxc
{
    public partial class cxc_AltaOrdenServicio2 : Utilerias.Comun
    {
        public cpplib.credencial Crd;

        #region Eventos ************************************************************
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
                Crd = (cpplib.credencial)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpTpSolicitud.Attributes.Add("onChange", "VistaRegPartidas();");

                ValidarSesionPartidas();
                lbUnidadNegocio.Text = admin.catunidadnegocio.SeleccionarPorId(Crd.UnidadNegocio).Titulo;
                this.txFhInicio.Attributes.Add("readonly", "true");
                this.txFhInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Llenarcombos();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ValidarSesionPartidas();
            Response.Redirect("espera.aspx");
        }

        protected void dpCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpCliente.SelectedIndex > 0)
            {
                //cpplib.CatClientes oCte = comun.admcatclientes.carga(Convert.ToInt32(dpCliente.SelectedValue)); //(new cpplib.admCatClientes()).carga(Convert.ToInt32(dpCliente.SelectedValue));
                modelos.Inicializar();
                modelos.cat_Clientes = admin.catclientes.SeleccionarPorId(int.Parse(dpCliente.SelectedValue));
                lbNombre.Text = modelos.cat_Clientes.Nombre;
                lbRfc.Text = modelos.cat_Clientes.Rfc;
                lbDireccion.Text = modelos.cat_Clientes.Direccion;
                lbCiudad.Text = modelos.cat_Clientes.Ciudad;
                lbEstado.Text = modelos.cat_Clientes.Estado;
                lbCp.Text = modelos.cat_Clientes.Cp;
                lbCorreo.Text = modelos.cat_Clientes.Correo;
                pnCliente.Visible = true;                                
            }
            else
            {
                pnCliente.Visible = false;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            cxc_ArchivoContrato oContrato = new cxc_ArchivoContrato();
            List<CXPCXC.Modelos.cxc_OrdenFactura> lstOrdenes = new List<CXPCXC.Modelos.cxc_OrdenFactura>();
            if (ValidarMonto())
            {
                if (ValidarContrato(oContrato))
                {
                    cxc_OrdenServicio OrdSvc = new cxc_OrdenServicio();
                    OrdSvc.IdCliente = Convert.ToInt32(dpCliente.SelectedValue);
                    OrdSvc.Rfc = lbRfc.Text;
                    OrdSvc.Cliente = lbNombre.Text;
                    OrdSvc.IdEmpresa = Convert.ToInt32(dpEmpresa.SelectedValue);
                    OrdSvc.Empresa = dpEmpresa.SelectedItem.Text;
                    OrdSvc.TipoSolicitud = int.Parse(dpTpSolicitud.SelectedValue);
                    OrdSvc.FechaInicio = DateTime.Parse(txFhInicio.Text);
                    OrdSvc.FechaTermino = CalcularFechaTermino();
                    OrdSvc.Periodos = Convert.ToInt32(txPeriodos.Text.ToString());
                    OrdSvc.TipoPeriodo = DaTipoPeriodo();
                    OrdSvc.CondicionPago = dpCodPago.SelectedItem.Text;
                    OrdSvc.CondicionPagoDias = Convert.ToInt32(dpCodPago.SelectedValue);
                    OrdSvc.TipoMoneda = dpMoneda.SelectedValue;
                    OrdSvc.IdCatServicio = Convert.ToInt32(dpServicio.SelectedValue);
                    OrdSvc.Servicio = dpServicio.SelectedItem.Text;
                    OrdSvc.Descripcion = txDescServicio.Text;
                    if (OrdSvc.Descripcion.Length > 128) 
                        OrdSvc.Descripcion = OrdSvc.Descripcion.Substring(0, 127);
                    OrdSvc.Proyecto = dpProyecto.SelectedValue;
                    OrdSvc.IdUsr = Crd.IdUsr;
                    OrdSvc.UnidadNegocio = Crd.UnidadNegocio;
                    if (chkEnvioCorreo.Checked) 
                        OrdSvc.EnviaCorreoClte = 1;
                    if (chkEspecial.Checked) 
                        OrdSvc.Especial = 1;

                    //Calcula el iva al importe dependiedo si la solicitud es cliente especial o no
                    if (OrdSvc.TipoSolicitud == (int)Enumeradores.enTipoSolicitud.Fijo)
                    {
                        decimal Total = Convert.ToDecimal(txMonto.Text.Replace("$", "").Replace(",", ""));
                        //if ((Total > 0) && (oOrdSrv.Especial == 0)) { Total = (((Total / 100) * 16) + Total); }
                        OrdSvc.Importe = Convert.ToDecimal(Total);
                        OrdSvc.Importe = Total;
                    }
                    OrdSvc.IdServicio = cxc.cxcordenservicoctrl.SeleccionarId_Agregado();
                    OrdSvc.Contrato = Convert.ToInt32(oContrato.Existe);
                    if (cxc.cxcordenservicio.Agregar_Registro(OrdSvc))
                    {
                        if (RegistrarOrdenFacturas(OrdSvc, lstOrdenes))
                        {
                            RegistrarContrato(OrdSvc.IdServicio, oContrato);
                            ValidarEnvioCorreo(lstOrdenes);
                            Response.Redirect("espera.aspx");
                        }
                    }
                }
            }
        }

        protected void BtnSubirPDF_Click(object sender, EventArgs e)
        {
            if (FileUpload2.FileName == "")
            {
                LblMensaje2.Text = "Seleccione un archivo";
                return;
            }
            else
            {
                LblMensaje2.Text = "";
            }

            FileUpload2.SaveAs(Server.MapPath(@"Descargas\") + FileUpload2.FileName);
            LblMensaje2.Text = "Archivo subido exitosamente";
        }

        protected void BtnSubirXML_Click(object sender, EventArgs e)
        {
            if (FileUpload3.FileName == "")
            {
                LblMensaje3.Text = "Seleccione un archivo";
                return;
            }
            else
            { 
                LblMensaje3.Text = "";
            }
            
            FileUpload3.SaveAs(Server.MapPath(@"Descargas\") + FileUpload3.FileName);
            LblMensaje3.Text = "Archivo subido exitosamente";
            ProcesarXML(Server.MapPath(@"Descargas\") + FileUpload3.FileName);
            //Procesar el archivo xml para que llene los campos
            //if (ExtraerDatosXML(Server.MapPath(@"Descargas\") + FileUpload3.FileName))
            //{
            //}
        }
        #endregion
        
        #region Métodos ************************************************************
        private void Llenarcombos()
        {
            admin.catclientes.Seleccionar_DropDownList_PorEmpresa(ref dpCliente, Crd.IdEmpresaTrabajo.ToString()); //LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(Crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            LlenarControles.LlenarDropDownList(ref dpEmpresa, comun.admempresasclientes.Seleccionar(Crd.IdEmpresaTrabajo.ToString()), "NombreEmpresa", "IdEmpresa");
            LlenarControles.LlenarDropDownList(ref dpServicio, comun.admcatservicios.ListaServicios(Crd.IdEmpresaTrabajo.ToString()), "Titulo", "Id");
            LlenarControles.LlenarDropDownList(ref dpCodPago, comun.admcatcondpago.ListaCondiconesPago(Crd.IdEmpresaTrabajo.ToString()), "Titulo", "Id");
            LlenarControles.LlenarDropDownList(ref dpProyecto, comun.admcatproyectos.ListaCatProyectos(Crd.IdEmpresaTrabajo.ToString()), "Titulo", "Id");
            admin.catmoneda.Seleccionar_DropDownList(ref dpMoneda); //LlenarControles.LlenarDropDownList(ref dpMoneda, comun.admcatmonedas.Seleccionar(), "Nombre", "Nombre");
            dpEmpresa.SelectedValue = Crd.IdEmpresaTrabajo.ToString();

        }

        private void ValidarSesionPartidas()
        {
            if (Session["Partidas"] != null)
            {
                Session.Remove("Partidas");
            }
        }
        
        private cpplib.OrdenServicio CargarDatosOrden()
        {
            cpplib.OrdenServicio oOrdSrv = new cpplib.OrdenServicio();
            oOrdSrv.IdCliente = Convert.ToInt32(dpCliente.SelectedValue);
            oOrdSrv.Rfc = lbRfc.Text;
            oOrdSrv.Cliente = lbNombre.Text;
            oOrdSrv.IdEmpresa = Convert.ToInt32(dpEmpresa.SelectedValue);
            //oOrdSrv.Empresa = dpEmpresa.SelectedItem.Text;
            oOrdSrv.TipoSolicitud = (cpplib.OrdenServicio.enTipoSolicitud)(Convert.ToInt32(dpTpSolicitud.SelectedValue));
            oOrdSrv.FechaInicio = Convert.ToDateTime(txFhInicio.Text);
            oOrdSrv.FechaTermino = CalcularFechaTermino();
            oOrdSrv.Periodos = Convert.ToInt32(txPeriodos.Text.ToString());
            oOrdSrv.TipoPeriodo = DaTipoPeriodo();
            oOrdSrv.CondicionPago = dpCodPago.SelectedItem.Text;
            oOrdSrv.CondicionPagoDias = Convert.ToInt32(dpCodPago.SelectedValue);
            oOrdSrv.TipoMoneda = dpMoneda.SelectedValue;
            oOrdSrv.IdCatServicio = Convert.ToInt32(dpServicio.SelectedValue);
            oOrdSrv.Servicio = dpServicio.SelectedItem.Text;
            oOrdSrv.Descripcion = txDescServicio.Text;
            if (oOrdSrv.Descripcion.Length > 128) 
                oOrdSrv.Descripcion = oOrdSrv.Descripcion.Substring(0, 127);
            oOrdSrv.Proyecto = dpProyecto.SelectedValue;
            oOrdSrv.IdUsr = Crd.IdUsr;
            oOrdSrv.UnidadNegocio = Crd.UnidadNegocio;
            if (chkEnvioCorreo.Checked) 
                oOrdSrv.EnviaCorreoClte = 1;
            if (chkEspecial.Checked) 
                oOrdSrv.Especial = 1;

            //Calcula el iva al importe dependiendo si la solicitud es cliente especial o no
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
            int resultado = 0;
            if (rbMes.Checked) 
                resultado = 1;
            if (rdBimestral.Checked) 
                resultado = 2;
            if (rdSemestral.Checked) 
                resultado = 6;
            if (rdAnual.Checked) 
                resultado = 12;
            return resultado;
        }

        private bool ValidarMonto()
        {
            bool resultado = false;
            if (dpTpSolicitud.SelectedValue == "1")
            {
                if (Convert.ToDecimal(txMonto.Text) > 0) 
                    resultado = true; 
                else 
                    ltMsg.Text = "El monto no es valido"; 
            }
            if (dpTpSolicitud.SelectedValue == "2") 
                resultado = true;
            return resultado;
        }

        /// <summary>
        /// Valida que el contrato exista físicamente en el sistema
        /// </summary>
        /// <param name="poContrato"></param>
        /// <returns></returns>
        private bool ValidarContrato(cxc_ArchivoContrato poContrato)
        {
            bool resultado = false;
            ltMsg.Text = "";
            try
            {
                if (fulContrato.HasFile)
                {
                    //cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
                    string RutaContrato = Server.MapPath(@"cxc_doc\");
                    poContrato.UbicacionTmp = RutaContrato + Crd.IdUsr.ToString() + "_C" + System.IO.Path.GetExtension(fulContrato.FileName);
                    if (System.IO.File.Exists(poContrato.UbicacionTmp)) 
                        System.IO.File.Delete(poContrato.UbicacionTmp); 
                    fulContrato.SaveAs(poContrato.UbicacionTmp);
                    if (System.IO.File.Exists(poContrato.UbicacionTmp))
                    {
                        resultado = true;
                        poContrato.Existe = true;
                    }
                    else 
                    { 
                        ltMsg.Text = "El contrato no se cargo, intente nuevamente"; 
                    }
                }
                else 
                { 
                    resultado = true; 
                }
            }
            catch (Exception ex)
            {
                ltMsg.Text = ex.Message.ToString();
            }
            return resultado;
        }
        
        //Sube archivo
        private void RegistrarContrato(int IdServicio, cxc_ArchivoContrato poContrato)
        {
            poContrato.IdServicio = IdServicio;
            poContrato.ArchivoDestino = IdServicio.ToString().PadLeft(6, '0') + "_C" + System.IO.Path.GetExtension(poContrato.UbicacionTmp);
            string Destino = Server.MapPath(@"cxc_doc\Contratos\") + poContrato.ArchivoDestino;
            if (System.IO.File.Exists(poContrato.UbicacionTmp)) 
                System.IO.File.Move(poContrato.UbicacionTmp, Destino);
            if (System.IO.File.Exists(Destino))
            {
                //cpplib .admArchivosContrato adm = new cpplib.admArchivosContrato ();
                //comun.admarchivoscontrato.Agrega(poContrato);  //adm.Agrega (poContrato);
                cxc.cxcarchivoscontrato.Agregar_Registro(poContrato);
            }
        }

        private bool RegistrarOrdenFacturas(cxc_OrdenServicio OrdSvc, List<CXPCXC.Modelos.cxc_OrdenFactura> lstOrdenes)
        {
            bool Resultado = true;
            int NumPeriodo = 0;
            //cpplib.admPartidasFactura admPartidas = new cpplib.admPartidasFactura();
            try
            {
                {
                    while (NumPeriodo < OrdSvc.Periodos)
                    {
                        NumPeriodo += 1;
                        CXPCXC.Modelos.cxc_OrdenFactura oOrdFact = new CXPCXC.Modelos.cxc_OrdenFactura();
                        oOrdFact.IdServicio = OrdSvc.IdServicio;
                        oOrdFact.FechaInicio = CalcularFechaDeOrdenFactura(NumPeriodo, OrdSvc.TipoPeriodo, OrdSvc.FechaInicio);
                        oOrdFact.FechaFactura = CalcularFechaDeOrdenFactura(NumPeriodo, OrdSvc.TipoPeriodo, OrdSvc.FechaInicio);
                        oOrdFact.IdCliente = OrdSvc.IdCliente;
                        oOrdFact.Rfc = OrdSvc.Rfc;
                        oOrdFact.Cliente = OrdSvc.Cliente;
                        oOrdFact.IdEmpresa = OrdSvc.IdEmpresa;
                        oOrdFact.Empresa = OrdSvc.Empresa;
                        oOrdFact.TipoSolicitud = (int)OrdSvc.TipoSolicitud;
                        oOrdFact.Importe = OrdSvc.Importe;
                        oOrdFact.IdCatServicio = OrdSvc.IdCatServicio;
                        oOrdFact.Servicio = OrdSvc.Servicio;
                        oOrdFact.Descripcion = OrdSvc.Descripcion;
                        oOrdFact.Anotaciones = TxAnotacion.Text.Trim();
                        if (oOrdFact.Anotaciones.Length > 255)
                            oOrdFact.Anotaciones = oOrdFact.Anotaciones.Substring(0, 254);
                        oOrdFact.CondicionPago = OrdSvc.CondicionPago;
                        oOrdFact.CondicionPagoDias = OrdSvc.CondicionPagoDias;
                        oOrdFact.TipoMoneda = OrdSvc.TipoMoneda;
                        oOrdFact.IdUsr = OrdSvc.IdUsr;
                        oOrdFact.UnidadNegocio = OrdSvc.UnidadNegocio;
                        oOrdFact.Proyecto = OrdSvc.Proyecto;
                        oOrdFact.Estado = (int)Enumeradores.EstadoOrdFac.Solicitud;
                        oOrdFact.EnviaCorreoClte = OrdSvc.EnviaCorreoClte;
                        oOrdFact.Especial = OrdSvc.Especial;
                        oOrdFact.FechaCompromisoPago = DateTime.Parse(oOrdFact.FechaFactura.AddDays(OrdSvc.CondicionPagoDias).ToString("dd/MM/yyyy"));

                        lstOrdenes.Add(oOrdFact);
                        //comun.admordenfactura.nueva(oOrdFact);  //admOrdFac.nueva(oOrdFact);
                        int idobtenido = 0;
                        cxc.cxcordenfactura.Agregar_Registro(oOrdFact, ref idobtenido);
                        RegistrarBitacora(oOrdFact.IdServicio, idobtenido, Enumeradores.EstadoOrdFac.Solicitud);
                    }
                }
            }
            catch (Exception)
            {
                Resultado = false;
            }

            return Resultado;
        }

        private DateTime CalcularFechaDeOrdenFactura(int NumPeriodo, int TipoPeriodo, DateTime FechaInicio)
        {
            DateTime Resultado = DateTime.Now;
            if (NumPeriodo == 1) 
                Resultado = FechaInicio;
            else 
                Resultado = FechaInicio.AddMonths((TipoPeriodo * (NumPeriodo - 1)));
            return Resultado;
        }

        private void RegistrarBitacora(int IdServicio, int IdOrdenFactura, CXPCXC.Modelos.Enumeradores.EstadoOrdFac Estado)
        {
            //cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            CXPCXC.Modelos.cxc_Bitacora oBitacora = new CXPCXC.Modelos.cxc_Bitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = Crd.IdUsr;
            oBitacora.Nombre = Crd.Nombre;
            oBitacora.Estado = Estado;

            //Anterior//bool Resultado = comun.admcxcbitacora.Registrar(oBitacora);  //(new cpplib.admCxcBitacora()).Registrar(oBitacora);
            bool Resultado = cxc.cxcbitacora.Agregar_Registro(oBitacora);
        }

        private void ValidarEnvioCorreo(List<CXPCXC.Modelos.cxc_OrdenFactura> lstOrdenes)
        {
            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            foreach (CXPCXC.Modelos.cxc_OrdenFactura Orden in lstOrdenes)
            {
                if (Orden.FechaInicio <= DateTime.Now)
                {
                    csGeneral admG = new csGeneral();
                    if (Orden.TipoSolicitud == (int)CXPCXC.Modelos.Enumeradores.enTipoSolicitud.Fijo)
                    {
                        if (Orden.Especial == 1)
                        {
                            //comun.admordenfactura.CambiaEstadoOrdenFactura(Orden.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.En_Cobro);
                            cxc.cxcordenfactura.Modificar_Estado(Orden.IdOrdenFactura, (int)CXPCXC.Modelos.Enumeradores.EstadoOrdFac.En_Cobro);
                            RegistrarBitacora(Orden.IdServicio, Orden.IdOrdenFactura, CXPCXC.Modelos.Enumeradores.EstadoOrdFac.En_Cobro);
                        }
                        else
                        {
                            //comun.admordenfactura.CambiaEstadoOrdenFactura(Orden.IdOrdenFactura.ToString(), cpplib.OrdenFactura.EstadoOrdFac.Generacion_Factura);
                            cxc.cxcordenfactura.Modificar_Estado(Orden.IdOrdenFactura, (int)CXPCXC.Modelos.Enumeradores.EstadoOrdFac.Generacion_Factura);
                            RegistrarBitacora(Orden.IdServicio, Orden.IdOrdenFactura, CXPCXC.Modelos.Enumeradores.EstadoOrdFac.Generacion_Factura);
                            //admG.EnviaCorreoOrdenFacturacion(Orden);
                        }
                    }
                    else
                    {
                        admG.EnviaCorreoSolDatosOrdenFactura(Orden.IdOrdenFactura.ToString());
                    }
                }
            }
        }

        private DateTime CalcularFechaTermino()
        {
            DateTime fh = Convert.ToDateTime(txFhInicio.Text);
            if (!String.IsNullOrEmpty(txPeriodos.Text))
            {
                int NumPeriodo = Convert.ToInt32(txPeriodos.Text);
                int TipoPeriodo = DaTipoPeriodo();
                fh = fh.AddMonths((TipoPeriodo * (NumPeriodo - 1)));
            }
            return fh;
        }

        private void ProcesarXML(string documento)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(documento);//Leer el XML

            //agregamos un Namespace, que usaremos para buscar que el nodo no exista:
            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");

            //Acceder a nodo "Comprobante"
            XmlNode nodeComprobante = doc.SelectSingleNode("//cfdi:Comprobante", nsm);

            //Obtener Folio, Serie, SubTotal y Total
            string varFolio = nodeComprobante.Attributes["Folio"].Value;
            string varFecha = nodeComprobante.Attributes["Fecha"].Value;
            string varSerie = nodeComprobante.Attributes["Serie"].Value;
            string varSubTotal = nodeComprobante.Attributes["SubTotal"].Value;
            string varDescuento = nodeComprobante.Attributes["Descuento"].Value;
            string varMoneda = nodeComprobante.Attributes["Moneda"].Value;
            string varTotal = nodeComprobante.Attributes["Total"].Value;

            //Validar que la factura sea para la empresa que se quiere aplicar
            //Acceder a nodo Receptor
            XmlNode nodeReceptor = nodeComprobante.SelectSingleNode("cfdi:Receptor", nsm);
            string varRFCReceptor = nodeReceptor.Attributes["Rfc"].Value;

            if (varRFCReceptor == admin.catempresas.SelecionarPorId(Crd.IdEmpresaTrabajo).Rfc) //comun.admcatempresa.ObtenerRFC(Crd.IdEmpresaTrabajo.ToString()))
            {

                //Acceder a nodo Emisor
                XmlNode nodeEmisor = nodeComprobante.SelectSingleNode("cfdi:Emisor", nsm);

                //Obtener RFC del Emisor
                string varRFC = nodeEmisor.Attributes["Rfc"].Value;

                txFhInicio.Text = varFecha;
                /*Procedimiento para que seleccione el cliente y muestre sus datos automaticamente */

                dpCliente.SelectedValue = admin.catclientes.Seleccionar_PorIdEmpresaRFC(Crd.IdEmpresaTrabajo, varRFC).Id.ToString(); // comun.admcatclientes.DaClienteXRfc(Crd.IdEmpresaTrabajo.ToString(), varRFC).Nombre;
                dpCliente_SelectedIndexChanged(null, null);
                
                /* fin procedimiento */

                txtSubTotal.Text = varSubTotal;
                txtDescuento.Text = varDescuento;
                txMonto.Text = varTotal;
                dpMoneda.SelectedIndex = varMoneda == "MXN" ? 1 : 2; //comun.admcatmonedas.Seleccionar_Nombre(varMoneda);
                

                XmlNode nodeConceptos = nodeComprobante.SelectSingleNode("cfdi:Conceptos", nsm);
                foreach (XmlNode node in nodeConceptos.SelectNodes("cfdi:Concepto", nsm))
                {
                    txDescServicio.Text += string.Format(node.Attributes["Descripcion"].Value + "//");
                }
            }
            else
            {
                ltMsg.Text = "La factura no corresponde con la empresa asignada. Fin de la operación.";
            }
        }

        #endregion

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static AjaxControlToolkit.CascadingDropDownNameValue[] xDatos(string knownCategoryValues, string category)
        {
            List<AjaxControlToolkit.CascadingDropDownNameValue> Respuesta = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            //if (category.Equals("Empresa"))
            //{
            //    cpplib.admCatCondPago adm = new cpplib.admCatCondPago();
            //    List<cpplib.Empresa> lstEmpresa = (new cpplib.admCatEmpresa()).ListaEmpresas();
            //    foreach (cpplib.Empresa oEmp in lstEmpresa)
            //    {
            //        Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oEmp.Nombre, oEmp.Id.ToString()));
            //    }
            //}
            //if (category.Equals("CdPago"))
            //{
            //    string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
            //    cpplib.admCatCondPago adm = new cpplib.admCatCondPago();
            //    List<cpplib.catCondPago> lstCat = adm.ListaCondiconesPago(ValSel);
            //    foreach (cpplib.catCondPago oCodP in lstCat)
            //    {
            //        Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oCodP.Titulo, oCodP.NumDias.ToString()));
            //    }
            //}

            //if (category.Equals("Proyecto"))
            //{
            //    string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
            //    cpplib.admCatProyectos adm = new cpplib.admCatProyectos();
            //    List<cpplib.CatProyectos> lstCat = adm.ListaCatProyectos(ValSel);
            //    foreach (cpplib.CatProyectos oProy in lstCat)
            //    {
            //        Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oProy.Titulo, oProy.Titulo));
            //    }
            //}

            //if (category.Equals("Servicio"))
            //{
            //    string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
            //    cpplib.admCatServicios adm = new cpplib.admCatServicios();
            //    List<cpplib.catServicios> lstCat = adm.ListaServicios(ValSel);
            //    foreach (cpplib.catServicios oProy in lstCat)
            //    {
            //        Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oProy.Titulo, oProy.Id.ToString()));
            //    }
            //}

            return Respuesta.ToArray();
        }
    }
}