using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace cpplib
{
    public class admCatRechazos
    {
        private int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cat_RechazosCtrl(Fecha) VALUES(getdate())";
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(SqlCmd);
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as Id");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("Id")) { Id = Convert.ToInt32(Datos.Rows[0]["Id"]); }
                }
            }
            return Id;
        }
        
        public bool Agrega(catRechazos pDatos)
        {
            bool resultado = false;
            int Id = daSiguienteIdentificador();
            if (Id > 0)
            {
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_Rechazos(");
                SqlCmd.Append("Id");
                SqlCmd.Append(",IdEmpresa");
                SqlCmd.Append(",Titulo");
                SqlCmd.Append(",FechaRegistro");
                SqlCmd.Append(",Activo");
                SqlCmd.Append(")");

                SqlCmd.Append(" VALUES (");
                SqlCmd.Append(Id.ToString());
                SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
                SqlCmd.Append(",'" + pDatos.Titulo + "'" );
                SqlCmd.Append(",getdate()");
                SqlCmd.Append("," + pDatos.Activo);
                SqlCmd.Append(");");
                mbd.BD BD = new mbd.BD();
                resultado = BD.EjecutaCmd(SqlCmd.ToString());

                BD.CierraBD();
            }
            return resultado;
        }

        public catRechazos carga(int pId)
        {
            catRechazos respuesta = new catRechazos();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Rechazos WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public void modifica(catRechazos oRchz)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_Rechazos SET");
            SqlCmd.Append(" Titulo='" + oRchz.Titulo + "'");
            SqlCmd.Append(" WHERE Id=" + oRchz.Id);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }

        public List<catRechazos> ListaRechazos(String IdEmpresa)
        {
            List<catRechazos> respuesta = new List<catRechazos>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Rechazos where IdEmpresa=" + IdEmpresa);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private catRechazos arma(DataRow pRegistro)
        {
            catRechazos respuesta = new catRechazos();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Titulo")) respuesta.Titulo = Convert.ToString(pRegistro["Titulo"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);

            return respuesta;
        }

        public bool Existe(catRechazos oCat)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Rechazos Where IdEmpresa =" + oCat.IdEmpresa + " and Titulo= '" + oCat.Titulo + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public List<valorTexto> DaComboRechazos(String IdEmpresa)
        {
            List<valorTexto> resultado = new List<valorTexto>();
            resultado.Add(new valorTexto("0", "SELECCIONAR"));
            string SqlCmd = "SELECT Titulo FROM cat_Rechazos WHERE IdEmpresa=" + IdEmpresa + " And Activo=1 order by Titulo";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(armaValorTexto(registro)); } }
            BD.CierraBD();

            return resultado;
        }

        private valorTexto armaValorTexto(DataRow pRegistro)
        {
            valorTexto resultado = new valorTexto();
            if (!pRegistro.IsNull("Titulo")) { resultado.Valor = (string)pRegistro["Titulo"]; }
            if (!pRegistro.IsNull("Titulo")) { resultado.Texto = ((string)pRegistro["Titulo"]).ToUpper(); }
            return resultado;
        }
    }

    public class catRechazos
    {
        private int mId= 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private String mTitulo = String.Empty;
        public String Titulo { get { return mTitulo; } set { mTitulo = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
}
