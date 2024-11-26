using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
   public class csConsultas
    {
        private string mPagina = String.Empty;
        public string Pagina { get { return mPagina; } set { mPagina = value; } }

        private string mDatos = String.Empty;
        public string Datos { get { return mDatos; } set { mDatos = value; } }
    }
}
