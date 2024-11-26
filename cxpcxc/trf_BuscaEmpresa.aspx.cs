using cxpcxc.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cxpcxc
{
    public partial class trf_BuscaEmpresa : Utilerias.Comun
    {
        protected void Page_Init(object sender, EventArgs e) 
        { 
            if (Session["credencial"] == null) 
                Response.Redirect("Default.aspx"); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Automatización JLVR
            cpplib.credencial Crd = (cpplib.credencial)Session["credencial"];            
            String Seccion = Convert.ToString(Request.Params["Sc"]);
            if (Seccion.Equals("Pvd")) 
                Response.Redirect("admProveedor.aspx?Id=" + Crd.IdEmpresaTrabajo);
            else if (Seccion.Equals("Usr")) 
                Response.Redirect("admUsuarios.aspx?Id=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("CodPag")) 
                Response.Redirect("admCatCodicionPago.aspx?Id=" + Crd.IdEmpresaTrabajo);
            else if (Seccion.Equals("Proy")) 
                Response.Redirect("admCatProyectos.aspx?Id=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("Rchz"))  
                Response.Redirect("admCatRechazos.aspx?Id=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("UdNg"))  
                Response.Redirect("admCatUnidadNegocio.aspx?Id=" + Crd.IdEmpresaTrabajo);

            //Posible eliminación
            else if (Seccion.Equals("Dir"))  
                Response.Redirect("trf_SolicitudesDireccion.aspx?Id=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("SolFd")) 
                Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("ConFd")) 
                Response.Redirect("trf_AutorizaFondosConsulta.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("rpPd"))  
                Response.Redirect("trf_Rep_pendientes.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("segLte"))  
                Response.Redirect("trf_SeguimientoLote.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 

            else if (Seccion.Equals("fmPagos"))  
                Response.Redirect("admCatFormasPago.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("cltes"))  
                Response.Redirect("admCatClientes.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("Srvc"))  
                Response.Redirect("admCatServicios.aspx?IdEmp=" + Crd.IdEmpresaTrabajo); 
            else if (Seccion.Equals("CabProv")) 
                Response.Redirect("trf_CambioProveedor.aspx?IdEmp=" + Crd.IdEmpresaTrabajo);


            //if (!IsPostBack){this.llenaEmpresas();} //Comentado, ya no es necesario elegir, que se vaya directo
        }

        protected void BtnCerrar_Click(object sender, EventArgs e) 
        {
            Response.Redirect("espera.aspx"); 
        }
        private void llenaEmpresas()
        {
            LlenarControles.LlenarDropDownList(ref dpEmpresa, comun.admcatempresa.ListaEmpresas(), "Nombre", "Id");
            //List<cpplib.Empresa> Lista = (new cpplib.admCatEmpresa()).ListaEmpresas();
            //dpEmpresa.DataSource = Lista;
            //dpEmpresa.DataTextField = "Nombre";
            //dpEmpresa.DataValueField = "Id";
            //dpEmpresa.DataBind();
            //dpEmpresa.Items.Insert(0, (new ListItem("Seleccionar", "0")));
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Request.Params["Sc"] != null)
            {
                String Seccion= Convert.ToString(Request.Params["Sc"]);
                if (Seccion.Equals("Pvd")) { Response.Redirect("admProveedor.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("Usr")) { Response.Redirect("admUsuarios.aspx?Id=" + dpEmpresa.SelectedValue);}
                else if (Seccion.Equals("CodPag")) { Response.Redirect("admCatCodicionPago.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("Proy")) { Response.Redirect("admCatProyectos.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("Rchz")) { Response.Redirect("admCatRechazos.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("UdNg")) { Response.Redirect("admCatUnidadNegocio.aspx?Id=" + dpEmpresa.SelectedValue); }

                    //POSIBLE ELIMINACION
                else if (Seccion.Equals("Dir")) { Response.Redirect("trf_SolicitudesDireccion.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("SolFd")) { Response.Redirect("trf_SolicitudesAutorizacion.aspx?Id=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("ConFd")) { Response.Redirect("trf_AutorizaFondosConsulta.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("rpPd")) { Response.Redirect("trf_Rep_pendientes.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("segLte")) { Response.Redirect("trf_SeguimientoLote.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                
                
                else if (Seccion.Equals("fmPagos")) { Response.Redirect("admCatFormasPago.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("cltes")) { Response.Redirect("admCatClientes.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("Srvc")) { Response.Redirect("admCatServicios.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                else if (Seccion.Equals("CabProv")) { Response.Redirect("trf_CambioProveedor.aspx?IdEmp=" + dpEmpresa.SelectedValue); }
                                        
                else { Response.Redirect("espera.aspx"); }
            }
        }
    }
}