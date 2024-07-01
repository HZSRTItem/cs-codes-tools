using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XMLReadWriteCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml_fn = @"E:\ImageData\Shadow\QingDao\qd20211023\qd_20211023_1_vrt.xml";
            string xmlPath = fn("t1.xml");


            XmlDocument xml_doc = new XmlDocument();
            xml_doc.Load(xml_fn);
            

            for (int i = 0; i < xml_doc.DocumentElement.ChildNodes.Count; i++)
            {
                Console.WriteLine(xml_doc.DocumentElement.ChildNodes[i].Name);
                if (xml_doc.DocumentElement.ChildNodes[i].Name == "VRTRasterBand")
                {
                    XmlNode xml_node = xml_doc.CreateElement("element", "Description", "");
                    xml_node.InnerText = "des " + i;
                    Console.WriteLine(xml_node.OuterXml);
                    xml_doc.DocumentElement.ChildNodes[i].AppendChild(xml_node);
                }
            }
            //foreach (XmlNode item in xml_ele.ChildNodes)
            //{
            //    if (item.Name == "VRTRasterBand")
            //    {
            //        Console.Write(item.Name);
            //        Console.Write(": ");
            //        Console.Write(item.ChildNodes[0].Name);
            //        Console.WriteLine();
            //        xml_node.InnerText = "des " + i++;
            //        item.AppendChild(xml_node);
            //    }
            //}

            xml_doc.Save(@"E:\ImageData\Shadow\QingDao\qd20211023\qd_20211023_1_vrt_2.xml");


            //XElement xElement = new XElement(
            //    new XElement("BookStore",
            //        new XElement("Book",
            //            new XElement("Name", "C#入门", new XAttribute("BookName", "C#")),
            //            new XElement("Author", "Martin", new XAttribute("Name", "Martin")),
            //            new XElement("Adress", "上海"),
            //            new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
            //            ),
            //        new XElement("Book",
            //            new XElement("Name", "WCF入门", new XAttribute("BookName", "WCF")),
            //            new XElement("Author", "Mary", new XAttribute("Name", "Mary")),
            //            new XElement("Adress", "北京"),
            //            new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
            //            )
            //            )
            //    );

            ////需要指定编码格式，否则在读取时会抛：根级别上的数据无效。 第 1 行 位置 1异常
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = new UTF8Encoding(false);
            //settings.Indent = true;
            //XmlWriter xw = XmlWriter.Create(xmlPath, settings);
            //xElement.Save(xw);
            ////写入文件
            //xw.Flush();
            //xw.Close();

            Console.WriteLine("End of this!");
            Console.ReadLine();

        }

        static string fn(string filename)
        {
            return Path.Combine(@"D:\CodeProjects\GDAL_CMD\VRT", filename);
        }

    }


}
