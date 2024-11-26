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
    public partial class trf_AutorizarDinamica : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
                hdIdUsr.Value = oCd.IdUsr.ToString();
                hdIdEmpresa.Value = oCd.IdEmpresaTrabajo.ToString();
                this.llenacatalogos();
                this.Inicializa();
            }
        }

        private void Inicializa(){
            this.DaSolicitudes();
            this.ConsultaListaAutoriacion();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenacatalogos()
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(hdIdEmpresa.Value), "Nombre", "Rfc");
            LlenarControles.LlenarDropDownList(ref dpUdNegocio, comun.admcatunidadnegocio.daComboUnidadNegocio(hdIdEmpresa.Value), "Texto", "Valor");
            LlenarControles.LlenarDropDownList(ref dpSolicitante, comun.admcredencial.DaUsuariosxEmpresaxSolicitante(hdIdEmpresa.Value), "Nombre", "IdUsr");

            //List<cpplib.CatProveedor> LstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(hdIdEmpresa.Value);

            //dpProveedor.DataSource = LstPvd;
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            ///*Unidad negocio*/
            //List<cpplib.valorTexto> lstUdNeg = (new cpplib.admCatUnidadNegocio()).daComboUnidadNegocio(hdIdEmpresa.Value);
            //dpUdNegocio.DataSource = lstUdNeg;
            //dpUdNegocio.DataValueField = "Valor";
            //dpUdNegocio.DataTextField = "Texto";
            //dpUdNegocio.DataBind();

            //List<cpplib.credencial> LstUsr = (new cpplib.admCredencial()).DaUsuariosxEmpresaxSolicitante(hdIdEmpresa.Value);
            //dpSolicitante.DataSource = LstUsr;
            //dpSolicitante.DataTextField = "Nombre";
            //dpSolicitante.DataValueField = "IdUsr";
            //dpSolicitante.DataBind();

            //dpSolicitante.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        private void DaSolicitudes()
        {
            lbTotPesos.Text = "0";
            lbTotDlls.Text = "0";
            ltMsg.Text = "";
            lbNumSolicitudes.Text = "";
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            String Consulta = this.DaConsulta();

            DataTable Lista = comun.admsolicitud.ListaXAutorizar(hdIdEmpresa.Value,hdIdUsr.Value,Consulta);
            if (Lista.Rows.Count > 0)
            {
                lbNumSolicitudes.Text = "Solicitudes disponibles: (" + Lista.Rows.Count.ToString() + ")";
                lbTotPesos.Text = Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                lbTotDlls.Text = Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2");
                //rptSolicitud.DataSource = Lista;
                //rptSolicitud.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptSolicitud, Lista);
            }
            else
            {
                ltMsg.Text = "No hay Solicitudes para autorización";
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
            }

        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                 
                if (oSol["ConFactura"].ToString().Equals("NO")) {
                    ((Image)e.Item.FindControl("imgConFactura")).ImageUrl = "~/img/sem_R.png";
                }
                
                if (oSol["Prioridad"].ToString().Equals ("1"))
                {
                    ((Image)e.Item.FindControl("imgPrioridad")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lkQuitaPrd")).Visible = true;
                }

                string Marcado = oSol["Marcado"].ToString();
                if ((Marcado != "0") && (Marcado != hdIdUsr.Value))
                {
                    ((ImageButton)e.Item.FindControl("btnAutrz")).Visible = false;
                }
            }
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
                if (e.CommandName.Equals("ver")) Response.Redirect("trf_VerSolAutorizacion.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_AutorizarDinamica");
                try
                {
                    if (e.CommandName.Equals("lkQuitaPrd"))
                    {
                        cpplib.admSolicitud adm = new cpplib.admSolicitud();
                        adm.AsignarMarcaPrioridad(Convert.ToInt32(e.CommandArgument.ToString()), 0);
                        ((LinkButton)e.Item.FindControl("lkQuitaPrd")).Visible = false;
                        ((Image)e.Item.FindControl("imgPrioridad")).Visible = false;
                    }

                    if (e.CommandName.Equals("btnAutrz"))
                    {
                        cpplib.admSolicitud adm = new cpplib.admSolicitud();
                        String lbPorPagar = ((Label)e.Item.FindControl("lbCantidadPagar")).Text.Replace(",", "");
                        adm.AsignarMarcaAutorizar(hdIdEmpresa.Value, Convert.ToInt32(e.CommandArgument), hdIdUsr.Value, lbPorPagar);
                        this.Inicializa();
                    }
                }
                catch (Exception) { this.Inicializa(); }
            
        }

        private void ConsultaListaAutoriacion()
        {
            //cpplib.admSolicitud adm = new cpplib.admSolicitud();
            DataTable Resultado = comun.admsolicitud.ListaAutorizacion(hdIdEmpresa.Value, hdIdUsr.Value);
            if (Resultado.Rows.Count > 0)
            {
                lbTotAutPesos.Text = Resultado.Compute("Sum(ImporteAutorizado)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(ImporteAutorizado)", "Moneda = 'pesos'")).ToString("C2");
                lbTotAutDlls.Text = Resultado.Compute("Sum(ImporteAutorizado)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(ImporteAutorizado)", "Moneda = 'Dolares'")).ToString("C2");
                //rptAutorizar.DataSource = Resultado;
                //rptAutorizar.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptAutorizar, Resultado);
            }
            else 
            {
                lbTotAutPesos.Text = "0"; lbTotAutDlls.Text = "0";
                rptAutorizar.DataSource = null;
                rptAutorizar.DataBind();
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.DaSolicitudes(); }

        private String DaConsulta()
        {
            string Consulta = string.Empty;

            if (dpProveedor.SelectedValue != "0") { Consulta = "  and S.RFC='" + dpProveedor.SelectedValue + "'"; }
            if (dpUdNegocio.SelectedValue != "0") { Consulta += " And S.UnidadNegocio=" + dpUdNegocio.SelectedValue; }
            if (dpSolicitante.SelectedValue != "0") { Consulta += " And S.IdUsr=" + dpSolicitante.SelectedValue; }

            return Consulta;
        }

        protected void txAutorizado_TextChanged(object sender, EventArgs e)
        {
            //cpplib .admSolicitud Adm = new cpplib.admSolicitud(); 
            foreach (RepeaterItem Reg in rptAutorizar.Items)
            {
                TextBox txAutorizado = (TextBox)(Reg.FindControl("txAutorizado"));
                if (!string.IsNullOrEmpty(txAutorizado.Text))
                {
                    decimal PorPagar = Convert.ToDecimal(((Label)(Reg.FindControl("lbPorPagar"))).Text);
                    decimal Autorizado = Convert.ToDecimal(txAutorizado.Text);
                    if ((Autorizado > 0) || (Autorizado < PorPagar))
                    {
                        ImageButton btnQuitar = (ImageButton)(Reg.FindControl("btnQuitar"));
                        comun.admsolicitud.ActulizaSaldoAutorizacion(hdIdEmpresa.Value, hdIdUsr.Value, btnQuitar.CommandArgument, Autorizado);
                    }
                }
            }
            ConsultaListaAutoriacion();
        }
          
        protected void rptAutorizar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btnQuitar"))
            {
                //cpplib.admSolicitud adm = new cpplib.admSolicitud();
                comun.admsolicitud.QuitarMarcaAutorizar(Convert.ToInt32(e.CommandArgument), hdIdUsr.Value);
                this.Inicializa();
            }
        }
                
        /// REGISTRA  EL LOTE AUTORIZADO
        
        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (lbTotAutDlls.Text !="0") { mpeTpCambio.Show(); }
            else {txTpCambio.Text="1"; this.RegistraLotes(); }
        }

        private void RegistraLotes()
        {
            List<cpplib.SolicitudFondos> LstAutorizada = ObtieneSolicitudes();
            if (LstAutorizada.Count >0)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                //cpplib.admFondos admFondos = new cpplib.admFondos();
                int IdFondeo = comun.admfondos.daSiguienteIdentificador();
                cpplib.LoteFondos Lote = CargadatosLote(IdFondeo, LstAutorizada, oCrd.IdUsr);
                
                if (chksinDeposito.Checked)
                {
                    if (AgregaSolicitudes(IdFondeo, LstAutorizada, cpplib.Solicitud.solEstado.Captura, cpplib.SolicitudFondos.enEstado.Con_fondos, oCrd))
                    {
                        if (comun.admfondos.RegistraLoteSinFondeo(Lote)) 
                        {
                            bool resultado = comun.admsolicitud.EliminaDatosAutoriacion(hdIdEmpresa.Value, hdIdUsr.Value);
                            csGeneral Gral = new csGeneral();
                            Gral.EnviaCorreoNotificacionLoteSinDeposito(oCrd.Nombre, Lote);
                        }
                    }
                }

                else {
                    if (AgregaSolicitudes(IdFondeo, LstAutorizada, cpplib.Solicitud.solEstado.Autorizacion, cpplib.SolicitudFondos.enEstado.Autorizado, oCrd))
                    {
                        if (comun.admfondos.RegistraLoteFondeo(Lote))
                        {
                            bool resultado = comun.admsolicitud.EliminaDatosAutoriacion(hdIdEmpresa.Value, hdIdUsr.Value);
                            csGeneral Gral = new csGeneral();
                            Gral.EnviaCorreoSolicitudFondosLote(oCrd.Nombre, Lote);
                        }
                    }
                }
                this.Inicializa();
            }
        }

        private List<cpplib.SolicitudFondos> ObtieneSolicitudes()
        {
            List<cpplib.SolicitudFondos> Resultado = new List<cpplib.SolicitudFondos>();
            foreach (RepeaterItem Reg in rptAutorizar.Items)
            {
                cpplib.SolicitudFondos osol = new cpplib.SolicitudFondos();
                int IdSol = Convert.ToInt32(((ImageButton)(Reg.FindControl("btnQuitar"))).CommandArgument);
                TextBox CantidadPagar = (TextBox)(Reg.FindControl("txAutorizado"));
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
            Lote.Empresa = (new cpplib.admCatEmpresa()).carga(Convert.ToInt32(hdIdEmpresa.Value)).Nombre;
            Lote.NoSolicitudes = Lista.Count;
            Lote.ImporteMx = Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.ImporteAutorizado);
            Lote.ImporteDlls = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.ImporteAutorizado);
            Lote.TipoCambio = Convert.ToDecimal(txTpCambio.Text);
            Lote.IdUsr = IdUsr;
            return Lote;
        }

        private bool AgregaSolicitudes(int IdFondos, List<cpplib.SolicitudFondos> lista, cpplib.Solicitud.solEstado EdoSol, cpplib.SolicitudFondos.enEstado EstadoFondos, cpplib.credencial oCrd)
        {
            bool resultado = false;
            //cpplib.admBitacoraSolicitud admBtc = new cpplib.admBitacoraSolicitud();
            cpplib.Bitacora oBitacora = new cpplib.Bitacora();
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            //cpplib.admFondos admFondos = new cpplib.admFondos();
            foreach (cpplib.SolicitudFondos oSol in lista)
            {
                oBitacora.IdUsr = oCrd.IdUsr;
                oBitacora.Nombre = oCrd.Nombre;
                oBitacora.Estado = EdoSol;
                oBitacora.IdSolicitud = oSol.IdSolicitud;
                oBitacora.Importe = oSol.ImporteAutorizado;

                comun.admbitacorasolicitud.Registrar(oBitacora);

                comun.admfondos.RegistraFactura(IdFondos, oSol.IdSolicitud, oSol.ImporteAutorizado,EstadoFondos);

                comun.admsolicitud.CambiaEstadoYCantPagoSolicitud(oSol.IdSolicitud, EdoSol, oSol.ImporteAutorizado);
            }

            resultado = true;

            return resultado;
        }

        protected void btnAceptaTpCambio_Click(object sender, EventArgs e) { this.RegistraLotes(); }
        
    }
}