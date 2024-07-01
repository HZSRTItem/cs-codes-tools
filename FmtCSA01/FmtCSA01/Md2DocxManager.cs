/*------------------------------------------------------------------------------
 * File    : Md2DocxManager
 * Time    : 2023/2/12 16:38:52
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[Md2DocxManager]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRTReadWriteCSA;
using System.IO;

namespace FmtCSA01
{
    class Md2DocxManager
    {

        SRTInfoFileRW srtInforw;
        Dictionary<string, SRTInfo> kvInfo;


        public Md2DocxManager(string srt_info_fn)
        {
            srtInforw = new SRTInfoFileRW(srt_info_fn);
            kvInfo = srtInforw.readAsDict();
        }

        public void saveToMdFile(string md_fn)
        {
            File.WriteAllText(md_fn, strMd());
        }

        public string strMd()
        {
            string ss = "---\n";
            int i = 0;
            foreach (KeyValuePair<string, SRTInfo> item in kvInfo)
            {
                if(item.Key == "hz_refer")
                {
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        ss += "#" + item.Value[j].Substring(1) + "\n";
                    }
                }
                else
                {
                    if (item.Value.Count == 1)
                    {
                        ss += item.Key + ": " + item.Value[0] + "\n";
                    }
                    else
                    {
                        ss += item.Key + ": \n" ;
                        for (int j = 0; j < item.Value.Count; j++)
                        {
                            ss += " - " + item.Value[j] + "\n";
                        }
                    }
                }
                i++;
            }
            ss += "---";
            return ss;
        }
    }
}
