using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SipacCorreo
{
    public class admPartidasFactura
    {

        public int daMaxNumeroPartida(int IdOrdenFactura)
        {
            int Id = 1;
            String SqlCmd = "Select Max(NoPartida) AS Id from cxc_PartidasFactura  where IdOrdenFactura=" + IdOrdenFactura.ToString();
            mbd.BD BD = new mbd.BD();
            DataTable Datos = BD.LeeDatos(SqlCmd);
            if (Datos.Rows.Count > 0)
            {
                if (!Datos.Rows[0].IsNull("Id")) { Id = (Convert.ToInt32(Datos.Rows[0]["Id"]) + 1); }
            }
            return Id;
        }

        public void  AgregaPartidas(int IdOrdenFactura,List<PartidasFactura> Lista ){
            int NumPartida=daMaxNumeroPartida(IdOrdenFactura);
            foreach (PartidasFactura oPrt in Lista) { 
                oPrt.IdOrdenFactura = IdOrdenFactura;
                oPrt.NoPartida = NumPartida;
                nueva(oPrt);
                NumPartida += 1;
            }
        }

        public void EliminaPartidas(int IdOrdenFactura)
        {
            mbd.BD BD = new mbd.BD();
            bool resultado = BD.EjecutaCmd(" Delete  cxc_PartidasFactura WHERE IdOrdenFactura=" + IdOrdenFactura.ToString());
            BD.CierraBD();
            }


        private bool nueva(PartidasFactura pDatos)
        {
            bool resultado = false;

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cxc_PartidasFactura(");
            SqlCmd.Append("IdOrdenFactura");
            SqlCmd.Append(",NoPartida");
            SqlCmd.Append(",Cantidad");
            SqlCmd.Append(",Codigo");
            SqlCmd.Append(",Descripcion");
            SqlCmd.Append(",Precio");
            
            SqlCmd.Append(")");

            SqlCmd.Append("VALUES (");
            SqlCmd.Append(pDatos.IdOrdenFactura.ToString());
            SqlCmd.Append("," + pDatos.NoPartida.ToString());
            SqlCmd.Append("," + pDatos.Cantidad.ToString());
            SqlCmd.Append(",'" + pDatos.Codigo + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append("," + pDatos.Precio);
            SqlCmd.Append(")");
            mbd.BD BD = new mbd.BD();
            resultado = BD.EjecutaCmd(SqlCmd.ToString());
            BD.CierraBD();

            return resultado;
        }

        public List<PartidasFactura> cargaPartidas(int pId)
        {
            List<PartidasFactura> respuesta = new List<PartidasFactura>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("SELECT * FROM cxc_PartidasFactura WHERE IdOrdenFactura=" + pId.ToString());
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private PartidasFactura arma(DataRow pRegistro)
        {
            PartidasFactura respuesta = new PartidasFactura();
            if (!pRegistro.IsNull("IdOrdenFactura")) respuesta.IdOrdenFactura = Convert.ToInt32(pRegistro["IdOrdenFactura"]);
            if (!pRegistro.IsNull("NoPartida")) respuesta.NoPartida = Convert.ToInt32(pRegistro["NoPartida"]);
            if (!pRegistro.IsNull("Cantidad")) respuesta.Cantidad = Convert.ToInt32(pRegistro["Cantidad"]);
            if (!pRegistro.IsNull("Codigo")) respuesta.Codigo = Convert.ToString(pRegistro["Codigo"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Precio")) respuesta.Precio = Convert.ToDecimal(pRegistro["Precio"]);
            return respuesta;
        }   
    }

    public class PartidasFactura
    {
        private int mIdOrdenFactura = 0;
        public int IdOrdenFactura { get { return mIdOrdenFactura; } set { mIdOrdenFactura = value; } }
        private int mNoPartida = 0;
        public int NoPartida { get { return mNoPartida; } set { mNoPartida = value; } }
        private int mCantidad = 0;
        public int Cantidad { get { return mCantidad; } set { mCantidad = value; } }
        private string mCodigo = String.Empty;
        public string Codigo { get { return mCodigo; } set { mCodigo = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        private decimal mPrecio = 0;
        public decimal Precio { get { return mPrecio; } set { mPrecio = value; } }
    }
}
