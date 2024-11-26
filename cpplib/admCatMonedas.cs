using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace cpplib
{
    public class admCatMonedas
    {
        public List<catMonedas> Seleccionar()
        {
            List<catMonedas> respuesta = new List<catMonedas>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT id,nombre FROM cat_Moneda");
            foreach (DataRow reg in datos.Rows)
            {
                catMonedas items = new catMonedas();
                items.Id = int.Parse(reg["id"].ToString());
                items.Nombre = reg["nombre"].ToString();
                respuesta.Add(items);
            }            
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public void DropDownList(ref DropDownList dropdownlist, string idempresa)
        { 
            //Por Definir
        }

        public string Seleccionar_Nombre(string abreviacion)
        {
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT nombre FROM cat_Moneda WHERE abreviacion='" + abreviacion + "'");
            BD.CierraBD();
            return datos.Rows[0][0].ToString();
        }
    }

    public class catMonedas
    { 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviacion { get; set; }
        public bool Activo { get; set; }
    }
}
