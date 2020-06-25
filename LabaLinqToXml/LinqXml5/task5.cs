using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using static System.Console;

namespace LinqXml5 {
     class task5 {
         public static void Main(string[] args) {
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "windows-1251", null));
            string[] lines = File.ReadAllLines("input.txt", Encoding.Default);
            int i = 1;
            XElement root = new XElement("root");
            foreach (var l in lines) {
                string[] words = l.Split();
                XElement line = new XElement("line", new XAttribute("num", i++));
                for (int k = 0; k < words.Count(); k++)
                    line.Add(new XElement("word", words[k], new XAttribute("num", k + 1)));
                root.Add(line);
            }
            xdoc.Add(root);
            WriteLine(xdoc.Declaration);
            WriteLine(xdoc);
            xdoc.Save("result.xml");
            ReadKey();
         }
     }
}
