using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cpplib
{
    public class admDirectorio
    {
        public String DadirectorioArchivo(DateTime FhFactura)
        {
            DateTime Fecha = Convert.ToDateTime(FhFactura);
            String Carpeta = Fecha.Year.ToString() + @"\" + Fecha.Month.ToString().PadLeft(2, '0');
            return Carpeta + @"\";
        }


        public String DaYCreaDirectorioArchivo(String DirRaiz,DateTime FhFactura)
        {
            DateTime Fecha = Convert.ToDateTime(FhFactura);
            String Carpeta = Fecha.Year.ToString() + @"\" + Fecha.Month.ToString().PadLeft(2, '0');
            if (!System.IO.Directory.Exists(DirRaiz + Carpeta)) { System.IO.Directory.CreateDirectory(DirRaiz+ Carpeta); }
            return Carpeta + @"\";
        }

        public bool ValidaDirectorio(String Directorio)
        {
            if (!System.IO.Directory.Exists(Directorio)) { System.IO.Directory.CreateDirectory(Directorio); }
            return true;
        }
    }
}
