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
    public partial class cxc_ConsultaDireccion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial Crd=(cpplib .credencial) Session["credencial"];
                hdIdEmpresa.Value = Crd.IdEmpresa.ToString (); 
                int Mes = Convert.ToInt32(DateTime.Now.Month);
                string Año = DateTime.Now.Year.ToString();
                dpMes.SelectedIndex = Mes;
                dpAño.Text = Año;
                this.llenaCombos();
                this.ListaOrdenfactura();
            }
        }

        private void llenaCombos()
        {
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaTodosClientes(), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaTodosClientes(); // (new cpplib.admCatClientes()).ListaTodosClientes();
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

            LlenarControles.LlenarDropDownList(ref dpServicio, comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value), "Texto", "Valor");
            ////List<cpplib.valorTexto> lstSrv = (new cpplib.admCatServicios()).DaComboServicios(hdIdEmpresa.Value);
            //dpServicio.DataSource = comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value);
            //dpServicio.DataTextField = "Texto";
            //dpServicio.DataValueField = "Valor";
            //dpServicio.DataBind();
            
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta);
            if (Lista.Count > 0)
            {
                rptOrdFact.DataSource = Lista;
                rptOrdFact.DataBind();

                lbCobrado.Text = Lista.Where(sol => sol.Estado== cpplib.OrdenFactura.EstadoOrdFac.Pagado).Sum(sol => sol.Importe).ToString("C2");
                lbXCobrar.Text = Lista.Where(sol => sol.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado).Sum(sol => sol.Importe).ToString("C2");
            }
            else
            {
                rptOrdFact.DataSource = Lista;
                rptOrdFact.DataBind();
                lbCobrado.Text = "0";
                lbXCobrar.Text = "0";
            }
        }
        private string Daconsulta()
        {
            string resultado = string.Empty;
            cpplib.credencial crd = (cpplib.credencial)Session["credencial"];
            resultado = ArmaConsulta(resultado, (" IdEmpresa='" + crd.IdEmpresa.ToString() + "'"));
            resultado = ArmaConsulta(resultado, (" Especial=1"));
            resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'"));
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpEstado.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" Estado='" + dpEstado.SelectedValue  + "'")); }
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
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                if (ordFac.Factura == 1)
                {
                    ImageButton imgFac = (ImageButton)e.Item.FindControl("imgbtnVerFac");
                    imgFac.Enabled = true;
                    imgFac.ImageUrl = "~/img/verFac.png";
                }

                //Coloca el semaforo
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Pagado)
                {
                    ImageButton imgCmpg = (ImageButton)e.Item.FindControl("imgbtnCompPago");
                    imgCmpg.Enabled = true;
                    imgCmpg.ImageUrl = "~/img/pago.png";

                    img.ImageUrl = "~/img/action_check.png";
                }
                else if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado) { img.ImageUrl = "~/img/cancelar.png"; }
                else {
                    DateTime FchActual = DateTime.Now;
                    int Dias = Convert.ToInt32((ordFac.FechaFactura - FchActual).TotalDays);
                    if (Dias <= 1){ img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
            }
        }

     #region SECCION  PARA MOSTRAR LA IMAGEN DE A FACTURA
        protected void imgbtnVerFac_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            int Id = Convert.ToInt32(wLink.CommandArgument.ToString());
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaFactura(Id);
            MuestraArchivo(Id, oArchivo.ArchivoDestino);
            pnpaginas.Visible = false;
            mpePopDocumento.Show();
        }
       
     #endregion

    #region SECCION  PARA MOSTRAR COMPRABANTE PAGO
        protected void imgbtnCompPago_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            hdIdOrdFactura.Value = wLink.CommandArgument.ToString();
            cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaDocumentoPago(Convert.ToInt32(hdIdOrdFactura.Value), 1);
            listadePaginas(Convert.ToInt32(hdIdOrdFactura.Value));
            MuestraArchivo(Convert.ToInt32(hdIdOrdFactura.Value), oArchivo.ArchivoDestino);
            mpePopDocumento.Show();
        }

        private void listadePaginas(int IdOrdFactura)
        {
            List<cpplib.cxcArchivo> Lista = comun.admarchivoscxc.ListaComprobantes(IdOrdFactura);
            if (Lista.Count > 1)
            {
                rpPaginas.DataSource = Lista;
                rpPaginas.DataBind();
                pnpaginas.Visible = true;
            }
        }
     #endregion

        private void MuestraArchivo(int IdOrdFactura, string ArchivoMostrar) {
            cpplib.OrdenFactura oFact = (new cpplib.admOrdenFactura()).carga(IdOrdFactura);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(oFact.FechaFactura));
            if (!String.IsNullOrEmpty(ArchivoMostrar))
            {
                String Archivo = Carpeta + ArchivoMostrar;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxc_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
            }
           else { ltDocumento.Text = "<embed src='\\img\\SinComprobante.pdf' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />"; }
        }

        protected void rpPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Docto")
            {
                cpplib.cxcArchivo oArchivo = comun.admarchivoscxc.cargaDocumentoPago(Convert.ToInt32(hdIdOrdFactura.Value), Convert.ToInt32(e.CommandArgument.ToString()));
                MuestraArchivo(Convert.ToInt32(hdIdOrdFactura.Value), oArchivo.ArchivoDestino);
                mpePopDocumento.Show();
            }
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static AjaxControlToolkit.CascadingDropDownNameValue[] xCatalogos(string knownCategoryValues, string category)
        {
            List<AjaxControlToolkit.CascadingDropDownNameValue> Respuesta = new List<AjaxControlToolkit.CascadingDropDownNameValue>();
            if (category.Equals("Empresa"))
            {
                cpplib.admCatCondPago adm = new cpplib.admCatCondPago();
                List<cpplib.Empresa> lstEmpresa = (new cpplib.admCatEmpresa()).ListaEmpresas();
                foreach (cpplib.Empresa oEmp in lstEmpresa)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oEmp.Nombre, oEmp.Id.ToString()));
                }
            }
            if (category.Equals("Servicio"))
            {
                string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
                cpplib.admCatServicios  adm = new cpplib.admCatServicios ();
                List<cpplib.catServicios> lstCat = adm.ListaServicios(ValSel);
                foreach (cpplib.catServicios oUN in lstCat)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oUN.Titulo, oUN.Id.ToString()));
                }
            }
            return Respuesta.ToArray();
        }
       
    }
}