using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_VerComprobante: Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                if (Request.Params["Id"] != null){
                    int IdSolicitud = Convert.ToInt32 (Request.Params["Id"]);
                    int IdDoc = Convert.ToInt32(Request.Params["IdDoc"]);
                    cpplib.Archivo oArchivo = comun.admarchivos.cargaComprobante(IdSolicitud, IdDoc);
                    cpplib.Solicitud oSol = comun.admsolicitud.carga(Convert.ToInt32(IdSolicitud));
                    String Carpeta = comun.admdirectorio.DadirectorioArchivo(oSol.FechaFactura);
                    String Archivo = Carpeta + oArchivo.ArchivoDestino;
                    if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo)){
                        string dirOrigen = "\\cxp_doc\\" + Archivo;
                        ltComprobante.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
                    }
                }
            }    
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            if (Request.Params["bk"] != null)
            {
                string regreso = Request.Params["bk"] + ".aspx";
                regreso = regreso + "?id=" + Request.Params["Id"];
                Response.Redirect(regreso);
            }
            else
            {
                Response.Redirect("espera.aspx");
            }
        }
    }
}