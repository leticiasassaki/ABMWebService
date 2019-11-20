using System.Linq;
using System.Web.Services;
using System.Xml.Linq;

namespace WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int ParsingXml(string xml)
        {
            try
            {
                var doc = XElement.Parse(xml);
                var commandValue = GetCommand(doc);
                var siteId = GetDeclaHeadSite(doc);

                if (commandValue!= "DEFAULT") return -1;
                else if (siteId != "DUB") return -2;

                return 0;
            }
            catch
            {
                return -3;
            }
        }

        private static string GetCommand(XElement doc)
        {
            return doc.Descendants("Declaration")
                .First()
                .Attributes("Command").First()
                .Value;
        }
        private static string GetDeclaHeadSite(XElement doc)
        {
            return doc.Descendants("DeclarationHeader")
                 .First()
                 .Element("SiteID")
                 .Value;
        }
    }
}
