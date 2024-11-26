using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class trf_SolicitudesDireccion : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString();
                    
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                this.txF_Inicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                    
                this.llenaCombos();

                this.ValidaConsultaPrevia();
                
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e){Response.Redirect("espera.aspx");}

        
        private void llenaCombos() 
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(hdIdEmpresa.Value), "Nombre", "Rfc");
            LlenarControles.LlenarDropDownList(ref dpUdNegocio, comun.admcatunidadnegocio.daComboUnidadNegocio(hdIdEmpresa.Value), "Texto", "Valor");

            ///*Proveedores*/
            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(hdIdEmpresa.Value);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Rfc";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            ///*Unidad negocio*/
            //List<cpplib.valorTexto> lstUdNeg = (new cpplib.admCatUnidadNegocio()).daComboUnidadNegocio(hdIdEmpresa.Value);
            //dpUdNegocio.DataSource = lstUdNeg;
            //dpUdNegocio.DataValueField = "Valor";
            //dpUdNegocio.DataTextField = "Texto";
            //dpUdNegocio.DataBind();

            /* Llena estado*/
            foreach (int value in Enum.GetValues(typeof(cpplib.Solicitud.solEstado)))
            {
                if (value != 60 && value != 30 && value != 40 && value != 10)
                {
                    var name = Enum.GetName(typeof(cpplib.Solicitud.solEstado), value);
                    dpEstado.Items.Add(new ListItem(name, value.ToString()));
                }
            }
        }

      
       protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) {
           ltMsg.Text = "";
           lbTotPesos.Text = "0";
           lbTotDlls.Text = "0";
           hdConsulta.Value= this.DaConsulta();

           //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
           DataTable Resultado = comun.admsolicitud.DaConsulaDireccion(hdIdEmpresa.Value, dpEstado.SelectedValue, txF_Inicio.Text, txF_Fin.Text, hdConsulta.Value);
            if (Resultado.Rows.Count > 0)
            {
                //rptSolicitud.DataSource = Resultado;
                //rptSolicitud.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rptSolicitud, Resultado);
                lbTotPesos.Text = Resultado.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                lbTotDlls.Text = Resultado.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2");
                pnContSolicitud.Visible = true;

                this.AgregaConsultaSesion(dpEstado.SelectedValue + "|" + txF_Inicio.Text + "|" + txF_Fin.Text + "|" + hdConsulta.Value);
            }
            else
            {
                rptSolicitud.DataSource = null;
                rptSolicitud.DataBind();
                ltMsg.Text = "No hay solicitudes para mostrar";
                pnContSolicitud.Visible = false;
            }
       }

        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("ver")) Response.Redirect("trf_VerSolDireccion.aspx?id=" + e.CommandArgument.ToString());
        }

        private string  DaConsulta() {
            string Resultado = string.Empty;

            if (!dpProveedor.SelectedValue.Equals("0")) { Resultado = " where RFC='" + dpProveedor.SelectedValue + "'"; }
            if (!dpUdNegocio.SelectedValue.Equals("0")) {
                if (string.IsNullOrEmpty(Resultado))
                {
                    Resultado = " where IdUnidadNegocio='" + dpUdNegocio.SelectedValue + "'";
                }
                else { Resultado += " and IdUnidadNegocio=" + dpUdNegocio.SelectedValue;}
            }

            return Resultado;
        }

        
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            DataTable Lista = comun.admsolicitud.DaConsulaDireccion(hdIdEmpresa.Value, dpEstado.SelectedValue, txF_Inicio.Text, txF_Fin.Text, hdConsulta.Value);
            Lista.Columns.Remove("IdSolicitud");
            String IdUsr = ((cpplib.credencial)Session["credencial"]).IdUsr.ToString();
            string Archivo = IdUsr.PadLeft(4, '0') + DateTime.Now.ToString("ddMMyy") + ".xls";
            if (Lista.Rows .Count> 0)
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
                    grdDatos.DataSource = Lista;
                    grdDatos.DataBind();
                    grdDatos.AllowPaging = false;
                    grdDatos.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
                  
        }

        
        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "trf_SolicitudesDireccion";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("trf_SolicitudesDireccion"))
                {
                    string[] valores = csConsulta.Datos.Split('|');
                    dpEstado.SelectedValue=valores[0]; 
                    txF_Inicio.Text=valores[1];
                    txF_Fin.Text = valores[2];
                    hdConsulta.Value = valores[3];

                    //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                    DataTable Resultado = comun.admsolicitud.DaConsulaDireccion(hdIdEmpresa.Value, valores[0], valores[1], valores[2], valores[3]);
                    if (Resultado.Rows.Count > 0)
                    {
                        rptSolicitud.DataSource = Resultado;
                        rptSolicitud.DataBind();
                        lbTotPesos.Text = Resultado.Compute("Sum(Importe)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(Importe)", "Moneda = 'pesos'")).ToString("C2");
                        lbTotDlls.Text = Resultado.Compute("Sum(Importe)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(Resultado.Compute("Sum(Importe)", "Moneda = 'Dolares'")).ToString("C2");
                        pnContSolicitud.Visible = true;
                    }
                }
            }
            
        }

        protected void rptSolicitud_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView oSol = (DataRowView)(e.Item.DataItem);
                Image oimg = (Image)e.Item.FindControl("imgConfactura");
                string Confactura = oSol["ConFactura"].ToString();
                if (Confactura == "SI") { oimg.ImageUrl = "~/img/sem_V.png"; }
                if (Confactura == "NO") { oimg.ImageUrl = "~/img/sem_R.png"; }
            }
        }


        //private DataTable RegresaTabla(List<cpplib.SolContabilidad> Lista)
        //{
        //    DataTable Datos = new DataTable();

        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(cpplib.SolContabilidad));
        //    foreach (PropertyDescriptor prop in properties)
        //        Datos.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        //    foreach (cpplib.SolContabilidad obj in Lista)
        //    {
        //        DataRow row = Datos.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(obj) ?? DBNull.Value;
        //        Datos.Rows.Add(row);
        //    }

        //    Datos.Columns.RemoveAt(0);

        //    return Datos;
        //}
                                
    }
}