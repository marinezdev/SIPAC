using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admCatEmpresa
    {
        public int nueva(Empresa pDatos)
        {
            int Id = 0;
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_Empresas (FechaRegistro,Rfc,Nombre,Activo)");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append(",'" + pDatos.Rfc + "'");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.Activo );
            SqlCmd.Append(");");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            if (resultado)
            {
                DataTable Datos = BD.LeeDatos("Select @@Identity as Id");
                if (Datos.Rows.Count > 0)
                {
                    if (!Datos.Rows[0].IsNull("Id")) { Id = Convert.ToInt32(Datos.Rows[0]["Id"]); }
                }
            }
            BD.CierraBD();
            return Id;
        }

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
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc= Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);
            if (!pRegistro.IsNull("Logo")) respuesta.Logo = Convert.ToString(pRegistro["Logo"]);
           return respuesta;
        }

        public string ObtenerRFC(string idempresa)
        {
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT rfc FROM cat_Empresas WHERE id=" + idempresa);
            BD.CierraBD();
            return datos.Rows[0][0].ToString();
        }

        public List<Empresa> ListaEmpresas()
        {
            List<Empresa> respuesta = new List<Empresa>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Empresas");
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(arma(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool Existe(Empresa oEmp)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Empresas WHERE Rfc='" + oEmp.Rfc + "' and Nombre= '" + oEmp.Nombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void modifica(Empresa oEmp)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_Empresas SET");
            SqlCmd.Append(" Rfc='" + oEmp.Rfc + "'");
            SqlCmd.Append(" ,Nombre='" + oEmp.Nombre + "'");
            SqlCmd.Append(" WHERE Id=" + oEmp.Id);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }
    }
    public class Empresa
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
        private string mLogo = String.Empty;
        public string Logo { get { return mLogo; } set { mLogo = value; } }
    }
}
