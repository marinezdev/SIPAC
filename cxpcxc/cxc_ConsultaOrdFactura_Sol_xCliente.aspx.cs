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
    public partial class cxc_ConsultaOrdFactura_Sol_xCliente : Utilerias.Comun
    {
        // Página clonada de:   cxc_ConsultaOrdFactura_Sol.aspx
        //              Menú:   Cuentas por cobrar / Partidas / Consultar

        // Detalle de la información mostrada.
        // Las consultas se encuentran basadas por el IdEmpresa, UnidadNegocio a la que se encuentra asociado el usaurio.


        protected void Page_Init(object sender, EventArgs e) 
        {
            if (Session["credencial"] == null) 
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void enviaMsgCliente(string pMensaje) 
        { 
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Mes = Convert.ToInt32(DateTime.Now.Month);
                string Año = DateTime.Now.Year.ToString();
                this.llenaCombos();

                if (!string.IsNullOrEmpty(Request.QueryString["Cliente"]))
                {
                    bool _error = false;
                    cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
                    //cpplib.clsSIPAC_Security _SIPACSeg = null;
                    //_SIPACSeg = new cpplib.clsSIPAC_Security();
                    string idCliente = "";
                    idCliente = Request["Cliente"].ToString();
                    idCliente = comun.clssipacsecurity.Decrypt(idCliente, ref _error);

                    if (!_error)
                    {
                        dpCliente.SelectedValue = idCliente;
                        dpCliente.Enabled = false;
                        ListaOrdenfactura_xCliente(int.Parse(crd.IdEmpresaTrabajo.ToString()), int.Parse(idCliente));
                    }

                    crd = null;
                    //_SIPACSeg = null;
                }
            }
        }

        private void llenaCombos()
        {
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            /* Llena clientes*/
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString()), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(crd.IdEmpresaTrabajo.ToString());  //Anterior: .ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            /* Llena estado*/
            dpEstado.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (int value in Enum.GetValues(typeof(cpplib.OrdenFactura.EstadoOrdFac)))
            {
                var name = Enum.GetName(typeof(cpplib.OrdenFactura.EstadoOrdFac), value);
                dpEstado.Items.Add(new ListItem(name, value.ToString()));
            }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) 
        {
            Response.Redirect("espera.aspx");
        }

        private void ListaOrdenfactura_xCliente(int IdEmpresa, int IdCliente)
        {
            // RMFs
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.Facturas_ConsultarGeneral(IdEmpresa, IdCliente);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }
            else
            {
                ltMsg.Text = "No hay información";
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }
            LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
        }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }
            else
            {
                ltMsg.Text = "No hay información";
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
            }

            LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
        }
        private string Daconsulta()
        {
            string resultado = string.Empty;
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            
            // resultado = ArmaConsulta(resultado, (" IdEmpresa='" + crd.IdEmpresaTrabajo.ToString() + "'"));
            // resultado = ArmaConsulta(resultado, (" UnidadNegocio='" + crd.UnidadNegocio.ToString() + "'"));
            
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpEstado.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" Estado='" + dpEstado.SelectedValue + "'")); }

            //if (!dpMes.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(MONTH,FECHAINICIO)=" + dpMes.SelectedValue)); }
            //if (!dpAño.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(YEAR,FECHAINICIO)=" + dpAño.SelectedValue)); }
            return resultado;
        }

        private string ArmaConsulta(string Cadena, string Dato)
        {
            if (string.IsNullOrEmpty(Cadena)) { Cadena = " where " + Dato; }
            else { Cadena += " and " + Dato; }
            return Cadena;
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) 
        { 
            this.ListaOrdenfactura(); 
        }

        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                if (ordFac.Factura == 1)
                {
                    ImageButton imgfact = (ImageButton)e.Item.FindControl("imgbtnVerFac");
                    imgfact.Enabled = true; imgfact.ImageUrl = "~/img/verFac.png";

                    //ImageButton imgdescarga = (ImageButton)e.Item.FindControl("imgbtnDescarga");
                    //imgdescarga.Enabled = true; imgdescarga.ImageUrl = "~/img/descarga.png";

                    //ImageButton imgmail = (ImageButton)e.Item.FindControl("imgbtnMail");
                    //if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.En_Cobro) { imgmail.Enabled = true; imgmail.ImageUrl = "~/img/email.png"; }
                }
            }
        }

        protected void rptsol_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDat") { Response.Redirect("cxc_VerOrdenFactura.aspx?ord=" + e.CommandArgument.ToString() + "&bk=cxc_ConsultaOrdFactura_Sol"); }
            if (e.CommandName == "Descarga")
            {
                Label lbFactura = (Label)e.Item.FindControl("lbFechafactura");
                DecargarFactura(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToDateTime(lbFactura.Text));
            }
            if (e.CommandName == "Mail")
            {
                Label lbFactura = (Label)e.Item.FindControl("lbFechafactura");
                List<string> lstArchivos = BuscarArchivoEnviar(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToDateTime(lbFactura.Text));
                csGeneral admcsGral = new csGeneral();
                admcsGral.EnviaCorreoConFacturaCliente(e.CommandArgument.ToString(), lstArchivos);
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

        #region DESCARGA DE LA FACTURA

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
                Destino = DirExp + oAr.ArchivoDestino + Path.GetExtension(oAr.ArchivoDestino);
                if (File.Exists(Origen)) { File.Copy(Origen, Destino, true); }
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


        #region SECCION PRA ENVIO DE CORREO

        private List<string> BuscarArchivoEnviar(int IdOrdenfactura, DateTime fechaFactura)
        {
            List<string> Lista = new List<string>();
            String DirRaiz = Server.MapPath(@"cxc_doc\");

            cpplib.admArchivosCxc admArch = new cpplib.admArchivosCxc();
            List<cpplib.cxcArchivo> LstArchivos = comun.admarchivoscxc.ListaArchivosSolicitud(IdOrdenfactura);
            string RutaFactura = comun.admdirectorio.DadirectorioArchivo(fechaFactura);

            foreach (cpplib.cxcArchivo oAr in LstArchivos)
            {
                if (oAr.Tipo != cpplib.cxcTipoArchivo.Comprobante) { Lista.Add(DirRaiz + RutaFactura + oAr.ArchivoDestino); }
            }
            return Lista;
        }

        #endregion



    }
}