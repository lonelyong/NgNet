using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class TestXml : TestBase
    {
        static string xmlfile1 = "";
        static string xmlfile2 = "";
        static TestXml()
        {
            xmlfile1 = AppDomain.CurrentDomain.BaseDirectory + "\\data.xml";
            xmlfile2 = AppDomain.CurrentDomain.BaseDirectory + "\\data2.xml";

        }
        public override void Test()
        {
            //TestXmlWriter();
            //TestXmlDocument();
            //TestXPathDocument();
            TestLinqToXml();
            Console.Read();
        }

        void TestXmlReader()
        {
            XmlReader xmlReader = XmlReader.Create(xmlfile1, new XmlReaderSettings() { CloseInput = true });
            while (!xmlReader.EOF)
            {
                Console.WriteLine(xmlReader.Name + " |nodetype:" + xmlReader.NodeType + " |value:" + xmlReader.Value);
                if (xmlReader.NodeType == XmlNodeType.Text)
                {
                    Console.Write(" |content:" + xmlReader.ReadContentAsString());
                }
                xmlReader.Read();
            }
        }
        void TestXmlWriter()
        {
            XmlWriter xmlWriter =XmlWriter.Create(xmlfile2, new XmlWriterSettings() { CloseOutput = true});
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("a1", "ns1");
            xmlWriter.WriteAttributeString("attr", "a1");
            xmlWriter.WriteValue("a1.text");
            xmlWriter.WriteStartElement("b1");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        void TestXmlDocument()
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load(xmlfile1);
            var nodelist = xmldoc.GetElementsByTagName("b1");
            foreach (XmlNode item in nodelist)
            {
                Console.WriteLine(item.OuterXml);
            }
            
        }

        void TestXPathDocument()
        {
            XPathDocument xpd = new XPathDocument(xmlfile1, XmlSpace.None);
            var xpn = xpd.CreateNavigator();
            var x = xpn.Select("/root/a1");
            foreach (XPathNavigator item in x)
            {
                Write(item.OuterXml);
            }
        }

        void TestLinqToXml()
        {
            XNamespace xNamespace = "http://www.baidu.com";
            var b2 = new XElement("b2", "b2");
            b2.SetAttributeValue("aaa", "dd");
            var doc = new XElement(xNamespace + "aaaa",new XElement("a" , new XElement("b1", "b1"),b2));
            WriteLine("");
            var xdoc = XDocument.Load(xmlfile1);
            var x = xdoc.Descendants("a1");
            x = xdoc.Elements("root").Elements("a1");
            Array.ForEach<XElement>(x.ToArray(), t=>WriteLine(t.Name));
            Write(doc);
        }
    }
}
