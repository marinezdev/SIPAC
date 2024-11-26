using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_EstadoFacturacion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString();

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
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value), "Nombre", "Id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value);       //Anterior: .ListaTodosClientes();
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
            //cpplib.admOrdenFactura admOrd = new cpplib.admOrdenFactura();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturas(Consulta);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);

                lbCobrado.Text = Lista.Where(sol => sol.Estado == cpplib.OrdenFactura.EstadoOrdFac.Pagado).Sum(sol => sol.Importe).ToString("C2");
                lbXCobrar.Text = Lista.Where(sol => sol.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado).Sum(sol => sol.Importe).ToString("C2");
                lbNumPartidas.Text = Lista.Count.ToString();
                pnOrdFact.Visible = true;
            }
            else
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
                lbCobrado.Text = "0";
                lbXCobrar.Text = "0";
                pnOrdFact.Visible = false;
            }
        }
        private string Daconsulta()
        {
            string resultado = string.Empty;
            
            resultado = ArmaConsulta(resultado, (" IdEmpresa='" + hdIdEmpresa.Value + "'")); 
            if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            if (!dpEstado.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" Estado='" + dpEstado.SelectedValue + "'")); }
            if (!dpServicio.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCatServicio='" + dpServicio.SelectedValue + "'")); }
            if (ckcteEspecial.Checked) { resultado = ArmaConsulta(resultado, (" Especial=1")); }
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
                else if (ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado) { 
                    img.ImageUrl = "~/img/cancelar.png";
                    ( (ImageButton)e.Item.FindControl("imgbtnCompPago")).Visible =false;
                    ((ImageButton)e.Item.FindControl("imgbtnNota")).Visible =true;
                }
                else
                {
                    DateTime FchActual = DateTime.Now;
                    int Dias = Convert.ToInt32((ordFac.FechaFactura - FchActual).TotalDays);
                    if ((Dias <= 1) && (ordFac.Estado != cpplib.OrdenFactura.EstadoOrdFac.Pagado)) { img.ImageUrl = "~/img/Sem_R.png"; }
                    if ((Dias > 1) && (Dias <= 5)) { img.ImageUrl = "~/img/Sem_A.png"; }
                    if (Dias > 5) { img.ImageUrl = "~/img/Sem_V.png"; }
                }
            }

        }

    #region SECCION PARA MOSTRAR LA IMAGEN DE A FACTURA
        protected void imgbtnVerFac_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            int Id = Convert.ToInt32(wLink.CommandArgument.ToString());
            cpplib.cxcArchivo oArchivo = (new cpplib.admArchivosCxc()).cargaFactura(Id);
            MuestraArchivo(Id, oArchivo.ArchivoDestino);
            pnpaginas.Visible = false;
            mpePopDocumento.Show();

            pnFacAsociadas.Visible = false;            
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
            FacturasAsociadas(Convert.ToInt32(hdIdOrdFactura.Value));
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

        private void FacturasAsociadas(int IdOrdFactura)
        {
            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            int Idgrupo = comun.admordenfactura.ExisteGrupo(IdOrdFactura);
            if (Idgrupo > 0)
            {
                List<cpplib.OrdenFactura> lista = comun.admordenfactura.DaGrupoFacturasPagadas(Idgrupo, IdOrdFactura);
                rpFacAsociadas.DataSource = lista;
                rpFacAsociadas.DataBind();
                lbTotFacAsoc.Text = lista.Sum(sol => sol.Importe).ToString("C2");
                pnFacAsociadas.Visible = true;
            }
            else{
                rpFacAsociadas.DataSource = null;
                rpFacAsociadas.DataBind();
                pnFacAsociadas.Visible = false;
            }
        }

        #endregion

        private void MuestraArchivo(int IdOrdFactura, string ArchivoMostrar)
        {
            cpplib.OrdenFactura oFact = comun.admordenfactura.carga(IdOrdFactura);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(oFact.FechaInicio));
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
                FacturasAsociadas(Convert.ToInt32(hdIdOrdFactura.Value));
                mpePopDocumento.Show();
            }
        }

        protected void imgbtnNota_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);
            int  IdOrdenFac= Convert.ToInt32 (wLink.CommandArgument.ToString());
            cpplib.OrdenFactura  osol = comun.admordenfactura.carga(IdOrdenFac);
            ltNota.Text = osol.Anotaciones;
            mpeNota.Show();
        }

        protected void btnCierraNota_Click(object sender, EventArgs e)
        {

        }

    }
}