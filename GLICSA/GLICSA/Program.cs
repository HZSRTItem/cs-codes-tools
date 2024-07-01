using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace GLICSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string tmp_fn_r = "__temp_gli_file_w.xml";
            string tmp_fn_w = "__temp_gli_file_w.xml";

            if (args.Length == 0)
            {
                Console.WriteLine("xmltotable xml_file csv_fn to_csv_fn front_name");
                return;
            }

            string xml_fn = args[0];
            string csv_fn = args[1];
            string to_csv_fn = args[2];
            string front_name = args[3];

            addLiangbian(xml_fn);
            xmlToTable(xml_fn, csv_fn, to_csv_fn, front_name);
        }

        static void addLiangbian(string xml_fn)
        {
            string text = File.ReadAllText(xml_fn);
            StreamWriter sr = new StreamWriter(xml_fn);
            sr.WriteLine("<GLI>");
            sr.Write(text);
            sr.WriteLine("</GLI>");
            sr.Close();
        }

        static void xmlToTable(string xml_fn, string csv_fn, string to_csv_fn, string front_name)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xml_fn);
            XmlNodeList xmlNodeList = document.GetElementsByTagName("Report");
            int n_columns = -1;

            StreamReader sr = new StreamReader(csv_fn);
            StreamWriter sw = new StreamWriter(to_csv_fn);

            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                if (n_columns < xmlNodeList[i].ChildNodes.Count)
                {
                    n_columns = xmlNodeList[i].ChildNodes.Count;
                }
            }

            writeLineFront(sr, sw);
            for (int i = 0; i < n_columns - 1; i++)
            {
                sw.Write(string.Format("{0}_{1},", front_name, i + 1));
            }
            sw.Write(string.Format("{0}_{1}\n", front_name, n_columns));

            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                writeLineFront(sr, sw);

                if (xmlNodeList[i].ChildNodes[0].Name == "Alert")
                {
                    Console.WriteLine("Warning: line {0} {1}", i + 2, xmlNodeList[i].ChildNodes[0].InnerText);
                    for (int j = 0; j < n_columns - 1; j++)
                    {
                        sw.Write(",");
                    }
                    sw.Write("\n");
                }
                else
                {
                    for (int j = 0; j < n_columns - 1; j++)
                    {
                        sw.Write("{0},", xmlNodeList[i].ChildNodes[j].ChildNodes[0].InnerText);
                    }
                    sw.Write("{0}\n", xmlNodeList[i].ChildNodes[n_columns - 1].ChildNodes[0].InnerText);
                }
            }

            sr.Close();
            sw.Close();
        }

        private static void writeLineFront(StreamReader sr, StreamWriter sw)
        {

            string line = sr.ReadLine();
            line = line.Trim();
            sw.Write(line);
            if (!line.EndsWith(","))
            {
                sw.Write(",");
            }
        }
    }
}
