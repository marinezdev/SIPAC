using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using cxpcxc.Utilerias;

namespace cxpcxc
{
    public partial class cxc_ConsultaOrdServicio : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
                hdIdEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString();
                this.llenaCombos();
                this.ValidaConsultaPrevia();
            }
        }

        private void llenaCombos()
        {
            LlenarControles.LlenarDropDownList(ref dpCliente, comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value), "Nombre", "id");
            //List<cpplib.CatClientes> lstClte = comun.admcatclientes.ListaClientesXEmpresa(hdIdEmpresa.Value); //Anterior: ListaTodosClientes()
            //dpCliente.DataSource = lstClte;
            //dpCliente.DataTextField = "Nombre";
            //dpCliente.DataValueField = "Id";
            //dpCliente.DataBind();
            //dpCliente.Items.Insert(0, (new ListItem("Seleccionar", "0")));

            LlenarControles.LlenarDropDownList(ref dpServicio, comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value), "Texto", "Valor");
            //List<cpplib.valorTexto> lstServicios = comun.admcatservicios.DaComboServicios(hdIdEmpresa.Value);
            //dpServicio.DataSource = lstServicios;
            //dpServicio.DataTextField = "Texto";
            //dpServicio.DataValueField = "Valor";
            //dpServicio.DataBind();
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        private void ListaOrdenServicio()
        {
            string Consulta = Daconsulta();
            this.AgregaConsultaSesion(Consulta);
            this.EjecutaConsulta(Consulta);
        }

        private void EjecutaConsulta(string Consulta)
        {
            cpplib.admOrdenServicio admOrd = new cpplib.admOrdenServicio();
            List<cpplib.OrdenServicio> Lista = comun.admordenservicio.ConsultaOrdenesServicio(Consulta);
            if (Lista.Count > 0)
            {
                //rptOrdSrv.DataSource = Lista;
                //rptOrdSrv.DataBind();
            }
            else
            {
                ltMsg.Text = "No hay información";
                //rptOrdSrv.DataSource = Lista;
                //rptOrdSrv.DataBind();
            }
            LlenarControles.LlenarRepeater(ref rptOrdSrv, Lista);
        
        }

        private string Daconsulta()
        {
            //string resultado = string.Empty;
            //resultado = ArmaConsulta(resultado, (" IdEmpresa='" + hdIdEmpresa .Value + "'")); 
            //if (!dpCliente.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCliente='" + dpCliente.SelectedValue + "'")); }
            //if (!dpServicio.SelectedValue.Equals("0")) { resultado = ArmaConsulta(resultado, (" IdCatServicio='" + dpServicio.SelectedValue + "'")); }
            //if (chkActivos.Checked) { resultado = ArmaConsulta(resultado, (" Estado=" + cpplib .OrdenServicio .EstadoOrdSvc .Abierto.ToString ("d") )); }
            //return resultado;

            string resultado = string.Empty;
            resultado = " and  IdEmpresa='" + hdIdEmpresa.Value + "'";
            if (!dpCliente.SelectedValue.Equals("0")) { resultado += "  and IdCliente='" + dpCliente.SelectedValue + "'"; }
            if (!dpServicio.SelectedValue.Equals("0")) { resultado += " and IdCatServicio='" + dpServicio.SelectedValue + "'"; }
            if (chkActivos.Checked) { resultado += " and Estado=" + cpplib.OrdenServicio.EstadoOrdSvc.Abierto.ToString("d"); }
            return resultado;
        }

        //private string ArmaConsulta(string Cadena, string Dato)
        //{
        //    if (string.IsNullOrEmpty(Cadena)) { Cadena = " where " + Dato; }
        //    else { Cadena += " and " + Dato; }
        //    return Cadena;
        //}

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e){ this.ListaOrdenServicio();}

        protected void rptOrdSrv_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerDat") { Response.Redirect("cxc_VerOrdenServicio.aspx?ord=" + e.CommandArgument.ToString()); }
            if (e.CommandName == "Descarga")
            {
                Label lbFactura = (Label)e.Item.FindControl("lbFechafactura");
                DecargarContrato(Convert.ToInt32(e.CommandArgument .ToString ()));
            }
            
        }
        
        private void DecargarContrato(int IdServicio)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["Credencial"];
            String DirExp = Server.MapPath(@"Descargas\") + oCrd.IdUsr.ToString().PadLeft(4, '0') + "_Contrato" + @"\";
            String DirZip = Server.MapPath(@"Descargas\") + oCrd.IdUsr.ToString().PadLeft(4, '0') + "_Contrato" + ".zip";

            this.PreparaDirectorioExportacion(DirExp);

            String DirRaiz = Server.MapPath(@"cxc_doc\Contratos\");
            cpplib.ArchivoContrato oAr = comun.admarchivoscontrato.carga(IdServicio);

            if (File.Exists(DirRaiz + oAr.ArchivoDestino)) { File.Copy((DirRaiz + oAr.ArchivoDestino), (DirExp + oAr.ArchivoDestino), true); }

            if (File.Exists(DirZip)) { File.Delete(DirZip); }

            ZipFile.CreateFromDirectory(DirExp, DirZip);

            if (File.Exists(DirZip)) { this.Enviar(DirZip); }
        }

        private void PreparaDirectorioExportacion(String DirExp)
        {
            if (System.IO.Directory.Exists(DirExp)) { System.IO.Directory.Delete(DirExp, true); };
            System.IO.Directory.CreateDirectory(DirExp);
        }
        private void Enviar(String DirZip)
        {
            System.IO.FileInfo ArchivoDescarga = new System.IO.FileInfo(DirZip);
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/x-compressed";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ArchivoDescarga.Name);
            Response.AppendHeader("Content-Length", ArchivoDescarga.Length.ToString());
            Response.WriteFile(DirZip);
            Response.End();
        }

        protected void rptOrdSrv_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenServicio ordSrv = (cpplib.OrdenServicio)(e.Item.DataItem);
                if (ordSrv.Contrato== 1){
                    ImageButton imgCont = (ImageButton)e.Item.FindControl("imgbtnDescarga");
                    imgCont.Enabled = true;
                    imgCont.ImageUrl = "~/img/dwContrato.png";
                }
                if (ordSrv.Estado.Equals (cpplib .OrdenServicio .EstadoOrdSvc.Cerrado))
                {
                    Image imgEdo = (Image)e.Item.FindControl("imgbtnverDat");
                    imgEdo.ImageUrl = "~/img/Candado_C.png";
                }
            }
        }

        private void AgregaConsultaSesion(string Datos)
        {
            cpplib.csConsultas csConsulta = new cpplib.csConsultas();
            csConsulta.Pagina = "cxc_ConsultaOrdServicio";
            csConsulta.Datos = Datos;
            Session["csConsultas"] = csConsulta;
        }

        private void ValidaConsultaPrevia()
        {
            if (Session["csConsultas"] != null)
            {
                cpplib.csConsultas csConsulta = ((cpplib.csConsultas)Session["csConsultas"]);
                if (csConsulta.Pagina.Equals("cxc_ConsultaOrdServicio"))
                {
                    this.EjecutaConsulta(csConsulta.Datos);
                }
            }
            else { this.ListaOrdenServicio(); }
        }
        
    }
}