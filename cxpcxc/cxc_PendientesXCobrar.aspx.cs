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
    public partial class cxc_PendientesXCobrar : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString(); 
                this.llenaCombos();
                this.ListaOrdenfactura();
            }
        }
        
        private void llenaCombos()
        {
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value);    //Anterior: ListaTodosClientes();
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            LlenarControles.LlenarDropDownList(ref dpServicio, comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value), "Texto", "Valor");
           //List<cpplib.valorTexto> lstServicios = comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value);
           // dpServicio.DataSource = lstServicios;
           // dpServicio.DataTextField = "Texto";
           // dpServicio.DataValueField = "Valor";
           // dpServicio.DataBind();
            
            /* Llena estado*/
            dpEstado.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (int value in Enum.GetValues(typeof(cpplib.OrdenFactura.EstadoOrdFac)))
            {
                var name = Enum.GetName(typeof(cpplib.OrdenFactura.EstadoOrdFac), value);
                dpEstado.Items.Add(new ListItem(name, value.ToString()));
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenfactura()
        {
            string Consulta = Daconsulta();
            //cpplib.admOrdenFactura admOrd =  new cpplib.admOrdenFactura();
            DataTable Lista = comun.admordenfactura.DaConsultaXCobrar(Consulta);
            if (Lista.Rows .Count> 0)
            {
                rptOrdFact.DataSource = Lista;
                rptOrdFact.DataBind();
                lbPendientePesos.Text = Lista.Compute("Sum(Importe)", "TipoMoneda = 'pesos' and Estado<>'Pagado'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "TipoMoneda = 'pesos' and Estado<>'Pagado'")).ToString("C2");
                lbPendienteDll.Text = Lista.Compute("Sum(Importe)", "TipoMoneda = 'Dolares' and Estado<>'Pagado'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "TipoMoneda = 'Dolares' and Estado<>'Pagado'")).ToString("C2");
                pnOrdFact.Visible = true;
                lbNumPartidas.Text = Lista.Rows.Count.ToString();
            }
            else
            {
                rptOrdFact.DataSource = null;
                rptOrdFact.DataBind();
                lbPendientePesos.Text = "0"; lbPendienteDll.Text = "0";
                pnOrdFact.Visible = false;
            }
        }

        private string Daconsulta()
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            string resultado = string.Empty;
            resultado = ArmaConsulta(resultado, (" F.IdEmpresa=" + oCrd.IdEmpresaTrabajo.ToString())); //Anterior: hdIdEmpresa .Value
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" F.IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpEstado.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" F.Estado='" + dpEstado.SelectedValue + "'")); }
            if (!dpServicio.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" F.IdCatServicio='" + dpServicio.SelectedValue + "'")); }
            if (ckcteEspecial.Checked) { resultado = ArmaConsulta(resultado, (" F.Especial=1"));}
            if (!dpMes.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(MONTH,F.FechaInicio)=" + dpMes.SelectedValue)); }
            if (!dpAño.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" DATEPART(YEAR,F.FechaInicio)=" + dpAño.SelectedValue)); }
            return resultado;
        }

        private string ArmaConsulta(string Cadena, string Dato)
        {
            if (string.IsNullOrEmpty(Cadena)) { Cadena = " and " + Dato; }
            else { Cadena += " and " + Dato; }
            return Cadena;
        }

        protected void imbtnConsulta_Click(object sender, ImageClickEventArgs e) { this.ListaOrdenfactura(); }

        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView ordFac = (DataRowView)(e.Item.DataItem);
                if (ordFac["Factura"].ToString () == "1")
                {
                    ImageButton imgFac = (ImageButton)e.Item.FindControl("imgbtnVerFac");
                    imgFac.Enabled = true;
                    imgFac.ImageUrl = "~/img/verFac.png";
                }

                //Coloca el semaforo
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                if (ordFac["Estado"].ToString() == cpplib.OrdenFactura.EstadoOrdFac.Pagado.ToString())
                {
                    img.ImageUrl = "~/img/action_check.png";
                }
                else if (ordFac["Estado"].ToString() == cpplib.OrdenFactura.EstadoOrdFac.Cancelado.ToString ()) { img.ImageUrl = "~/img/cancelar.png"; }
                else if(ordFac["Estado"].ToString() == cpplib.OrdenFactura.EstadoOrdFac.En_Cobro.ToString ()) {
                    DateTime FchActual = DateTime.Now;
                    DateTime FechaFact = Convert.ToDateTime(ordFac["Vencimiento"]);
                    int Dias = Convert.ToInt32((FechaFact - FchActual).TotalDays);
                    if (Dias <= 1) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
                else {
      
                    DateTime FchActual = DateTime.Now;
                    DateTime FechaFact = Convert.ToDateTime(ordFac["FechaFactura"]);
                    int Dias = Convert.ToInt32((FechaFact - FchActual).TotalDays);
                    if (Dias <= 1) { img.ImageUrl = "~/img/Sem_R.png"; }
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
            mpePopDocumento.Show();
        }
        #endregion

        private void MuestraArchivo(int IdOrdFactura,string ArchivoDestino)
        {
            cpplib.OrdenFactura oFact = comun.admordenfactura.carga(IdOrdFactura);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(oFact.FechaFactura));
            if (!String.IsNullOrEmpty(ArchivoDestino ))
            {
                String Archivo = Carpeta + ArchivoDestino ;
                if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
                {
                    string dirOrigen = "\\cxc_doc\\" + Archivo;
                    ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                }
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
            if (category.Equals("UnidadNegocio"))
            {
                string ValSel = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["Empresa"];
                cpplib.admCatUnidadNegocio adm = new cpplib.admCatUnidadNegocio();
                List<cpplib.CatUnidadNegocio > lstCat = adm.ListaUnidadNegocio(ValSel);
                foreach (cpplib.CatUnidadNegocio oUN in lstCat)
                {
                    Respuesta.Add(new AjaxControlToolkit.CascadingDropDownNameValue(oUN.Titulo, oUN.Id.ToString ()));
                }
            }
            return Respuesta.ToArray();
        }

    }
}