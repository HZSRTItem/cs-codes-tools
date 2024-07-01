/*------------------------------------------------------------------------------
 * File    : SRTReadWrite
 * Time    : 2023/1/29 17:53:42
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTReadWrite]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SRTReadWriteCSA
{
    public class SRTFileRW
    {

        protected string SRTFileName = null;
        protected FileStream Fs = null;
        protected StreamReader RStream = null;
        protected StreamWriter WStream = null;
        protected BinaryReader RBStream = null;
        protected BinaryWriter WBStream = null;
        public SRTFileRW()
        {

        }

        public SRTFileRW(string srt_filename)
        {
            initFromFile(srt_filename);
        }

        public void initFromFile(string srt_filename)
        {
            SRTFileName = srt_filename;
        }

        /// <summary>
        /// open srt file
        /// </summary>
        /// <param name="open_mode">r/w/a/rb/wb</param>
        /// <returns>is open</returns>
        protected bool Open(string open_mode = "r")
        {
            Close();
            if (open_mode == "r")
            {
                RStream = new StreamReader(SRTFileName);
            }
            else if (open_mode == "w")
            {
                WStream = new StreamWriter(SRTFileName);
            }
            else if (open_mode == "a")
            {
                WStream = new StreamWriter(SRTFileName, true);
            }
            else if (open_mode == "rb")
            {
                Fs = new FileStream(SRTFileName, FileMode.Open, FileAccess.Read);
                RBStream = new BinaryReader(Fs);
            }
            else if (open_mode == "wb")
            {
                Fs = new FileStream(SRTFileName, FileMode.OpenOrCreate, FileAccess.Write);
                WBStream = new BinaryWriter(Fs);
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// close srt file
        /// </summary>
        protected void Close()
        {
            RStream?.Close();
            WStream?.Close();
            RBStream?.Close();
            WBStream?.Close();
            Fs?.Close();
            RStream = null;
            WStream = null;
            RBStream = null;
            WBStream = null;
            Fs = null;
        }

    }

    public class SRTInfoFileRW : SRTFileRW
    {
        /// <summary>
        /// Info Mark
        /// </summary>
        public string Mark = "";

        private List<string> infos = new List<string>(16);

        public SRTInfoFileRW(string srt_filename) : base(srt_filename)
        {

        }

        public SRTInfoFileRW() { }

        /// <summary>
        /// open srt file
        /// </summary>
        /// <param name="open_mode">r/w/a/rb/wb</param>
        /// <returns>is open</returns>
        public bool open(string open_mode = "r")
        {
            bool ret = base.Open(open_mode);
            Mark = "";
            return ret;
        }

        /// <summary>
        /// close srt file
        /// </summary>
        public void close()
        {
            base.Close();
            Mark = "";
        }

        /// <summary>
        /// read a line from file. not read Annotation line.
        /// If read mark line, record a mark and read next line.
        /// </summary>
        /// <returns>line</returns>
        public string readLine()
        {
            string line = RStream.ReadLine();

            while (line != null)
            {
                if (line.Trim() == "")
                {
                    line = RStream.ReadLine();
                }
                else if (line.Length > 0)
                {
                    line = line.Trim();
                    if (line[0] == '#')
                    {
                        line = RStream.ReadLine();
                    }
                    else
                    {
                        if (line == ">")
                        {
                            line = RStream.ReadLine();
                        }
                        else if (line[0] == '>')
                        {
                            string t = line.Substring(1);
                            Mark = t.Trim();
                            line = RStream.ReadLine();
                        }
                        else
                        {
                            return line;
                        }
                    }
                }
                else
                {
                    line = RStream.ReadLine();
                }
            }

            return line;
        }

        public string[] readByMark(string mark)
        {
            open();
            infos.Clear();
            string line = readLine();
            if (line == null)
            {
                close();
                return null;
            }
            else
            {
                while (line != null)
                {
                    if (Mark == mark)
                    {
                        infos.Add(line);
                    }
                }
            }
            close();
            if (infos.Count == 0)
            {
                return null;
            }
            else
            {
                string[] t = infos.ToArray();
                infos.Clear();
                infos.TrimExcess();
                return t;
            }
        }

        public Dictionary<string, SRTInfo> readAsDict(params string[] marks)
        {
            open();
            Dictionary<string, SRTInfo> dict = new Dictionary<string, SRTInfo>();
            if (marks.Length == 0)
            {
                string line = readLine();
                while (line != null)
                {
                    if (!dict.ContainsKey(Mark))
                    {
                        dict.Add(Mark, new SRTInfo());
                    }
                    dict[Mark].Add(line);
                    line = readLine();
                }
            }
            else
            {
                string line = readLine();
                while (line != null)
                {
                    if (marks.Contains(Mark))
                    {
                        if (!dict.ContainsKey(Mark))
                        {
                            dict.Add(Mark, new SRTInfo());
                        }
                        dict[Mark].Add(line);
                        line = readLine();
                    }
                }
            }
            foreach (KeyValuePair<string, SRTInfo> item in dict)
            {
                item.Value.ReduceMemory();
            }
            close();
            return dict;
        }

        public void saveAsDict(Dictionary<string, SRTInfo> dict)
        {
            open("w");
            foreach (KeyValuePair<string, SRTInfo> item in dict)
            {
                writeLineMark(item.Key);
                writeLine();
                for (int i = 0; i < item.Value.Count; i++)
                {
                    writeLine(item.Value[i]);
                }
                writeLine();
            }
            close();
        }

        public void writeLine(string line = "")
        {
            WStream.WriteLine(line);
        }

        public void writeLineAnnotation(string line)
        {
            WStream.Write("# ");
            WStream.WriteLine(line);
        }

        public void writeLineMark(string line = "")
        {
            WStream.Write("> ");
            WStream.WriteLine(line);
        }
    }

    public class SRTInfo
    {
        List<string> infos = new List<string>();

        public void Add(string info)
        {
            infos.Add(info);
        }

        public void Clear()
        {
            infos.Clear();
        }

        public string this[int i]
        {
            set { infos[i] = value; }
            get { return infos[i]; }
        }

        public int Count
        {
            get { return infos.Count; }
        }

        public void ReduceMemory()
        {
            infos.TrimExcess();
        }

        public string get(int i = 0)
        {
            return infos[i];
        }

        public int getAsInt(int i = 0)
        {
            return int.Parse(infos[i]);
        }

        public double getAsDouble(int i = 0)
        {
            return int.Parse(infos[i]);
        }

    }
}
