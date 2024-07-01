using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmtCSA01
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                return;
            }
            string info_fn = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "md2docx_info.txt");
            //Md2DocxManager md2DocxManager = new Md2DocxManager(@"D:\SpecialProjects\Fmt\md2docx_info.txt");
            Md2DocxManager md2DocxManager = new Md2DocxManager(info_fn);
            try
            {
                md2DocxManager.saveToMdFile(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void Usage()
        {
            Console.WriteLine("srt_mkmd2docx [opt:out.md]\n" +
                "(C)Copyright 2023, ZhengHan. All rights reserved.\n");
        }
    }
}
