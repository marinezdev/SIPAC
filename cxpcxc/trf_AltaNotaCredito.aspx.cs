using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_AltaNotaCredito : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txFecha.Attributes.Add("readonly", "true");
                txF_Inicio.Attributes.Add("readonly", "true");
                txF_Fin.Attributes.Add("readonly", "true");

                txFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_Fecha.EndDate = DateTime.Now;

                txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_txF_Inicio.EndDate = DateTime.Now;
                txF_Inicio.Text = DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy");
                ce_txF_Fin.EndDate = DateTime.Now;

                cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
                this.llenacatalogos(oCdr.IdEmpresaTrabajo.ToString());
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_ConsultaNotaCredito.aspx"); }

        private void llenacatalogos(string IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");
            //List<cpplib.CatProveedor> LstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = LstPvd;
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            string Archivo = oCredencial.IdUsr.ToString().PadLeft(6, '0') + "NC" +  ".pdf"; ;
            String Directorio = Server.MapPath(@"cxp_doc\") + Archivo;
            string VerArchivo = "\\cxp_doc\\" + Archivo;

            if (CargaArchivo(Directorio)) { PintaRecibo(VerArchivo); txNombreDocto.Text = Archivo; }
        }

        private bool CargaArchivo(String Directorio)
        {
            bool resultado = false;
            try
            {
                if (fulNota.HasFile)
                {
                    if (System.IO.Path.GetExtension(fulNota.FileName).ToUpper().Equals(".PDF"))
                    {
                        if (System.IO.File.Exists(Directorio)) { System.IO.File.Delete(Directorio); }
                        fulNota.SaveAs(Directorio);
                        if (System.IO.File.Exists(Directorio))
                        {
                            resultado = true;
                        }
                        else { ltMsg.Text = "El archivo no se cargo, intente nuevamente"; }
                    }
                    else { ltMsg.Text = "El archivo debe ser PDF"; }
                }
                else { ltMsg.Text = "Agregue el archivo"; }
            }
            catch (Exception ex) { ltMsg.Text = ex.Message.ToString(); }
            return resultado;
        }

        private void PintaRecibo(String pArchivo)
        {
            if (!pArchivo.Equals("undefined") && !string.IsNullOrEmpty(pArchivo))
            {
                ltDocumento.Text = "<embed src='" + pArchivo + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void btnSelOrigen_Click(object sender, EventArgs e)
        {
            if (dpProveedor.SelectedIndex > 0)
            {
                pnSelSolOrigen.Visible = true;
                pnIrOrigen.Visible = false;
            }
        }            

        

        protected void btnCancelarOrigen_Click(object sender, EventArgs e){pnSelSolOrigen.Visible = false;pnIrOrigen.Visible = true;}

        protected void rptListaOrigen_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Solicitud")) { 
                cpplib.Solicitud  oResultado= comun.admsolicitud.carga (Convert .ToInt32 (e.CommandArgument));
                txIdSolicitudOrigen.Text  = oResultado.IdSolicitud.ToString();
                ltSolOrigen.Text = "<table runat ='server' style ='font-size :13px'>";
                ltSolOrigen.Text += "<tr><td><b>Fecha:</b></td><td>" + oResultado.FechaFactura.ToString("dd/MM/yyyy") + "</td></tr>";
                ltSolOrigen.Text += "<tr><td><b>Factura:</b></td><td>" + oResultado.Factura.ToString() + "</td></tr>";
                ltSolOrigen.Text += "<tr><td><b>Proveedor:</b></td><td>" + oResultado.Proveedor.ToString() + "</td></tr>";
                ltSolOrigen.Text += "<tr><td><b>Importe:</b></td><td>" + oResultado.Importe.ToString("C2") + "</td></tr>";
                ltSolOrigen.Text += "</table>";
                
                pnSelSolOrigen.Visible = false;
                pnIrOrigen.Visible = true;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            cpplib.cxpNotaCredito oNotaCredito= CargaDatos();
            //cpplib.admCxpNotaCredito adm = new cpplib.admCxpNotaCredito();
            int IdNotaCredito = comun.admcxpnotacredito.nueva(oNotaCredito);
            if (IdNotaCredito > 0)
            {
                cpplib.cxpNotaCreditoArchivo  oArchivo = DatosArchivo(IdNotaCredito);
                if (CopiarArchivoRuta(txNombreDocto.Text, oArchivo.Nombre))
                {      
                    if(comun.admcxpnotacredito.AgregarArchivo(oArchivo)){
                        Response.Redirect("trf_ConsultaNotaCredito.aspx");
                    }
                }
            }
            else { ltMsg.Text = "La nota de credito no se registro intente nuevamente"; }
        }
        
        private cpplib.cxpNotaCredito CargaDatos() 
        {
            cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
            cpplib.cxpNotaCredito resultado = new cpplib.cxpNotaCredito();

            resultado.IdEmpresa = oCdr.IdEmpresaTrabajo;
            resultado.Fecha = Convert.ToDateTime(txFecha.Text);
            resultado.Rfc = dpProveedor.SelectedValue;
            resultado.Proveedor = dpProveedor.SelectedItem.Text;
            if (txDescripcion.Text.Trim().Length > 255) { resultado.Descripcion = txDescripcion.Text.Trim().Substring(0, 254); } else { resultado.Descripcion = txDescripcion.Text; }
            resultado.Importe = Convert.ToDecimal(txImporte.Text );
            resultado.ImportePendiente = Convert.ToDecimal(txImporte.Text);
            resultado.Moneda = dpTpMoneda.SelectedValue;
            resultado.Estado = cpplib.cxpNotaCredito.enEstado.Activa;
            resultado.IdUsr = oCdr.IdUsr;
            resultado.IdSolicitudOrigen = Convert.ToInt32 (txIdSolicitudOrigen.Text) ;
            return resultado;
        }

        private cpplib.cxpNotaCreditoArchivo DatosArchivo(int IdNotaCredito)
        {
            cpplib.cxpNotaCreditoArchivo resultado = new cpplib.cxpNotaCreditoArchivo();
            resultado.IdNotaCredito = IdNotaCredito;
            resultado.Tipo = cpplib.cxpNotaCreditoArchivo.TipoArchivo.PDF;
            resultado.Nombre = IdNotaCredito.ToString().PadLeft(6, '0') + DateTime.Now.Millisecond.ToString().PadLeft(6, '0') + ".pdf"; ;
            return resultado;
        }

        private bool CopiarArchivoRuta(string NombreOrigen, string NombreDestino) {
            bool resultado = false;
            string Oigen=Server.MapPath(@"cxp_doc\" + NombreOrigen);
            string Destino=Server.MapPath(@"cxp_doc\NotasCredito\" + NombreDestino );
            try{
                System.IO.File.Copy(Oigen, Destino);
                if (System.IO.File.Exists(Destino)) { resultado = true; }
            }catch (Exception){throw;}
            return resultado;
        }

        protected void imbtnBuscaOrigen_Click(object sender, ImageClickEventArgs e)
        {
            cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
            List<cpplib.Solicitud> Resultado = comun.admsolicitud.ListaSolicitudesIngresoNotaCredito(oCd.IdEmpresaTrabajo, oCd.UnidadNegocio, dpProveedor.SelectedValue,txF_Inicio .Text ,txF_Fin .Text );
            rptListaOrigen.DataSource = Resultado;
            rptListaOrigen.DataBind();
        }


        //private void limpiaArchAnteriores(string pRutaDeTrabajo, string Prefijo ){
        //    String archAnterior = Prefijo.ToString() + "*.*";
        //    String[] archivos = System.IO.Directory.GetFiles(pRutaDeTrabajo, archAnterior);
        //    if (archivos != null){
        //        foreach ( string arch in archivos){
        //            System.IO.File.Delete(arch);
        //        }
        //    }
        //}
    }
}