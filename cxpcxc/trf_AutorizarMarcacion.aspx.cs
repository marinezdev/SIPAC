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
    public partial class trf_AutorizarMarcacion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cpplib .credencial oCd =(cpplib.credencial)Session["credencial"];
                hdIdUsr.Value = oCd.IdUsr.ToString();
                HdIdEmpresa.Value = oCd.IdEmpresaTrabajo.ToString ();
                this.llenacatalogos(HdIdEmpresa.Value);
                this.DaSolicitudes();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void llenacatalogos(String IdEmpresa) 
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Rfc");
            LlenarControles.LlenarDropDownList(ref dpUdNegocio, comun.admcatunidadnegocio.daComboUnidadNegocio(IdEmpresa), "Texto", "Valor");
            LlenarControles.LlenarDropDownList(ref dpSolicitante, comun.admcredencial.DaUsuariosxEmpresaxSolicitante(IdEmpresa), "Nombre", "IdUsr");

            //List<cpplib.CatProveedor> LstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            
            //dpProveedor.DataSource = LstPvd;
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            ///*Unidad negocio*/
            //List<cpplib.valorTexto> lstUdNeg = (new cpplib.admCatUnidadNegocio()).daComboUnidadNegocio(IdEmpresa);
            //dpUdNegocio.DataSource = lstUdNeg;
            //dpUdNegocio.DataValueField = "Valor";
            //dpUdNegocio.DataTextField = "Texto";
            //dpUdNegocio.DataBind();

            //List<cpplib.credencial> LstUsr = (new cpplib.admCredencial()).DaUsuariosxEmpresaxSolicitante(IdEmpresa);
            //dpSolicitante.DataSource = LstUsr;
            //dpSolicitante.DataTextField = "Nombre";
            //dpSolicitante.DataValueField = "IdUsr";
            //dpSolicitante.DataBind();

            //dpSolicitante.Items.Insert(0,new ListItem("Seleccionar", "0")); 
        }
        
        private void DaSolicitudes()
        {
            lbTotPesos.Text = "0";
            lbTotDlls.Text = "0";
            lbTotAutPesos.Text = "0";
            lbTotAutDlls .Text = "0";
            ltMsg.Text = "";
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            String Consulta = this.DaConsulta();

            DataTable Lista = admSol.ListaSolicitudesXAutorizar(HdIdEmpresa.Value, Consulta);
            if (Lista.Rows .Count > 0) 
            {
                lbTotPesos.Text = Lista.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                lbTotDlls.Text = Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Lista.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2"); 
                rptSolicitud.DataSource = Lista;
                rptSolicitud.DataBind();
                pnContenido.Visible = true;
            }
            else
            {
                pnContenido.Visible = false;
                ltMsg.Text = "No hay Solicitudes para autorización";
            }

            CalculaMontosAutorizacion(admSol);
            
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                Response.Redirect("trf_VerSolAutorizacion.aspx?id=" + e.CommandArgument.ToString() + "&bk=trf_AutorizarMarcacion");
            }
            
            if ((e.CommandName.Equals("btnInactivo")) || (e.CommandName.Equals("btnActivo")))
            {
                cpplib.admSolicitud adm = new cpplib.admSolicitud();
                ImageButton btnActivo = (ImageButton)e.Item.FindControl("btnActivo");
                ImageButton btnInActivo = (ImageButton)e.Item.FindControl("btnInactivo");
                if (e.CommandName.Equals("btnActivo"))
                {
                    adm.QuitarMarcaAutorizar(Convert.ToInt32(e.CommandArgument), hdIdUsr.Value);
                    btnActivo.Visible = false;
                    btnInActivo.Visible = true; ;
                }

                if (e.CommandName.Equals("btnInactivo"))
                {
                    string CantidadPagar = ((Label )e.Item.FindControl("lbCantidadPagar")).Text.Replace (",","");
                    adm.AsignarMarcaAutorizar(HdIdEmpresa.Value, Convert.ToInt32(e.CommandArgument), hdIdUsr.Value, CantidadPagar);
                    btnActivo.Visible = true;
                    btnInActivo.Visible = false;
                }

                CalculaMontosAutorizacion(adm);
            }

            if (e.CommandName.Equals("lkQuitaPrd")) 
            {
                cpplib.admSolicitud adm = new cpplib.admSolicitud();
                adm.AsignarMarcaPrioridad(Convert.ToInt32(e.CommandArgument.ToString()), 0);
                ((LinkButton)e.Item.FindControl("lkQuitaPrd")).Visible = false;
                ((Image)e.Item.FindControl("imgPrioridad")).Visible =false;
            }
        }

        private void CalculaMontosAutorizacion(cpplib.admSolicitud adm)
        {
            DataTable Resultado = adm.DaSumaPreautorizacion(HdIdEmpresa.Value, hdIdUsr.Value);
            if (Resultado.Rows.Count > 0)
            {
                lbTotAutPesos.Text = Resultado.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(CantidadPagar)", "Moneda = 'pesos'")).ToString("C2");
                lbTotAutDlls.Text = Resultado.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(CantidadPagar)", "Moneda = 'Dolares'")).ToString("C2");
            }
            else
            {
                lbTotAutPesos.Text = "0";
                lbTotAutDlls.Text = "0";
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) 
        { 
            this.DaSolicitudes(); 
        }

        private String DaConsulta()
        {
            string Consulta = string.Empty;

            if (dpProveedor.SelectedValue != "0") 
            { 
                Consulta = "  and S.RFC='" + dpProveedor.SelectedValue + "'"; 
            }

            if (dpUdNegocio.SelectedValue != "0") 
            {
                Consulta += " And S.UnidadNegocio=" + dpUdNegocio.SelectedValue; 
            }

            if (dpSolicitante.SelectedValue != "0") 
            {
                Consulta += " And S.IdUsr=" + dpSolicitante.SelectedValue; 
            }

            return Consulta;
        }

        private void limpiar() 
        {
            dpProveedor.SelectedIndex = 0;
            dpSolicitante .SelectedIndex = 0;
            dpUdNegocio .SelectedIndex = 0;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                
                if (oSol["Prioridad"].ToString() == "1") 
                {
                    ((Image)e.Item.FindControl("imgPrioridad")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lkQuitaPrd")).Visible = true; 
                }
                               
                string Marcado = oSol["Marcado"].ToString();
                if ((Marcado != "0"))
                {
                    if (Marcado == hdIdUsr.Value)
                    {
                        ((ImageButton)e.Item.FindControl("btnInactivo")).Visible = false;
                        ((ImageButton)e.Item.FindControl("btnActivo")).Visible = true;
                    } 

                    if (Marcado!= hdIdUsr.Value)
                    {
                        ((ImageButton)e.Item.FindControl("btnInactivo")).Visible = false;
                        ((ImageButton)e.Item.FindControl("btnActivo")).Visible = false;
                    }
                }

                Image oimg = (Image)e.Item.FindControl("imgConFactura");
                string Confactura = oSol["ConFactura"].ToString();
                if (Confactura == "SI") 
                {
                    oimg.ImageUrl = "~/img/sem_V.png"; 
                }

                if (Confactura == "NO") 
                { 
                    oimg.ImageUrl = "~/img/sem_R.png"; 
                }
            }
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("trf_AutorizarProcesar.aspx?Id=" + HdIdEmpresa.Value);
        }

    }
}