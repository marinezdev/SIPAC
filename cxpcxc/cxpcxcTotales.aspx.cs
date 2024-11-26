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
    public partial class cxpcxcTotales : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txFhTermino.Attributes.Add("ReadOnly", "true");
                int dia = DateTime.Now.Day - 1;
                txFhTermino.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txFhInicio.Attributes.Add("ReadOnly", "true");
                txFhInicio.Text = DateTime.Now.AddDays(-dia).ToString("dd/MM/yyyy");
                
                this.IniciaConsulta();
            }

            Pintagrafica();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        protected void btncxcDetalles_Click(object sender, EventArgs e){mvContenedor.ActiveViewIndex = 1;}

        protected void btncxpDetalles_Click(object sender, EventArgs e){mvContenedor.ActiveViewIndex = 2;}

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e){this.IniciaConsulta();}


        private void IniciaConsulta() {
            if (!(string.IsNullOrEmpty(txFhInicio.Text)) && !(string.IsNullOrEmpty(txFhTermino.Text)))
            {
                this.Limpiar();
                this.ExtraerDatosCobranza();
                this.ExtraerDatosPagos();
                this.CalculaDiferenciaIVA();
                Pintagrafica();
                pnDatos.Visible = true;
            }
        }

        private void ExtraerDatosCobranza()
        {
            //cpplib.admCxcProyeccionCobranza adm = new cpplib.admCxcProyeccionCobranza();
            cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
            DataTable resultado = comun.admcxcproyeccioncobranza.DaCobranzaRealPorProyecto(oCd.IdEmpresaTrabajo, txFhInicio.Text, txFhTermino.Text);
            if (resultado.Rows.Count > 0)
            {
                string cxcPesos = resultado.Compute("Sum(Pesos)", "").ToString() == "" ? "0" : Convert.ToDecimal(resultado.Compute("Sum(Pesos)", "")).ToString();
                string cxcDolares = resultado.Compute("Sum(Dolares)", "").ToString() == "" ? "0" : Convert.ToDecimal(resultado.Compute("Sum(dolares)", "")).ToString("C2");
                if (!string.IsNullOrEmpty(cxcPesos))
                {
                    double Total = Convert.ToDouble(cxcPesos);
                    double Subtotal = (Total / 1.16);
                    lbcxcTotal.Text = Total.ToString("C2");
                    lbcxcIva.Text = (Total - Subtotal).ToString("C2");
                    lbcxcSubtotal.Text = Subtotal.ToString("C2");

                    
                }

                LlenarControles.LLenarRepeaterDataTable(ref rpCobranza, comun.admcxcproyeccioncobranza.DaFacturasCobranzaReal(oCd.IdEmpresaTrabajo, txFhInicio.Text, txFhTermino.Text));
                //DataTable lstFactura = comun.admcxcproyeccioncobranza.DaFacturasCobranzaReal(oCd.IdEmpresaTrabajo, txFhInicio.Text, txFhTermino.Text);
                //if (lstFactura.Rows.Count > 0) 
                //{
                //    rpCobranza.DataSource = lstFactura;
                //    rpCobranza.DataBind();
                //}
                //else
                //{
                //    rpCobranza.DataSource = null;
                //    rpCobranza.DataBind();
                //}
            }
       }

        private void ExtraerDatosPagos()
        {
            cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
            DataTable LstSol = comun.admsolicitud.ListaSolicitudesParaIVA(oCd.IdEmpresaTrabajo.ToString(), txFhInicio.Text, txFhTermino.Text);

            if (LstSol.Rows.Count > 0)
            {
                string cxpPesos = LstSol.Compute("Sum(ImportePagado)", "Moneda = 'pesos'").ToString() == "" ? "0" : Convert.ToDecimal(LstSol.Compute("Sum(ImportePagado)", "Moneda = 'pesos'")).ToString();
                string cxpDolares = LstSol.Compute("Sum(ImportePagado)", "Moneda = 'Dolares'").ToString() == "" ? "0" : Convert.ToDecimal(LstSol.Compute("Sum(ImportePagado)", "Moneda = 'Dolares'")).ToString("C2");
                if (!string.IsNullOrEmpty(cxpPesos))
                {
                    double Total = Convert.ToDouble(cxpPesos);
                    double Subtotal = (Total / 1.16);
                    lbcxpTotal.Text = Total.ToString("C2");
                    lbcxpIva.Text = (Total - Subtotal).ToString("C2");
                    lbcxpSubtotal.Text = Subtotal.ToString("C2");

                    //rptPagos.DataSource = LstSol;
                    //rptPagos.DataBind();
                    LlenarControles.LLenarRepeaterDataTable(ref rptPagos, LstSol);
                }
            }
            else 
            {
                rptPagos.DataSource = null;
                rptPagos.DataBind();
            }

        }

        private void Pintagrafica() {
            double cxc = Convert.ToDouble(lbcxcTotal.Text.Replace(",", "").Replace("$", ""));
            double cxp = Convert.ToDouble(lbcxpTotal.Text.Replace(",", "").Replace ("$",""));
            chtTotales.Series["sreCXC"].Points.Clear();
            chtTotales.Series["sreCXC"].Points.AddY(cxc);

            chtTotales.Series["sreCXP"].Points.Clear();
            chtTotales.Series["sreCXP"].Points.AddY(cxp);
        }

        private void CalculaDiferenciaIVA() {
            Double cxpIva = 0;
            Double cxcIva = 0;
            if (!string.IsNullOrEmpty(lbcxpIva.Text)) { cxpIva = Convert.ToDouble(lbcxpIva.Text.Replace(",", "").Replace ("$","")); }
            if (!string.IsNullOrEmpty(lbcxcIva.Text)) { cxcIva = Convert.ToDouble(lbcxcIva.Text.Replace(",", "").Replace ("$","")); }

            Double DifIva = cxpIva - cxcIva;
            if (DifIva < 0) { DifIva = DifIva * -1;}
            lbDifIva.Text = DifIva.ToString("C2");

            }

        private void Limpiar() {
            mvContenedor.ActiveViewIndex = 0;
            lbcxcDll.Text = "0";
            lbcxpDll.Text ="0";
            lbcxcSubtotal.Text ="0";
            lbcxpSubtotal.Text ="0";
            lbcxcIva.Text ="0";
            lbcxpIva.Text ="0";
            lbcxcTotal.Text ="0";
            lbcxpTotal.Text ="0";
            lbDifIva.Text ="0";
        }

                
    }
}