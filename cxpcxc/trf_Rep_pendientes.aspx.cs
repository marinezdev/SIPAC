using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace cxpcxc
{
    public partial class trf_Rep_pendientes : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];

                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_txF_Fin.EndDate = DateTime.Now;

                hdIdEmpresa.Value = oCdr.IdEmpresaTrabajo.ToString();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void Procesa()
        {
            if (dpTipo.SelectedValue.Equals("D")) { LlenaReporteDetallado(hdIdEmpresa.Value); }
            if (dpTipo.SelectedValue.Equals("G")) { LlenaReporteGral(hdIdEmpresa.Value); }
        }

        private void LlenaReporteDetallado( string IdEmpresa)
        {
            List<cpplib.Solicitud> Lista = comun.admsolicitud.ReportePendientesDetallado(IdEmpresa, txF_Inicio.Text, txF_Fin.Text, Convert.ToInt32(chkCompleto.Checked));

            cpplib.Empresa oemp = comun.admcatempresa.carga(Convert.ToInt32(IdEmpresa));

            ReportParameterCollection parametrosReporte = new ReportParameterCollection();
            string TotalPesos= Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.Importe).ToString("C2");
            string TotalDll = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.Importe).ToString("C2");
            parametrosReporte.Add(new ReportParameter("TotPesos", TotalPesos));
            parametrosReporte.Add(new ReportParameter("TotDolares",TotalDll));
            parametrosReporte.Add(new ReportParameter("Empresa", oemp.Nombre));

            ReportDataSource rdsDetallado = new ReportDataSource("repPendientes");
            rdsDetallado.Value = Lista;

            rpvPendientes.Reset();
            rpvPendientes.ProcessingMode = ProcessingMode.Local;
            rpvPendientes.LocalReport.ReportPath = "rpPendientes.rdlc"; ;
            rpvPendientes.LocalReport.SetParameters(parametrosReporte);
            rpvPendientes.LocalReport.DataSources.Add(rdsDetallado);
            rpvPendientes.LocalReport.Refresh();
        }
        
        private void LlenaReporteGral(string IdEmpresa)
        {

            List<cpplib.repPendientesGral> Lista = comun.admsolicitud.ReportePendientesGral(IdEmpresa, txF_Inicio.Text, txF_Fin.Text, Convert.ToInt32(chkCompleto.Checked));

            cpplib.Empresa oemp = comun.admcatempresa.carga(Convert.ToInt32(IdEmpresa));

            ReportParameterCollection parametrosReporte = new ReportParameterCollection();
            parametrosReporte.Add(new ReportParameter("TotPesos", Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.Importe).ToString("C2")));
            parametrosReporte.Add(new ReportParameter("TotDolares", Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.Importe).ToString("C2")));
            parametrosReporte.Add(new ReportParameter("Empresa", oemp.Nombre));

            ReportDataSource rdsGeneral = new ReportDataSource("repPendientes");
            rdsGeneral.Value = Lista;

            rpvPendientes.Reset();
            rpvPendientes.ProcessingMode = ProcessingMode.Local;
            rpvPendientes.LocalReport.ReportPath = "rpPendientesGral.rdlc"; ;
            rpvPendientes.LocalReport.SetParameters(parametrosReporte);
            rpvPendientes.LocalReport.DataSources.Add(rdsGeneral);
            rpvPendientes.LocalReport.Refresh();
        }  

        protected void btnConsultar_Click(object sender, ImageClickEventArgs e){Procesa();}

        protected void chkCompleto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompleto.Checked) { pnPeriodo.Enabled = false; }
            if (!chkCompleto.Checked) { pnPeriodo.Enabled = true; }
        }

        //private void VerReporte(ReportViewer Reporte){
        //    cpplib.credencial Credencial = (cpplib.credencial)Session["credencial"];
        //    string dirLocalTemporal = Server.MapPath(".") + "\\Descargas\\";
        //    string prefijoArchivo = "rp" + Credencial.IdUsr.ToString().PadLeft(3, '0') + "_";
        //    string extencionArchivo = ".pdf";
        //    string archivo = prefijoArchivo + extencionArchivo;

        //    limpiaDirTrabajo(dirLocalTemporal, prefijoArchivo, extencionArchivo);

        //    if (creaPDF(Reporte, dirLocalTemporal + archivo))
        //    {
        //        string dirOrigen = "\\Descargas\\" + archivo;
        //        ltPendientes.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
        //    }
        //    else { ltMsg.Text = "No se genero el archivo, intente nuevamente"; }
        //}

        //public void limpiaDirTrabajo(string pDirectorio, string pPrefijo, string pExtencion)
        //{
        //    foreach (string fichero in System.IO.Directory.GetFiles(pDirectorio, pPrefijo + "*" + pExtencion))
        //    {
        //        System.IO.File.Delete(fichero);
        //    }
        //}

        //public bool creaPDF(ReportViewer reporte, string pArchivo)
        //{
        //    bool resultado = false;
        //    Warning[] warnings;
        //    string[] streamids;
        //    string mimeType;
        //    string encoding;
        //    string extension;
        //    if (File.Exists(pArchivo)) File.Delete(pArchivo);
        //    byte[] bytes = reporte.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
        //    FileStream fs = new FileStream(pArchivo, System.IO.FileMode.Create);
        //    fs.Write(bytes, 0, bytes.Length);
        //    fs.Close();
        //    resultado = true;
        //    return resultado;
        //}
    }
}