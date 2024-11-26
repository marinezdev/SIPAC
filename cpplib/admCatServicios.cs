using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace cpplib
{
    public class admCatServicios
    {
        
        public bool Agrega(catServicios pDatos)
        {
            bool resultado = false;
            
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_Servicios(");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",IdEmpresa");
            SqlCmd.Append(",Titulo");
            SqlCmd.Append(",Activo");

            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
            SqlCmd.Append(",'" + pDatos.Titulo + "'");
            SqlCmd.Append("," + pDatos.Activo.ToString () );
            SqlCmd.Append(");");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());

            BD.CierraBD();

            return resultado;
            
        }


        public List<catServicios> ListaServicios(String IdEmpresa)
        {
            List<catServicios> respuesta = new List<catServicios>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Servicios where IdEmpresa=" + IdEmpresa);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private catServicios arma(DataRow pRegistro)
        {
            catServicios respuesta = new catServicios();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Titulo")) respuesta.Titulo = Convert.ToString(pRegistro["Titulo"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);



            return respuesta;
        }

        public catServicios carga(int pId)
        {
            catServicios respuesta = new catServicios();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Servicios WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Existe(catServicios oCat)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Servicios Where IdEmpresa =" + oCat.IdEmpresa + " and Titulo= '" + oCat.Titulo + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void modifica(catServicios oCodPg)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_Servicios SET");
            SqlCmd.Append(" Titulo='" + oCodPg.Titulo + "'");
            SqlCmd.Append(" WHERE Id=" + oCodPg.Id);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }

        public List<valorTexto> DaComboServicios(String IdEmpresa)
        {
            List<valorTexto> resultado = new List<valorTexto>();
            resultado.Add(new valorTexto("0", "SELECCIONAR"));
            string SqlCmd = "SELECT Id, Titulo AS Nombre FROM cat_Servicios WHERE IdEmpresa=" + IdEmpresa + " And Activo=1 order by Titulo";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) 
            { 
                foreach (DataRow registro in datos.Rows) 
                { 
                    resultado.Add(armaValorTexto(registro)); 
                } 
            }
            BD.CierraBD();

            return resultado;
        }

        private valorTexto armaValorTexto(DataRow pRegistro)
        {
            valorTexto resultado = new valorTexto();
            if (!pRegistro.IsNull("Id")) { resultado.Valor = pRegistro["Id"].ToString (); }
            if (!pRegistro.IsNull("Nombre")) { resultado.Texto = ((string)pRegistro["Nombre"]).ToUpper(); }
            return resultado;
        }

        public string DaNombreImagen(int pId)
        {
            string NomImagen = string.Empty; 
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT imagen FROM cat_Servicios WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) {
                if (!datos.Rows[0].IsNull("imagen")) { NomImagen = datos.Rows[0]["imagen"].ToString(); }
            }
            datos.Dispose();
            BD.CierraBD();
            return NomImagen;
        }
    }

    public class catServicios
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private String mTitulo = String.Empty;
        public String Titulo { get { return mTitulo; } set { mTitulo = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
}
