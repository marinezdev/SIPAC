using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SipacCorreo
{
    public class admCatEmpresa
    {
       public Empresa carga(int pId)
        {
            Empresa respuesta = new Empresa();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Empresas WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private Empresa arma(DataRow pRegistro)
        {
            Empresa respuesta = new Empresa();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
           return respuesta;
        }
              
    }
    public class Empresa
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
