using CXPCXC.Datos.Tablas;
using CXPCXC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPCXC.Negocio.Sistema
{
    /// <summary>
    /// Procedimientos de administración del sistema, catalogos, usuarios, validaciones de acceso
    /// </summary>
    public class Sistema
    {
        
        public cat_Clientes catclientes;
        public cat_Empresas catempresas;
        public cat_Moneda catmoneda;
        public cat_UnidadNegocio catunidadnegocio;
        public Usuario usuario;

        public Sistema()
        {
            catclientes = new cat_Clientes();
            catempresas = new cat_Empresas();
            catmoneda = new cat_Moneda();
            catunidadnegocio = new cat_UnidadNegocio();
            usuario = new Usuario();
        }
    }
}
