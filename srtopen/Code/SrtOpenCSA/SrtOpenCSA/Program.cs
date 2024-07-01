using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SrtOpenCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgsFmt argsFmt = new ArgsFmt();
            argsFmt.Descinfos = "Open some commonly used documents";
            argsFmt.AddArgs("file name", "the name of the document to be opened");
            argsFmt.AddArgs("--l", "Get information about all open files");
            argsFmt.AddArgs("--help", "Get help information");
            argsFmt.Fit(args);

            if (argsFmt.GetArgOne("--l") == " ")
            {
                DirectoryInfo directoryinfo = new DirectoryInfo(@"D:\srtopen");
                FileInfo[] fileInfo = directoryinfo.GetFiles();
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    Console.WriteLine((i + 1).ToString() + " " + Path.GetFileNameWithoutExtension(fileInfo[i].Name));
                }
                return;
            }

            if (argsFmt.GetArgOne("--help") == " ")
            {
                Console.WriteLine(argsFmt.GetHelpInfo("> srt_useopen"));
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                return;
            }

            string file0 = argsFmt.GetArgOne();
            if (file0 != "")
            {
                try
                {
                    int n = int.Parse(file0);
                    DirectoryInfo directoryinfo = new DirectoryInfo(@"D:\srtopen");
                    FileInfo[] fileInfo = directoryinfo.GetFiles();
                    FileInfo fileInfo1 = new FileInfo(argsFmt.GetArgOne());
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        if (n == i + 1)
                        {
                            Process.Start(fileInfo[i].FullName);
                            Console.WriteLine("open: " + fileInfo[i].FullName);
                            return;
                        }
                    }
                }
                catch
                {
                    DirectoryInfo directoryinfo = new DirectoryInfo(@"D:\srtopen");
                    FileInfo[] fileInfo = directoryinfo.GetFiles();
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        if (file0 == Path.GetFileNameWithoutExtension(fileInfo[i].Name))
                        {
                            Process.Start(fileInfo[i].FullName);
                            return;
                        }
                    }
                }

                Console.WriteLine("not find: " + file0);
            }

            Console.WriteLine(argsFmt.GetHelpInfo("> srt_useopen"));
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }
    }
}
