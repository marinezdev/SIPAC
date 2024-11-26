
namespace cxpcxc.Utilerias
{
    /// <summary>
    /// Clase comun de acceso a objetos instanciados
    /// </summary>
    public class Comun : System.Web.UI.Page
    {
        /// <summary>
        /// Instancia de acceso general procesos crud (procedimiento directo y actual)
        /// </summary>
        public cpplib.Comun comun;

        //Nueva implementación (toma la instaciación de nuevos proyectos) ***************************** 
        public CXPCXC.Modelos.Modelos modelos;

        public CXPCXC.Negocio.CXC.CXC cxc;
        public CXPCXC.Negocio.Sistema.Sistema admin;


        //*****************************************************************************
        
        public Comun()
        {
            comun = new cpplib.Comun();

            modelos = new CXPCXC.Modelos.Modelos();
            
            cxc = new CXPCXC.Negocio.CXC.CXC(); 
            admin = new CXPCXC.Negocio.Sistema.Sistema();            
        }


    }
}