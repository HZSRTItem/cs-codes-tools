using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReferAutoWFA
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                ReferHZ.DcodeRefFile(args[0]);
                Console.WriteLine(ReferHZ.GB_T7714_2015());
            }

            //ReferHZ.DcodeRefFile("savedrecs.txt");
            //Console.WriteLine(ReferHZ.GB_T7714_2015());

            Console.WriteLine("> srt_refer [refer file]");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            //Console.ReadLine();
        }
    }
}
