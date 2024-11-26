using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_Moneda
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.cat_Moneda> Seleccionar()
        {
            b.ExecuteCommandSP("cat_Moneda_Seleccionar");
            List<mod.cat_Moneda> resultado = new List<mod.cat_Moneda>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_Moneda item = new mod.cat_Moneda();
                item.Abreviacion = reader["abreviacion"].ToString();
                item.Nombre = reader["nombre"].ToString();
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected string SeleccionarPorAbreviacion(string abreviacion)
        {
            b.ExecuteCommandSP("cat_Moneda_Seleccionar_Nombre_PorAbreviacion");
            b.AddParameter("@abreviacion", abreviacion, SqlDbType.NVarChar, 50);
            return b.SelectString();
        }

        

    }
}
