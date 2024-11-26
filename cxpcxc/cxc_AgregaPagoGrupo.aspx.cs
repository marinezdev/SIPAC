using cpplib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_AgregaPagoGrupo : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.txF_Pago.Attributes.Add("readonly", "true");
                CargaOrdenes();
                if (!ValidaImportes()) { ltMsg.Text = "La seleccion no es correcta, verifique patidas sin monto y del mismo cliente"; pnRegComprobante.Visible = false; }
            }
        }

        private void CargaOrdenes(){
            cpplib .credencial ocrd= (cpplib.credencial)Session ["credencial"];
            List<cpplib.OrdenFactura> lista = comun.admordenfactura.DaGrupoFacturasMarcadasPago(ocrd.IdEmpresaTrabajo.ToString(), ocrd.IdUsr.ToString());
            if (lista.Count > 0)
            {
                rptOrdFact.DataSource = lista;
                rptOrdFact.DataBind();

                lbTotPesos.Text = lista.Where(sol => sol.TipoMoneda == "Pesos").Sum(sol => sol.Importe).ToString("C2");
                LbTotDll.Text = lista.Where(sol => sol.TipoMoneda == "Dolares").Sum(sol => sol.Importe).ToString("C2");
            }
            else { btnRegPago.Visible = false; }
        }
        
        protected void btnRegPago_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            List<cpplib.cxcArchivo> lstArchivos = (List<cpplib.cxcArchivo>)Session["ArchivosPago"];
            cpplib.credencial Ocrd = (cpplib.credencial)Session["credencial"];

            int Idgrupo = 0;
            //cpplib.admOrdenFactura admOdFact = new cpplib.admOrdenFactura();
            cpplib.admArchivosCxc admComp = new cpplib.admArchivosCxc();
            if (rptOrdFact.Items.Count > 1) 
                Idgrupo = comun.admordenfactura.daSiguienteIdgrupoPago();
            foreach (RepeaterItem Reg in rptOrdFact.Items)
            {
                string IdServicio = ((Label)(Reg.FindControl("lbServicio"))).Text;
                string IdOrdenFactura = ((Label)(Reg.FindControl("lbIdOrdFac"))).Text;
                string Fecha = ((Label)(Reg.FindControl("FechaInicio"))).Text;
                string NumFactura = ((Label)(Reg.FindControl("lbNumFac"))).Text;

                if (CopiaArchivosFactura(lstArchivos, Fecha, NumFactura, IdOrdenFactura))
                {
                    comun.admordenfactura.CambiaEstadoOrdenFactura(IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.Pagado);
                    comun.admordenfactura.QuitarMarcarGrupoPago(IdOrdenFactura, Ocrd.IdUsr.ToString());
                    if (Idgrupo > 0) 
                        comun.admordenfactura.AgregaOrdenalGrupo(Idgrupo, Convert.ToInt32(IdOrdenFactura)); 
                    RegistraBitacora(Convert.ToInt32(IdServicio), Convert.ToInt32(IdOrdenFactura),txF_Pago.Text, cpplib.OrdenFactura.EstadoOrdFac.Pagado);
                }
            }
            Session.Remove("ArchivosPago");
            Response.Redirect("cxc_SeleccionaGrupoPago.aspx");
        }

        private bool ValidaImportes()
        {
            bool continuar = true;
            if (rptOrdFact.Items.Count > 0)
            {
                RepeaterItem dt = rptOrdFact.Items[0];
                string Cte = ((Label)(dt.FindControl("lbCliente"))).Text;

                foreach (RepeaterItem Reg in rptOrdFact.Items)
                {
                    Label lbCliente = (Label)(Reg.FindControl("lbCliente"));
                    if (!Cte.Equals(lbCliente.Text)) { continuar = false; }
                }
            }else{ continuar = false; }
            return continuar;
        }
        
        private bool CopiaArchivosFactura(List<cpplib.cxcArchivo> lstArchivos, string Fecha, string NumFactura, string IdOrdenFactura)
        {
            bool Resultado = true;
            //cpplib.admDirectorio admDir = new cpplib.admDirectorio();
            //cpplib.admArchivosCxc admComp = new cpplib.admArchivosCxc();

            String RutaDestino = Server.MapPath(@"cxc_doc\");
            String CarpetaUbicacion = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(Fecha)); //admDir.DadirectorioArchivo(Convert.ToDateTime(Fecha));
            bool ValidaDir = comun.admdirectorio.ValidaDirectorio(RutaDestino + CarpetaUbicacion); //admDir.ValidaDirectorio(RutaDestino + CarpetaUbicacion);

            String Origen = string.Empty;
            string Destino= string.Empty;
            cpplib.cxcArchivo obj = new cpplib.cxcArchivo();
            int IdDocto = comun.admarchivoscxc.daNumeroComprobante(Convert.ToInt32(IdOrdenFactura)); //admComp.daNumeroComprobante(Convert.ToInt32(IdOrdenFactura));
            
            foreach (cpplib.cxcArchivo oAr in lstArchivos){
                obj .ArchvioOrigen = oAr .ArchvioOrigen ;
                obj.ArchivoDestino = oAr.ArchivoDestino; 
                obj.IdOrdenFactura = Convert.ToInt32(IdOrdenFactura);
                obj.IdDocumento = IdDocto;
                obj.Tipo = cpplib.cxcTipoArchivo.Comprobante;
                Origen = RutaDestino + oAr.ArchivoDestino;
                if (!string.IsNullOrEmpty(NumFactura))
                {
                    obj.ArchivoDestino = IdOrdenFactura.PadLeft(6, '0') + "_D" + obj.IdDocumento.ToString() + "_" + NumFactura.PadLeft(6, '0') + ".PDF";
                }
                else { obj.ArchivoDestino = IdOrdenFactura.PadLeft(6, '0') + "_D" + obj.IdDocumento.ToString() + ".PDF"; }

                Destino = RutaDestino + CarpetaUbicacion + obj.ArchivoDestino;

                System.IO.File.Copy(Origen, Destino, true);
                if (System.IO.File.Exists(Origen))
                {
                    comun.admarchivoscxc.Agrega(obj); //admComp.Agrega(obj); 
                    Resultado =Resultado && true; 
                }
                IdDocto += 1;
            } 
            return Resultado;
        }

        private void RegistraBitacora(int IdServicio, int IdOrdenFactura, string FechaPago, cpplib.OrdenFactura.EstadoOrdFac Estado)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.cxcBitacora oBitacora = new cpplib.cxcBitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = oCrd.IdUsr;
            oBitacora.Nombre = oCrd.Nombre;
            oBitacora.Estado = Estado;
            oBitacora.FechaRegistro = Convert.ToDateTime(FechaPago);

            bool Resultado = (new cpplib.admCxcBitacora()).RegistrarPago (oBitacora);
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("cxc_SeleccionaGrupoPago.aspx"); }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";

            List<cpplib.cxcArchivo> LstArchivos=new List<cpplib.cxcArchivo> ();
            if (MostrarArchivos(LstArchivos)) {
                ltNumDoctos.Text = "TOTAL DE DOCUMENTOS CARGADOS: " + LstArchivos.Count.ToString();
                ltDocumento.Text = String.Empty;
                string VerArchivo =String.Empty;
                int contador=1;
                foreach (cpplib.cxcArchivo oAr in LstArchivos){
                    VerArchivo = "\\cxc_doc\\" + oAr.ArchivoDestino;
                    ltDocumento.Text += "<div style ='background-color:#006600; height :25px; color :white; font-size:15px '>DOCUMENTO: " + contador.ToString() + "<hr /></div><br/>";
                    ltDocumento.Text += "<embed src='" + VerArchivo + "'width='95%' height='400px' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' /><br/><br/>";
                    contador += 1;
                }
                pnlDocumento.Visible = true;
                btnRegPago.Visible = true;
                Session["ArchivosPago"] = LstArchivos;
            }
        }

        private bool  MostrarArchivos(List<cpplib.cxcArchivo> LstArchivos){

            bool Resultado = true;
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            String RutaDestino = Server.MapPath(@"cxc_doc\");
            String Archivo = string.Empty;
            int Contador=0;
            foreach (HttpPostedFile postedFile in fulComprobante.PostedFiles)
            {
                string ext = System.IO.Path.GetExtension(postedFile.FileName);
                if ((postedFile.FileName.Length <= 64) && (ext.ToUpper().Equals(".PDF")))
                {
                    Archivo = oCredencial.IdUsr.ToString() + "_" + Contador.ToString() + "_C.PDF";
                    postedFile.SaveAs(RutaDestino + Archivo);
                    if (!System.IO.File.Exists(RutaDestino + Archivo)) { Resultado = Resultado && false; }
                    cpplib.cxcArchivo oArh = new cpplib.cxcArchivo();
                    oArh.ArchvioOrigen = postedFile.FileName;
                    oArh.ArchivoDestino = Archivo;
                    LstArchivos.Add(oArh);
                }
                else { Resultado = Resultado && false; }
                Contador += 1;
            }

            return Resultado;
        }

    }
}