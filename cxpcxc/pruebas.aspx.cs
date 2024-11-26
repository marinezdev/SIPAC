using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;

namespace cxpcxc
{
    public partial class pruebas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("espera.aspx");
        }

        protected void btnObtenerIP_Click(object sender, EventArgs e)
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"];
            string pc = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;

            ltMsg.Text = "IP: " + ip + "   PC:" + pc;


            String strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            lblIPAddress.Text = Convert.ToString(ipEntry.AddressList[ipEntry.AddressList.Length - 1])  ;
            lblHostName.Text = Convert.ToString(ipEntry.HostName)  ;
  
        //Find IP Address Behind Proxy Or Client Machine In ASP.NET  
            String IPAdd = String.Empty ;
            IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ;
  
            if( String.IsNullOrEmpty(IPAdd) ){  
                IPAdd = Request.ServerVariables["REMOTE_ADDR"];
                lblIPBehindProxy.Text = IPAdd;
            }

        }

        protected void btnObjDinamico_Click(object sender, EventArgs e)
        {
            //// Creating a dynamic dictionary.
            //dynamic person = new cpplib.DynamicDictionary();

            //// Adding new dynamic properties. 
            //// The TrySetMember method is called.
            //person.FirstName = "Ellen";
            //person.LastName = "Adams";
            //person.Edad = 17;

            //// Getting values of the dynamic properties.
            //// The TryGetMember method is called.
            //// Note that property names are case-insensitive.
            //Console.WriteLine(person.firstname + " " + person.lastname + " " + person.Edad );

            //// Getting the value of the Count property.
            //// The TryGetMember is not called, 
            //// because the property is defined in the class.
            //Console.WriteLine(
            //    "Number of dynamic properties:" + person.Count);

            //// The following statement throws an exception at run time.
            //// There is no "address" property,
            //// so the TryGetMember method returns false and this causes a
            //// RuntimeBinderException.
            //// Console.WriteLine(person.address);

        }
    }
}