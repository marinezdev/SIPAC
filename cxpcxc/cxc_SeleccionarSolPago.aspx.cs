using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class cxc_SeleccionarSolPago : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Mes =Convert.ToInt32 (DateTime.Now.Month);
                string Año = DateTime.Now.Year.ToString();
                dpMes.SelectedIndex = Mes;
                dpAño.Text = Año;
                this.llenaCombos();
                this.ListaOrdenfactura();
            }
        }

        private void llenaCombos()
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(oCrd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(oCrd.IdEmpresaTrabajo.ToString());    //Anterior: ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta);
            //if (Lista.Count > 0)
            //{
            //    rptOrdFact.DataSource = Lista;
            //    rptOrdFact.DataBind();
            //}
            //else
            //{
            //    rptOrdFact.DataSource = Lista;
            //    rptOrdFact.DataBind();
            //}
            LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
        }
        private string Daconsulta()
        {
            string resultado = string.Empty;
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpMes.SelectedValue.Equals("0")) { resultado= ArmaConsulta(resultado, (" DATEPART(MONTH,FECHAINICIO)=" + dpMes.SelectedValue)); }
            if (!dpAño.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(YEAR,FECHAINICIO)=" + dpAño.SelectedValue)); }
            
            return resultado;
        }

        private string  ArmaConsulta(string Cadena, string Dato){
            if (string.IsNullOrEmpty (Cadena)){Cadena=" where " + Dato;}
            else{Cadena+=" and " + Dato;}
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

            }
        }
        
        protected void rptsol_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDat") { Response.Redirect("cxc_AgregaPago.aspx?ord=" + e.CommandArgument.ToString()); }
            if (e.CommandName == "Descarga")
            {
                Label lbFactura = (Label)e.Item.FindControl("lbFechafactura");
                DecargarFactura(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToDateTime(lbFactura.Text));
            }
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
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaFactura(IdOrdFactura);
            cpplib.OrdenFactura oFact = comun.admordenfactura.carga(IdOrdFactura);

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

    }
}