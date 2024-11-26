using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_AgregaMarcaPrioridad : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
                hdIdUsr.Value = ocd.IdUsr.ToString();
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                this.llenaCombos(ocd.IdEmpresa.ToString());
                this.CargaSolicitudes();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("trf_SolicitudesRegistro.aspx"); }

        private void llenaCombos(String IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");
            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) { this.CargaSolicitudes(); }

        private void CargaSolicitudes() {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            List<cpplib.Solicitud> Lista = comun.admsolicitud.DaSolEnRegistroXUsuario(hdIdUsr.Value, DaConsulta()); 
            if (Lista.Count > 0)
            {
                //rptSolicitud.DataSource = Lista;
                //rptSolicitud.DataBind();
                LlenarControles.LlenarRepeater(ref rptSolicitud, Lista);
                pnSolicitud.Visible = true;
                CalculaMontoPrioridades();
            }
            else 
            { 
                rptSolicitud.DataSource = null; 
                rptSolicitud.DataBind(); 
                ltMsg.Text = "No hay solicitudes para mostrar"; 
                pnSolicitud.Visible = false; 
            }
        }

        private String DaConsulta()
        {
            string Consulta = string.Empty;

            if (dpProveedor.SelectedValue != "0") { Consulta += " And Rfc='" + dpProveedor.SelectedValue + "'"; }
            if (!String.IsNullOrEmpty(txF_Inicio.Text) && !String.IsNullOrEmpty(txF_Fin.Text))
            {
                Consulta += " And (FechaRegistro >='" + txF_Inicio.Text + "' and FechaRegistro < DATEADD(dd,1,'" + txF_Fin.Text + "'))";
            }

            return Consulta;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.Solicitud oSol = (cpplib.Solicitud)(e.Item.DataItem);
                if (oSol.Prioridad == 1) { ((ImageButton)e.Item.FindControl("btnActivo")).Visible =true; }
                else if (oSol.Prioridad == 0) { ((ImageButton)e.Item.FindControl("btnInactivo")).Visible = true; }
            }
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
             ImageButton btnActivo = (ImageButton)e.Item.FindControl("btnActivo");
             ImageButton btnInActivo = (ImageButton)e.Item.FindControl("btnInactivo");
              cpplib.admSolicitud  adm= new cpplib.admSolicitud();
   
            if (e.CommandName.Equals("btnInactivo")) {
                adm.AsignarMarcaPrioridad(Convert.ToInt32(e.CommandArgument), 1);
                btnActivo.Visible = true;
                btnInActivo.Visible = false;
                }
            if (e.CommandName.Equals("btnActivo")) {
                adm.AsignarMarcaPrioridad(Convert.ToInt32(e.CommandArgument), 0);
                btnActivo.Visible = false;
                btnInActivo.Visible = true;
            }
            CalculaMontoPrioridades();
        }


        protected void CalculaMontoPrioridades()
        {
            decimal TotPesos = 0;
            decimal TotDlls = 0;
            foreach (RepeaterItem Reg in rptSolicitud.Items)
            {
                if (((ImageButton)(Reg.FindControl("btnActivo"))).Visible == true) {

                    String Moneda = ((Label)(Reg.FindControl("lbMoneda"))).Text;

                    if (Moneda.Equals("Pesos"))
                    {
                        TotPesos += Convert.ToDecimal(((Label)(Reg.FindControl("lbImporte"))).Text);
                    }
                    else if (Moneda.Equals("Dolares"))
                    {
                        TotDlls += Convert.ToDecimal(((Label)(Reg.FindControl("lbImporte"))).Text);
                    
                    }
                }
            }
            lbTotPesos.Text = TotPesos.ToString("C2");
            lbTotDlls.Text = TotDlls.ToString("C2");
        }

    }
}