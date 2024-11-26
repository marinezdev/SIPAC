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
    public partial class trf_SeguimientoLote : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void Consulta()
        {
            ltMsg.Text = "";
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            //cpplib.admFondos admfd = new cpplib.admFondos();
            DataTable lista = comun.admfondos.ConsultaDeSeguimientoLotes(oCrd.IdEmpresaTrabajo, txF_Inicio.Text, txF_Fin.Text, dpTipo.SelectedValue);

            if (lista.Rows.Count > 0)
            {
                //rptLotes.DataSource = lista;
                //rptLotes.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptLotes, lista);
            }
            else
            { 
                ltMsg.Text = "No hay Solicitudes disponibles";
                rptLotes.DataSource = null;
                rptLotes.DataBind();
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { Consulta(); }

        protected void rptLotes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           if(e.CommandName.Equals("Detalle")){
                CargaDetalle(e.CommandArgument.ToString());
            }
        }

        protected void rptLotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                if (oSol["estado"].Equals("AUTORIZADO")) 
                { 
                    ((Image)(e.Item.FindControl("imgSem"))).ImageUrl = "~/img/sem_A.png"; 
                    ((Image)(e.Item.FindControl("imgbtnDetalle"))).Visible = false; 
                }
                else
                {
                    if (Convert.ToInt32(oSol["NoPagadas"]) > 0) 
                    { 
                        ((Image)(e.Item.FindControl("imgSem"))).ImageUrl = "~/img/sem_R.png"; 
                    }
                }
            }
        }

        private void CargaDetalle(string IdFondeo)
        {
            cpplib.admFondos admfd = new cpplib.admFondos();
            cpplib.LoteFondos solFdos = admfd.carga(IdFondeo);
            lbLote.Text = solFdos.IdFondeo.ToString();
            lbTotal.Text = solFdos.Total.ToString("c2");
            if (solFdos.Estado == cpplib.LoteFondos.SolEdoFondos.Autorizado) { lbTotalFd.Text = "EN PROCESO"; lbTotalFd.ForeColor = System.Drawing.Color.Red; }
            else { lbTotalFd.Text = solFdos.TotalAprob.ToString("c2"); }
            lbTC.Text = solFdos.TipoCambio.ToString();
            lbTotSol.Text = "SOLICITUDES (" + solFdos.NoSolicitudes.ToString() + ")";

            DataTable Lista = admfd.DaDetalleSolicitudesXFondos(solFdos.IdFondeo.ToString());
            
            lbSolPed.Text = Lista.Select("EdoSol='30'").Count().ToString ();
            lbMontoPendientePago.Text = Lista.Compute("Sum(ImporteAutorizado)", "EdoSol='30'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(ImporteAutorizado)", "EdoSol='30'")).ToString("C2");

            //rpSol.DataSource = Lista;
            //rpSol.DataBind();
            LlenarControles.LLenarRepeaterDataTable(ref rpSol, Lista);

            pnDetalleLote.Visible = true;
            pnConsulta.Visible = false;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            pnDetalleLote.Visible = false;
            pnConsulta.Visible = true;
        }

        protected void rpSol_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                string EdoSol = oSol["EdoSol"].ToString() ;
                string EdoFondeo = oSol["Estado"].ToString();
                if (EdoSol.Equals("30"))
                {
                    ((Image)(e.Item.FindControl("imgSem"))).ImageUrl = "~/img/sem_R.png";
                    ((Image)(e.Item.FindControl("imgbtnPago"))).Visible =false; 
                }

                if (EdoFondeo.Equals("0"))
                {
                    ((Image)(e.Item.FindControl("imgChek"))).ImageUrl = "~/img/delete.png";
                    ((Image)(e.Item.FindControl("imgSem"))).ImageUrl = "~/img/delete.png";
                    ((Image)(e.Item.FindControl("imgbtnPago"))).Visible = false; 
                }
                if (oSol["confactura"].ToString().Equals("NO")) { ((Image)(e.Item.FindControl("imgbtnVer"))).Visible = false; }
            }
        }

        protected void imgbtnVer_Click(object sender, ImageClickEventArgs e)
        {
           ImageButton wLink = (ImageButton)(sender);
           MuestraArchivo(Convert.ToInt32 (wLink.CommandArgument.ToString()));
           mpePopDocumento.Show();
        }


        private void MuestraArchivo(int IdSolicitud)
        {
            cpplib.Archivo oArchivo = comun.admarchivos.cargaFactura(IdSolicitud);
            cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSolicitud));
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
            String Archivo = Carpeta + oArchivo.ArchivoDestino;
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }

        }

        protected void imgbtnPago_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton wLink = (ImageButton)(sender);

            int IdSol = Convert.ToInt32(wLink.CommandArgument.ToString());
            List<cpplib.Archivo> LstArchivos = comun.admarchivos.ListaArchivosSolicitud(IdSol);
            cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSol));
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
            
            ltDocumento.Text = String.Empty;
            string VerArchivo = String.Empty;
            int Contador = 1;
            foreach (cpplib.Archivo oAr in LstArchivos)
            {
                if (oAr.Tipo == cpplib.TipoArchivo.Comprobante)
                {
                    VerArchivo = "\\cxp_doc\\" + Carpeta + oAr.ArchivoDestino;
                    ltDocumento.Text += "<div style ='background-color:#006600; height :25px; color :white; font-size:15px '>DOCUMENTO: " + Contador.ToString() + "<hr /></div><br/>";
                    ltDocumento.Text += "<embed src='" + VerArchivo + "'width='100%' height='480px' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' /><br/><br/>";
                    Contador += 1;
                }
            }
                
            mpePopDocumento.Show();
        }
                
    }
}