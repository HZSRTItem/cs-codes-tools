/*------------------------------------------------------------------------------
 * File    : SRTFileRW
 * Time    : 2023/1/7 15:10:19
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTFileRW]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QgisJYBuildWFA
{
    class SRTFileRW
    {
        public string FileName = "";
        StreamReader sr = null;
        StreamWriter sw = null;


        public SRTFileRW()
        {

        }

        public SRTFileRW(string filename)
        {
            FileName = filename;
        }

        public void initFromFile(string filename)
        {
            Close();
            FileName = filename;
        }

        public bool Open(string open_mode = "r")
        {
            Close();
            if(open_mode == "r")
            {
                sr = new StreamReader(FileName);
            }
            if (open_mode == "w")
            {
                sw = new StreamWriter(FileName);
            }
            return true;
        }

        public void WriteLine(string line="")
        {
            sw.WriteLine(line);
        }

        public void WriteLineMark(string line = "")
        {
            sw.WriteLine("");
            sw.WriteLine("> " + line);
            sw.WriteLine("");
        }

        public void WriteLineNote(string line = "")
        {
            sw.WriteLine("# " + line);
        }

        public void Close()
        {
            sr?.Close();
            sw?.Close();
            sr = null;
            sw = null;
        }
    }
}
