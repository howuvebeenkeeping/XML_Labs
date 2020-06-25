using System.Xml.Linq;
using System;
using System.Collections.Generic;
using static System.Console;

namespace LinqXml50
{
    class task50
    {
        static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("task50.xml");
            TimeSpan sum = new TimeSpan();
            IEnumerable<XElement> elements = xdoc.Root.Elements();
            foreach(var element in elements) {
                TimeSpan? time = (TimeSpan?) element.Attribute("time");
                sum += time ?? new TimeSpan(1, 0, 0, 0);
            }
            xdoc.Root.AddFirst(new XElement("total-time", sum));
            WriteLine(xdoc.Declaration);
            WriteLine(xdoc);
            xdoc.Save("result.xml");
            ReadKey();
        }
    }
}
