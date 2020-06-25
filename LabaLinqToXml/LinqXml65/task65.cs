using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System;
using static System.Console;

namespace LinqXml65
{
    class task65 {
        static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("task65.xml");
            List<XElement> IDs = xdoc.Root.Elements().ToList();
            Dictionary<string, List<object[]>> years_id_time = new Dictionary<string, List<object[]>>();
            List<XElement> years = new List<XElement>();
            List<XElement> ttSortedById = new List<XElement>();
            List<int> been = new List<int>();
            TimeSpan time = new TimeSpan();
            IDs.Descendants("date").GroupBy(x => ((DateTime)x).Year.ToString()).ToList().ForEach(x => years_id_time[x.Key] = new List<object[]>());
            foreach (var id in IDs) 
                id.Elements("info").ToList().ForEach(x => years_id_time[((DateTime)x.Element("date")).Year.ToString()].Add(new object[] { int.Parse(id.Name.ToString().Substring(2, id.Name.ToString().Length - 2)), (TimeSpan)x.Element("time") }));
            foreach (var d in years_id_time) {
                XElement year = new XElement("year", new XAttribute("value", d.Key));
                for (int i = 0; i < d.Value.Count(); i++) {
                    int id = int.Parse(d.Value[i][0].ToString());
                    if (!been.Contains(id)) {
                        been.Add(id);
                        d.Value.Where(x => int.Parse(x[0].ToString()) == id).Select(x => (TimeSpan)x[1]).ToList().ForEach(x => time += x);
                        ttSortedById.Add(new XElement("total-time", time.TotalMinutes, new XAttribute("id", id)));
                        time = new TimeSpan();
                    }
                }
                ttSortedById.OrderBy(x => x.Attribute("id").Value.Length).ThenBy(x => x.Attribute("id").Value).ToList().ForEach(x => year.Add(x));
                years.Add(year);
                been.Clear();
                ttSortedById.Clear();
            }
            #region Без Linq
            //foreach (var d in dict) {
            //    XElement year = new XElement("year", new XAttribute("value", d.Key));
            //    List<int> been = new List<int>();
            //    TimeSpan time = new TimeSpan();
            //    for(int i = 0; i < d.Value.Count(); i++) {
            //        int id = int.Parse(d.Value[i][0].ToString());
            //        if (!been.Contains(id)) {
            //            if (i != 0)
            //                year.Add(new XElement("total-time", time, new XAttribute("id", been.Last())));
            //            been.Add(id);
            //            time = (TimeSpan)d.Value[i][1];
            //        }
            //        else {
            //            time += (TimeSpan)d.Value[i][1];
            //        }
            //        if (i == d.Value.Count() - 1)
            //            year.Add(new XElement("total-time", time, new XAttribute("id", been.Last())));
            //    }
            //    newElements.Add(year);
            //}
            #endregion Без Linq
            xdoc.Root.RemoveAll();
            years.OrderBy(x => x.Attribute("value").Value).ToList().ForEach(x => xdoc.Root.Add(x));
            WriteLine(xdoc.Declaration);
            WriteLine(xdoc);
            xdoc.Save("result.xml");
            ReadKey();
        }
    }
}
