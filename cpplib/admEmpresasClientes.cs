using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

namespace cpplib
{
    public class admEmpresasClientes
    {
        public List<EmpresasClientes> Seleccionar(string idempresa)
        {
            List<EmpresasClientes> respuesta = new List<EmpresasClientes>();
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasClientes_Seleccionar_PorEmpresa " + idempresa);
            foreach (DataRow reg in datos.Rows) 
            { 
                respuesta.Add(Armar(reg)); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        public bool SeleccionarEstadoActual(string idempresa, string idcliente)
        {
            bool respuesta = false;
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos("exec EmpresasClientes_Seleccionar_EstadoActual " + idempresa + "," + idcliente);
            if (datos.Rows.Count > 0) 
            { 
                if (!datos.Rows[0].IsNull("activo")) 
                    respuesta = bool.Parse(datos.Rows[0]["activo"].ToString()); 
            }
            datos.Dispose();
            BD.CierraBD();
            return respuesta;
        }

        private EmpresasClientes Armar(DataRow pRegistro)
        {
            EmpresasClientes respuesta = new EmpresasClientes();
            if (!pRegistro.IsNull("IdEmpresa")) 
                respuesta.IdEmpresa = int.Parse(pRegistro["idempresa"].ToString());
            if (!pRegistro.IsNull("IdCliente")) 
                respuesta.IdCliente = int.Parse(pRegistro["idcliente"].ToString());
            if (!pRegistro.IsNull("Activo")) 
                respuesta.Activo = bool.Parse(pRegistro["activo"].ToString());
            if (!pRegistro.IsNull("empresa"))
                respuesta.NombreEmpresa = pRegistro["empresa"].ToString();
            if (!pRegistro.IsNull("cliente"))
                respuesta.NombreCliente = pRegistro["cliente"].ToString();
            return respuesta;
        }


        public bool Agregar(EmpresasClientes items)
        {
            bool resultado = false;
            string consulta = "exec EmpresasClientes_Agregar " + items.IdEmpresa + "," + items.IdCliente + "," + items.Activo;
            mbd.BD BD = new mbd.BD();
            try
            {
                 BD.EjecutaCmd(consulta);
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                BD.CierraBD();
            }
            return resultado;
        }
        
        public bool Modificar(EmpresasClientes items)
        {
            bool resultado = false;
            int valor = items.Activo == true ? 1 : 0;
            string consulta = "exec EmpresasClientes_Modificar " + items.IdEmpresa + "," + items.IdCliente + "," + valor;
            mbd.BD BD = new mbd.BD();
            try
            {
                BD.EjecutaCmd(consulta);
                resultado = true;
            }
            catch
            {
                resultado = false;
            }
            finally
            {
                BD.CierraBD();
            }
            return resultado;
        }
    }

    public class EmpresasClientes
    { 
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public bool Activo { get; set; }

        public string NombreEmpresa { get; set; }
        public string NombreCliente { get; set; }
    }
}
