using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Negocio.Sistema
{
    public class cat_Moneda : Datos.Tablas.cat_Moneda
    {

        public string Seleccionar_NombrePorAbreviacion(string abreviacion)
        {
            return SeleccionarPorAbreviacion(abreviacion);
        }

        public void Seleccionar_DropDownList(ref DropDownList dropdownlist)
        {
            dropdownlist.DataSource = Seleccionar();
            dropdownlist.DataTextField = "Nombre";
            dropdownlist.DataValueField = "Abreviacion";
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
    }
}
