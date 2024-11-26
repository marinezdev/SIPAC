using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace cxpcxc.Utilerias
{
    public static class LlenarControles
    {
        public static void LlenarDropDownList<T>(ref DropDownList dropdownlist, List<T> lista, string nombre, string valor)
        {
            dropdownlist.DataSource = lista;
            dropdownlist.DataTextField = nombre;
            dropdownlist.DataValueField = valor;
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public static void LlenarRepeater<T>(ref Repeater repeater, List<T> lista)
        {
            repeater.DataSource = lista;
            repeater.DataBind();
        }

        public static void LLenarRepeaterDataTable(ref Repeater repeater, DataTable datatable)
        {
            repeater.DataSource = datatable;
            repeater.DataBind();
        }


    }
}