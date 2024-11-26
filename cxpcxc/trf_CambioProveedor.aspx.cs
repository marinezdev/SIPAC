using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_CambioProveedor : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = Crd.IdEmpresaTrabajo.ToString(); // Anterior: Request.Params["IdEmp"].ToString();
                this.txF_Inicio.Attributes.Add("readonly", "true");
                this.txF_Fin.Attributes.Add("readonly", "true");
                txF_Inicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txF_Fin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.llenaCombos(Crd.IdEmpresaTrabajo.ToString());
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        { 
            Response.Redirect("espera.aspx"); 
        }

        private void llenaCombos(String IdEmpresa)
        {
            LlenarControles.LlenarDropDownList(ref dpProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Id");
            LlenarControles.LlenarDropDownList(ref dpCambioProveedor, comun.admcatproveedor.ListaTodosProveedores(IdEmpresa), "Nombre", "Id");

            //List<cpplib.CatProveedor> lstPvd = (new cpplib.admCatProveedor()).ListaTodosProveedores(IdEmpresa);
            //dpProveedor.DataSource = lstPvd;
            //dpProveedor.DataValueField = "Id";
            //dpProveedor.DataTextField = "Nombre";
            //dpProveedor.DataBind();
            //dpProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));

            //dpCambioProveedor.DataSource = lstPvd;
            //dpCambioProveedor.DataValueField = "Id";
            //dpCambioProveedor.DataTextField = "Nombre";
            //dpCambioProveedor.DataBind();
            //dpCambioProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e) 
        { 
            AplicaConsulta(); 
        }

        private void AplicaConsulta() {
            if ((dpProveedor.SelectedValue != "0") && (dpProveedor.SelectedValue != "0"))
            {
                //cpplib.admSolicitud admSol = new cpplib.admSolicitud();
                List<cpplib.Solicitud> Lista = comun.admsolicitud.ConsultaSolCambioProveedor(hdIdEmpresa.Value, dpProveedor.SelectedValue, txF_Inicio.Text, txF_Fin.Text);
                if (Lista.Count > 0)
                {
                    //rptSolicitud.DataSource = Lista;
                    //rptSolicitud.DataBind();
                    LlenarControles.LlenarRepeater(ref rptSolicitud, Lista);
                    lbNumSolicitudes.Text = "SOLICITUDES (" + Lista.Count.ToString() + ")";
                    pnSolicitud.Visible = true;
                }
                else
                {
                    rptSolicitud.DataSource = null; 
                    rptSolicitud.DataBind(); 
                    pnSolicitud.Visible = false;
                }
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            mtvContenedor.ActiveViewIndex =0 ;
        }
        
        protected void rptSolicitud_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                int idSolicitud = Convert.ToInt32( e.CommandArgument.ToString());
                llenaSolicitud(idSolicitud);
                mtvContenedor.ActiveViewIndex = 1;
            }
        }

        private void llenaSolicitud(int IdSol)
        {
            cpplib.admSolicitud admSol = new cpplib.admSolicitud();
            cpplib.Solicitud osol = admSol.carga(IdSol);
            lbIdSolicitud .Text = osol.IdSolicitud.ToString ();
            lbIdCatProveedor.Text = osol.IdProveedor.ToString(); 
            lbProveedor.Text = osol.Proveedor;
            lbRfc.Text = osol.Rfc;
            lbFactura.Text = osol.Factura;
            lbFhFactura.Text = osol.FechaFactura.ToString("dd/MM/yyyy");
            lbImporte.Text = osol.Importe.ToString("C2");
            lbConcepto.Text = osol.Concepto;
            if (osol.ConFactura == cpplib.Solicitud.enConFactura.SI) 
                MuestraArchivo(IdSol, osol.FechaFactura); 
            else 
                pnlDocumento.Visible = false;
        }

        private void MuestraArchivo(int IdSolicitud, DateTime FechaFactura )
        {
            cpplib.Archivo oFact = comun.admarchivos.cargaFactura(IdSolicitud);
            String Carpeta = comun.admdirectorio.DadirectorioArchivo(Convert.ToDateTime(FechaFactura));
            String Archivo = Carpeta + oFact.ArchivoDestino;
            if (!Archivo.Equals("undefined") && !string.IsNullOrEmpty(Archivo))
            {
                string dirOrigen = "\\cxp_doc\\" + Archivo;
                ltDocumento.Text = "<embed src='" + dirOrigen + "' width='100%' height='100%' alt='pdf' pluginspage='http://get.adobe.com/es/reader/' />";
            }
        }

        protected void dpCambioProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpCambioProveedor.SelectedValue != lbIdCatProveedor.Text) 
                ltMsg.Text = "";
            else 
                ltMsg.Text = "EL proveedor seleccionado NO es correcto";
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (dpCambioProveedor.SelectedValue != lbIdCatProveedor.Text)
            {
                int IdSolicitud = Convert.ToInt32(lbIdSolicitud.Text);
                int idproveedor = Convert.ToInt32(dpCambioProveedor.SelectedValue);
                if (Modificar(IdSolicitud, idproveedor))
                {
                    AplicaConsulta();
                    AgregaBitacoraEventos(IdSolicitud);
                    mtvContenedor.ActiveViewIndex = 0;
                }
            }
        }
        
        private bool  Modificar(int IdSolicitud, int idproveedor)
        {
            cpplib.Cuenta oCta = comun.admcuenta.DaPrimerCuenta(idproveedor);
            cpplib.CatProveedor oProv = comun.admcatproveedor.carga(idproveedor);
            bool resultado = comun.admsolicitud.ActulizaProveedorSolicitud(IdSolicitud, oProv, oCta);
            return resultado;
        }

        private void AgregaBitacoraEventos(int IdSolicitud){
            cpplib.credencial ocdr = (cpplib.credencial)Session["credencial"];
            cpplib.BitacoraEventos Btc = new cpplib.BitacoraEventos();
            Btc.IdSolicitud =IdSolicitud;
            Btc.IdUsr =ocdr.IdUsr ;
            Btc.Nombre =ocdr.Nombre ;
            Btc.Descripcion = "MODIFICACION DE PROVEEDOR";
            bool resultado = comun.admcxpbitacoraeventos.Registrar(Btc);
        }

    }
}