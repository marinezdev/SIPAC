using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admCatProveedor
    {
        private int daSiguienteIdentificador()
        {
            int Id = 0;
            String SqlCmd = "INSERT INTO cat_ProveedorCtrl(Fecha) VALUES(getdate())";
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

        public int nuevo(CatProveedor pDatos)
        {
            int Id = daSiguienteIdentificador();
            if (Id > 0)
            {
                bool resultado = false;
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_Proveedor (");
                SqlCmd.Append("Id");
                SqlCmd.Append(",FechaRegistro");
                SqlCmd.Append(",IdEmpresa");
                SqlCmd.Append(",Nombre");
                SqlCmd.Append(",Rfc");
                SqlCmd.Append(",Direccion");
                SqlCmd.Append(",Ciudad");
                SqlCmd.Append(",Estado");
                SqlCmd.Append(",Cp");
                SqlCmd.Append(",Contacto");
                SqlCmd.Append(",Correo");
                SqlCmd.Append(",Telefono");
                SqlCmd.Append(",Extencion");
                SqlCmd.Append(",SinFactura");
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
                SqlCmd.Append(",'" + pDatos.Estado + "'");
                SqlCmd.Append(",'" + pDatos.Cp + "'");
                SqlCmd.Append(",'" + pDatos.Contacto + "'");
                SqlCmd.Append(",'" + pDatos.Correo + "'");
                SqlCmd.Append(",'" + pDatos.Telefono + "'");
                SqlCmd.Append(",'" + pDatos.Extencion + "'");
                SqlCmd.Append("," + pDatos.SinFactura.ToString("d"));
                SqlCmd.Append("," + pDatos.Activo );
                SqlCmd.Append(");");

                mbd.BD BD = new mbd.BD();
                resultado = BD.EjecutaCmd(SqlCmd.ToString());
                
                BD.CierraBD();
            }
           
            return Id;
        }

        public CatProveedor carga(int pId)
        {
            CatProveedor respuesta = new CatProveedor();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public CatProveedor DaProvedorXRfc(string IdEmpresa, String pRfc)
        {
            CatProveedor respuesta = new CatProveedor();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor WHERE IdEmpresa =" + IdEmpresa + " and  Rfc='" + pRfc + "'");
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<CatProveedor> ListaTodosProveedores(string IdEmpresa)
        {
            List<CatProveedor> respuesta = new List<CatProveedor>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor WHERE IdEmpresa =" + IdEmpresa + " order by Nombre");
            foreach ( DataRow reg in datos.Rows ){respuesta.Add (arma(reg));}
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<CatProveedor> ListaTodosProveedoresActivos(string IdEmpresa)
        {
            List<CatProveedor> respuesta = new List<CatProveedor>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor WHERE IdEmpresa =" + IdEmpresa + " and Activo=1 order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public List<CatProveedor> LstProveedoresConFactura(string IdEmpresa)
        {
            List<CatProveedor> respuesta = new List<CatProveedor>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor Where IdEmpresa =" + IdEmpresa + " and SinFactura=0 order by Nombre");
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(arma(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }


        public List<CatProveedor> LstProveedoresSinFactura(string IdEmpresa)
        {
            List<CatProveedor> respuesta = new List<CatProveedor>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor Where IdEmpresa =" + IdEmpresa + " and SinFactura=1 and Activo=1 order by Nombre");
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(arma(reg));
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private CatProveedor arma(DataRow pRegistro)
        {
            CatProveedor respuesta = new CatProveedor();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdEmpresa")) respuesta.IdEmpresa = Convert.ToInt32(pRegistro["IdEmpresa"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Direccion")) respuesta.Direccion = Convert.ToString(pRegistro["Direccion"]);
            if (!pRegistro.IsNull("Ciudad")) respuesta.Ciudad = Convert.ToString(pRegistro["Ciudad"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("Cp")) respuesta.Cp = Convert.ToString(pRegistro["Cp"]);
            if (!pRegistro.IsNull("Contacto")) respuesta.Contacto = Convert.ToString(pRegistro["Contacto"]);
            if (!pRegistro.IsNull("Correo")) respuesta.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Telefono")) respuesta.Telefono = Convert.ToString(pRegistro["Telefono"]);
            if (!pRegistro.IsNull("Extencion")) respuesta.Extencion = Convert.ToString(pRegistro["Extencion"]);
            if (!pRegistro.IsNull("SinFactura")) respuesta.SinFactura = (CatProveedor.enSinFactura)(pRegistro["SinFactura"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);

            return respuesta;
        }

        public bool Existe(string IdEmpresa,string pRfc)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cat_Proveedor Where IdEmpresa =" + IdEmpresa + " and Rfc= '" + pRfc + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.CierraBD();
            return resultado;
        }

        public void modifica(CatProveedor oPvd)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_Proveedor SET");
            SqlCmd.Append(" Nombre='" + oPvd.Nombre + "'");
            SqlCmd.Append(",Rfc='" + oPvd.Rfc+ "'");
            SqlCmd.Append(",Direccion='" + oPvd.Direccion + "'");
            SqlCmd.Append(",Ciudad='" + oPvd.Ciudad + "'");
            SqlCmd.Append(",Estado='" + oPvd.Estado + "'");
            SqlCmd.Append(",Cp='" + oPvd.Cp + "'");
            SqlCmd.Append(",Contacto='" + oPvd.Contacto+ "'");
            SqlCmd.Append(",Correo='" + oPvd.Correo + "'");
            SqlCmd.Append(",Telefono='" + oPvd.Telefono + "'");
            SqlCmd.Append(",Extencion='" + oPvd.Extencion + "'");
            SqlCmd.Append(",SinFactura=" + oPvd.SinFactura.ToString("d"));
            SqlCmd.Append(",Activo=" + oPvd.Activo.ToString());
            SqlCmd.Append(" WHERE Id=" + oPvd.Id);
            mbd.BD BD = new mbd.BD();
            BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();
        }
    }

    public class CatProveedor
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
        private string mContacto = String.Empty;
        public string Contacto { get { return mContacto; } set { mContacto = value; } }
        private string mCorreo = String.Empty;
        public string Correo { get { return mCorreo; } set { mCorreo = value; } }
        private string mTelefono = String.Empty;
        public string Telefono { get { return mTelefono; } set { mTelefono = value; } }
        private string mExtencion = String.Empty;
        public string Extencion { get { return mExtencion; } set { mExtencion = value; } }
        private enSinFactura mSinFactura = enSinFactura.NO;
        public enSinFactura SinFactura { get { return mSinFactura; } set { mSinFactura = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }

        public enum enSinFactura { NO = 0, SI = 1 }
    }
    
}
