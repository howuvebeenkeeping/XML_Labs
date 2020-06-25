using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Console;

namespace LinqXml35
{
    class task35
    {
        static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("task35.xml");
            IEnumerable<XElement> elements2lvl = xdoc.Root.Elements().Elements();
            foreach(var x in elements2lvl)
                x.Add(new XAttribute("child-count", x.Elements().Count()));
            WriteLine(xdoc.Declaration);
            WriteLine(xdoc);
            xdoc.Save("result.xml");
            ReadKey();
        }
    }
}
