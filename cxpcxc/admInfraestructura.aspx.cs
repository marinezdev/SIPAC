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
    public partial class admInfraestructura : Utilerias.Comun
    {
        // Referencia de ejemplo para esta página:   trf_ConsultaGralSolicitudes.aspx

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
                cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = ocd.IdEmpresaTrabajo.ToString();
                hdIdUsr.Value = ocd.IdUsr.ToString();

                //this.txF_Inicio.Attributes.Add("readonly", "true");
                //this.txF_Fin.Attributes.Add("readonly", "true");
                //txF_Inicio.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                //txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //ce_txF_Inicio.EndDate = DateTime.Now;
                //ce_txF_Fin.EndDate = DateTime.Now;

                this.llenaCombos(hdIdEmpresa.Value);
                //this.ValidaConsultaPrevia();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            Response.Redirect("espera.aspx"); 
        }

        private void llenaCombos(String IdEmpresa)
        {
            dpEstado.Items.Add(new ListItem("Ocupado", "1"));
            dpEstado.Items.Add(new ListItem("Desocupado", "0"));
            dpEstado.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            dpFiltroNumerico1.Items.Add(new ListItem("Igual", "="));
            dpFiltroNumerico1.Items.Add(new ListItem("Mayor Igual", ">="));
            dpFiltroNumerico1.Items.Add(new ListItem("Mayor", ">"));
            dpFiltroNumerico1.Items.Add(new ListItem("Menor Igual", "<="));
            dpFiltroNumerico1.Items.Add(new ListItem("Menor", "<"));
            dpFiltroNumerico1.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            dpSubClasificacion.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            //cpplib.clsSIPAC _SIPAC = new cpplib.clsSIPAC();
            //List<cpplib.SIPAC_Instalaciones> SIPAC_Instalaciones =  _SIPAC.Instalaciones_Get(0);
            LlenarControles.LlenarDropDownList(ref dpClasificacion, comun.clssipac.Instalaciones_Get(0), "Nombre", "Id");
            //dpClasificacion.DataSource = comun.clssipac.Instalaciones_Get(0); //SIPAC_Instalaciones;
            //dpClasificacion.DataValueField = "Id";
            //dpClasificacion.DataTextField = "Nombre";
            //dpClasificacion.DataBind();
            //dpClasificacion.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            //List<cpplib.SIPAC_Clientes> SIPAC_Clientes = _SIPAC.Clientes_Get(int.Parse(IdEmpresa));
            LlenarControles.LlenarDropDownList(ref dpClientes, comun.clssipac.Clientes_Get(int.Parse(IdEmpresa)), "Nombre", "Id");
            //dpClientes.DataSource = comun.clssipac.Clientes_Get(int.Parse(IdEmpresa)); //SIPAC_Clientes;
            //dpClientes.DataValueField = "Id";
            //dpClientes.DataTextField = "Nombre";
            //dpClientes.DataBind();
            //dpClientes.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            //_SIPAC = null;
            //SIPAC_Instalaciones = null;
            //SIPAC_Clientes = null;
        }

        protected void dpClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = dpClasificacion.SelectedValue;

            if (valor != "-1")
            {
                //cpplib.clsSIPAC _SIPAC = new cpplib.clsSIPAC();
                //List<cpplib.SIPAC_Instalaciones> SIPAC_Instalaciones = _SIPAC.Instalaciones_Get(int.Parse(valor));
                LlenarControles.LlenarDropDownList(ref dpSubClasificacion, comun.clssipac.Instalaciones_Get(int.Parse(valor)), "Nombre", "Id");
                //dpSubClasificacion.DataSource = comun.clssipac.Instalaciones_Get(int.Parse(valor)); //SIPAC_Instalaciones;
                //dpSubClasificacion.DataValueField = "Id";
                //dpSubClasificacion.DataTextField = "Nombre";
                //dpSubClasificacion.DataBind();
                //dpSubClasificacion.Items.Insert(0, new ListItem("Seleccionar", "-1"));
            }
            else
            {
                lkLimpiar_Click(sender, e);
            }
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) 
        { 
            this.ConsultaNormal(); 
        }

        private void ConsultaNormal()
        {
            hdConsulta.Value = this.DaFiltradoConsulta();
            this.EjecutaConsulta(hdConsulta.Value);
        }

        private void EjecutaConsulta(string CondicionWHERE)
        {
            ltMsg.Text = "";
            cpplib.clsSIPAC _SIPAC = new cpplib.clsSIPAC();

            //if (!string.IsNullOrEmpty(Consulta))
            //{
            List<cpplib.SIPAC_InstalacionesOcupacion> Lista = comun.clssipac.Instalaciones_Get(CondicionWHERE); //_SIPAC.Instalaciones_Get(CondicionWHERE);
            if (Lista != null)
            {
                if (Lista.Count > 0)
                {
                    //lbTotalSol.Text = "SOLICITUDES (" + Lista.Count.ToString() + ")";
                    rptSolicitud.DataSource = Lista;
                    rptSolicitud.DataBind();
                    pnSolicitud.Visible = true;

                    //lbTotPesos.Text = Lista.Where(sol => sol.Moneda == "Pesos").Sum(sol => sol.Importe).ToString("C2");
                    //lbTotDlls.Text = Lista.Where(sol => sol.Moneda == "Dolares").Sum(sol => sol.Importe).ToString("C2");

                    this.AgregaConsultaSesion(hdConsulta.Value);
                }
                else
                {
                    rptSolicitud.DataSource = null;
                    rptSolicitud.DataBind();
                    ltMsg.Text = "No hay instalaciones para mostrar";
                    pnSolicitud.Visible = false;
                }
            }
            else
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                ltMsg.Text = "No hay instalaciones para mostrar";
                pnSolicitud.Visible = false;
            }

            _SIPAC = null;
        }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver"))
            {
                //cpplib.clsSIPAC_Security _SIPACSeg = null;
                //_SIPACSeg = new cpplib.clsSIPAC_Security();

                string _parametro = "";
                _parametro = e.CommandArgument.ToString();
                //_parametro = HttpUtility.UrlEncode(_SIPACSeg.Encrypt(e.CommandArgument.ToString()));
                _parametro = HttpUtility.UrlEncode(comun.clssipacsecurity.Encrypt(e.CommandArgument.ToString()));

                Response.Redirect("cxc_ConsultaOrdFactura_Sol_xCliente.aspx?Cliente=" + _parametro);
                //_SIPACSeg = null;
            }
        }

        private String DaFiltradoConsulta()
        {
            string Consulta = string.Empty;

            // Filtro de Clientes
            if (dpClientes.SelectedValue != "-1")
            { 
                Consulta += " AND cat_Clientes.Id = " + dpClientes.SelectedValue.ToString();
            }

            // Filtro de Estado
            if (dpEstado.SelectedValue != "-1")
            {
                Consulta += " AND cat_Instalaciones.Ocupado = " + dpEstado.SelectedValue.ToString(); 
            }

            // Filtro de Instalaciones (1er Nivel)
            if (dpClasificacion.SelectedValue != "-1")
            {
                Consulta += " AND cat_Instalaciones.IdParent = " + dpClasificacion.SelectedValue.ToString();
            }

            // Filtro de Instalaciones (1er Nivel)
            if (dpSubClasificacion.SelectedValue != "-1")
            {
                Consulta += " AND cat_Instalaciones.Id = " + dpSubClasificacion.SelectedValue.ToString();
            }

            // Filtro de M2
            if (dpFiltroNumerico1.SelectedValue != "-1") 
            {
                if (txM2.Text.Trim().Length > 0)
                {
                    int Valor = int.Parse(txM2.Text.Trim());
                    Consulta += " AND cat_Instalaciones.M2 " + dpFiltroNumerico1.SelectedValue + " " + Valor.ToString();
                }
                else
                {
                    ltMsg.Text = "Debe indicar un valor para el filtro de M2 (metros cuadrados)";
                }
            }

            return Consulta;
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image oimg = (Image)e.Item.FindControl("imgOcupacion");
                string Imagen = String.Empty;
                cpplib.SIPAC_InstalacionesOcupacion _InstalacionesOcupa = (cpplib.SIPAC_InstalacionesOcupacion)(e.Item.DataItem);
                switch (_InstalacionesOcupa.Ocupado)
                {
                    case false:
                        oimg.ImageUrl = "~/img/sem_R.png";
                        break;
                    case true:
                        oimg.ImageUrl = "~/img/Sem_V.png";
                        break;
                    default:
                        oimg.ImageUrl = "~/img/Sem_A.png";
                        break;
                }
            }
        }

        private void AgregaConsultaSesion(string Datos)
        {
            //cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            //csConsulta.Pagina = "trf_ConsultaGralSolicitudes";
            //csConsulta.Datos = Datos;
            //Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            //if (Session["csConsultas"] != null)
            //{
            //    cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
            //    if (csConsulta.Pagina.Equals("trf_ConsultaGralSolicitudes"))
            //    {
            //        hdConsulta.Value = csConsulta.Datos;
            //        this.EjecutaConsulta(csConsulta.Datos);
            //    }
            //}
            //else 
            //{ 
            //    this.ConsultaNormal(); 
            //}
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time. */
        }

        protected void btnExportar_Click(object sender, ImageClickEventArgs e)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            //cpplib.credencial ocd = (cpplib.credencial)Session["credencial"];
            //DataTable Lista = admSol.DaSolXEmpresaExportar(Convert.ToInt32(hdIdEmpresa.Value), hdConsulta.Value);
            //string Archivo = hdIdUsr.Value.PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy") + ".xls";
            //if (Lista.Rows.Count > 0)
            //{
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=" + Archivo);
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (System.IO.StringWriter sw = new System.IO.StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        GridView grdDatos = new GridView();
            //        grdDatos.DataSource = Lista;
            //        grdDatos.DataBind();
            //        grdDatos.AllowPaging = false;
            //        grdDatos.RenderControl(hw);
            //        Response.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }

        protected void lkLimpiar_Click(object sender, EventArgs e)
        {
            dpSubClasificacion.Items.Clear();
            dpSubClasificacion.Items.Insert(0, new ListItem("Seleccionar", "-1"));

            dpClientes.SelectedIndex = 0;
            dpClasificacion.SelectedIndex = 0;
            dpSubClasificacion.SelectedIndex = 0;
            dpEstado.SelectedIndex = 0;
            dpFiltroNumerico1.SelectedIndex = 0;
            txM2.Text = "";
            pnSolicitud.Visible = false;
        }
    }
}