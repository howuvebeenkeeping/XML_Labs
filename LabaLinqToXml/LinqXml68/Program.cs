using System.Xml.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

namespace LinqXml68
{
    class Program
    {
        static void Main(string[] args) {
            XDocument doc = XDocument.Load("task68.xml");
            List<XElement> elements = new List<XElement>();
            foreach (var x in doc.Root.Elements()) {
                XElement station = new XElement(
                    "station",
                    new XAttribute("company", x.Element("company").Value),
                    new XAttribute("street", x.Element("street").Value)
                );
                XElement info = new XElement(
                    "info",
                    new XElement("brand", x.Element("brand").Value),
                    new XElement("price", x.Element("price").Value)
                );
                station.Add(info);
                elements.Add(station);
            }
            doc.Root.Elements().Remove();
            foreach (var x in elements)
                doc.Root.Add(x);
            doc.Save("result.xml");
        }
    }
}
