using System.Xml.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

namespace LinqXml83
{
    class Program
    {
        static void Main(string[] args) {
            XDocument doc = XDocument.Load("task83.xml");
            List<XElement> elements = new List<XElement>();
            foreach (var x in doc.Root.Elements()) {
                XElement mark= new XElement(
                    "mark",
                    new XAttribute("subject", x.Element("subject").Value),
                    new XElement("name", x.Element("name").Value, new XAttribute("class", x.Element("class").Value)),
                    new XElement("value", x.Element("mark").Value)
                );
                elements.Add(mark);
            }
            doc.Root.Elements().Remove();
            foreach (var x in elements)
                doc.Root.Add(x);
            doc.Save("result.xml");
        }
    }
}
