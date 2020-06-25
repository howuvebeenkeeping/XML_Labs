using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Console;

namespace LinqXml20 {
    class task20 {
        public static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("task20.xml");
            IEnumerable<XElement> elements = xdoc.Root.Elements();
            foreach (var element in elements) {
                var max = element.Descendants().Count() == 0
                    ? -1
                    : element.Descendants().Max(x => x.Attributes().Count());
                var names = element.Descendants()
                    .Where(x => x.Attributes().Count() == max)
                    .Select(x => x.Name.ToString())
                    .OrderBy(x => x)
                    .GroupBy(x => x)
                    .ToList();
                Write(element.Name + " " + max + " ");
                if (max != -1)
                    names.ForEach(x => Write(x.Key + " "));
                else
                    Write("no child");
                WriteLine();
            }
            ReadKey();
        }
    }
}
