using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace cxpcxc
{
    public partial class trf_AutorizacionPre : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    hdIdEmpresa.Value = Convert.ToString(Request.Params["Id"]);
                    ltEmpresa.Text = (new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value)).Nombre;
                    DaSolicitudes();
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + hdIdEmpresa.Value); }

        private void DaSolicitudes()
        {
            cpplib .credencial oCd =(cpplib.credencial)Session["credencial"];
            lbTotPesos.Text = "0";
            lbTotDlls.Text = "0";
            lbTotAutPesos.Text = "0";
            lbTotAutDlls.Text = "0";
            ltMsg.Text = "";
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();

            DataTable Lista = admSol.ListaPreAutorizar(hdIdEmpresa.Value, oCd.IdUsr);
            if (Lista.Rows.Count > 0)
            {
                lbTotPesos.Text = Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'")).ToString("C2");
                lbTotDlls.Text = Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'")).ToString("C2");
                lbTotAutPesos.Text = Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'")).ToString("C2");
                lbTotAutDlls.Text = Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'")).ToString("C2");
                
                rptSolicitud.DataSource = Lista;
                rptSolicitud.DataBind();
                pnSolicitud.Visible = true;
            }
            else
            {
                pnSolicitud.Visible = false;
                ltMsg.Text = "No hay Solicitudes para autorización";
            }
         }
        
        protected void chkAutorizar_CheckedChanged(object sender, EventArgs e)
        {
            decimal TotPesos = 0;
            decimal TotDlls = 0;
            foreach (RepeaterItem Reg in rptSolicitud.Items)
            {
                if (((CheckBox)(Reg.FindControl("chkAutorizar"))).Checked)
                {
                    String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;
                    TextBox txCantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                    if (!string.IsNullOrEmpty(txCantidadPagar.Text))
                    {
                        if (Moneda.Equals("Pesos")) { TotPesos += Convert.ToDecimal(txCantidadPagar.Text); }
                        else if (Moneda.Equals("Dolares")) { TotDlls += Convert.ToDecimal(txCantidadPagar.Text); }
                    }
                    else { txCantidadPagar.Text = "0"; }
                }
            }
            lbTotAutPesos.Text = TotPesos.ToString("C2");
            lbTotAutDlls.Text = TotDlls.ToString("C2");
        }

        
        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                Image oPrd = (Image)e.Item.FindControl("imgPrioridad");
                if (oSol["Prioridad"].ToString() == "1") { oPrd.Visible = true; }
            }
        }

        

        protected void btnAceptaTpCambio_Click(object sender, EventArgs e) { 
            this.RegistraLotes();
            Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" +   hdIdEmpresa.Value );
        }

        private void RegistraLotes()
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.admFondos admFondos = new cpplib.admFondos();
            cpplib.LoteFondos Lote = new cpplib.LoteFondos();
            csGeneral Gral = new csGeneral();

            List<cpplib.SolicitudFondos> LstFondos = new List<cpplib.SolicitudFondos>();
            List<cpplib.SolicitudFondos> lstSinFondos = new List<cpplib.SolicitudFondos>();

            this. ObtieneSolicitudes(LstFondos, lstSinFondos);
            
            if (LstFondos.Count > 0) {
                int IdFondeo = admFondos.daSiguienteIdentificador();
                Lote = CargadatosLote(IdFondeo, LstFondos, oCrd.IdUsr );
                if (AgregaSolicitudes(IdFondeo, LstFondos, cpplib.Solicitud.solEstado.Autorizacion, oCrd))
                {
                    if (admFondos.RegistraLoteFondeo(Lote))
                    {
                        Gral.EnviaCorreoSolicitudFondos(oCrd.Nombre, Lote);
                    }
                }
            }

            if (lstSinFondos.Count > 0)
            {
                int IdFondeo = admFondos.daSiguienteIdentificador();
                Lote = CargadatosLote(IdFondeo, lstSinFondos,oCrd.IdUsr );
                if (AgregaSolicitudes(IdFondeo, lstSinFondos, cpplib.Solicitud.solEstado.Captura, oCrd))
                {
                    bool status = admFondos.RegistraLoteSinFondeo(Lote);
                    //if (admFondos.RegistraLoteSinFondeo(Lote))
                    //{
                    //    Gral.EnviaCorreoSolicitudFondos(oCrd.Nombre, Lote);
                    //}
                }
            }
        }

        private void ObtieneSolicitudes(List<cpplib.SolicitudFondos> LstFondos, List<cpplib.SolicitudFondos> lstSinFondos)
        {
            foreach (RepeaterItem Reg in rptSolicitud.Items)
            {
                cpplib.SolicitudFondos osol = new cpplib.SolicitudFondos();
                int IdSol = Convert.ToInt32(((Label)(Reg.FindControl("lbIdSol"))).Text);
                TextBox CantidadPagar = (TextBox)(Reg.FindControl("txCantidadPagar"));
                String Moneda = ((Label)(Reg.FindControl("lbTpMoneda"))).Text;

                if (((CheckBox)(Reg.FindControl("chkAutorizar"))).Checked)
                {
                    if (!string.IsNullOrEmpty(CantidadPagar.Text))
                    {
                        osol.IdSolicitud = IdSol;
                        osol.Moneda = Moneda;
                        osol.ImporteAutorizado = Convert.ToDecimal(CantidadPagar.Text);
                        LstFondos.Add(osol);
                    }
                }
                else
                {
                    osol.IdSolicitud = IdSol;
                    osol.Moneda = Moneda;
                    osol.ImporteAutorizado = Convert.ToDecimal(CantidadPagar.Text);
                    lstSinFondos.Add(osol);
                }
            }
        }

        private cpplib.LoteFondos CargadatosLote(int IdFondeo ,List<cpplib.SolicitudFondos> Lista,int IdUsr)
        {
            cpplib.credencial oCredencial = (cpplib.credencial)Session["credencial"];
            cpplib.LoteFondos Lote = new cpplib.LoteFondos();
            Lote.IdFondeo = IdFondeo;
            Lote.IdEmpresa = Convert.ToInt32(hdIdEmpresa.Value);
            Lote.Empresa = ltEmpresa.Text;
            Lote.NoSolicitudes = Lista.Count;
            Lote.ImporteMx = Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.ImporteAutorizado );
            Lote.ImporteDlls = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.ImporteAutorizado);
            Lote.TipoCambio = Convert.ToDecimal(txTpCambio.Text);
            Lote.IdUsr = IdUsr;
            return Lote;
        }

        private bool AgregaSolicitudes(int IdFondos, List<cpplib.SolicitudFondos> lista, cpplib.Solicitud.solEstado EdoSol, cpplib.credencial oCrd)
        {
            bool resultado = false;
            cpplib.admBitacoraSolicitud admBtc = new cpplib.admBitacoraSolicitud();
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.admFondos admFondos = new cpplib.admFondos();
            foreach (cpplib.SolicitudFondos oSol in lista ){
                oBitacora.IdUsr = oCrd.IdUsr ;
                oBitacora.Nombre = oCrd.Nombre;
                oBitacora.Estado = EdoSol;
                oBitacora.IdSolicitud = oSol.IdSolicitud;
                oBitacora.Importe = oSol.ImporteAutorizado;
                
                admBtc.Registrar(oBitacora);

                admFondos.RegistraFactura(IdFondos, oSol.IdSolicitud, oSol.ImporteAutorizado);
                
                admSol.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, EdoSol, oSol.ImporteAutorizado);
            }

            resultado = true;

            return resultado;
        }
    }
}