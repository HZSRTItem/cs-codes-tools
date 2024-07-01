/*------------------------------------------------------------------------------
 * File    : SRTRecord
 * Time    : 2023/8/1 13:24:35
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTRecord]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Net.Http;
using System.Xml;

namespace CURLCSA
{
    public class SRTRecord
    {
        public DateTime time;
        public string timeString;
        public string text = "";
        public int isDelete = 0;
        public int n = 0;
        public string forget = "NONE";

        public SRTRecord(DateTime time, string text)
        {
            this.time = time;
            this.text = text;
            this.timeString = time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public SRTRecord()
        {
        }

        public string show(int show_type)
        {
            return show(show_type, n);
        }

        public string show(int show_type, int n)
        {
            
            if (show_type == 1)
            {
                string out_str = string.Format("{0,3}: ", n);
                out_str += time.ToString("yyyy-MM-dd HH:mm:ss");
                out_str += " | ";
                string text_tmp2 = text.Replace('|', ' ');
                int n_tmp2 = lengthString(text_tmp2);
                string text_tmp;

                if (n_tmp2 >= 76)
                {
                    text_tmp = subString(text_tmp2, 73);
                    text_tmp += "...";
                }
                else
                {
                    text_tmp = text_tmp2;
                }
                out_str += text_tmp;
                return out_str;
            }
            else if (show_type == 2)
            {
                string out_str = string.Format("<{0}: ", n);
                out_str += time.ToString("yyyy-MM-dd HH:mm:ss");
                out_str += "> \n";
                string text_tmp2 = text.Replace('|', '\n');
                out_str += text_tmp2;
                return out_str;
            }

            return null;
        }

        static int lengthString(string text)
        {
            int n = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if ((int)text[i] > 127)
                {
                    n += 2;
                }
                else
                {
                    n += 1;
                }
            }
            return n;
        }

        static string subString(string text, int n_sub)
        {
            int n = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if ((int)text[i] > 127)
                {
                    n += 2;
                }
                else
                {
                    n += 1;
                }
                if (n >= n_sub)
                {
                    string text_tmp = text.Substring(0, i);
                    text = text_tmp;
                    break;
                }
            }
            return text;
        }
    }

    public class SRTRecordCollection
    {
        public Dictionary<string, SRTRecord> collections = new Dictionary<string, SRTRecord>();

        public string add(SRTRecord record)
        {
            if (collections.ContainsKey(record.timeString))
            {
                collections[record.timeString] = record;
            }
            else
            {
                collections.Add(record.timeString, record);

            }
            return record.timeString;
        }

        public SRTRecord[] ToArray()
        {
            return collections.Values.ToArray();
        }

        public void show(int show_type)
        {
            int n = 1;
            foreach (string item in collections.Keys)
            {
                if (collections[item].isDelete == 0)
                {
                    string out_string = collections[item].show(show_type, n);
                    if (out_string != null)
                    {
                        Console.WriteLine(out_string);
                        ++n;
                    }
                }
            }
        }

        public void show(SRTRecord[] records, int show_type)
        {
            if (records == null)
            {
                return;
            }
            for (int i = 0; i < records.Length; i++)
            {
                string out_string = records[i].show(show_type, records[i].n);
                if (out_string != null)
                {
                    Console.WriteLine(out_string);
                }
            }
        }

        public SRTRecord[] find(string find_str, int n_doing)
        {
            List<SRTRecord> records = new List<SRTRecord>();
            int n = 0;
            foreach (string item in collections.Keys)
            {
                if (collections[item].isDelete == 0)
                {
                    n++;
                    if (collections[item].text.Contains(find_str))
                    {
                        SRTRecord r = collections[item];
                        r.n = n;
                        records.Add(r);
                    }
                }
            }

            SRTRecord[] records_tmp = null;
            if (Math.Abs(n_doing) >= records.Count)
            {
                records_tmp = records.ToArray();
            }
            else
            {
                records_tmp = new SRTRecord[Math.Abs(n_doing)];
                if (n_doing > 0)
                {
                    for (int i = 0; i < n_doing; i++)
                    {
                        records_tmp[i] = records[i];
                    }
                }
                else
                {
                    int doing_index = 0;
                    for (int i = records.Count - 1; i >= 0; i--)
                    {
                        records_tmp[doing_index++] = records[i];
                        if (doing_index == records_tmp.Length)
                        {
                            break;
                        }
                    }
                    Array.Reverse(records_tmp);
                }
            }
            return records_tmp;
        }

        public SRTRecord delete(int n_doing)
        {
            string del_key = _get(ref n_doing);

            if (del_key != null)
            {
                SRTRecord record = new SRTRecord()
                {
                    time = collections[del_key].time,
                    timeString = collections[del_key].timeString,
                    text = collections[del_key].text,
                    isDelete = 1,
                    n = collections[del_key].n
                };
                collections[del_key] = record;
                return record;
            }
            return collections[del_key];
        }

        private string _get(ref int n_doing)
        {
            int n = 0;
            foreach (string item in collections.Keys)
            {
                if (collections[item].isDelete == 0)
                {
                    n++;
                }
            }
            if (n_doing < 0)
            {
                n_doing += n + 1;
            }

            string del_key = null;
            if (n_doing <= 0 | n_doing > n)
            {
                del_key = null;
            }
            else
            {
                n = 0;
                foreach (string item in collections.Keys)
                {
                    if (collections[item].isDelete == 0)
                    {
                        n++;
                        if (n == n_doing)
                        {
                            del_key = item;
                            break;
                        }
                    }
                }
            }

            return del_key;
        }

        public SRTRecord[] gets(int n_doing)
        {
            List<SRTRecord> records = new List<SRTRecord>();
            int n = 0;
            foreach (string item in collections.Keys)
            {
                if (collections[item].isDelete == 0)
                {
                    n++;
                    collections[item].n = n;
                    SRTRecord r = collections[item];
                    r.n = n;
                    records.Add(r);
                }
            }

            SRTRecord[] records_tmp;
            if (Math.Abs(n_doing) >= records.Count)
            {
                records_tmp = records.ToArray();
            }
            else
            {
                records_tmp = new SRTRecord[Math.Abs(n_doing)];
                if (n_doing > 0)
                {
                    n = 0;
                    for (int i = 0; i < records.Count; i++)
                    {
                        records_tmp[n++] = records[i];
                        if (n == n_doing)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    n = 0;
                    for (int i = records.Count - 1; i >= 0; i--)
                    {
                        records_tmp[n++] = records[i];
                        if (n == records_tmp.Length)
                        {
                            break;
                        }
                    }
                    Array.Reverse(records_tmp);
                }
            }
            return records_tmp;
        }

        public SRTRecord get(int n_doing)
        {
            string k = _get(ref n_doing);
            if (k != null)
            {
                return collections[k];
            }
            return null;
        }

        public void Sort()
        {
            Dictionary<string, SRTRecord> collections_tmp = collections.OrderBy(o => o.Value.time).ToDictionary(o => o.Key, p => p.Value);
            collections = collections_tmp;
        }

        public SRTRecord this[int i]
        {
            get { return get(i); }
        }

        public static SRTRecord[] loadXMLFile(Stream xml_stream)
        {
            XmlTextReader xtr = new XmlTextReader(xml_stream);
            SRTRecord record = null;
            List<SRTRecord> records = new List<SRTRecord>();
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element)
                {
                    switch (xtr.Name)
                    {
                        case "SRTRecord":
                            record = new SRTRecord();
                            break;

                        case "time":
                            record.time = DateTime.Parse(xtr.ReadElementContentAsString());
                            break;

                        case "timeString":
                            record.timeString = xtr.ReadElementContentAsString();
                            record.timeString = record.timeString.Trim();
                            break;

                        case "text":
                            record.text = xtr.ReadElementContentAsString();
                            record.text = record.text.Trim();
                            break;

                        case "isDelete":
                            record.isDelete = int.Parse(xtr.ReadElementContentAsString());
                            break;

                        case "n":
                            record.n = int.Parse(xtr.ReadElementContentAsString());
                            break;

                        case "forget":
                            record.forget = xtr.ReadElementContentAsString();
                            record.forget = record.forget.Trim();
                            break;

                        default:

                            break;
                    }
                }

                if (xtr.NodeType == XmlNodeType.EndElement)
                {
                    if (xtr.Name == "SRTRecord")
                    {
                        if (record != null)
                        {
                            records.Add(record);
                            Console.WriteLine(record.show(1));
                        }
                    }
                }
            }
            xtr.Close();
            Console.Read();

            return records.ToArray();
        }

        public static SRTRecord[] loadXMLFile(string xml_fn)
        {
            using (FileStream fs = File.OpenRead(xml_fn))
            {
                return loadXMLFile(fs);
            }
        }

        public static void saveXMLFile(string xml_fn, SRTRecord[] records)
        {
            using (FileStream fs = File.OpenWrite(xml_fn))
            {
                saveXMLFile(fs, records);
            }
        }

        public static void saveXMLFile(Stream xml_stream, SRTRecord[] records)
        {

        }
    }

    public class SRTRecordRun
    {
        private string recordFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "srtrecords.xml");
        private readonly XmlSerializer XmlSer = new XmlSerializer(typeof(SRTRecord[]));
        private SRTRecordCollection collection = new SRTRecordCollection();

        public async Task runAsync(string[] argv)
        {
            DateTime dt = DateTime.Now;
            backRecordFileName();
            await loadRecordAsync();

            readRecordsFromFile(recordFileName);
            readRecordsFromFile(recordFileName + "-update");

            //readOldRecordFile(@"D:\CodeProjects\CppProj\Record\srtrecord\Release\srt_record_tmp.txt");

            collection.Sort();
            saveToFile(recordFileName);

            if (argv.Contains("--help"))
            {
                Usage();
            }
            else
            {
                DefaultRun(argv, dt);
            }

            saveToFile(recordFileName);
            await updateRecordAsync();
        }

        private void Usage()
        {
            Console.WriteLine("sr [*options] ...");
            Console.WriteLine("@not options:");
            Console.WriteLine("    number find string");
            Console.WriteLine("    number del");
            Console.WriteLine("@args:");
            Console.WriteLine("    -f file: add full filename");
            Console.WriteLine("    -st n: show type 1|2|3 ");
            Console.WriteLine("@options:");
            Console.WriteLine("    --help");
            Console.WriteLine("    --not-forget forget help");
            Console.WriteLine("    ");
            Console.WriteLine("    ");
        }

        private void DefaultRun(string[] argv, DateTime dt)
        {
            string find_str = null;
            string doing_str = null;
            string front_str = "";
            int to_type = 1;
            int n_doing = 0;

            for (int i = 0; i < argv.Length; i++)
            {
                if (argv[i] == "find" & i < argv.Length - 1)
                {
                    find_str = argv[++i];
                    doing_str = "find";
                }
                else if (argv[i] == "del")
                {
                    doing_str = "del";
                }
                else if (argv[i] == "-f" & i < argv.Length - 1)
                {
                    front_str += Path.GetFullPath(argv[++i]);
                }
                else if (argv[i] == "-st" & i < argv.Length - 1)
                {
                    try
                    {
                        to_type = int.Parse(argv[++i]);
                    }
                    catch
                    {
                        to_type = 1;
                    }
                }
                else if (n_doing == 0)
                {
                    try
                    {
                        n_doing = int.Parse(argv[i]);
                    }
                    catch
                    {
                        n_doing = 0;
                    }
                }
            }

            if (n_doing == 0)
            {
                mkRecord(dt, front_str, null);
            }
            else
            {
                if (to_type == 3)
                {
                    SRTRecord r = collection.get(n_doing);
                    Console.WriteLine(r.show(2, r.n));
                }
                else
                {
                    SRTRecord[] records;
                    if (doing_str == "find")
                    {
                        Console.WriteLine(">>> Find ------");
                        records = collection.find(find_str, n_doing);
                    }
                    else if (doing_str == "del")
                    {
                        Console.WriteLine(">>> Delete ----");
                        SRTRecord record = collection.delete(n_doing);
                        if (record == null)
                        {
                            records = null;
                            Console.WriteLine("Can not find {0}", n_doing);
                        }
                        else
                        {
                            records = new SRTRecord[1];
                            records[0] = record;
                        }
                    }
                    else
                    {
                        records = collection.gets(n_doing);
                    }

                    collection.show(records, to_type);
                }
            }
        }

        private void backRecordFileName()
        {
            if (File.Exists(recordFileName))
            {
                File.Copy(recordFileName, recordFileName + "-back", true);
            }
        }

        private async Task loadRecordAsync()
        {
            string fn = recordFileName + "-update";
            if (File.Exists(fn))
            {
                File.Delete(fn);
            }
            await loadFromJianHuoYunAsync(fn, recordFileName);
        }

        private bool readRecordsFromFile(string filename)
        {
            if (filename == recordFileName)
            {
                if (!File.Exists(filename))
                {
                    string text = "<?xml version=\"1.0\"?><ArrayOfSRTRecord xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><SRTRecord><time>2023-08-01T18:27:04.4357349+08:00</time><timeString>2023-08-01 06:27:04</timeString><text>first</text><isDelete>0</isDelete><n>1</n></SRTRecord></ArrayOfSRTRecord>";
                    File.WriteAllText(filename, text);
                }
            }
            if (File.Exists(filename))
            {
                using (FileStream fs = File.OpenRead(filename))
                {
                    try
                    {
                        //SRTRecord[] records = (SRTRecord[])XmlSer.Deserialize(fs);
                        SRTRecord[] records = SRTRecordCollection.loadXMLFile(fs);
                        for (int i = 0; i < records.Length; i++)
                        {
                            collection.add(records[i]);
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Warning: Can not load records from file {0}", filename);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void saveToFile(string fn)
        {
            if (File.Exists(fn))
            {
                File.Delete(fn);
            }
            using (FileStream fs = File.OpenWrite(fn))
            {
                SRTRecord[] records = collection.ToArray();
                XmlSer.Serialize(fs, records);
            }
        }

        private async Task updateRecordAsync()
        {
            await updateFromJianHuoYun(recordFileName);
        }

        private void mkRecord(DateTime dt, string front_str, string filename)
        {
            string text;
            if (filename == null)
            {
                text = readStdIn();
            }
            else
            {
                text = File.ReadAllText(filename);
            }
            if (front_str != "")
            {
                text += " ";
                text += front_str;
            }

            if (text != "")
            {
                SRTRecord record = new SRTRecord(dt, text);
                collection.add(record);
            }
        }

        static string readStdIn()
        {
            StringBuilder lines = new StringBuilder();
            while (true)
            {
                string line = Console.ReadLine();
                lines.Append(line);

                if (line == "")
                {
                    break;
                }
                else
                {
                    lines.Append("|");
                }
            }
            string lines_str = lines.ToString();
            lines_str = lines_str.Trim('|');

            return lines_str;
        }

        private void readOldRecordFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line = sr.ReadLine();
            char[] splits = new char[1];
            splits[0] = '|';
            while (line != null)
            {
                line = line.Trim();
                if (line == "")
                {
                    continue;
                }
                string[] lines = line.Split(splits, 2);
                Console.WriteLine(line);
                DateTime dt = DateTime.Parse(lines[0]);
                if (lines[1][0] == ' ')
                {
                    string tmp = lines[1].Substring(1);
                    lines[1] = tmp;
                }
                SRTRecord record = new SRTRecord(dt, lines[1]);
                collection.add(record);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        static async Task loadFromJianHuoYunAsync(string to_fn, string record_fn)
        {
            string responseBody = "";
            try
            {
                HttpClient client = new HttpClient();
                string fn = Path.GetFileName(record_fn);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://dav.jianguoyun.com/dav/Data/sr/" + fn);
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("970203199@qq.com:abzkgs73rxtkqnvx")));
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                StreamWriter sr = new StreamWriter(to_fn);
                sr.Write(responseBody);
                sr.Close();
            }
            catch
            {
                Console.WriteLine("Warning: Can not load records from HTTP.");
            }
        }

        static async Task updateFromJianHuoYun(string record_fn)
        {
            string responseBody = "";
            try
            {
                HttpClient client = new HttpClient();
                string fn = Path.GetFileName(record_fn);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://dav.jianguoyun.com/dav/Data/sr/" + fn);
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("970203199@qq.com:abzkgs73rxtkqnvx")));
                request.Content = new ByteArrayContent(File.ReadAllBytes(record_fn));
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                Console.WriteLine("Warning: Can not update records to HTTP.");
            }
        }

    }
}
