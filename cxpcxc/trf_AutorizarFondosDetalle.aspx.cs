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
    public partial class trf_AutorizarFondosDetalle : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) 
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciaAutorizacion();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("trf_AutorizarFondos.aspx");
        }

        private void IniciaAutorizacion()
        {
            string IdFondeo = Request.Params["idfd"].ToString();

            //cpplib.admFondos admfd = new cpplib.admFondos();
            cpplib.LoteFondos solFdos = comun.admfondos.carga(IdFondeo);
            lblote.Text = solFdos.IdFondeo.ToString();
            lbTotal.Text = solFdos.Total.ToString("c2");
            lbTC.Text = solFdos.TipoCambio.ToString();
            //rptSolDet.DataSource = comun.admfondos.DaDetalleSolicitudesXFondos(solFdos.IdFondeo.ToString());
            //rptSolDet.DataBind();
            LlenarControles.LLenarRepeaterDataTable(ref rptSolDet, comun.admfondos.DaDetalleSolicitudesXFondos(solFdos.IdFondeo.ToString()));
        }

        protected void chkAutorizar_CheckedChanged(object sender, EventArgs e)
        {
            decimal TotPesos = 0;
            foreach (RepeaterItem Reg in rptSolDet.Items)
            {
                if (((CheckBox)(Reg.FindControl("chkAutorizar"))).Checked)
                {
                    String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;
                    TextBox txCantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                    if (!string.IsNullOrEmpty(txCantidadPagar.Text))
                    {
                        if (Moneda.Equals("Pesos")) 
                        {
                            TotPesos += Convert.ToDecimal(txCantidadPagar.Text); 
                        }
                        else if (Moneda.Equals("Dolares")) 
                        { 
                            TotPesos += Convert.ToDecimal(txCantidadPagar.Text); 
                        }
                    }
                }
            }
            lbTotal.Text = TotPesos.ToString("C2");
        }

        protected void rptSolDet_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) 
            { 
                Response.Redirect("trf_VeSolFacturaFondos.aspx?id=" + e.CommandArgument.ToString() + "&idfd=" + lblote.Text + "&bk=trf_AutorizarFondosDetalle"); 
            }
        }

        protected void rptSolDet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                if (oSol["Moneda"].ToString() == "Dolares")
                {
                    decimal cantidad = (Convert.ToDecimal(oSol["ImporteAutorizado"]) * Convert.ToDecimal(lbTC.Text));
                    ((TextBox)e.Item.FindControl("txCantidadPagar")).Text = cantidad.ToString("C2");
                }
            }
        }

        protected void btnAceptarFodos_Click(object sender, EventArgs e)
        {
            ltMsg.Text = "";
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            int NoSolAprobadas = 0;
            decimal TotalAProb = 0;
            List<cpplib.ArchivoFodos> Lista = (List<cpplib.ArchivoFodos>)Session["ArchivosFondeo"];
            
            if (CopiaArchivos(lblote.Text, Lista))
            {
                if (ProcesaSolicitudes(ref NoSolAprobadas, ref TotalAProb))
                {
                    cpplib.admFondos admfd = new cpplib.admFondos();
                    admfd.ActualizaSolicitudFondos(lblote.Text, NoSolAprobadas, TotalAProb, oCredencial.IdUsr);
                    EnviaCorreo();
                    Session.Remove("ArchivosFondeo");
                    Response.Redirect("trf_AutorizarFondos.aspx");
                }
            }else { ltMsg.Text = "No se pudo agregar el archivo intente nuevamente"; }
        }


        private bool CopiaArchivos(string IdLote, List<cpplib.ArchivoFodos> Lista)
        {
            bool resultado = true;
            String RutaTemp = Server.MapPath(@"cxc_doc\");
            string RutaDestino = Server.MapPath(@"cxp_doc\Fondos\");

            cpplib.admFondos adm = new cpplib.admFondos();
            int IdDocumento = adm.daNumeroComprobante(Convert.ToInt32(IdLote));
            String Origen = string.Empty;
            string Destino = string.Empty;
            cpplib.ArchivoFodos obj = new cpplib.ArchivoFodos();
            try
            {
                foreach ( cpplib.ArchivoFodos oArh in Lista)
                {
                    obj.ArchivoDestino = oArh.ArchivoDestino;
                    obj.ArchivoOrigen  = oArh.ArchivoOrigen;
                    obj.IdFondeo = Convert.ToInt32(IdLote);
                    obj.IdDocumento = IdDocumento;
                    obj.ArchivoDestino = IdLote.PadLeft(6, '0') + "_" + IdDocumento.ToString().PadLeft(2, '0') + "_" + DateTime.Now.ToString("ddMMyyyy") + ".PDF";

                    Origen = RutaTemp + oArh.ArchivoDestino;
                    Destino = RutaDestino + obj.ArchivoDestino;

                    EliminaArchivosAnteriores(Destino);
                    System.IO.File.Copy(Origen, Destino);

                    if (File.Exists(Destino)){adm.AgregaArchivo(obj);}
                    IdDocumento += 1;
                }
            }
            catch (Exception ex) { ltMsg.Text = ex.Message.ToString(); resultado = false; }
            return resultado;
        }

        private bool  InsertaArchivos(List<cpplib.ArchivoFodos > Lista ){
            bool Resultado = true;
            //cpplib.admFondos admfd = new cpplib.admFondos();
            foreach (cpplib.ArchivoFodos oAr in Lista)
            {
                if (!comun.admfondos.AgregaArchivo(oAr)) 
                { 
                    return false;
                }
            }    
            return Resultado;
        }
        
        private void EliminaArchivosAnteriores(String ArhPdf) 
        { 
            if (File.Exists(ArhPdf)) 
            {
                File.Delete(ArhPdf); 
            } 
        }

        private bool ProcesaSolicitudes(ref int NoSolAprob,ref decimal totalAprob)
        {
            bool resultado = false;
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            //cpplib.admFondos admfd = new cpplib.admFondos();

            //cpplib.admBitacoraSolicitud admBtc = new cpplib.admBitacoraSolicitud();
                       
            int IdUsr = oCredencial.IdUsr;
            string Nombre = oCredencial.Nombre;

            foreach (RepeaterItem Reg in rptSolDet.Items)
            {
                int IdSol = Convert.ToInt32(((ImageButton)(Reg.FindControl("imgbtnVer"))).CommandArgument.ToString());
                TextBox txCantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                if (!string.IsNullOrEmpty(txCantidadPagar.Text))
                {
                    decimal CantidadPagar = Convert.ToDecimal(txCantidadPagar.Text);
                    
                    if (((CheckBox)(Reg.FindControl("chkAutorizar"))).Checked)
                    {
                        NoSolAprob += 1;
                        totalAprob += CantidadPagar;
                        String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;
                        if (Moneda.Equals("Dolares")) { CantidadPagar = Math.Round((Convert.ToDecimal(CantidadPagar) / Convert.ToDecimal(lbTC.Text)), 2); }

                        comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(IdSol, cpplib.Solicitud.solEstado.Fondos, CantidadPagar);
                        comun.admfondos.Actualizafactura(lblote.Text, IdSol, CantidadPagar,cpplib.SolicitudFondos.enEstado.Con_fondos);
                        RegistraBitacora(comun.admbitacorasolicitud, IdSol, IdUsr, Nombre, CantidadPagar);
                    }
                    else
                    {
                        comun.admfondos.ActualizaEstadofactura(lblote.Text, IdSol,cpplib.SolicitudFondos.enEstado .Rechazado);
                        comun.admsolicitud.CambiaEstadoSolicitud(IdSol, cpplib.Solicitud.solEstado.Solicitud);
                    }
                }
            }

            // ESTA FUNCION PASA TODO LO DE FONDEO A CAPTURA; SE MANEJO ASI PORQUE AUN HABIA SOLICITUDES EN CAPTURA
            comun.admsolicitud.CambiaSolicitudFondeoAcaptura(lblote.Text);
            resultado = true;

            return resultado;
        }

        private void EnviaCorreo()
        {
            csGeneral cg = new csGeneral();
            cg.EnviaCorreoAutorizacionFondos(lblote.Text);
        }

        private void RegistraBitacora(cpplib.admBitacoraSolicitud admBtc, int IdSol,int idUsr, string Nombre, decimal CantidadPagar)
        {
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            oBitacora.IdSolicitud = IdSol;
            oBitacora.IdUsr = idUsr;
            oBitacora.Nombre = Nombre;
            oBitacora.Estado = cpplib.Solicitud.solEstado.Fondos;
            oBitacora.Importe = CantidadPagar;  
            admBtc.Registrar(oBitacora);
        }
        
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
               else 
               { 
                    Resultado = Resultado && false; 
               }
               Contador += 1;
           }

           return Resultado;
       }
    }
}