using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class trf_RegistroPagos : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txF_Pago.Attributes.Add("readonly", "true");
                txCantidad.Attributes.Add("readonly", "true");
                cpplib.credencial cdr = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = cdr.IdEmpresaTrabajo.ToString();
                hdIdUsr.Value = cdr.IdUsr.ToString();
                llenaCatalogos(hdIdEmpresa.Value);
                this.DaSolicitudesPago();
            }
        }

        private void llenaCatalogos(string IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");
            LlenarControles.LlenarDropDownList(ref dpProyecto, comun.admcatproyectos.DaComboProyectos(IdEmpresa), "Texto", "Valor");

            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("SELECCIONAR", "0"));

            //List<cpplib.valorTexto> lstProyectos = (new cpplib.admCatProyectos()).DaComboProyectos(IdEmpresa);

            //dpProyecto.DataSource = lstProyectos;
            //dpProyecto.DataValueField = "Valor";
            //dpProyecto.DataTextField = "Texto";
            //dpProyecto.DataBind();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void DaSolicitudesPago()
        {
            ltMsg.Text = "";
            //cpplib.admFondos admSol = new cpplib.admFondos ();
            DataTable Lista = comun.admfondos.ListaSolicitudesXPagar(hdIdEmpresa.Value, dpProveedor.SelectedValue, dpProyecto.SelectedValue);
            if (Lista.Rows.Count > 0)
            {
                lbNumSol.Text = " TOTAL DE SOLICITUDES: (" + Lista.Rows.Count .ToString ()+ ")";
                //rptSolicitud.DataSource = Lista;
                //rptSolicitud.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptSolicitud, Lista);
                lbTotPesos.Text = Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                lbTotDlls.Text = Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2"); 
            } 
            else 
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                btnContinuar.Visible = false;
                ltMsg.Text = "No hay Solicitudes Disponibles";
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { DaSolicitudesPago(); }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if ((e.CommandName.Equals("btnInactivo")) || (e.CommandName.Equals("btnActivo")))
            {
                string dts = e.CommandArgument.ToString();
                cpplib.admFondos adm = new cpplib.admFondos();
                ImageButton btnActivo = (ImageButton)e.Item.FindControl("btnActivo");
                ImageButton btnInActivo = (ImageButton)e.Item.FindControl("btnInactivo");
                int IdLte = Convert.ToInt32(((Label)(e.Item.FindControl("lbLote"))).Text);
                int IdSol = Convert.ToInt32(((Label)(e.Item.FindControl("lbIdsol"))).Text);

                if (e.CommandName.Equals("btnActivo"))
                {
                    adm.QuitarMarcarGrupoPago(IdLte, IdSol, hdIdUsr.Value);
                    btnActivo.Visible = false; btnInActivo.Visible = true;
                }

                if (e.CommandName.Equals("btnInactivo"))
                {
                    adm.PonerMarcarGrupoPago(IdLte, IdSol, hdIdUsr.Value);
                    btnActivo.Visible = true; btnInActivo.Visible = false;
                }
            }
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                string Marcado = oSol["Marcado"].ToString(); 

                if (Marcado != "0")
                {
                    ImageButton Inc = (ImageButton)e.Item.FindControl("btnInactivo");
                    ImageButton Act = (ImageButton)e.Item.FindControl("btnActivo");
                    if (Marcado == hdIdUsr.Value) 
                    { 
                        Inc.Visible = false; 
                        Act.Visible = true; 
                    }
                    else 
                    { 
                        Inc.Visible = false; 
                        Act.Visible = false; 
                    }
                }
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            txF_Pago.Text = "";
            ltMsg.Text = "";
            ltMsgRegistro.Text = "";
            txNota.Text ="" ;
            txCantidad.Text = "";
            txTc.Text = "";

            DataTable LstSol = comun.admfondos.DasolMarcadasPago(hdIdEmpresa.Value,hdIdUsr.Value);

            if (LstSol.Rows.Count > 0)
            {
                //rpSolSel.DataSource = LstSol;
                //rpSolSel.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rpSolSel, LstSol);

                if (seleccionCorrecta())
                {
                    if(hdTipoMoneda.Value.Equals("Pesos"))
                    {
                        txCantidad.Text = LstSol.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(LstSol.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'")).ToString();
                        txTc.Text = "0";
                        pnTC.Visible = false;
                    }
                    else
                    {
                        lbPagar.Text = LstSol.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(LstSol.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'")).ToString();
                        txCantidad.Text = "";
                        pnTC.Visible = true;
                    }
                    pnRegistro.Visible = true; 
                    pnSeleccion.Visible = false;
                }
                else 
                    ltMsg.Text = "La seleccion no es correcta, asegurese que es del mismo proveedor y tipo de moneda ";
            }
        }
        
        private bool seleccionCorrecta()
        {
            bool resultado = true;

            string PvdBase = string.Empty;
            string Pvd = string.Empty;
            string MonedaBase = string.Empty;
            string Moneda = string.Empty;
            string Idsol = string.Empty;

            foreach (RepeaterItem Reg in rpSolSel.Items)
            {
                Pvd = ((Label)(Reg.FindControl("lbPrv"))).Text;
                Moneda = ((Label)(Reg.FindControl("lbMoneda"))).Text;
                Idsol = ((Label)(Reg.FindControl("lbIdsol"))).Text;
                if (string.IsNullOrEmpty(PvdBase)) 
                { 
                    PvdBase = Pvd; 
                    MonedaBase = Moneda; 
                }
                else
                {
                    if (!(PvdBase.Equals(Pvd) && MonedaBase.Equals(Moneda))) 
                    {
                        resultado = false; break; 
                    }
                }
            }

            hdTipoMoneda.Value = MonedaBase; 
            return resultado ;
        }

        protected void btnRegresar_Click(object sender, EventArgs e) { RegresarSeleccion(); }
        
        private void RegresarSeleccion() {
            pnRegistro.Visible = false;
            pnSeleccion.Visible = true;
            pnlDocumento.Visible = false;
            txNumDoctos.Text = "0";
            DaSolicitudesPago();
        }

        protected void btnVerDocto_Click(object sender, EventArgs e)
        {
            ltMsgRegistro.Text = "";
            List<cpplib.Archivo> LstArchivos = new List<cpplib.Archivo>();
            if (MostrarArchivos(LstArchivos))
            {
                txNumDoctos.Text = LstArchivos.Count.ToString();
                ltDocumento.Text = String.Empty;
                string VerArchivo = String.Empty;
                int contador = 1;
                foreach (cpplib.Archivo oAr in LstArchivos)
                {
                    //VerArchivo = "\\cxp_doc\\" + oAr.ArchivoDestino;
                    VerArchivo = "cxp_doc\\" + oAr.ArchivoDestino;
                    ltDocumento.Text += "<div style ='background-color:#006600; height :25px; color :white; font-size:15px '>DOCUMENTO: " + contador.ToString() + "<hr /></div><br/>";
                    ltDocumento.Text += "<embed src='" + VerArchivo + "'width='98%' height='500px' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' /><br/><br/>";
                    contador += 1;
                }
                
                Session["ArchivosPago"] = LstArchivos;
                pnlDocumento.Visible = true;
            }
            else 
                ltMsgRegistro.Text = "El o los Archivos no cumplen con las especificaciones (nombre menor a 64, y del tipo PDF )";
        }

        private bool MostrarArchivos(List<cpplib.Archivo> LstArchivos)
        {
            bool Resultado = true;
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String Archivo = string.Empty;
            int Contador = 0;
            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                string ext = System.IO.Path.GetExtension(postedFile.FileName);
                if ((postedFile.FileName.Length <= 64) && (ext.ToUpper().Equals(".PDF")))
                {
                    Archivo = oCredencial.IdUsr.ToString() + "_" + Contador.ToString() + "_C.PDF";
                    postedFile.SaveAs(RutaDestino + Archivo);
                    if (!System.IO.File.Exists(RutaDestino + Archivo)) 
                    { 
                        Resultado = Resultado && false; 
                    }
                    cpplib.Archivo oArh = new cpplib.Archivo();
                    oArh.ArchvioOrigen = postedFile.FileName;
                    oArh.ArchivoDestino = Archivo;
                    LstArchivos.Add(oArh);
                }
                else 
                { 
                    Resultado = Resultado && false; 
                }
                Contador += 1;
            }

            return Resultado;
        }

        protected void btnTc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txTc.Text))
            {
                Decimal importe = Convert.ToDecimal(lbPagar.Text);
                Decimal TC = Convert.ToDecimal(txTc.Text);
                txCantidad.Text = (Math.Round((importe * TC), 2)).ToString();
            }
            else 
            { 
                txCantidad.Text = ""; 
            }
        }

        protected void btnRegPago_Click(object sender, EventArgs e)
        {
            List<cpplib.Archivo> LstArchivos = (List<cpplib.Archivo>)Session["ArchivosPago"];
            if (LstArchivos !=null) {
                if (LstArchivos.Count > 0)
                {
                    //cpplib.admFondos admFd = new cpplib.admFondos ();
                    cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                    //cpplib.admArchivos admComp = new cpplib.admArchivos();
                    //cpplib.admCxpConciliarPago admPg = new cpplib.admCxpConciliarPago();

                    int IdPago = comun.admcxpconciliarpago.daSiguienteIdentificador();
                    int Idote = 0;
                    int IdSol = 0; 
                    
                    foreach (RepeaterItem Reg in rpSolSel.Items)
                    {
                        Idote =Convert.ToInt32 ( ((Label)(Reg.FindControl("lbLote"))).Text);
                        IdSol = Convert.ToInt32 (((Label)(Reg.FindControl("lbIdsol"))).Text);

                        cpplib.Solicitud oSol = admSol.carga(IdSol);
                        decimal TotalPagosSol = comun.admarchivos.DaImporteTotalComprobantes(IdSol);
                        
                        if (CopiaArchivosSolicitud(oSol, LstArchivos, IdPago))
                        {
                            cpplib.Solicitud.solEstado EdoSol = cpplib.Solicitud.solEstado.Pagado;
                            Decimal Pagado = TotalPagosSol + oSol.CantidadPagar;

                            if (Pagado < oSol.Importe) 
                                EdoSol = cpplib.Solicitud.solEstado.PagoParcial; 
                            
                            comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, EdoSol, (oSol.Importe - Pagado));
                            comun.admfondos.ActualizaDatosPagoFactura(Idote, IdSol, IdPago, cpplib.SolicitudFondos.enEstado.Pagado);

                            this.RegistraBitacora(oSol.IdSolicitud, EdoSol, oSol.CantidadPagar);
                        }
                    }
                    
                    cpplib.cxpPagos oPago = daObjPago(IdPago);
                    bool resultado = comun.admcxpconciliarpago.Registra(oPago);

                    RegresarSeleccion();
               }
            }
        }
        
        private bool CopiaArchivosSolicitud(cpplib.Solicitud oSol ,List<cpplib.Archivo> Lista, int IdPago)
        {
            bool Resultado = true;
            int contador = 1;

            cpplib.admDirectorio admDir = new cpplib.admDirectorio();
            cpplib.admArchivos admComp = new cpplib.admArchivos();

            String RutaDestino = Server.MapPath(@"cxp_doc\");
            String CarpetaUbicacion =  admDir.DadirectorioArchivo(Convert.ToDateTime(oSol.FechaFactura));
            
            bool ValidaDir = admDir.ValidaDirectorio(RutaDestino + CarpetaUbicacion);
            int IdDocto = admComp.daNumeroComprobante(oSol.IdSolicitud);
                        
            foreach (cpplib.Archivo oA in Lista)
            {
                cpplib.Archivo oComp = new cpplib.Archivo();
                string Ext = System.IO.Path.GetExtension(oA.ArchvioOrigen);
                
                oComp.IdSolicitud = oSol.IdSolicitud;
                oComp.Tipo = cpplib.TipoArchivo.Comprobante;
                oComp.IdDocumento = IdDocto;
                oComp.ArchvioOrigen = oA.ArchvioOrigen;
                oComp.ArchivoDestino = oSol.IdSolicitud.ToString().PadLeft(6, '0') + "_D" + IdDocto.ToString() + "_" + oSol.Factura.ToString ().PadLeft(6, '0') + Ext;
                oComp.IdPago = IdPago;

                if (contador == 1)
                    AgregaCantidadAlPrimerComprobante(oComp, oSol.Moneda ,oSol.CantidadPagar);

                string Destino = RutaDestino + CarpetaUbicacion + oComp.ArchivoDestino;
                string origen=RutaDestino + oA.ArchivoDestino;
                
                System.IO.File.Copy(origen, Destino,true);
                if (!System.IO.File.Exists(Destino)) 
                    Resultado = Resultado && false; 
                
                admComp.Agrega(oComp);
                
                IdDocto += 1;
                contador += 1;
            }
            return Resultado;
        }
        
        private void AgregaCantidadAlPrimerComprobante(cpplib.Archivo Comp, string Moneda,  Decimal CantidadPagar)
        {
            if (Moneda.Equals("Dolares"))
            {
                Comp.Cantidad = CantidadPagar;
                Comp.TipoCambio = Convert.ToDecimal(txTc.Text);
                Comp.Pesos = Convert.ToDecimal(CantidadPagar * Comp.TipoCambio);
            }
            else
            {
                Comp.Cantidad = CantidadPagar;
                Comp.Pesos = CantidadPagar;
            }
           
            Comp.Nota = txNota.Text.Trim();
            if (Comp.Nota.Length > 255) 
                Comp.Nota = Comp.Nota.Substring(1, 255); 
            
        }

        private void RegistraBitacora(int IdSolicitud, cpplib.Solicitud.solEstado pEstado, decimal Cantidad)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSolicitud;
            oBitacora.IdUsr = oCredencial.IdUsr;
            oBitacora.Nombre = oCredencial.Nombre;
            oBitacora.Importe = Cantidad;
            oBitacora.Estado = pEstado;

            oBitacora.FechaRegistro = Convert.ToDateTime(txF_Pago.Text.Trim());
            
            bool Resultado = comun.admbitacorasolicitud.RegistrarPago (oBitacora);
        }

        private cpplib.cxpPagos daObjPago(int IdPago)
        {
            try
            {
                cpplib.cxpPagos oPago = new cpplib.cxpPagos();
                oPago.IdPago = IdPago;
                oPago.Referencia = "0";
                oPago.Banco = "0";
                if (hdTipoMoneda.Value.Equals("Pesos")) { oPago.TipoCambio = 1; }
                else { oPago.TipoCambio = Convert.ToInt32(txTc.Text); }
                oPago.Importe = Convert.ToDecimal(txCantidad.Text);
                if (hdTipoMoneda.Value.Equals("Pesos")) { oPago.Moneda = cpplib.cxpPagos.enMoneda.Pesos; }
                else if (hdTipoMoneda.Value.Equals("Dolares")) { oPago.Moneda = cpplib.cxpPagos.enMoneda.Dolares; }
                oPago.IdUsr = Convert.ToInt32(hdIdUsr.Value);
                return oPago;
            }
            catch (Exception)
            {                
                throw;
            }
            
        }       
             
    }
}