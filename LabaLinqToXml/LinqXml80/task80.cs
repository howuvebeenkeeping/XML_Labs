using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System;
using static System.Console;

namespace LinqXml80
{
    class task80
    {
        static void Main(string[] args) {
            XDocument xdoc = XDocument.Load("task80.xml");
            IEnumerable<XElement> houses = xdoc.Root.Elements();
            List<XElement> newHouses = new List<XElement>();
            foreach(var house in houses) {
                var entrance = new[] {
                    house.Descendants("flat").Where(x => int.Parse(x.Attribute("value").Value) <= 36).Count(),
                    house.Descendants("flat").Select(x => int.Parse(x.Attribute("value").Value)).Where(x => x > 36 && x <= 72).Count(),
                    house.Descendants("flat").Select(x => int.Parse(x.Attribute("value").Value)).Where(x => x > 72 && x <= 118).Count(),
                    house.Descendants("flat").Select(x => int.Parse(x.Attribute("value").Value)).Where(x => x > 118 && x <= 144).Count()
                };
                XElement newHouse = new XElement("house" + house.Attribute("number").Value);
                List<XElement> people = new List<XElement>();
                XElement entr = new XElement("entrance");
                for(int i = 1; i <= entrance.Count(); i++) {
                    if (entrance[i-1] != 0) {
                        entr = new XElement("entrance" + i);
                        switch (i) {
                            case 1:
                                people = house.Elements().Where(x => int.Parse(x.Element("flat").Attribute("value").Value) <= 36).ToList();
                                break;
                            case 2:
                                people = house.Elements().Where(x => int.Parse(x.Element("flat").Attribute("value").Value) > 36 && int.Parse(x.Element("flat").Attribute("value").Value) <= 72).ToList();
                                break;
                            case 3:
                                people = house.Elements().Where(x => int.Parse(x.Element("flat").Attribute("value").Value) > 72 && int.Parse(x.Element("flat").Attribute("value").Value) <= 118).ToList();
                                break;
                            case 4:
                                people = house.Elements().Where(x => int.Parse(x.Element("flat").Attribute("value").Value) > 118 && int.Parse(x.Element("flat").Attribute("value").Value) <= 144).ToList();
                                break;
                        }
                        var total_debt = people.Select(x => double.Parse(x.Element("debt").Attribute("value").Value.Replace('.', ','))).Sum();
                        entr.Add(new XAttribute("total-debt", total_debt.ToString().Replace(',', '.')), new XAttribute("count", people.Count()));
                        List<XElement> tmpFlat = new List<XElement>();
                        foreach (var x in people) 
                            tmpFlat.Add(new XElement("flat" + x.Element("flat").Attribute("value").Value, new XAttribute("name", x.Name.ToString().Replace('_', ' '))));
                        tmpFlat.OrderBy(x => x.Name.ToString().Length).ThenBy(x => x.Name.ToString()).ToList().ForEach(x => entr.Add(x));
                        newHouse.Add(entr);
                    }
                }
                newHouses.Add(newHouse);
            }
            xdoc.Root.RemoveAll();
            newHouses.OrderBy(x => x.Name.ToString().Length).ThenBy(x => x.Name.ToString()).ToList().ForEach(x => xdoc.Root.Add(x));
            WriteLine(xdoc.Declaration);
            WriteLine(xdoc);
            xdoc.Save("result.xml");
            ReadKey();
        }
    }
}
