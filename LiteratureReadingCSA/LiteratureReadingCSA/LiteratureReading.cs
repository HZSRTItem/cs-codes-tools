/*------------------------------------------------------------------------------
 * File    : LiteratureReading
 * Time    : 2023/8/3 20:43:48
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[LiteratureReading]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Globalization;
using System.Xml;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Xml.Serialization;

namespace LiteratureReadingCSA
{
    public class Literature
    {
        public static DateTime InitDateTime = DateTime.Now;

        public string OID = "";
        public DateTime OIDTime;
        public string Title = "";
        public string Text = "";
        public Dictionary<string, string> KeyValues = new Dictionary<string, string>();

        private string[] texts;
        private string line_tmp = "--------------------------------------------------------";
        private IFormatProvider ifp = new CultureInfo("zh-CN", true);

        public Literature(string[] texts)
        {
            this.texts = texts;
            OIDTime = getTime();
            OID = getTimeString(OIDTime);
            Title = texts[0].Substring(2);
            Title = Title.Trim();
            getKeyValues();
        }

        public Literature()
        {
            OIDTime = getTime();
            OID = getTimeString(OIDTime);
            Title = "";
            Text = "";
            addKeyValues("OID", OID);
        }

        private string getTimeString(DateTime dateTime)
        {
            return "H" + dateTime.ToString("yyyyMMddHHmmss");
        }

        private DateTime getTime()
        {
            DateTime dateTime = InitDateTime;
            InitDateTime = InitDateTime.AddSeconds(-1);
            return dateTime;
        }

        private void getKeyValues()
        {
            string[] lines;
            char[] split_chars = new char[1];
            split_chars[0] = ':';
            int n_start = -1;
            int n_end = -1;

            string line;
            for (int i = 1; i < texts.Length; i++)
            {
                line = texts[i].Trim();
                if (n_start < 0)
                {
                    if (n_start != -1)
                    {
                        Text += texts[i] + "\n";
                    }

                    if (line.StartsWith("```"))
                    {
                        n_start = i;
                    }
                }
                else
                {
                    if (n_end != -1)
                    {
                        Text += texts[i] + "\n";
                    }

                    if (line.StartsWith("```"))
                    {
                        n_end = i;
                    }
                }
            }

            for (int i = n_start; i < n_end; i++)
            {
                if (texts[i].Contains(":"))
                {
                    line = texts[i].Trim();
                    lines = line.Split(split_chars, 2);
                    lines[0] = lines[0].Trim();
                    lines[0] = lines[0].ToUpper();
                    lines[1] = lines[1].Trim();
                    KeyValues.Add(lines[0], lines[1]);
                }
            }

            Text = Text.Trim();
            tiaoText();
            if (!KeyValues.ContainsKey("OID"))
            {
                KeyValues.Add("OID", OID);
                Console.WriteLine("Warning: Title of \"{0}\" can not find OID and init OID=\"{1}\"", Title, OID);
            }
            else
            {
                OID = KeyValues["OID"];
                OIDTime = DateTime.ParseExact(OID.Substring(1), "yyyyMMddHHmmss", ifp);
            }
        }

        private void tiaoText()
        {
            string[] text_list = Text.Split('\n');
            Text = "";
            bool shangyige = true;
            for (int i = 0; i < text_list.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(text_list[i]))
                {
                    if (shangyige)
                    {
                        continue;
                    }
                    shangyige = true;
                }
                else
                {
                    shangyige = false;
                }
                Text += text_list[i] + "\n";
            }
            Text = Text.Trim();
        }

        public void addKeyValues(string k, string v)
        {
            k = k.ToUpper();
            if (KeyValues.ContainsKey(k))
            {
                KeyValues[k] += " | " + v;
            }
            else
            {
                KeyValues.Add(k, v);
            }
        }

        public string show(int show_type = 1)
        {
            string o_str = "";
            if (show_type == 1)
            {
                o_str = string.Format("{0} | {1}", OID, Title);
            }
            else if (show_type == 2)
            {
                o_str = string.Format("{0}: {1}\n", OID, Title);
                foreach (KeyValuePair<string,string> keyValue in KeyValues)
                {
                    o_str += string.Format("  - {0}: \"{1}\"\n", keyValue.Key, keyValue.Value);
                }
                o_str += "<TEXT>\n";
                o_str += Text;
            }
            return o_str;
        }
    }

    public class LiteratureCollection
    {
        public Dictionary<string, Literature> Literatures = new Dictionary<string, Literature>();

        public void addLiterature(string[] texts)
        {
            Literature literature = new Literature(texts);
            addLiterature(literature);
        }

        public void addLiterature(Literature literature)
        {
            if (Literatures.ContainsKey(literature.OID))
            {
                Literatures[literature.OID] = literature;
            }
            else
            {
                Literatures.Add(literature.OID, literature);
            }
        }

        public int readFromMarkDown(string filename)
        {
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(filename);
            string line = sr.ReadLine();

            while (line != null)
            {
                if (line.StartsWith("# "))
                {
                    if (list.Count > 0)
                    {
                        addLiterature(list.ToArray());
                        list.Clear();
                    }
                }

                list.Add(line);
                line = sr.ReadLine();
            }
            if (list.Count > 0)
            {
                addLiterature(list.ToArray());
                list.Clear();
            }

            sr.Close();
            return 0;
        }

        public void saveToMarkDown(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            foreach (string oid in Literatures.Keys)
            {
                Literature literature = Literatures[oid];
                sw.Write("# ");
                sw.Write(literature.Title);
                sw.WriteLine("\n\n``` YAML");
                foreach (string key in literature.KeyValues.Keys)
                {
                    sw.Write(key);
                    sw.Write(": ");
                    sw.Write(literature.KeyValues[key]);
                    sw.Write("\n");
                }
                sw.WriteLine("```\n");
                if (literature.Text != "")
                {
                    sw.WriteLine(literature.Text);
                    sw.WriteLine();
                }
            }
            sw.Close();
        }

        public void saveToExcel(string filename)
        {
            string[] names = getYamlNames();

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            IRow row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("N");
            for (int c = 0; c < names.Length; c++)
            {
                cell = row.CreateCell(c + 1);
                cell.SetCellValue(names[c]);
            }
            cell = row.CreateCell(names.Length + 1);
            cell.SetCellValue("MarkDown");

            string[] oids = Literatures.Keys.ToArray();
            for (int i = 0; i < Literatures.Count; i++)
            {
                Literature literature = Literatures[oids[i]];
                row = sheet.CreateRow(i + 1);
                cell = row.CreateCell(0);
                cell.SetCellValue(i + 1);
                for (int j = 0; j < names.Length; j++)
                {
                    cell = row.CreateCell(j + 1);
                    if (literature.KeyValues.ContainsKey(names[j]))
                    {
                        cell.SetCellValue(literature.KeyValues[names[j]]);
                    }
                }
                cell = row.CreateCell(names.Length + 1);
                cell.SetCellValue(literature.Text);
            }

            using (FileStream fs = File.OpenWrite(filename))
            {
                workbook.Write(fs);
            }
        }

        public string[] getYamlNames()
        {
            List<string> list = new List<string>();
            foreach (string oid in Literatures.Keys)
            {
                Literature literature = Literatures[oid];
                foreach (string key in literature.KeyValues.Keys)
                {
                    if (!list.Contains(key))
                    {
                        list.Add(key);
                    }
                }
            }
            return list.ToArray();
        }

        public void addFromRISFile(string ris_fn)
        {
            // "D:\GroupMeeting\Articles\Read\Temp\S0012821X21005859.ris"
            Literature literature = FromRISFile(ris_fn);

            addLiterature(literature);
        }

        public static Literature FromRISFile(string ris_fn)
        {
            Literature literature = new Literature();
            StreamReader sr = new StreamReader(ris_fn);
            string line = sr.ReadLine();
            while (line != null)
            {
                line = line.Trim();
                if (line == "")
                {
                    continue;
                }
                if (line == "ER  -")
                {
                    break;
                }

                KeyValuePair<string, string> kv = risLineToKV(line);
                if (kv.Key != "")
                {
                    if (kv.Key == "T1")
                    {
                        literature.Title = kv.Value;
                        literature.addKeyValues("Title", kv.Value);
                    }
                    else if (kv.Key == "AU")
                    {
                        literature.addKeyValues("AUTHOR", kv.Value);
                    }
                    else if (kv.Key == "JO")
                    {
                        literature.addKeyValues("journal", kv.Value);
                    }
                    else if (kv.Key == "PY")
                    {
                        literature.addKeyValues("date", kv.Value);
                    }
                    else if (kv.Key == "DA")
                    {
                        literature.addKeyValues("date", kv.Value);
                    }
                    else if (kv.Key == "DO")
                    {
                        literature.addKeyValues("doi", kv.Value);
                    }
                    else if (kv.Key == "UR")
                    {
                        literature.addKeyValues("url", kv.Value);
                    }
                    else if (kv.Key == "KW")
                    {
                        literature.addKeyValues("keywords", kv.Value);
                    }
                    else if (kv.Key == "AB")
                    {
                        literature.addKeyValues("abstract", kv.Value);
                    }
                    else
                    {
                        literature.addKeyValues(kv.Key, kv.Value);
                    }
                }

                line = sr.ReadLine();
            }
            sr.Close();
            return literature;
        }

        private static KeyValuePair<string, string> risLineToKV(string line)
        {
            int n = line.IndexOf('-');
            if (n < 0)
            {
                return new KeyValuePair<string, string>("", "");
            }
            string k = line.Substring(0, n);
            k = k.Trim();
            string v = line.Substring(n + 1);
            v = v.Trim();
            return new KeyValuePair<string, string>(k, v);
        }

        public void show(int show_type = 1)
        {
            int n = 1;
            foreach (Literature literature in Literatures.Values)
            {
                Console.Write("{0,2}: ", n++);
                Console.WriteLine(literature.show(show_type));
            }
        }
    }

    public class LiteratureRunConfig
    {
        public string mkfn = null;
        public string initdirname = null;

        public static LiteratureRunConfig FromXMLFile(string xml_fn)
        {
            if (File.Exists(xml_fn))
            {
                using (FileStream fs = File.OpenRead(xml_fn))
                {
                    try
                    {
                        XmlSerializer XmlSer = new XmlSerializer(typeof(LiteratureRunConfig));
                        LiteratureRunConfig config = (LiteratureRunConfig)XmlSer.Deserialize(fs);
                        return config;
                    }
                    catch
                    {

                    }
                }
                return new LiteratureRunConfig();
            }
            else
            {
                return new LiteratureRunConfig();
            }
        }

        public static void saveToXMLFile(LiteratureRunConfig config, string xml_fn)
        {
            using (FileStream fs = File.OpenWrite(xml_fn))
            {
                //try
                //{
                XmlSerializer XmlSer = new XmlSerializer(typeof(LiteratureRunConfig));
                XmlSer.Serialize(fs, config);
                //}
                //catch
                //{

                //}
            }
        }
    }

    public class LiteratureRun
    {
        public string thisDir = AppDomain.CurrentDomain.BaseDirectory;
        public string configXMLFile;
        public LiteratureRunConfig config;
        public LiteratureCollection literatureCollection = new LiteratureCollection();

        public LiteratureRun()
        {
            configXMLFile = Path.Combine(thisDir, "lr_config.xml");
            config = LiteratureRunConfig.FromXMLFile(configXMLFile);
        }

        public void Usage()
        {
            Console.WriteLine("lr [*options] *locations");
            Console.WriteLine("    --yaml_keys");
            Console.WriteLine("    --add title");
            Console.WriteLine("    --ris ris_file");
            Console.WriteLine("    --show opt:oid");
            Console.WriteLine("    --md md_file");
            Console.WriteLine("    --excel excel_file");
            Console.WriteLine("    --back");
            Console.WriteLine("    --help");
        }

        public void Run(string[] args)
        {
            getMarkDownFileName();
            backDay();
            Console.WriteLine(config.mkfn);
            if (args.Length == 0)
            {

                return;
            }

            Dictionary<string, string> kvs = new Dictionary<string, string>((int)(args.Length / 2) + 2);
            List<string> locs = new List<string>(args.Length);
            List<string> options = new List<string>(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--"))
                {
                    options.Add(args[i]);
                }
                else if (args[i].StartsWith("-") & i < args.Length - 1)
                {
                    kvs.Add(args[i], args[++i]);
                }
                else
                {
                    locs.Add(args[i]);
                }
            }

            if (options.Count == 0)
            {
                Console.WriteLine("options not know.", options);
                return;
            }

            if (options[0] == "--back")
            {
                back();
                return;
            }

            if (options[0] == "--help")
            {
                Usage();
                return;
            }

            literatureCollection.readFromMarkDown(config.mkfn);

            if (options[0] == "--yaml_keys")
            {
                yaml_keys();
            }
            else if (options[0] == "--add")
            {
                add(locs);
            }
            else if (options[0] == "--ris")
            {
                ris(locs);
            }
            else if (options[0] == "--show")
            {
                show(locs);
            }
            else if (options[0] == "--md")
            {
                savetomarkdown(locs);
            }
            else if (options[0] == "--excel")
            {
                savetomarkexcel(locs);
            }
            else
            {
                Console.WriteLine("{0} not know.", options[0]);
            }

            literatureCollection.saveToMarkDown(config.mkfn);
            LiteratureRunConfig.saveToXMLFile(config, configXMLFile);

        }

        private void savetomarkexcel(List<string> locs)
        {
            if (locs.Count < 1)
            {
                Console.WriteLine("Error: can not find excel file.");
            }
            else
            {
                try
                {
                    literatureCollection.saveToExcel(locs[0]);
                    Console.WriteLine("Save to {0}", locs[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void savetomarkdown(List<string> locs)
        {
            if (locs.Count < 1)
            {
                Console.WriteLine("Error: can not find markdown file.");
            }
            else
            {
                try
                {
                    literatureCollection.saveToMarkDown(locs[0]);
                    Console.WriteLine("Save to {0}", locs[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void show(List<string> locs)
        {
            if (locs.Count < 1)
            {
                literatureCollection.show(1);
            }
            else
            {
                if (literatureCollection.Literatures.ContainsKey(locs[0]))
                {
                    Console.WriteLine(literatureCollection.Literatures[locs[0]].show(2));
                }
                else
                {
                    Console.WriteLine("Can not find OID={0}", locs[0]);
                }
            }
        }

        private void ris(List<string> locs)
        {
            if (locs.Count < 1)
            {
                Console.WriteLine("Error: can not find ris file.");
            }
            else
            {
                Literature literature = LiteratureCollection.FromRISFile(locs[0]);
                if (literature.Title == "")
                {
                    Console.WriteLine("Can not find title in file {0}", locs[0]);
                }
                else
                {
                    literatureCollection.addLiterature(literature);
                    Console.WriteLine("Add Literature Title of \"{0}\"", literature.Title);
                }
            }
        }

        private void add(List<string> locs)
        {
            if (locs.Count < 1)
            {
                Console.WriteLine("Error: can not find Title.");
            }
            else
            {
                Literature literature = new Literature();
                literature.Title = locs[0];
                literatureCollection.addLiterature(literature);
                Console.WriteLine(literature.show(1));
            }
        }
        
        private void yaml_keys()
        {
            string[] names = literatureCollection.getYamlNames();
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine(names[i]);
            }
        }

        private void getMarkDownFileName()
        {
            if (config.mkfn == null)
            {
                Console.WriteLine("Please enter markdown filename:");
                while (true)
                {
                    string line = Console.ReadLine();
                    line = Path.GetFullPath(line);
                    line = Path.ChangeExtension(line, ".md");
                    string dirname = Path.GetDirectoryName(line);
                    if (!Directory.Exists(dirname))
                    {
                        try
                        {
                            Directory.CreateDirectory(dirname);
                        }
                        catch
                        {
                            Console.WriteLine("Warning: Can not create directory {0} and enter again", dirname);
                            continue;
                        }
                    }
                    config.mkfn = line;
                    config.initdirname = dirname;
                    File.WriteAllText(line, "");
                    LiteratureRunConfig.saveToXMLFile(config, configXMLFile);
                    break;
                }
            }
        }

        private void back()
        {
            string back_1 = Path.Combine(config.initdirname, "back");
            if (!Directory.Exists(back_1))
            {
                Directory.CreateDirectory(back_1);
            }
            string back_2 = Path.Combine(back_1, "markdowns");
            if (!Directory.Exists(back_2))
            {
                Directory.CreateDirectory(back_2);
            }
            string backfilename = DateTime.Now.ToString("yyyyMMddHHmmss");
            backfilename += ".md";
            backfilename = Path.Combine(back_2, backfilename);

            if (File.Exists(config.mkfn))
            {
                File.Copy(config.mkfn, backfilename);
            }
            else
            {
                Console.WriteLine("Warning: can not find markdown file {0}", config.mkfn);
            }
        }

        private void backDay()
        {
            string back_1 = Path.Combine(config.initdirname, "back");
            if (!Directory.Exists(back_1))
            {
                Directory.CreateDirectory(back_1);
            }
            string back_2 = Path.Combine(back_1, "days");
            if (!Directory.Exists(back_2))
            {
                Directory.CreateDirectory(back_2);
            }
            string backfilename = DateTime.Now.ToString("yyyyMMdd");
            backfilename += "000000.md";
            backfilename = Path.Combine(back_2, backfilename);

            if (File.Exists(config.mkfn))
            {
                File.Copy(config.mkfn, backfilename, true);
            }

        }
    }
}
