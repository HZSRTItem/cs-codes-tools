using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtFileFmtCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            SrtFileFmt srtFileFmt = new SrtFileFmt(@"t01.txt");
            srtFileFmt.Open(SrtFileOpenOpts.Append);
            srtFileFmt.WriteHelpInfo("------------------------------------------------------------------------------");
            srtFileFmt.WriteHelpInfo(" * File    : t01.txt ");
            srtFileFmt.WriteHelpInfo(" * Author  : Zheng Han ");
            srtFileFmt.WriteHelpInfo(" * Contact : hzsongrentou1580@gmail.com ");
            srtFileFmt.WriteHelpInfo(" * License : (C)Copyright 2022, ZhengHan. All rights reserved.");
            srtFileFmt.WriteHelpInfo("------------------------------------------------------------------------------");
            srtFileFmt.WriteLine("markinfo0", "1234511111111");
            srtFileFmt.WriteLine("markinfo0", "2341");
            srtFileFmt.WriteLine("111111111241111");
            srtFileFmt.WriteLine( "11113511111111");
            srtFileFmt.WriteLine("markinfo1", "1111111114541111");
            srtFileFmt.WriteLine("ma2nfo0", "11111111111111111");
            srtFileFmt.Close();

            srtFileFmt.Open(SrtFileOpenOpts.Read);
            srtFileFmt.SetActivityMark("markinfo0");
            string line = srtFileFmt.GetLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = srtFileFmt.GetLine();
            }
            Console.WriteLine("vvvvvv");
            Console.ReadLine();
        }
    }
}
