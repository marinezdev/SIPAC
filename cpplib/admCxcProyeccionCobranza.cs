using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admCxcProyeccionCobranza
    {
        public DataTable DaTotalProyeccionXFecha(int IdEmpresa, DateTime FhInicio, DateTime FhTermino)
        {
            mbd.BD BD = new mbd.BD();
            
            string SqlCmd = "select TipoMoneda ,sum(Importe) as Total  from cxc_OrdenFactura ";
            SqlCmd += " where  IdEmpresa =" + IdEmpresa.ToString() + " and  (FechaCompromisoPago>='" + FhInicio.ToString("dd/MM/yyyy") + "' and FechaCompromisoPago<DATEADD (dd,1,'" + FhTermino.ToString("dd/MM/yyyy") + "'))";
            SqlCmd += " and Estado<" + OrdenFactura.EstadoOrdFac.Cancelado.ToString ("d") ;
            SqlCmd += " group by TipoMoneda ";
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public DataTable DaGrupoProyeccion(int IdEmpresa, DateTime FhInicio, DateTime FhTermino)
        {
            mbd.BD BD = new mbd.BD();

            string SqlCmd = "select proyecto,TipoMoneda ,sum(Importe) as Total  from cxc_OrdenFactura ";
            SqlCmd += " where  IdEmpresa =" + IdEmpresa.ToString() + " and  (FechaCompromisoPago>='" + FhInicio.ToString("dd/MM/yyyy") + "' and FechaCompromisoPago<DATEADD (dd,1,'" + FhTermino.ToString("dd/MM/yyyy") + "'))";
            SqlCmd += " and Estado<" + OrdenFactura.EstadoOrdFac.Cancelado.ToString("d");
            SqlCmd += " group by proyecto,TipoMoneda ";
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public DataTable DaFacturasProyeccion(int IdEmpresa, DateTime FhInicio, DateTime FhTermino)
        {
            mbd.BD BD = new mbd.BD();

            string SqlCmd = "select * from cxc_OrdenFactura ";
            SqlCmd += " where  IdEmpresa =" + IdEmpresa.ToString() + " and  (FechaCompromisoPago>='" + FhInicio.ToString("dd/MM/yyyy") + "' and FechaCompromisoPago<DATEADD (dd,1,'" + FhTermino.ToString("dd/MM/yyyy") + "'))";
            SqlCmd += " and Estado<" + OrdenFactura.EstadoOrdFac.Cancelado.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public bool PendientesActulizarFechaCompromiso(int IdEmpresa, DateTime FhInicio)
        {
            bool resultado = false;
            mbd.BD BD = new mbd.BD();
            string SqlCmd = "select * from cxc_OrdenFactura ";
            SqlCmd += " where  IdEmpresa =" + IdEmpresa.ToString() + " and  FechaCompromisoPago<'" + FhInicio.ToString("dd/MM/yyyy") + "'" ;
            SqlCmd += " and Estado<" + OrdenFactura.EstadoOrdFac.Pagado.ToString("d");
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) { resultado = true; }
            BD.CierraBD();
            return resultado;
        }

        public DataTable  DaCobranzaRealPorProyecto(int IdEmpresa, string  FhInicio, string FhTermino)
        {
            mbd.BD BD = new mbd.BD();
            string SqlCmd = "select Proyecto,(select sum(importe) where F.TipoMoneda='Pesos')as Pesos,(select sum(importe) where F.TipoMoneda='Dolares')as Dolares";
            SqlCmd += " from cxc_Bitacora B inner join cxc_OrdenFactura as F on f.IdOrdenFactura=b.IdOrdenFactura";
            SqlCmd += " Where F.IdEmpresa =" + IdEmpresa.ToString() + " and  F.Especial=0"  +  " and  B.Estado=" + OrdenFactura.EstadoOrdFac.Pagado.ToString("d");
            SqlCmd += " and (B.FechaRegistro>'"+ FhInicio + "' and B.FechaRegistro<DATEADD (dd,1,'" + FhTermino + "'))";
            SqlCmd += " group by F.Proyecto,F.TipoMoneda ";
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }

        public DataTable DaFacturasCobranzaReal(int IdEmpresa, string FhInicio, string FhTermino)
        {
            mbd.BD BD = new mbd.BD();
            string SqlCmd = "select B.IdOrdenFactura,B.FechaRegistro,F.Cliente,F.NumFactura,F.FechaFactura,F.Proyecto ,F.Importe,F.TipoMoneda ";
            SqlCmd += " from cxc_Bitacora B inner join cxc_OrdenFactura as F ";
            SqlCmd += " on F.IdOrdenFactura=B.IdOrdenFactura ";
            SqlCmd += " Where F.IdEmpresa =" + IdEmpresa.ToString() ;
            SqlCmd += " and  F.Especial=0";
            SqlCmd += " and  B.Estado=" + OrdenFactura.EstadoOrdFac.Pagado.ToString("d");
            SqlCmd += " and (B.FechaRegistro>'" + FhInicio + "' and B.FechaRegistro<DATEADD (dd,1,'" + FhTermino + "'))";
            SqlCmd += " order by F.Cliente,F.Proyecto";
            
            DataTable datos = BD.LeeDatos(SqlCmd.ToString());
            BD.CierraBD();
            return datos;
        }
        
        private cxcProyCobranza arma(DataRow pRegistro)
        {
            cxcProyCobranza respuesta = new cxcProyCobranza();
            if (!pRegistro.IsNull("FechaInicio")) respuesta.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("FechaFinal")) respuesta.FechaFinal = Convert.ToDateTime(pRegistro["FechaFinal"]);
            return respuesta;
        }   
    }
        
    public class cxcProyCobranza
    {
        private string mSemana = string.Empty;
        public string Semana { get { return mSemana; } set { mSemana = value; } }
        private DateTime mFechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return mFechaInicio; } set { mFechaInicio = value; } }
        private DateTime mFechaFinal = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFinal { get { return mFechaFinal; } set { mFechaFinal = value; } }
        private decimal mPesos = 0;
        public decimal Pesos { get { return mPesos; } set { mPesos = value; } }
        private decimal mDolares = 0;
        public decimal Dolares { get { return mDolares; } set { mDolares = value; } }
    }
}
