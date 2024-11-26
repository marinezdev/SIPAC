using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
   public class admCatClientes
    {
        private int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cat_ClientesCtrl(Fecha) VALUES(getdate())";
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

        public int nuevo(CatClientes pDatos)
        {
            int Id = daSiguienteIdentificador();
            if (Id > 0)
            {
                bool resultado = false;
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_Clientes (");
                SqlCmd.Append("Id");
                SqlCmd.Append(",FechaRegistro");
                SqlCmd.Append(",IdEmpresa");
                SqlCmd.Append(",Nombre");
                SqlCmd.Append(",Rfc");
                SqlCmd.Append(",Direccion");
                SqlCmd.Append(",Ciudad");
                SqlCmd.Append(",Estado");
                SqlCmd.Append(",Cp");
                SqlCmd.Append(",ContactoProy");
                SqlCmd.Append(",ContactoFact");
                SqlCmd.Append(",Correo");
                SqlCmd.Append(",Telefono");
                SqlCmd.Append(",Extencion");
                SqlCmd.Append(",Activo");
                SqlCmd.Append(")");

                SqlCmd.Append(" VALUES (");
                SqlCmd.Append(Id.ToString());
                SqlCmd.Append(",getdate()");
                SqlCmd.Append("," + pDatos.IdEmpresa);
                SqlCmd.Append(",'" + pDatos.Nombre + "'");
                SqlCmd.Append(",'" + pDatos.Rfc + "'");
                SqlCmd.Append(",'" + pDatos.Direccion + "'");
                SqlCmd.Append(",'" + pDatos.Ciudad + "'");
                SqlCmd.Append(",'" + pDatos.Estado+ "'");
                SqlCmd.Append(",'" + pDatos.Cp + "'");
                SqlCmd.Append(",'" + pDatos.ContactoProyecto + "'");
                SqlCmd.Append(",'" + pDatos.ContactoFacturacion + "'");
                SqlCmd.Append(",'" + pDatos.Correo + "'");
                SqlCmd.Append(",'" + pDatos.Telefono + "'");
                SqlCmd.Append(",'" + pDatos.Extencion + "'");
                SqlCmd.Append("," + pDatos.Activo);
                SqlCmd.Append(");");
                mbd.BD BD = new mbd.BD();
                resultado = BD.EjecutaCmd(SqlCmd.ToString());

                BD.CierraBD();
            }
            return Id;
        }

        public CatClientes carga(int pId)
        {
            CatClientes respuesta = new CatClientes();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Clientes WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public CatClientes DaClienteXRfc(string IdEmpresa, String pRfc)
        {
            CatClientes respuesta = new CatClientes();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Clientes WHERE IdEmpresa =" + IdEmpresa + " and  Rfc='" + pRfc + "'");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<CatClientes> ListaClientesXEmpresa(string IdEmpresa)
        {
            List<CatClientes> respuesta = new List<CatClientes>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Clientes WHERE IdEmpresa =" + IdEmpresa + " order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<CatClientes> ListaTodosClientes()
        {
            List<CatClientes> respuesta = new List<CatClientes>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Clientes order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private CatClientes arma(DataRow pRegistro)
        {
            CatClientes respuesta = new CatClientes();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Direccion")) respuesta.Direccion = Convert.ToString(pRegistro["Direccion"]);
            if (!pRegistro.IsNull("Ciudad")) respuesta.Ciudad = Convert.ToString(pRegistro["Ciudad"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Cp")) respuesta.Cp = Convert.ToString(pRegistro["Cp"]);
            if (!pRegistro.IsNull("ContactoProy")) respuesta.ContactoProyecto = Convert.ToString(pRegistro["ContactoProy"]);
            if (!pRegistro.IsNull("ContactoFact")) respuesta.ContactoFacturacion = Convert.ToString(pRegistro["ContactoFact"]);
            if (!pRegistro.IsNull("Correo")) respuesta.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Telefono")) respuesta.Telefono = Convert.ToString(pRegistro["Telefono"]);
            if (!pRegistro.IsNull("Extencion")) respuesta.Extencion = Convert.ToString(pRegistro["Extencion"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);
            
            return respuesta;
        }

        public bool Existe(string pRfc)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Clientes Where Rfc= '" + pRfc + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void modifica(CatClientes oCte)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_Clientes SET");
            SqlCmd.Append(" Nombre='" + oCte.Nombre + "'");
            SqlCmd.Append(",Rfc='" + oCte.Rfc + "'");
            SqlCmd.Append(",Direccion='" + oCte.Direccion + "'");
            SqlCmd.Append(",Ciudad='" + oCte.Ciudad + "'");
            SqlCmd.Append(",Estado='" + oCte.Estado + "'");
            SqlCmd.Append(",Cp='" + oCte.Cp + "'");
            SqlCmd.Append(",ContactoProy='" + oCte.ContactoProyecto + "'");
            SqlCmd.Append(",ContactoFact='" + oCte.ContactoFacturacion + "'");
            SqlCmd.Append(",Correo='" + oCte.Correo + "'");
            SqlCmd.Append(",Telefono='" + oCte.Telefono + "'");
            SqlCmd.Append(",Extencion='" + oCte.Extencion + "'");
            SqlCmd.Append(" WHERE Id=" + oCte.Id);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }

        public string  DaCorreoCliente(int pId)
        {
            string respuesta = string.Empty;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT Correo FROM cat_Clientes WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { if (!datos.Rows[0].IsNull("Correo")) respuesta = Convert.ToString(datos.Rows[0]["Correo"]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }
    }

    public class CatClientes
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdEmpresa = 0;
        public int IdEmpresa { get { return mIdEmpresa; } set { mIdEmpresa = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mDireccion = String.Empty;
        public string Direccion { get { return mDireccion; } set { mDireccion = value; } }
        private string mCiudad = String.Empty;
        public string Ciudad { get { return mCiudad; } set { mCiudad = value; } }
        private string mEstado = String.Empty;
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        private string mCp = String.Empty;
        public string Cp { get { return mCp; } set { mCp = value; } }
        private string mContactoProyecto = String.Empty;
        public string ContactoProyecto { get { return mContactoProyecto; } set { mContactoProyecto = value; } }
        private string mContactoFacturacion = String.Empty;
        public string ContactoFacturacion { get { return mContactoFacturacion; } set { mContactoFacturacion = value; } }
        private string mCorreo = String.Empty;
        public string Correo { get { return mCorreo; } set { mCorreo = value; } }
        private string mTelefono = String.Empty;
        public string Telefono { get { return mTelefono; } set { mTelefono = value; } }
        private string mExtencion = String.Empty;
        public string Extencion { get { return mExtencion; } set { mExtencion = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
 }
