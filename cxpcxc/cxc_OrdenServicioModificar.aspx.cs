using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class cxc_OrdenServicioModificar : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                int IdOrden = Convert.ToInt32(Request.Params["ord"].ToString());
                this.txNuevaFhTermino.Attributes.Add("readonly", "true");
                this.llenadatos(IdOrden); 
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) { Response.Redirect("cxc_ConsultaOrdServicioModificar.aspx"); }


        private void llenadatos(int IdOrden)
        {
            cpplib.OrdenServicio orSrv = comun.admordenservicio.carga(IdOrden);
            lbOrdServicio.Text = orSrv.IdServicio.ToString();
            lbCliente.Text = orSrv.Cliente;
            lbEmpresa.Text = orSrv.Empresa;
            lbFhInicio.Text = orSrv.FechaInicio.ToString("dd/MM/yyyy");
            lbFhFin.Text = orSrv.FechaTermino.ToString("dd/MM/yyyy");
            ce_NuevaFhTermino.StartDate = orSrv.FechaTermino;
            txNuevaFhTermino.Text = orSrv.FechaTermino.ToString("dd/MM/yyyy");
            lbPeriodos.Text = orSrv.Periodos.ToString();
            lbTpSolicitud.Text = orSrv.TipoSolicitud.ToString();
            lbCodPago.Text = orSrv.CondicionPago;
            txTotal.Text = orSrv.Importe.ToString();
            lbMoneda.Text = orSrv.TipoMoneda;
            lbDescripcion.Text = orSrv.Descripcion;
            txDescServicio.Text = orSrv.Descripcion;
            chkEspecial.Checked = Convert.ToBoolean(orSrv.Especial);

            if (orSrv.TipoPeriodo == 1) rbMes.Checked =true;
            if (orSrv.TipoPeriodo == 2) rdBimestral.Checked = true;
            if (orSrv.TipoPeriodo == 6) rdSemestral.Checked = true;
            if (orSrv.TipoPeriodo == 12) rdAnual.Checked = true;

            this.MuestraListaOrdenes(orSrv.IdServicio);
            
        }

        private void MuestraListaOrdenes(int Idservicio) {
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.ConsultaFacturasXIdServicio(Idservicio);
            if (Lista.Count > 0)
            {
                //rptOrdFact.DataSource = Lista;
                //rptOrdFact.DataBind();
                LlenarControles.LlenarRepeater(ref rptOrdFact, Lista);
            }
        }
                
        protected void rptOrdFact_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.OrdenFactura ordFac = (cpplib.OrdenFactura)(e.Item.DataItem);
                //Coloca el semaforo
                Image img = (Image)(e.Item.FindControl("imgVencimiento"));
                CheckBox chk = (CheckBox)(e.Item.FindControl("chkMarcar"));

                if (ordFac.Estado > cpplib.OrdenFactura.EstadoOrdFac.Solicitud) { img.ImageUrl = "~/img/Sem_R.png"; chk.Visible = false; }
                else if ((ordFac.Estado == cpplib.OrdenFactura.EstadoOrdFac.Cancelado)) { img.ImageUrl = "~/img/Sem_V.png"; }
            }
        }

        protected void btnModMonto_Click(object sender, EventArgs e)
        {
            string total = txTotal.Text.Replace("$", "").Replace(",", "");
            string Descripcion = txDescServicio.Text.Trim();
            if (Descripcion.Length > 128) { Descripcion = Descripcion.Substring(0, 127); }
            
            if ((total != "") && (Descripcion.Length > 0))
            {
                //cpplib.admOrdenServicio admOrdSvc = new cpplib.admOrdenServicio();
                ActualizafacturasOrden(lbOrdServicio.Text, total, Descripcion);
                bool resultado = comun.admordenservicio.ActualizaMontoOrden(lbOrdServicio.Text, total, Descripcion);
                
                if (fulContrato.HasFile) { RegistraContrato(); }

                Response.Redirect("cxc_ConsultaOrdServicioModificar.aspx");
            }
            else {enviaMsgCliente("Datos incompletos en Monto y (o) la descripcion"); }
        }

        private void ActualizafacturasOrden(string IdServicio,string Total,string Descripcion)
        { 
            //cpplib.admOrdenFactura adm = new cpplib.admOrdenFactura ();
            List<cpplib.OrdenFactura> Lista = comun.admordenfactura.DaRegistrosDeOrdenXFacturar(IdServicio);
            string GrupoID = string.Empty; 
            foreach (cpplib.OrdenFactura ord in Lista){
                if (string.IsNullOrEmpty(GrupoID)) { GrupoID = ord.IdOrdenFactura.ToString(); } else { GrupoID += "," + ord.IdOrdenFactura.ToString(); }
            }
            bool resultado = comun.admordenfactura.ActulizaMontoDescGrupoFacturas(GrupoID, Total,Descripcion ); 
        }
                
        protected void btnCancelaElimina_Click(object sender, EventArgs e)
        {
            cpplib.admOrdenFactura admOrdFc = new cpplib.admOrdenFactura();
            int IdServicio = Convert.ToInt32(lbOrdServicio.Text);

            foreach (RepeaterItem Reg in rptOrdFact.Items)
            {
                if (((CheckBox)(Reg.FindControl("chkMarcar"))).Checked)
                {
                    int IdOrdFact = Convert.ToInt32(((Label)(Reg.FindControl("lbOrderFac"))).Text);
                    if (rdEliminar.Checked)
                    {
                        admOrdFc.Eliminar(IdOrdFact);
                        bool resultado = comun.admcxcbitacora.Eliminar(IdOrdFact);
                        resultado = comun.admarchivoscxc.Eliminar(IdOrdFact); 
                    }

                    if (rdCancelar.Checked)
                    {
                        admOrdFc.CambiaEstadoOrdenFactura(IdOrdFact.ToString(), cpplib.OrdenFactura.EstadoOrdFac.Cancelado);
                        RegistraBitacora(Convert.ToInt32(lbOrdServicio.Text), IdOrdFact, cpplib.OrdenFactura.EstadoOrdFac.Cancelado); 
                    }
                }
            }
            
            if (rdEliminar.Checked) 
                comun.admordenservicio.ActualizaDatosOrden(IdServicio);

            this.llenadatos(IdServicio); 
        }
                
        private void RegistraBitacora(int IdServicio, int IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac Estado)
        {
            cpplib.credencial oCrd = (cpplib.credencial)Session["credencial"];
            cpplib.cxcBitacora oBitacora = new cpplib.cxcBitacora();
            oBitacora.IdServicio = IdServicio;
            oBitacora.IdOrdenFactura = IdOrdenFactura;
            oBitacora.IdUsr = oCrd.IdUsr;
            oBitacora.Nombre = oCrd.Nombre;
            oBitacora.Estado = Estado;

            bool Resultado = comun.admcxcbitacora.Registrar(oBitacora);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DateTime fechaActual = Convert.ToDateTime(lbFhFin.Text);
            DateTime FechaTErmino = Convert.ToDateTime(txNuevaFhTermino.Text);
            int TipoPeriodo = DaTipoPeriodo();

            int NoPartidas = Math.Abs((fechaActual.Month - FechaTErmino.Month )+ 12 * (fechaActual.Year - FechaTErmino.Year));
            NoPartidas = (NoPartidas / TipoPeriodo);

            if(NoPartidas>0){
                int PeriodosActuales = Convert.ToInt32(lbPeriodos.Text);
                int PeridosTotales = PeriodosActuales + NoPartidas;
                    
                DateTime FhInicio = Convert.ToDateTime(lbFhInicio.Text);

                if (RegistraOrdenFacturas(Convert.ToInt32(lbOrdServicio.Text), FhInicio, TipoPeriodo, PeriodosActuales, PeridosTotales))
                {
                    DateTime FechaTermino = CalculaFechaDeOrdenFactura(PeridosTotales, TipoPeriodo, FhInicio);
                    //cpplib.admOrdenServicio admOrdSvc = new cpplib.admOrdenServicio();
                    comun.admordenservicio.ActualizaDatosOrden(lbOrdServicio.Text, FechaTermino, PeridosTotales);
                    Response.Redirect("cxc_ConsultaOrdServicioModificar.aspx");
                }
            }
            else {enviaMsgCliente("No es posible agregar mas partidas, la fecha esta dentro del mismo rango!"); }
        }

        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }

        private bool RegistraOrdenFacturas(int idServicio, DateTime FhInicio, int TipoPeriodo, int PeriodosActuales, int PeridosTotales)
        {
            bool Resultado = true;
            DateTime Fecha = DateTime.Now;

            //cpplib.admOrdenFactura admOrdFac = new cpplib.admOrdenFactura();
            //cpplib.admPartidasFactura admPartidas = new cpplib.admPartidasFactura();

            cpplib.OrdenFactura oOrdFact = comun.admordenfactura.DaUltimaFacturaOrdenServicio(idServicio);
            List<cpplib.PartidasFactura> lstPartidas = comun.admpartidasfactura.cargaPartidas(oOrdFact.IdOrdenFactura);
            
            try
            {
                while (PeriodosActuales < PeridosTotales)
                {
                    PeriodosActuales += 1;
                    Fecha = CalculaFechaDeOrdenFactura(PeriodosActuales, TipoPeriodo, FhInicio);
                    oOrdFact.IdOrdenFactura = comun.admordenfactura.daSiguienteIdentificador();
                    oOrdFact.FechaInicio = Fecha;
                    oOrdFact.FechaFactura = Fecha;
                    oOrdFact.Estado = cpplib.OrdenFactura.EstadoOrdFac.Solicitud;
                    oOrdFact.Factura = 0;
                    oOrdFact.FhCompromisoPago = Fecha.AddDays(oOrdFact.CondicionPagoDias).ToString("dd/MM/yyyy");
                    comun.admordenfactura.nueva(oOrdFact);
                    comun.admpartidasfactura.AgregaPartidas(oOrdFact.IdOrdenFactura, lstPartidas);
                    RegistraBitacora(oOrdFact.IdServicio, oOrdFact.IdOrdenFactura, cpplib.OrdenFactura.EstadoOrdFac.Solicitud);
                }
            }
            catch (Exception) { Resultado = false; }

            return Resultado;
        }

        private DateTime CalculaFechaDeOrdenFactura(int NumPeriodo, int TipoPeriodo, DateTime FechaInicio)
        {
            DateTime Resultado = DateTime.Now;
            if (NumPeriodo == 1) { Resultado = FechaInicio; }
            else { Resultado = FechaInicio.AddMonths((TipoPeriodo * (NumPeriodo - 1))); }
            return Resultado;
        }
        
        private int DaTipoPeriodo()
        {
            int resultado = 0;
            if (rbMes.Checked) { resultado = 1; }
            if (rdBimestral.Checked) { resultado = 2; }
            if (rdSemestral.Checked) { resultado = 6; }
            if (rdAnual.Checked) { resultado = 12; }
            return resultado;
        }

        private void RegistraContrato()
        {
            cpplib.ArchivoContrato oContrato = comun.admarchivoscontrato.carga(Convert.ToInt32(lbOrdServicio.Text));
            string Archivo = lbOrdServicio.Text.PadLeft(6, '0') + "_C" + System.IO.Path.GetExtension(fulContrato.FileName);
            String Destino = Server.MapPath(@"cxc_doc\Contratos\") + Archivo;
            if (Cargacontrato(Destino))
            {
                cpplib.admArchivosContrato admCont = new cpplib.admArchivosContrato();
                oContrato.IdServicio = Convert.ToInt32(lbOrdServicio.Text);
                oContrato.ArchivoDestino = Archivo;
                if (!oContrato.Exite)
                {
                    cpplib.admOrdenServicio admsrv = new cpplib.admOrdenServicio();
                    admCont.Agrega(oContrato);
                    admsrv.ActualizaEstadoContrato(lbOrdServicio.Text, "1");
                }
                else { admCont.Actualiza(oContrato); }
            }
        }

        private bool Cargacontrato(string Destino)
        {
            bool resultado = false;
            if (System.IO.File.Exists(Destino)) { System.IO.File.Delete(Destino); }
            fulContrato.SaveAs(Destino);
            if (System.IO.File.Exists(Destino)) { resultado = true; }
            return resultado;
        }
                
    }
}