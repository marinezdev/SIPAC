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
    public partial class trf_AutorizarProcesar : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    hdIdEmpresa.Value = Convert.ToString(Request.Params["Id"]);
                    DaSolicitudes();
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            Response.Redirect("trf_AutorizarMarcacion.aspx"); 
        }

        private void DaSolicitudes()
        {
            cpplib .credencial oCd =(cpplib.credencial)Session["credencial"];
            lbTotPesos.Text = "0";
            lbTotCFPesos.Text = "0";
            lbTotCFDlls.Text = "0";
            
            ltMsg.Text = "";
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();

            DataTable Lista = comun.admsolicitud.ListaPreAutorizar(hdIdEmpresa.Value, oCd.IdUsr);
            if (Lista.Rows.Count > 0)
            {
                decimal TotalPesos =Convert.ToDecimal( Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'")).ToString());
                decimal TotalDll = Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'")).ToString());

                lbTotPesos.Text = TotalPesos.ToString("C2");
                lbTotCFPesos.Text = TotalPesos.ToString("C2");
                lbTotDlls.Text = TotalDll.ToString("C2");
                lbTotCFDlls.Text = TotalDll.ToString("C2");

                if (TotalDll > 0) { hdPedirTipoCambio.Value = "1"; } else { hdPedirTipoCambio.Value = "0"; txTpCambio.Text = "1"; }
                
                //rptSolicitud.DataSource = Lista;
                //rptSolicitud.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptSolicitud, Lista);
                pnSolicitud.Visible = true;
            }
            else
            {
                pnSolicitud.Visible = false;
                ltMsg.Text = "No hay Solicitudes para autorización";
            }
         }

        protected void txCantidadPagar_TextChanged(object sender, EventArgs e)
        {
            decimal CFPesos = 0;
            decimal CFDlls = 0;

            foreach (RepeaterItem Reg in rptSolicitud.Items)
            {
                String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;
                TextBox txCantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                decimal importe = Convert.ToDecimal(((Label)(Reg.FindControl("lbPorPagar"))).Text);

                if (!string.IsNullOrEmpty(txCantidadPagar.Text))
                {
                    decimal CantPagar = Convert.ToDecimal(txCantidadPagar.Text);
                    if (CantPagar <= importe)
                    {
                        if (Moneda.Equals("Pesos")) 
                        {
                            CFPesos += Convert.ToDecimal(txCantidadPagar.Text); 
                        }
                        else if (Moneda.Equals("Dolares")) 
                        { 
                            CFDlls += Convert.ToDecimal(txCantidadPagar.Text); 
                        }
                    }
                    else 
                    { 
                        txCantidadPagar.Text = "0"; 
                    }
                }
                else 
                { 
                    txCantidadPagar.Text = "0"; 
                }
            }
            lbTotCFPesos.Text = CFPesos.ToString("C2");
            lbTotCFDlls.Text = CFDlls.ToString("C2");
        }
        
        protected bool cantidadesCorrectas(RepeaterItem Reg)
        {
            bool resultado = true;

            TextBox txCantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
            if (!string.IsNullOrEmpty(txCantidadPagar.Text))
            {
                decimal importe = Convert.ToDecimal(((Label)(Reg.FindControl("lbPorPagar"))).Text);
                decimal CantPagar = Convert.ToDecimal(txCantidadPagar.Text);
                if (!((CantPagar > 0) && (CantPagar <= importe))) 
                { 
                    resultado = false; 
                }
            }
            else 
            {
                resultado = false;
            }
            
            return resultado;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                Image oPrd = (Image)e.Item.FindControl("imgPrioridad");
                if (oSol["Prioridad"].ToString() == "1") 
                { 
                    oPrd.Visible = true; 
                }

                Image oimg = (Image)e.Item.FindControl("imgConfactura");
                if (oSol["Confactura"].ToString () == "SI") 
                { 
                    oimg.ImageUrl = "~/img/sem_V.png"; 
                }

                if (oSol["Confactura"].ToString() == "NO") 
                {
                    oimg.ImageUrl = "~/img/sem_R.png"; 
                }
            }
        }

        protected void btnAceptaTpCambio_Click(object sender, EventArgs e) 
        {
            this.RegistraLotes();
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (hdPedirTipoCambio.Value.Equals("1")) 
            {
                mpeTpCambio.Show(); 
            }
            else 
            { 
                this.RegistraLotes();
            }
        }

        private void RegistraLotes()
        {
            List<cpplib.SolicitudFondos> LstAutorizada = ObtieneSolicitudes();
            if (LstAutorizada.Count > 0)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                //cpplib.admFondos admFondos = new cpplib.admFondos();
                int IdFondeo = comun.admfondos.daSiguienteIdentificador();
                cpplib.LoteFondos Lote = CargadatosLote(IdFondeo, LstAutorizada, oCrd.IdUsr);

                if (AgregaSolicitudes(IdFondeo, LstAutorizada, cpplib.Solicitud.solEstado.Autorizacion,  oCrd))
                {
                    if (comun.admfondos.RegistraLoteFondeo(Lote))
                    {
                        bool resultado = (new cpplib.admSolicitud()).EliminaDatosAutoriacion(hdIdEmpresa.Value, oCrd.IdUsr.ToString () );
                        csGeneral Gral = new csGeneral();
                        Gral.EnviaCorreoSolicitudFondosLote(oCrd.Nombre, Lote);
                    }
                }
                Response.Redirect("trf_AutorizarMarcacion.aspx");
            }
            else 
            {
                ltMsg.Text = "Las cantidades en las solicitudes no son correctas"; 
            }
        }

        private List<cpplib.SolicitudFondos> ObtieneSolicitudes()
        {
            List<cpplib.SolicitudFondos> Resultado = new List<cpplib.SolicitudFondos>();
            foreach (RepeaterItem Reg in rptSolicitud.Items)
            {
                if (!cantidadesCorrectas(Reg)) 
                { 
                    Resultado.Clear() ; break; 
                }

                cpplib.SolicitudFondos osol = new cpplib.SolicitudFondos();
                int IdSol = Convert.ToInt32(((Label)(Reg.FindControl("lbIdSol"))).Text);
                TextBox CantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;
                osol.IdSolicitud = IdSol;
                osol.Moneda = Moneda;
                osol.ImporteAutorizado = Convert.ToDecimal(CantidadPagar.Text);
                Resultado.Add(osol);
            }
            return Resultado;
        }
        
        private cpplib.LoteFondos CargadatosLote(int IdFondeo, List<cpplib.SolicitudFondos> Lista, int IdUsr)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.LoteFondos Lote = new cpplib.LoteFondos();
            Lote.IdFondeo = IdFondeo;
            Lote.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            Lote.Empresa = comun.admcatempresa.carga(Convert.ToInt32(hdIdEmpresa.Value)).Nombre;
            Lote.NoSolicitudes = Lista.Count;
            Lote.ImporteMx = Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.ImporteAutorizado);
            Lote.ImporteDlls = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.ImporteAutorizado);
            Lote.TipoCambio = Convert.ToDecimal(txTpCambio.Text);
            Lote.IdUsr = IdUsr;
            return Lote;
        }
        
        private bool AgregaSolicitudes(int IdFondos, List<cpplib.SolicitudFondos> lista, cpplib.Solicitud.solEstado EdoSol, cpplib.credencial oCrd)
        {
            bool resultado = false;
            //cpplib.admBitacoraSolicitud admBtc = new cpplib.admBitacoraSolicitud();
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            //cpplib.admFondos admFondos = new cpplib.admFondos();
            foreach (cpplib.SolicitudFondos oSol in lista )
            {
                oBitacora.IdUsr = oCrd.IdUsr ;
                oBitacora.Nombre = oCrd.Nombre;
                oBitacora.Estado = EdoSol;
                oBitacora.IdSolicitud = oSol.IdSolicitud;
                oBitacora.Importe = oSol.ImporteAutorizado;
                
                comun.admfondos.RegistraFactura(IdFondos, oSol.IdSolicitud, oSol.ImporteAutorizado, cpplib.SolicitudFondos.enEstado.Con_fondos );
                comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, EdoSol, oSol.ImporteAutorizado);
                comun.admbitacorasolicitud.Registrar(oBitacora);
            }

            resultado = true;

            return resultado;
        }
    }
}