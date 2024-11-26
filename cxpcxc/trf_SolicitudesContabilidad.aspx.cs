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
    public partial class trf_SolicitudesContabilidad : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpMes.SelectedValue =DateTime.Now.Month.ToString();
                dpAño.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["Credencial"];

            if (oCredencial.Grupo == cpplib.credencial.usrGrupo.Admsys) {
                this.ExportaSolicitudes(oCredencial.IdEmpresaTrabajo, oCredencial.IdUsr, dpAño.SelectedValue, dpMes.SelectedValue);
            }
            else
            {
                if (!SolicitudSinFactura(oCredencial.IdEmpresaTrabajo, dpAño.SelectedValue, dpMes.SelectedValue))
                {
                    this.ExportaSolicitudes(oCredencial.IdEmpresaTrabajo, oCredencial.IdUsr, dpAño.SelectedValue, dpMes.SelectedValue);
                }
            }
         }

        private void PreparaDirectorioExportacion(String DirExp) 
        {
            if (System.IO.Directory.Exists(DirExp)) 
                System.IO.Directory.Delete(DirExp, true); 
            System.IO.Directory.CreateDirectory(DirExp);
        }

        private bool CopiaTodoslosArchivos( ref DataTable lstSolicitud, String DirExp)
        {
            bool resultado = false;

            int IdSolicitud = 0;
            DateTime FhFactura = new DateTime(2000, 1, 1, 0, 0, 0);
            String NomArchivoBase = String.Empty;
            String Origen = String.Empty;
            String Destino = String.Empty;
            String DirRaiz = Server.MapPath(@"cxp_doc\");
            String NotasPago = String.Empty;
            String DirProveedor = String.Empty;
            String Carpeta = String.Empty;
                       
            //cpplib.admArchivos admArch = new cpplib.admArchivos();
           
            foreach (DataRow Reg in lstSolicitud.Rows)
            {
                try
                {
                    if (DirProveedor != Reg["PROVEEDOR"].ToString()) { 
                        DirProveedor = Reg["PROVEEDOR"].ToString(); 
                        Carpeta = DirExp + DirProveedor + @"\"; 
                        if (!System.IO.Directory.Exists(Carpeta)) { System.IO.Directory.CreateDirectory(Carpeta); };
                    }
                
                    NotasPago = string.Empty;
                    IdSolicitud = Convert.ToInt32(Reg["SOLICITUD"]);
                    FhFactura = Convert.ToDateTime(Reg["FECHAFACTURA"]);

                    List<cpplib.Archivo> LstArchivos = comun.admarchivos.ListaArchivosSolicitud(IdSolicitud);
                    if (LstArchivos.Count > 0)
                    {
                        NomArchivoBase = DaNombreArchivoBase(Carpeta, LstArchivos[0].ArchivoDestino );
                        Reg["NOMBRE_ARCHIVO"] = NomArchivoBase;
                        string RutaFactura = (new cpplib.admDirectorio()).DadirectorioArchivo(FhFactura);
                        foreach (cpplib.Archivo oAr in LstArchivos)
                        {
                            Origen = DirRaiz + RutaFactura + oAr.ArchivoDestino;
                            if (oAr.Tipo != cpplib.TipoArchivo.Comprobante) {
                                Destino = Carpeta + NomArchivoBase + Path.GetExtension(oAr.ArchivoDestino);
                            }else{
                                Destino = Carpeta + NomArchivoBase + "_D" + oAr.IdDocumento.ToString() + Path.GetExtension(oAr.ArchivoDestino); 
                            }
                            if (File.Exists(Origen)) { File.Copy(Origen, Destino,true); }
                            
                            if (!string.IsNullOrEmpty(oAr.Nota)){NotasPago += (oAr.Nota +";");}
                        }
                    }
                    Reg["NOTAS_PAGO"] = NotasPago;
                }
                catch (Exception) { DirProveedor = ""; }
            }
            resultado = true;
           return resultado;
        }

        private String DaNombreArchivoBase(String DirExp,String Archivo) {
            String Extencion = Path.GetExtension(Archivo);
            Archivo = Path.GetFileNameWithoutExtension(Archivo);
            
            int Cont=1;
            while ((File.Exists(DirExp + Archivo + Extencion)==true))
            {
                Archivo = Archivo + "_" + Cont.ToString();
                Cont += 1;
            }

            return Archivo;
        }
        private void CrearArchivoExcel(DataTable lstSolicitud, String DirExp) {
            
            DirExp = DirExp + "Solicitudes_" + DateTime .Now .ToString ("ddMMyy") + ".xls";

            lstSolicitud.Columns.RemoveAt(0);  
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

            GridView grdDatos = new GridView();
            grdDatos.DataSource = lstSolicitud;
            grdDatos.DataBind();
            grdDatos.AllowPaging = false;
            grdDatos.RenderControl(hw);

            string Datos = sw.ToString();
            File.WriteAllText(DirExp, Datos);
            sw.Close();
            grdDatos.Dispose();
            
        }

        private void Enviar(String DirZip) {

            System.IO.FileInfo ArchivoDescarga = new System.IO.FileInfo(DirZip);

            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/x-compressed";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ArchivoDescarga.Name);
            Response.AppendHeader("Content-Length", ArchivoDescarga.Length.ToString());
            Response.WriteFile(DirZip);
            Response.End();
        }
        
        private void ExportaSolicitudes(int IdEmpresa,int IdUsr,String Anio, String Mes) {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["Credencial"];
            try
            {
                DataTable lstSolicitud = (new cpplib.admSolicitud()).ListaSolXExportarContabilidad(IdEmpresa.ToString(), Anio, Mes);
                if (lstSolicitud.Rows.Count > 0)
                {
                    String DirExp = Server.MapPath(@"Descargas\") + "Exp_" + IdUsr.ToString().PadLeft(4, '0') + @"\";
                    String DirZip = Server.MapPath(@"Descargas\") + "Exp" + IdUsr.ToString().PadLeft(4, '0') + ".zip";

                    this.PreparaDirectorioExportacion(DirExp);

                    if (CopiaTodoslosArchivos(ref lstSolicitud, DirExp))
                    {
                        this.CrearArchivoExcel(lstSolicitud, DirExp);
                        if (File.Exists(DirZip)) { File.Delete(DirZip); }

                        ZipFile.CreateFromDirectory(DirExp, DirZip);

                        if (File.Exists(DirZip))
                        {
                            this.Enviar(DirZip);
                        }
                    }
                }
                else { ltMsg.Text = "No hay Solcitudes disponibles"; }
            }
            catch (Exception) { ltMsg.Text = "No se Pueden generar el archivo de solicitudes, Intente nuevamente"; }
        
        }

        private bool SolicitudSinFactura(int IdEmpresa,String Anio, String Mes) {
            bool Resultado = false;

            DataTable dtSinFactura = comun.admsolicitud.ExitenSolSinFacturaContabilidad(IdEmpresa.ToString() , Anio, Mes);
            if (dtSinFactura.Rows.Count > 0)
            {
                rptSolSinFactura.DataSource = dtSinFactura;
                rptSolSinFactura.DataBind();
                ltMsg.Text = "Los datos no se pueden exportar, existen solicitudes que no tienen factura.";
                pnSolSinFactura.Visible = true;
                Resultado = true;
            }
            else 
            { 
                pnSolSinFactura.Visible = false; 
                rptSolSinFactura.DataSource = null; 
                rptSolSinFactura.DataBind(); 
            }

            return Resultado;
        }
                
        protected void imgbtnExportarSinFactuio_Click(object sender, ImageClickEventArgs e)
        {
            String IdEmpresa = ((cpplib.credencial)Session["credencial"]).IdEmpresaTrabajo.ToString();
            DataTable dtSinFactura = comun.admsolicitud.ExitenSolSinFacturaContabilidad(IdEmpresa.ToString(), dpAño.SelectedValue, dpMes.SelectedValue);
            string Archivo = "SinFactura_" + DateTime.Now.ToString("ddMMyyhhmmss") + ".xls";

            if (dtSinFactura.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Archivo);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView grdDatos = new GridView();
                    grdDatos.DataSource = dtSinFactura;
                    grdDatos.DataBind();
                    grdDatos.AllowPaging = false;
                    grdDatos.RenderControl(hw);
                    Response.Charset = "UTF-8";
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }


    }
}