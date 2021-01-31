using System.Xml.Linq;

namespace AddressValidator.Data.Extensions
{
    public static class XmlElementExtensions
    {
        public static string GetXmlElement(this XElement element, string name)
        {
            var el = element.Element(name);

            if (el != null)
            {
                return el.Value;
            }

            return "";
        }

        public static string GetXmlAttribute(this XElement element, string name)
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
