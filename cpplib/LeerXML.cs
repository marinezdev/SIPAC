using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
    public class LeerXML
    {
        public DatosXML ExtraerDatos(string ArhXml)
        {
            DatosXML Resultado = new DatosXML();
            try
            {
                Resultado = XMLVersion32(ArhXml);
                if (string.IsNullOrEmpty(Resultado.Rfc)) {
                    Resultado = XMLVersion33(ArhXml);                
                }
            }
            catch (Exception) { }
            
            return Resultado;
        }

        private DatosXML XMLVersion32(string ArhXml)
        {
            DatosXML Resultado = new DatosXML();
            System.Xml.XmlTextReader lectorXml = new System.Xml.XmlTextReader(ArhXml);
            try
            {
                System.Xml.Serialization.XmlSerializer serializador = new System.Xml.Serialization.XmlSerializer(typeof(libFacVrs32.Comprobante));
                libFacVrs32.Comprobante Factura = (libFacVrs32.Comprobante)serializador.Deserialize(lectorXml);
                Resultado.Nombre = Factura.Emisor.nombre;
                Resultado.Rfc = Factura.Emisor.rfc;
                Resultado.Folio = Factura.folio;
                Resultado.Fecha = Factura.fecha;
                Resultado.Total = Factura.total;
                Resultado.Sello = Factura.sello.ToString();
                Resultado.Receptor.Nombre = Factura.Receptor.nombre;
                Resultado.Receptor.Rfc = Factura.Receptor.rfc;
                //string datos = Factura.Impuestos.totalImpuestosTrasladados.ToString ();
                foreach (libFacVrs32.ComprobanteConcepto cpo in Factura.Conceptos){Resultado.Concepto += cpo.descripcion.Replace("'","") + ", ";}
            }
            catch (Exception)
            {
            }
            
            lectorXml.Close();
            return Resultado;
        }
        
        private DatosXML XMLVersion33(string ArhXml)
        {
            DatosXML Resultado = new DatosXML();
            System.Xml.XmlTextReader lectorXml = new System.Xml.XmlTextReader(ArhXml);
            try
            {
                System.Xml.Serialization.XmlSerializer serializador = new System.Xml.Serialization.XmlSerializer(typeof(libFacVrs33.Comprobante));
                libFacVrs33.Comprobante Factura = (libFacVrs33.Comprobante)serializador.Deserialize(lectorXml);
                Resultado.Nombre = Factura.Emisor.Nombre;
                Resultado.Rfc = Factura.Emisor.Rfc;
                Resultado.Folio = Factura.Folio;
                Resultado.Fecha = Convert.ToDateTime(Factura.Fecha);
                Resultado.Total = Convert.ToDecimal(Factura.Total);
                Resultado.Sello = Factura.Sello.ToString() + 'x';
                Resultado.Receptor.Nombre = Factura.Receptor.Nombre;
                Resultado.Receptor.Rfc = Factura.Receptor.Rfc;
                //string datos = Factura.Impuestos.TotalImpuestosTrasladados.ToString();
                foreach (libFacVrs33.ComprobanteConcepto cpo in Factura.Conceptos)
                {
                    Resultado.Concepto += cpo.Descripcion.Replace("'", "") + ", ";
                }
            }
            catch (Exception) 
            {
            }

            lectorXml.Close();
            return Resultado;
        }
    }

    public class DatosXML
    {
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mFolio = String.Empty;
        public string Folio { get { return mFolio; } set { mFolio = value; } }
        private DateTime mFecha = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime Fecha { get { return mFecha; } set { mFecha = value; } }
        private decimal mTotal = 0;
        public decimal Total { get { return mTotal; } set { mTotal = value; } }
        private string mConcepto = String.Empty;
        public string Concepto { get { return mConcepto; } set { mConcepto = value; } }
        private string mSello = String.Empty;
        public string Sello { get { return mSello; } set { mSello = value; } }
        private Receptor mReceptor = new Receptor();
        public Receptor Receptor { get { return mReceptor; } set { mReceptor = value; } }
    }

    public class Receptor
        {
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
    }
}
