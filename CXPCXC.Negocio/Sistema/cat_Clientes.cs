using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.Sistema
{
    public class cat_Clientes : Datos.Tablas.cat_Clientes
    {
        public mod.cat_Clientes SeleccionarPorId(int id)
        {            
            return Seleccionar_PorId(id);
        }

        public void Seleccionar_DropDownList_PorEmpresa(ref DropDownList dropdownlist, string idempresa)
        {
            dropdownlist.DataSource = Seleccionar_PorIdEmpresa(idempresa);
            dropdownlist.DataTextField = "Nombre";
            dropdownlist.DataValueField = "Id";
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public mod.cat_Clientes Seleccionar_PorIdEmpresaRFC(int idempresa, string rfc)
        {
            return SeleccionarPorIdEmpresaRFC(idempresa, rfc);
        }

        public void Procesamiento(CXPCXC.Modelos.Modelos modelos)
        { 
            
        }
    }
}
