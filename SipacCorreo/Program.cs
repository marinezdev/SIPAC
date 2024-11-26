using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace SipacCorreo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            try
            {
                RegistraEjecucion();
                string DiasProceso=Properties.Settings .Default.Dias;
                string Dia= DateTime.Now.DayOfWeek.ToString ("d") ;
                
                string[] horario = Properties.Settings.Default.horario.Split(',');
                int horaInicio = Convert.ToInt32(horario[0]);
                int horaTermino = Convert.ToInt32(horario[1]);
                int horaActual = DateTime.Now.Hour;

                if (DiasProceso.Contains(Dia)){ 
                    if (horaActual >= horaInicio && horaActual < horaTermino)
                    {
                        ProcesaCuentasPorPagar();

                        procesaCuentasPorCobrar();

                    }
                }
            }
            catch (Exception ex){ registralog(ex);}
        }

        static void ProcesaCuentasPorPagar()
        {
            admDatos admDatos = new admDatos();
            List<string> lstEmpresas = admDatos.ListaEmpresas();
            if (lstEmpresas.Count > 0)
            {
                general csgeneral = new general();
                foreach (string Emp in lstEmpresas)
                {
                    csgeneral.EnviaCorreoDireccionXSolicitudAutorizacion(Emp);
                }
            }
        }

        static void procesaCuentasPorCobrar() {
            admCxC adm = new admCxC ();
            adm.procesaPartidasDia();
            adm.ValidaPendientedeFacturar();
            adm.ValidaPendientesCobro ();
        }
        

        static void registralog(Exception  ex){
             System.IO.StreamWriter file = null;
             try
             {
                 file = new System.IO.StreamWriter(Properties .Settings .Default.errLog,true);
                 file.WriteLine(DateTime.Now.ToString () + " ERROR: " + ex.StackTrace.ToString () +  " "  +  ex.Message .ToString ());

                 file.Close();
                }
             catch (Exception){if (file != null){file.Close();}}
        }

        static void RegistraEjecucion()
        {
            System.IO.StreamWriter file = null;
            try
            {
                file = new System.IO.StreamWriter(Properties.Settings.Default.errLog, true);
                file.WriteLine(DateTime.Now.ToString() + " INICIADO: " + DateTime .Now.ToString() );

                file.Close();
            }
            catch (Exception) { if (file != null) { file.Close(); } }
        }
    }
}
