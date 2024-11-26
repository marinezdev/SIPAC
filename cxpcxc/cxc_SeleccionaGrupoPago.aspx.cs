using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using System.Data;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class cxc_SeleccionaGrupoPago : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCrd= (cpplib.credencial)Session["credencial"];
                hdIdUsr.Value = oCrd.IdUsr.ToString();
                hdIdEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString(); 
                dpMes.SelectedIndex = Convert.ToInt32(DateTime.Now.Month);
                dpAño.Text = DateTime.Now.Year.ToString(); ;
                this.llenaCombos();
                this.ListaOrdenfactura();
            }
        }

        private void llenaCombos()
        {
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value);       //Anterior: ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            LlenarControles.LlenarDropDownList(ref dpServicio, comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value), "Texto", "Valor");
            //List<cpplib.valorTexto> lstServicios = comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value);
            //dpServicio.DataSource = lstServicios;
            //dpServicio.DataTextField = "Texto";
            //dpServicio.DataValueField = "Valor";
            //dpServicio.DataBind();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta);
            if (Lista.Count > 0)
            {
                rptOrdFact.DataSource = Lista;
                rptOrdFact.DataBind();
                ObtieneMontosSeleccionado(comun.admordenfactura.DaListaFactursMarcadasPago(hdIdUsr.Value));
                lbNumPartidas.Text = Lista.Count.ToString ();
            }
            else 
            { 
                //rptOrdFact.DataSource = Lista; 
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
                lbNumPartidas.Text = "0"; 
            }
        }

        private string Daconsulta()
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            string resultado = string.Empty;
            //if (!oCrd.Grupo.Equals(cpplib.credencial.usrGrupo.Presidencia)) { resultado = ArmaConsulta(resultado, ("Especial=0"));}
            
            resultado = ArmaConsulta(resultado, ("IdEmpresa=" + oCrd.IdEmpresaTrabajo.ToString())); //Anterior: hdIdEmpresa .Value
            resultado = ArmaConsulta(resultado, ("(Estado=" + cpplib.OrdenFactura.EstadoOrdFac.Solicitud.ToString("d") + " or Estado=" + cpplib.OrdenFactura.EstadoOrdFac.En_Cobro.ToString("d") + ")"));
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpServicio.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCatServicio='" + dpServicio.SelectedValue + "'")); }
            if (!dpMes.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(MONTH,FECHAINICIO)=" + dpMes.SelectedValue)); }
            if (!dpAño.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(YEAR,FECHAINICIO)=" + dpAño.SelectedValue)); }

            return resultado;
        }

        private string ArmaConsulta(string Cadena, string Dato)
        {
            if (string.IsNullOrEmpty(Cadena)) { Cadena = " where " + Dato; }
            else { Cadena += " and " + Dato; }
            return Cadena;
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.ListaOrdenfactura(); }

        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura oFac = (cpplib.OrdenFactura)(e.Item.DataItem);

                if (oFac.Factura == 1)
                {
                    ImageButton imgfact = (ImageButton)e.Item.FindControl("imgbtnVerFac");
                    imgfact.Enabled = true; imgfact.ImageUrl = "~/img/verFac.png";

                    ImageButton imgdescarga = (ImageButton)e.Item.FindControl("imgbtnDescarga");
                    imgdescarga.Enabled = true; imgdescarga.ImageUrl = "~/img/descarga.png";
                }

                //Coloca el semaforo
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                if (oFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Pagado){img.ImageUrl = "~/img/action_check.png";}
                else if (oFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado) { img.ImageUrl = "~/img/cancelar.png"; }
                else
                {
                    DateTime FchActual = DateTime.Now;
                    int Dias = Convert.ToInt32((oFac.FechaInicio - FchActual).TotalDays);
                    if ((Dias <= 1) && (oFac.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
                                
                if (oFac.Marcado.ToString() != "0")
                {
                    ImageButton Inc=(ImageButton)e.Item.FindControl("btnInactivo");
                    ImageButton Act=(ImageButton)e.Item.FindControl("btnActivo");
                    if (oFac.Marcado.ToString() == hdIdUsr.Value) { Inc.Visible = false; Act.Visible = true; }
                    else { Inc.Visible = false; Act.Visible = false ;}
                }
            }
        }

        protected void rptOrdFact_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Descarga")
            {
                Label lbFactura = (Label)e.Item.FindControl("lbFechafactura");
                DecargarFactura(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToDateTime(lbFactura.Text));
            }

            if ((e.CommandName.Equals("btnInactivo")) || (e.CommandName.Equals("btnActivo")))
            {
                //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
                ImageButton btnActivo = (ImageButton)e.Item.FindControl("btnActivo");
                ImageButton btnInActivo = (ImageButton)e.Item.FindControl("btnInactivo");
                if (e.CommandName.Equals("btnActivo"))
                {
                    comun.admordenfactura.QuitarMarcarGrupoPago(e.CommandArgument.ToString(), hdIdUsr.Value);
                    ObtieneMontosSeleccionado(comun.admordenfactura.DaListaFactursMarcadasPago(hdIdUsr.Value));
                    btnActivo.Visible = false;
                    btnInActivo.Visible = true; ;
                }

                if (e.CommandName.Equals("btnInactivo"))
                {
                    if(comun.admordenfactura.PonerMarcarGrupoPago(e.CommandArgument.ToString(), hdIdUsr.Value)){
                        ObtieneMontosSeleccionado(comun.admordenfactura.DaListaFactursMarcadasPago(hdIdUsr.Value));
                        btnActivo.Visible = true;
                        btnInActivo.Visible = false;
                    }
                }
            }
        }

        private void ObtieneMontosSeleccionado(DataTable Lista){
            if (Lista.Rows.Count > 0)
            {
                lbTotalPesos.Text = Lista.Compute("Sum(Importe)", "TipoMoneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(importe)", "TipoMoneda = 'pesos'")).ToString("C2"); ;
                lbTotalDolares.Text = Lista.Compute("Sum(Importe)", "TipoMoneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(importe)", "TipoMoneda  = 'Dolares'")).ToString("C2"); ; ;
            }
            else { lbTotalPesos.Text = "0"; lbTotalDolares.Text = "0"; }
        
        }
        
        #region SECCION  PARA MOSTRAR LA IMAGEN DE A FACTURA
        protected void imgbtnVerFac_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            MuestraFactura(Convert.ToInt32(wLink.CommandArgument.ToString()));
            mpePopDocumento.Show();
        }

        private void MuestraFactura(int IdOrdFactura)
        {
            cpplib.cxcArchivo oArchivo = (new cpplib.admArchivosCxc()).cargaFactura(IdOrdFactura);
            cpplib.OrdenFactura oFact = (new cpplib.admOrdenFactura()).carga(IdOrdFactura);

            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(oFact.FechaInicio));
            if (!String.IsNullOrEmpty(oArchivo.ArchivoDestino))
            {
                String Archivo = Carpeta + oArchivo.ArchivoDestino;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxc_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
            }
        }

        #endregion

        #region DESCARGA ACHIVOS DE FACTURA

        private void DecargarFactura(int IdOrdenfactura, DateTime fechaFactura)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["Credencial"];
            String DirExp = Server.MapPath(@"Descargas\") + oCrd.IdUsr.ToString().PadLeft(4, '0') + "_FACT" + @"\";
            String DirZip = Server.MapPath(@"Descargas\") + oCrd.IdUsr.ToString().PadLeft(4, '0') + "_FACT" + ".zip";

            this.PreparaDirectorioExportacion(DirExp);

            CopiaArchivos(IdOrdenfactura, fechaFactura, DirExp);

            if (File.Exists(DirZip)) { File.Delete(DirZip); }

            ZipFile.CreateFromDirectory(DirExp, DirZip);

            if (File.Exists(DirZip)) { this.Enviar(DirZip); }
        }

        private void CopiaArchivos(int IdOrdenfactura, DateTime fechaFactura, string DirExp)
        {
            String Origen = String.Empty;
            String Destino = String.Empty;
            String DirRaiz = Server.MapPath(@"cxc_doc\");

            //cpplib.admArchivosCxc admArch = new cpplib.admArchivosCxc();
            List<cpplib.cxcArchivo> LstArchivos = comun.admarchivoscxc.ListaArchivosSolicitud(IdOrdenfactura);
            string RutaFactura = comun.admdirectorio.DadirectorioArchivo(fechaFactura);

            foreach (cpplib.cxcArchivo oAr in LstArchivos)
            {
                Origen = DirRaiz + RutaFactura + oAr.ArchivoDestino;
                Destino = DirExp + oAr.ArchivoDestino;
                if (File.Exists(Origen)) { File.Copy(Origen, Destino); }
            }
        }

        private void PreparaDirectorioExportacion(String DirExp)
        {
            if (System.IO.Directory.Exists(DirExp)) { System.IO.Directory.Delete(DirExp, true); };
            System.IO.Directory.CreateDirectory(DirExp);
        }
        private void Enviar(String DirZip)
        {
            System.IO.FileInfo ArchivoDescarga = new System.IO.FileInfo(DirZip);
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/x-compressed";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ArchivoDescarga.Name);
            Response.AppendHeader("Content-Length", ArchivoDescarga.Length.ToString());
            Response.WriteFile(DirZip);
            Response.End();
        }

        #endregion

        protected void btnContinuar_Click(object sender, EventArgs e){Response.Redirect("cxc_AgregaPagoGrupo.aspx");}

        protected void lkQuitarMarcas_Click(object sender, EventArgs e)
        {
            //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura();
            comun.admordenfactura.QuitarTodasMarcasGrupoPago(hdIdUsr.Value);
            ObtieneMontosSeleccionado(comun.admordenfactura.DaListaFactursMarcadasPago(hdIdUsr.Value));
            this.ListaOrdenfactura();

        }
    }
}