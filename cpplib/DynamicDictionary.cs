using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
namespace cpplib
{
    public class DynamicDictionary: DynamicObject
    {

        // Diccionario in terno.
        Dictionary<string, object> dictionary= new Dictionary<string, object>();

        // Esta propiedad devuelve el número de elementos en el diccionario interno.
        public int Count{get{return dictionary.Count;}}

        // Si intenta obtener un valor de una propiedad no definida en la clase, se llama a este método.
        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            
            //Convertir el nombre de la propiedad en minúsculas para que los nombres de propiedad se vuelvan insensibles a mayúsculas y minúsculas.
            string name = binder.Name.ToLower();
            // Si el nombre de la propiedad se encuentra en un diccionario,  establezca el parámetro de resultado en el valor de la propiedad y devuelva true. De lo contrario, devuelve false.
            return dictionary.TryGetValue(name, out result);
        }

        // Si intenta establecer un valor de una propiedad que no está definida en la clase, se llama a este método.
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Convertir el nombre de la propiedad en minúscula para que los nombres de propiedad se vuelvan insensibles a mayúsculas y minúsculas.
            dictionary[binder.Name.ToLower()] = value;

            // Siempre puede agregar un valor a un diccionario, por lo que este método siempre devuelve true.
            return true;
        }
    }
}
