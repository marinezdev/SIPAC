using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace cpplib
{
    public class admCatalogos
    {
       
    }

    public class valorTexto
    {
        public string Valor { get; set; }
        public string Texto { get; set; }

        public valorTexto() { }
        public valorTexto(string pValor, string pTexto) { Valor = pValor; Texto = pTexto; }
    }
}
