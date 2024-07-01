/*------------------------------------------------------------------------------
 * File    : Program.cs
 * Time    : 2022/10/28 17:08:38
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Class1]
 * 写一个可以保存写过的代码的文件
 * 
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SaveCodesCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string info = "";
            string code_type = "";
            string code_file = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-type" & i < args.Length - 1)
                {
                    code_type = args[++i];
                }
                else if (args[i] == "--help")
                {
                    Console.WriteLine(Usage());
                    return;
                }
                else if (args[i] == "-f" & i < args.Length - 1)
                {
                    code_file = args[++i];
                }
                else if (args[i] == "--all")
                {
                    FindAllFile();
                    return;
                }
                else
                {
                    info += args[i] + " ";
                }
            }

            DateTime GMTime = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            string save_file = Path.Combine(SAVEDIR, GMTime.ToString("yyyy-MM-dd") + ".md");

            StreamWriter sw = new StreamWriter(save_file, append: true);
            string line = "";

            if (code_file != "")
            {
                if (File.Exists(code_file))
                {
                    sw.WriteLine("\n# " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                    sw.WriteLine("");
                    info = "> Code File: " + Path.GetFullPath(code_file);
                    switch (Path.GetExtension(code_file))
                    {
                        case ".py":
                            code_type = "Python";
                            break;
                        case ".c":
                            code_type = "C";
                            break;
                        case ".cpp":
                            code_type = "C++";
                            break;
                        case ".js":
                            code_type = "Java Script";
                            break;
                        case ".java":
                            code_type = "Java";
                            break;
                        case ".html":
                            code_type = "Html";
                            break;
                        case ".css":
                            code_type = "CSS";
                            break;
                        case ".cs":
                            code_type = "C#";
                            break;
                        case ".h":
                            code_type = "C";
                            break;
                        default: break;
                    }

                    sw.WriteLine("");
                    if (info != "")
                    {
                        sw.WriteLine(info);
                        sw.WriteLine("");
                    }
                    sw.Write("``` " + code_type);
                    StreamReader sr = new StreamReader(code_file);
                    while (line != null)
                    {
                        sw.WriteLine(line);
                        line = sr.ReadLine();
                    }
                    sw.WriteLine("``` ");
                    sr.Close();
                    sw.Close();
                    Console.WriteLine("Have been save to `{0}`\n", save_file);
                }
                else
                {
                    Console.WriteLine("Can not find code file " + code_file);
                }
            }
            else
            {
                List<string> lines = new List<string>(1000);
                int iline = 1;
                Console.WriteLine("Please input the code to save codes. * input `--exit` to stop");
                if (code_type != "")
                {
                    Console.WriteLine("- Code Type: " + code_type);
                }
                Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
                while (line != "--exit")
                {
                    Console.Write("{0, 3}> ", iline);
                    lines.Add(line);
                    line = Console.ReadLine();
                    //Console.WriteLine("Out[{0}]: {1}", iline, line);
                    iline++;
                }
                sw.WriteLine("\n# " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                sw.WriteLine("");
                if (info != "")
                {
                    sw.WriteLine(info);
                    sw.WriteLine("");
                }
                sw.Write("``` " + code_type);
                foreach (string item in lines)
                {
                    sw.WriteLine(item);
                }
                sw.WriteLine("``` ");
                sw.Close();
                Console.WriteLine("Have been save to `{0}`\nPress any key to end the program... ", save_file);
                Console.Read();
            }

        }

        static string SAVEDIR = @"D:\Utils\SaveCodes";

        static string Usage()
        {
            string usage = "srt_addcodes info* [-type c|c++|python|c#|matlab|...]\n" +
                "    [--help] [-f code file] [--all]\n" +
                "    [--all]: find all save code files into a file\n" +
                "(C)Copyright 2022, ZhengHan. All rights reserved.";
            return usage;
        }

        static void FindAllFile()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(SAVEDIR);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            List<FileInfo> f = new List<FileInfo>(fileInfos);
            f.AsEnumerable().OrderBy(s => s.FullName).ToList();
            string save_f = SAVEDIR + ".md";
            StreamWriter sw = new StreamWriter(save_f);
            for (int i = 0; i < f.Count; i++)
            {
                StreamReader sr = new StreamReader(f[i].FullName);
                sw.WriteLine(sr.ReadToEnd());
                sr.Close();
            }
            sw.Close();
            Console.WriteLine("Have been save to file `{0}`", save_f);
        }
    }
}
