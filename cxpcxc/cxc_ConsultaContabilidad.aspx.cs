using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace cxpcxc
{
    public partial class cxc_ConsultaContabilidad : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpMes.SelectedValue = DateTime.Now.Month.ToString();
                dpAño.SelectedValue = DateTime.Now.Year.ToString();
        
            }
        }
        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCdr = (cpplib.credencial)Session["credencial"];
            this.ExportaSolicitudes(oCdr.IdEmpresaTrabajo.ToString (), dpAño.SelectedValue, dpMes.SelectedValue);
        }

        private void PreparaDirectorioExportacion(String DirExp)
        {
            if (System.IO.Directory.Exists(DirExp)) { System.IO.Directory.Delete(DirExp, true); };
            System.IO.Directory.CreateDirectory(DirExp);
        }

        private bool CopiaTodoslosArchivos(ref DataTable Lista, String DirExp)
        {
            bool resultado = false;

            int IdOrdFactura = 0;
            DateTime FhFactura = new DateTime(2000, 1, 1, 0, 0, 0);
            String NomArchivoBase = String.Empty;
            String Origen = String.Empty;
            String Destino = String.Empty;
            String DirRaiz = Server.MapPath(@"cxc_doc\");
            String DirCliente = String.Empty;
            String Carpeta = String.Empty;

            cpplib.admArchivosCxc admArch = new cpplib.admArchivosCxc();

            foreach (DataRow Reg in Lista.Rows)
            {
                try
                {
                    if (DirCliente != Reg["CLIENTE"].ToString())
                    {
                        DirCliente = Reg["CLIENTE"].ToString();
                        Carpeta = DirExp + DirCliente + @"\";
                        if (!System.IO.Directory.Exists(Carpeta)) { System.IO.Directory.CreateDirectory(Carpeta); };
                    }

                    IdOrdFactura = Convert.ToInt32(Reg["IDORDENFACTURA"]);
                    //FhFactura = Convert.ToDateTime(Reg["FECHAINICIO"]);
                    FhFactura = Convert.ToDateTime(Reg["FECHAFACTURA"]);

                    List<cpplib.cxcArchivo> LstArchivos = comun.admarchivoscxc.ListaArchivosSolicitud(IdOrdFactura); //admArch.ListaArchivosSolicitud(IdOrdFactura);
                    if (LstArchivos.Count > 0)
                    {
                        NomArchivoBase = DaNombreArchivoBase(Carpeta, LstArchivos[0].ArchvioOrigen);
                        Reg["NOMBRE_ARCHIVO"] = NomArchivoBase;
                        string RutaFactura = comun.admdirectorio.DadirectorioArchivo(FhFactura); //(new cpplib.admDirectorio()).DadirectorioArchivo(FhFactura);
                        foreach (cpplib.cxcArchivo oAr in LstArchivos)
                        {
                            Origen = DirRaiz + RutaFactura + oAr.ArchivoDestino;
                            if (oAr.Tipo != cpplib.cxcTipoArchivo.Comprobante)
                            {
                                Destino = Carpeta + NomArchivoBase + Path.GetExtension(oAr.ArchivoDestino);
                            }
                            else
                            {
                                Destino = Carpeta + NomArchivoBase + "_D" + oAr.IdDocumento.ToString() + Path.GetExtension(oAr.ArchivoDestino);
                            }
                            if (File.Exists(Origen)) { File.Copy(Origen, Destino); }
                        }
                    }
                }
                catch (Exception) { DirCliente = ""; throw; }
            }
            resultado = true;
            return resultado;
        }

        private String DaNombreArchivoBase(String DirExp, String Archivo)
        {
            String Extencion = Path.GetExtension(Archivo);
            Archivo = Path.GetFileNameWithoutExtension(Archivo);

            int Cont = 1;
            while ((File.Exists(DirExp + Archivo + Extencion) == true))
            {
                Archivo = Archivo + "_" + Cont.ToString();
                Cont += 1;
            }

            return Archivo;
        }
        private void CrearArchivoExcel(DataTable Lista, String DirExp)
        {

            DirExp = DirExp + "Cobros_" + DateTime.Now.ToString("ddMMyy") + ".xls";

            Lista.Columns.RemoveAt(0);
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

            GridView grdDatos = new GridView();
            grdDatos.DataSource = Lista;
            grdDatos.DataBind();
            grdDatos.AllowPaging = false;
            grdDatos.RenderControl(hw);

            string Datos = sw.ToString();
            File.WriteAllText(DirExp, Datos);
            sw.Close();
            grdDatos.Dispose();

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

        private void ExportaSolicitudes(string IdEmpresa ,String Anio, String Mes)
        {
            cpplib.credencial oCrd= (cpplib.credencial)Session["Credencial"];
            try
            {
                DataTable LstOrdFactura = comun.admordenfactura.ListaFacturasPagadas(IdEmpresa, Anio, Mes); //(new cpplib.admOrdenFactura()).ListaFacturasPagadas(IdEmpresa, Anio, Mes);
                if (LstOrdFactura.Rows.Count > 0)
                {
                    String DirExp = Server.MapPath(@"Descargas\") + "ExpCxc_" + oCrd.IdUsr.ToString().PadLeft(4, '0') + @"\";
                    String DirZip = Server.MapPath(@"Descargas\") + "ExpCxc" + oCrd.IdUsr.ToString().PadLeft(4, '0') + ".zip";

                    this.PreparaDirectorioExportacion(DirExp);

                    if (CopiaTodoslosArchivos(ref LstOrdFactura, DirExp))
                    {
                        this.CrearArchivoExcel(LstOrdFactura, DirExp);
                        if (File.Exists(DirZip)) 
                            File.Delete(DirZip);

                        ZipFile.CreateFromDirectory(DirExp, DirZip);

                        if (File.Exists(DirZip))
                            this.Enviar(DirZip);
                    }
                }
                else 
                    ltMsg.Text = "No hay información disponibles";
            }
            catch (Exception) 
            { 
                ltMsg.Text = "No se pueden generar el archivo con la información, Intente nuevamente"; 
            }

        }
                
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }
    }
}