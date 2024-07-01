/*------------------------------------------------------------------------------
 * File    : Program
 * Time    : 2022-4-18 16:18:16
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of Main
 *      https://blog.csdn.net/zhushiq1234/article/details/52204587
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementCSA
{
    class Program
    {
        static void Main(string[] args)
        {

            //string look_dir = "C:\\";
            //string csv_file = "look.xlsx";
            //WriteAllFileInfo.Fit(look_dir, csv_file);

            string look_dir = "";
            string excel_file = "";
            if (args.Length == 0)
            {
                look_dir = Directory.GetCurrentDirectory();
                excel_file = Path.Combine(look_dir, Path.GetFileName(look_dir) + "_look.xlsx");
                WriteAllFileInfo.Fit(look_dir, excel_file);
                Console.WriteLine("Success");
            }
            else if (args.Length == 1)
            {
                if (Directory.Exists(args[0]))
                {
                    look_dir = args[0];
                    excel_file = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(Directory.GetCurrentDirectory()) + "_look.xlsx");
                    WriteAllFileInfo.Fit(look_dir, excel_file);
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Error: Look Dir Not Find");
                }
            }
            else if (args.Length == 2)
            {
                if (Directory.Exists(args[0]))
                {
                    look_dir = args[0];
                    excel_file = Path.GetFullPath(args[1]);
                    if (Directory.Exists(Path.GetDirectoryName(excel_file)))
                    {
                        excel_file = Path.GetFileNameWithoutExtension(excel_file) + ".xlsx";
                        WriteAllFileInfo.Fit(look_dir, excel_file);
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Error: Can not create out csv file");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Look Dir Not Find");
                }
            }

            Console.WriteLine("srt_lookdir [/ dir file] [/ out excel file]");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");

            //WriteAllFileInfo.Fit(@"D:\code\cs", "t01.xlsx");

            //Console.ReadLine();
        }

        //static void Director(string in_dir)
        //{
        //    DirectoryInfo d = new DirectoryInfo(in_dir);
        //    FileInfo[] fsinfos = d.GetFiles();
        //    DirectoryInfo[] directoryInfos = d.GetDirectories();
        //    foreach (FileInfo item in fsinfos)
        //    {

        //    }

        //    double aa = 0;
        //}      

    }
}
