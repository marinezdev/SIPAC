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
    public partial class cxc_ProyeccionCobranza : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cpplib .credencial  oCrd= (cpplib .credencial)Session["credencial"];
                hdIEmpresa.Value = oCrd.IdEmpresaTrabajo.ToString ();

                txFhInicio.Attributes.Add("ReadOnly", "true");
                txFhInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_imbtnInicio.StartDate =DateTime.Now;
                txFhTermino.Attributes.Add("ReadOnly", "true");
                txFhTermino.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ce_imbtnTermino.StartDate = DateTime.Now;

                
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("espera.aspx"); }

        protected void imbtnconsulta_Click(object sender, ImageClickEventArgs e)
        {
            pnDetalles.Visible = false;
            List<cpplib.cxcProyCobranza> resultado =Procesa(txFhInicio .Text ,txFhTermino.Text);
            if(resultado.Count  >0){
                rpProyeccion.DataSource =resultado ;
                rpProyeccion.DataBind();

                chtProyeccion.DataSource = resultado;
                chtProyeccion.Series["srePesos"].XValueMember = "Semana";
                chtProyeccion.Series["srePesos"].YValueMembers = "Pesos";
                chtProyeccion.Series["sreDolares"].XValueMember = "Semana";
                chtProyeccion.Series["sreDolares"].YValueMembers = "Dolares";
                chtProyeccion.DataBind();
            }else{
                rpProyeccion.DataSource =null;
                rpProyeccion.DataBind();
            }
        }

        private List<cpplib.cxcProyCobranza> Procesa( string FhInicio, string FhTermino) {
            List<cpplib.cxcProyCobranza> resultado = new List<cpplib.cxcProyCobranza>();

            int Contador = 1;
            DateTime Inicio = Convert.ToDateTime(FhInicio) ;
            DateTime Termino = Convert.ToDateTime(FhTermino) ;
            DateTime Fechatmp= DateTime.Now;
            //cpplib.admCxcProyeccionCobranza adm = new cpplib.admCxcProyeccionCobranza();
            while (Inicio <= Termino) { 
                Fechatmp=Inicio.AddDays(6);
                DataTable Datos = comun.admcxcproyeccioncobranza.DaTotalProyeccionXFecha (Convert.ToInt32(hdIEmpresa.Value),Inicio,Fechatmp );
                cpplib.cxcProyCobranza oPry= new cpplib.cxcProyCobranza();
                oPry.Semana ="Semana " + Contador .ToString ();
                oPry.FechaInicio =Inicio ;
                oPry.FechaFinal =Fechatmp;
                foreach (DataRow reg in Datos.Rows ){
                    if(reg["TipoMoneda"].Equals ("Pesos"))
                        oPry.Pesos += Convert.ToDecimal (reg["Total"]);
                    if (reg["TipoMoneda"].Equals("Dolares")) 
                        oPry.Dolares += Convert.ToDecimal(reg["Total"]);
                }
                resultado.Add(oPry);
                Inicio = Fechatmp.AddDays(1);
                Contador += 1;
            }

            //Valida pendientes Actulizar fecha COmpromiso deacuerdo a la fecha inicio

            if (comun.admcxcproyeccioncobranza.PendientesActulizarFechaCompromiso(Convert.ToInt32(hdIEmpresa.Value), Convert.ToDateTime(FhInicio))) 
                lkActzFechaComp.Text = "Hay Solicitudes pendientes que requieren actualizar la fecha compromiso de pago";

            return resultado;
        }

        protected void rpProyeccion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                cpplib.cxcProyCobranza oProy = (cpplib.cxcProyCobranza)(e.Item.DataItem);
                LinkButton lkDetalle = (LinkButton)e.Item.FindControl("lkDetalle");
                lkDetalle.CommandArgument = oProy.Semana + "|" + oProy.FechaInicio.ToString("dd/MM/yyyy") + "|" + oProy.FechaFinal.ToString("dd/MM/yyyy");
                if ((oProy.Dolares == 0) && (oProy.Pesos == 0)) { lkDetalle.Visible = false; }
            }
        }

        protected void rpProyeccion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("lkDetalle"))
            {
                string[] Datos=e.CommandArgument.ToString().Split('|');

                if (Datos.Count() > 0) {
                    lbTitSemana.Text = Datos[0];
                    DateTime Inicio = Convert.ToDateTime(Datos[1]);
                    DateTime Termino = Convert.ToDateTime(Datos[2]);

                    cpplib.admCxcProyeccionCobranza adm = new cpplib.admCxcProyeccionCobranza();
                    DataTable dtsProyecto = adm.DaGrupoProyeccion(Convert.ToInt32(hdIEmpresa.Value), Inicio, Termino);
                    DataTable dtsfacturas = adm.DaFacturasProyeccion(Convert.ToInt32(hdIEmpresa.Value), Inicio, Termino);

                    //rpProyecto.DataSource = dtsProyecto;
                    //rpProyecto.DataBind();
                    //rpfacturas.DataSource = dtsfacturas;
                    //rpfacturas.DataBind();

                    LlenarControles.LLenarRepeaterDataTable(ref rpProyecto, dtsProyecto);
                    LlenarControles.LLenarRepeaterDataTable(ref rpfacturas, dtsfacturas);

                    pnDetalles.Visible = true;
                }
            }
        }

        protected void lkActzFechaComp_Click(object sender, EventArgs e)
        {
            Response.Redirect("cxc_PendientesXCobrar.aspx");
        }
        
    }
}