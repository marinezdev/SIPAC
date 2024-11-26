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
    public partial class cxc_repCobranzaReal : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCd.IdEmpresaTrabajo.ToString();

                txFhInicio.Attributes.Add("ReadOnly", "true");
                txFhInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txFhTermino.Attributes.Add("ReadOnly", "true");
                txFhTermino.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e)
        {
            //cpplib.admCxcProyeccionCobranza adm = new cpplib.admCxcProyeccionCobranza();
            DataTable resultado = comun.admcxcproyeccioncobranza.DaCobranzaRealPorProyecto(Convert.ToInt32(hdIdEmpresa.Value), txFhInicio.Text, txFhTermino.Text);

            if (resultado.Rows.Count > 0)
            {
                chtCobranzaReal.DataSource = resultado;
                chtCobranzaReal.Series["srePesos"].XValueMember = "proyecto";
                chtCobranzaReal.Series["srePesos"].YValueMembers = "Pesos";
                chtCobranzaReal.Series["sreDolares"].XValueMember = "proyecto";
                chtCobranzaReal.Series["sreDolares"].YValueMembers = "Dolares";
                chtCobranzaReal.DataBind();

                lbTotPesos.Text = resultado.Compute("Sum(Pesos)", "").ToString() == "" ? "0" : Convert.ToDecimal(resultado.Compute("Sum(pesos)", "")).ToString("C2");
                lbTotDolares.Text = resultado.Compute("Sum(Dolares)", "").ToString() == "" ? "0" : Convert.ToDecimal(resultado.Compute("Sum(dolares)", "")).ToString("C2");
                pnFacturas.Visible = true;
            }
            else { pnFacturas.Visible = false; lbTotPesos.Text = "0"; lbTotDolares.Text = "0"; }

            DataTable lstFactura = comun.admcxcproyeccioncobranza.DaFacturasCobranzaReal(Convert.ToInt32(hdIdEmpresa.Value), txFhInicio.Text, txFhTermino.Text);
            if (lstFactura.Rows.Count > 0) 
            {
                //rpFactutras.DataSource = lstFactura;
                //rpFactutras.DataBind();
                LlenarControles.LLenarRepeaterDataTable(ref rpFactutras, lstFactura);
            }
        }

    }
}