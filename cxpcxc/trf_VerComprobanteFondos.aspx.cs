using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_VerComprobanteFondos : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["idfd"] != null)
                {
                    hdIdFondos.Value = Request.Params["idfd"].ToString();
                    MuestraPaginas();
                    PintaArchivo("1");
                }
            }
        }

        private void MuestraPaginas() 
        {
            List<cpplib.ArchivoFodos> Lista = comun.admfondos.DaLista(hdIdFondos.Value);
            if (Lista.Count > 1) 
            {
                //rpPaginas.DataSource = Lista;
                //rpPaginas.DataBind();
                LlenarControles.LlenarRepeater(ref rpPaginas, Lista);
                pnpaginas.Visible = true;
            }

            //phPagina.Controls.Clear();
            // foreach (cpplib.ArchivoFodos oAr in Lista)
            //{
                
            //    LinkButton lnk = new LinkButton();
            //    lnk.ID = "pg" + oAr.IdDocumento.ToString();
            //    lnk.Text = oAr.IdDocumento.ToString();
            //    lnk.CommandArgument = oAr.IdDocumento.ToString();
            //    lnk.Click += new System.EventHandler(lnk_Click);
            //    Literal lt = new Literal();
            //    lt.Text = "&nbsp;&nbsp;";
                
            //    phPagina.Controls.Add(lnk);
            //    phPagina.Controls.Add(lt);
            //}
                
        }

        private void PintaArchivo(string Pagina) 
        {
            hdIdFondos.Value = Request.Params["idfd"].ToString();
            string Archivo = comun.admfondos.cargaArchivo(hdIdFondos.Value,Pagina).ArchivoDestino;
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\Fondos\\" + Archivo;
                ltComprobante.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            if (Request.Params["bk"] != null)
            {
                string regreso = Request.Params["bk"] + ".aspx";
                //regreso = regreso + "?IdEmp=" + Request.Params["IdEmp"];
                Response.Redirect(regreso);
            }
            else
            {
                Response.Redirect("espera.aspx");
            }
        }

        
        protected void rpPaginas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Docto") 
                PintaArchivo(e.CommandArgument.ToString());
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            PintaArchivo(lnk.CommandArgument.ToString());

        }

    }
}