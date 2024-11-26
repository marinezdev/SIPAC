using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class trf_AutorizaFondosConsultaDet : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IdFondeo = Request.Params["idfd"];
                CargaDetalle(IdFondeo);
            }
        }

        private void CargaDetalle(string IdFondeo)
        {
            //cpplib.admFondos admfd = new cpplib.admFondos();
            cpplib.LoteFondos solFdos = comun.admfondos.carga(IdFondeo);
            hdIdEmpresa.Value = solFdos.IdEmpresa.ToString(); 
            lbLote.Text = solFdos.IdFondeo.ToString();
            lbTotal.Text = solFdos.Total.ToString("c2");
            if (solFdos.Estado == cpplib.LoteFondos.SolEdoFondos.Autorizado ) { lbTotalFd.Text = "EN PROCESO"; lbTotalFd.ForeColor = System.Drawing.Color.Red; }
            if (solFdos.Estado == cpplib.LoteFondos.SolEdoFondos.Con_Fondos) { pnAnexaPago.Visible = true; }
            else { lbTotalFd.Text = solFdos.TotalAprob.ToString("c2"); }
            lbTC.Text = solFdos.TipoCambio.ToString();
            lbTotSol.Text = "SOLICITUDES (" + solFdos.NoSolicitudes.ToString() + ")";
         

            //rpSolDet.DataSource = comun.admfondos.DaDetalleSolicitudesXFondos(solFdos.IdFondeo.ToString());
            //rpSolDet.DataBind();
            LlenarControles.LLenarRepeaterDataTable(ref rpSolDet, comun.admfondos.DaDetalleSolicitudesXFondos(solFdos.IdFondeo.ToString()));
        }

        protected void rpSolDet_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) { Response.Redirect("trf_VeSolFacturaFondos.aspx?id=" + e.CommandArgument.ToString() + "&idfd=" + lbLote.Text + "&bk=trf_AutorizaFondosConsultaDet"); }
        }

        protected void rpSolDet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                if (oSol["Estado"].ToString() == "0") { 
                    ((Image)e.Item.FindControl("imgChek")).ImageUrl = "~/img/delete.png";
                }
            }
        }

        protected void btnCerrarConsul_Click(object sender, EventArgs e) { Response.Redirect("trf_AutorizaFondosConsulta.aspx") ;}

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";

            List<cpplib.ArchivoFodos> LstArchivos = new List<cpplib.ArchivoFodos>();
            if (MostrarArchivos(LstArchivos))
            {
                ltNumDoctos.Text = "TOTAL DE COPROBANTES: " + LstArchivos.Count.ToString();
                ltDocumento.Text = String.Empty;
                string VerArchivo = String.Empty;
                int contador = 1;
                foreach (cpplib.ArchivoFodos oAr in LstArchivos)
                {
                    VerArchivo = "\\cxc_doc\\" + oAr.ArchivoDestino;
                    ltDocumento.Text += "<div style ='background-color:#006600; height :25px; color :white; font-size:15px '>COMPROBANTE: " + contador.ToString() + "<hr /></div><br/>";
                    ltDocumento.Text += "<embed src='" + VerArchivo + "'width='90%' height='350px' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' /><br/><br/>";
                    contador += 1;
                }
                pnlDocumento.Visible = true;
                Session["ArchivosFondeo"] = LstArchivos;
            }
        }

        private bool MostrarArchivos(List<cpplib.ArchivoFodos> LstArchivos)
        {
            bool Resultado = true;
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxc_doc\");
            String Archivo = string.Empty;
            int Contador = 0;
            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                string ext = System.IO.Path.GetExtension(postedFile.FileName);
                string NombreArchivo = System.IO.Path.GetFileName(postedFile.FileName);
                if ((NombreArchivo.Length <= 64) && (ext.ToUpper().Equals(".PDF")))
                {
                    Archivo = oCredencial.IdUsr.ToString() + "_" + Contador.ToString() + "_C.PDF";
                    postedFile.SaveAs(RutaDestino + Archivo);
                    if (!System.IO.File.Exists(RutaDestino + Archivo)) { Resultado = Resultado && false; }
                    cpplib.ArchivoFodos oArh = new cpplib.ArchivoFodos();
                    oArh.ArchivoOrigen = postedFile.FileName;
                    oArh.ArchivoDestino = Archivo;
                    LstArchivos.Add(oArh);
                }
                else { Resultado = Resultado && false; }
                Contador += 1;
            }
            return Resultado;
        }

        protected void btnNvoComprobante_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            List<cpplib.ArchivoFodos> Lista = (List<cpplib.ArchivoFodos>)Session["ArchivosFondeo"];

            if (CopiaArchivos(lbLote.Text, Lista))
            {
                Session.Remove("ArchivosFondeo");
                Response.Redirect("trf_AutorizaFondosConsulta.aspx");
            }
            else { ltMsg.Text = "No se pudo agregar el archivo intente nuevamente"; }
        }

        private bool CopiaArchivos(string IdLote, List<cpplib.ArchivoFodos> Lista)
        {
            bool resultado = true;
            String RutaTemp = Server.MapPath(@"cxc_doc\");
            string RutaDestino = Server.MapPath(@"cxp_doc\Fondos\");

            //cpplib.admFondos adm = new cpplib.admFondos();
            int IdDocumento = comun.admfondos.daNumeroComprobante(Convert.ToInt32(IdLote));
            String Origen = string.Empty;
            string Destino = string.Empty;
            cpplib.ArchivoFodos obj = new cpplib.ArchivoFodos();
            try
            {
                foreach (cpplib.ArchivoFodos oArh in Lista)
                {
                    obj.ArchivoDestino = oArh.ArchivoDestino;
                    obj.ArchivoOrigen = oArh.ArchivoOrigen;
                    obj.IdFondeo = Convert.ToInt32(IdLote);
                    obj.IdDocumento = IdDocumento;
                    obj.ArchivoDestino = IdLote.PadLeft(6, '0') + "_" + IdDocumento.ToString().PadLeft(2, '0') + "_" + DateTime.Now.ToString("ddMMyyyy") + ".PDF";

                    Origen = RutaTemp + oArh.ArchivoDestino;
                    Destino = RutaDestino + obj.ArchivoDestino;

                    EliminaArchivosAnteriores(Destino);
                    System.IO.File.Copy(Origen, Destino);

                    if (File.Exists(Destino)) 
                        comun.admfondos.AgregaArchivo(obj); 
                    IdDocumento += 1;
                }
            }
            catch (Exception ex) { ltMsg.Text = ex.Message.ToString(); resultado = false; }
            return resultado;
        }

        private void EliminaArchivosAnteriores(String ArhPdf) { if (File.Exists(ArhPdf)) { File.Delete(ArhPdf); } }
    }
}