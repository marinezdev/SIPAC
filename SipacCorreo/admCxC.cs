using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipacCorreo
{
    public class admCxC
    {
        public void procesaPartidasDia()
        {
            int idUsr = 0;
            admOrdenFactura adm = new admOrdenFactura();
            List<OrdenFactura> lstFac = adm.DaPendientesDia();
            if (lstFac.Count > 0) { idUsr = lstFac[0].IdUsr; }
            
            List<Pendiente> lstPendientes = new List<Pendiente>();
            foreach (OrdenFactura oFac in lstFac)
            {
                if (oFac.IdUsr != idUsr)
                {
                    general admgral = new general();
                    admgral.EnviaCorreoSolDatosparaFacturar(idUsr, lstPendientes);
                    idUsr = oFac.IdUsr;
                    lstPendientes.Clear();
                    lstPendientes.Add(AgregaPendiente(oFac));
                }

                if ((oFac.TipoSolicitud.Equals (OrdenServicio.enTipoSolicitud.Fijo)) && (oFac.Especial.Equals(0)))
                {
                    adm.CambiaEstadoOrdenFactura(oFac.IdOrdenFactura.ToString (), OrdenFactura.EstadoOrdFac.Emisio_Factura);
                }
                else if ((oFac.TipoSolicitud.Equals (OrdenServicio.enTipoSolicitud.Fijo)) && (oFac.Especial.Equals(1))){
                    adm.CambiaEstadoOrdenFactura(oFac.IdOrdenFactura.ToString (), OrdenFactura.EstadoOrdFac.En_Cobro);
                }
                else if (oFac.TipoSolicitud.Equals (OrdenServicio.enTipoSolicitud.Variable) ){
                    lstPendientes.Add(AgregaPendiente(oFac));
                }
            }
            if (lstPendientes.Count > 0) {
                general admgral = new general();
                admgral.EnviaCorreoSolDatosparaFacturar(idUsr, lstPendientes);
            }
        }

        public void  ValidaPendientedeFacturar()
        {
            List<Pendiente> lstPendientes = new List<Pendiente>();
            List<OrdenFactura> lstFac = (new admOrdenFactura()).DaListaporFacturar();
            foreach (OrdenFactura oFac in lstFac)
            {
                lstPendientes.Add(AgregaPendiente(oFac));
            }
            if (lstPendientes.Count > 0)
            {
                general admgral = new general();
                admgral.EnviaCorreoAFacturacion(lstPendientes);
            }
        }

        public void ValidaPendientesCobro()
        {
            int IdEmpresa = 0;
            admOrdenFactura adm = new admOrdenFactura();
            List<OrdenFactura> lstFac = adm.DaListaporCobrar();
            if (lstFac.Count > 0) {IdEmpresa = lstFac[0].IdEmpresa ; }

            List<Pendiente> lstPendientes = new List<Pendiente>();
            foreach (OrdenFactura oFac in lstFac)
            {
                if (oFac.IdEmpresa != IdEmpresa)
                {
                    general admgral = new general();
                    admgral.EnviaCorreoSolDatosparaFacturar(IdEmpresa, lstPendientes);
                    IdEmpresa = oFac.IdEmpresa;
                    lstPendientes.Clear();
                    lstPendientes.Add(AgregaPendiente(oFac));
                }
                else { lstPendientes.Add(AgregaPendiente(oFac)); }
            }

            if (lstPendientes.Count > 0)
            {
                general admgral = new general();
                admgral.EnviaCorreoACobranza(IdEmpresa, lstPendientes);
            }
        }

        private Pendiente AgregaPendiente(OrdenFactura factura)
        {
            Pendiente obj = new Pendiente();
            obj.IdOrden = factura.IdOrdenFactura;
            obj.FechaInicio = factura.FechaInicio;
            obj.Empresa = factura.Empresa;
            obj.Cliente = factura.Cliente;
            obj.Servicio = factura.Servicio ;
            obj.Descripcion = factura.Descripcion;
            return obj;
        }
    }

    public class Pendiente
    {
        private int mIdOrden = 0;
        public int IdOrden { get { return mIdOrden; } set { mIdOrden = value; } }
        private DateTime mFechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return mFechaInicio; } set { mFechaInicio = value; } }
        private string mEmpresa = string.Empty;
        public string Empresa { get { return mEmpresa; } set { mEmpresa = value; } }
        private string mCliente = string.Empty;
        public string Cliente { get { return mCliente; } set { mCliente = value; } }
        private string mServicio = string.Empty;
        public string Servicio { get { return mServicio; } set { mServicio = value; } }
        private string mDescripcion = string.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
    }

}
