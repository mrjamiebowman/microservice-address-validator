using System.Xml.Linq;

namespace AddressValidator.Data.Extensions
{
    public static class XmlElementExtensions
    {
        public static string GetXMLElement(this XElement element, string name)
        {
            var el = element.Element(name);

            if (el != null)
            {
                return el.Value;
            }

            return "";
        }

        public static string GetXMLAttribute(this XElement element, string name)
        {
            var el = element.Attribute(name);

            if (el != null)
            {
                return el.Value;
            }

            return "";
        }
    }
}
