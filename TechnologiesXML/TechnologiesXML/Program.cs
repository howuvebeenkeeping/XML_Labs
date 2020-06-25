using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Schema;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Text;
using System.IO;
using System.Xml.Xsl;

namespace TechnologiesXML {
    internal class Program {
        static bool isValid = true;
        public static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("timetable.xml");

            // a. Получить все занятия на данной неделе.
            var WeekTasks = ((IEnumerable<object>)xdoc.XPathEvaluate("//day/lesson/@title")).GroupBy(x => ((XAttribute)x).Value); //day/*/@title
            Print("a. Все занятия на данной неделе:", WeekTasks);

            // b. Получить все аудитории, в которых проходят занятия.
            var Audiences = xdoc.XPathSelectElements("//audience").GroupBy(x => x.Value); //.OrderBy(x => x.Key);
            Print("b. Все аудитории, в которых проходят занятия", Audiences);

            // c. Получить все практические занятия на неделе.
            var Practics = ((IEnumerable<object>)xdoc.XPathEvaluate("//lesson[type='практика']/@title")).GroupBy(x => ((XAttribute)x).Value); //lesson/type[text()='практика']/../@title
            Print("c. Все практические занятия на неделе", Practics);

            // d. Получить все лекции, проводимые в указанной аудитории (102).
            var Lections102 = ((IEnumerable<object>)xdoc.XPathEvaluate("//lesson[type='лекция' and audience='102']/@title")).GroupBy(x => ((XAttribute)x).Value);
            Print("d. Все лекции, проводимые в 102 аудитории", Lections102);

            // e. Получить список всех преподавателей, проводящих практики в указанной аудитории (509).
            var Teachers509 = xdoc.XPathSelectElements("//lesson[type='практика' and audience='509']/teacher").GroupBy(x => x.Value);
            Print("e. Cписок всех преподавателей, проводящих практики в 509 аудитории", Teachers509);

            // f. Получить последнее занятие для каждого дня делели.
            var LastLessons = (IEnumerable<object>)xdoc.XPathEvaluate("//lesson[last()]/@title");
            Print("f. Последнее занятия для каждого дня недели", LastLessons);

            // g. Получить общее количество занятий за всю неделю.
            var LessonsCount = xdoc.XPathEvaluate("sum(//day/@count)");
            Print("g. Общее количество занятий за неделю", int.Parse(LessonsCount.ToString()));

            // Валидация по DTD
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationMessageBack);
            XmlReader xmlreader = XmlReader.Create("timetable.xml", settings);
            Print("Валидация документа по DTD:");
            while (xmlreader.Read())
                if (!isValid) break;
            if (isValid)
                WriteLine("Документ является валидным");
            isValid = true;

            // Валидация по схеме XSD
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", XmlReader.Create("schema_timetable.xsd", new XmlReaderSettings() {
                ProhibitDtd = false
            }));
            Print("Валидация документа по схеме XSD:");
            xdoc.Validate(schema, new ValidationEventHandler(ValidationMessageBack));
            if (isValid)
                WriteLine("Документ является валидным");

            // XSLT-преобразование в TXT
            File.Create("XSLT_timetable.txt").Close();
            XslCompiledTransform xslt_txt = new XslCompiledTransform();
            xslt_txt.Load("txt_timetable.xsl");
            XmlReader reader_txt = XmlReader.Create("timetable.xml", new XmlReaderSettings() {
                ProhibitDtd = false
            });
            XmlWriter writer_txt = XmlWriter.Create("XSLT_timetable.txt", new XmlWriterSettings() {
                ConformanceLevel = ConformanceLevel.Auto
            });
            xslt_txt.Transform(reader_txt, writer_txt);

            // XSLT-преобразование в HTML
            File.Create("XSLT_timetable.html").Close();
            XslCompiledTransform xslt_html = new XslCompiledTransform();
            XmlReader reader_html = XmlReader.Create("timetable.xml", new XmlReaderSettings() {
                ProhibitDtd = false
            });
            XmlWriter writer_html = XmlWriter.Create("XSLT_timetable.html", new XmlWriterSettings() {
                ConformanceLevel = ConformanceLevel.Auto
            });
            xslt_html.Load("html_timetable.xsl");
            xslt_html.Transform(reader_html, writer_html);

            ReadKey();
        }

        private static void ValidationMessageBack(object sender, ValidationEventArgs e) {
            isValid = false;
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            WriteLine($"Документ не является валидным: {e.Message}");
            System.Console.ResetColor();
        }

        static void Print(string text, IEnumerable<IGrouping<object, object>> group) {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            WriteLine(text);
            System.Console.ResetColor();
            foreach (var x in group)
                WriteLine(x.Key);
        }

        static void Print(string text, IEnumerable<object> group) {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            WriteLine(text);
            System.Console.ResetColor();
            foreach (var x in group)
                WriteLine(((XAttribute)x).Value);
        }

        static void Print(string text, int n) {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            WriteLine(text);
            System.Console.ResetColor();
            WriteLine(n);
        }
        static void Print(string text) {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            WriteLine(text);
            System.Console.ResetColor();
        }
    }
}