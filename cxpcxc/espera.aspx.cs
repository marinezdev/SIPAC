using System;
using System.Data;
using System.Web;
using System.Web.Services;

namespace cxpcxc
{
    public partial class espera : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) 
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];
            if (
                Crd.Grupo.Equals(cpplib.credencial.usrGrupo.Direccion) || 
                Crd.Grupo.Equals(cpplib.credencial.usrGrupo.Presidencia) ||
                Crd.Grupo.Equals(cpplib.credencial.usrGrupo.systemall)
                ) 
            { 
                CargaInformacionDireccion(Crd); 
            } 
        }


        private void CargaInformacionDireccion(cpplib.credencial Crd )
        {
            pnDireccion.Visible = true;
            DataTable Dts = comun.admsolicitud.ConsultaPendientesXdiasretraso(Crd.IdEmpresaTrabajo);

            lbTotalSolPd.Text = "TOTAL :  " + Dts.Compute("Sum(TOTAL)", "").ToString() == "" ? "0" : "TOTAL :  " + Dts.Compute("Sum(TOTAL)", "").ToString();
            
            chtSolPendXDias.DataSource = Dts;
            chtSolPendXDias.DataBind();
            chtSolPendXDias.Series["sreDias"].XValueMember = "Solicitudes";
            chtSolPendXDias.Series["sreDias"].YValueMembers = "Total";
                
        }

        [WebMethod()]
        public static bool KeepActiveSession()
        {
            if (HttpContext.Current.Session["credencial"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}