using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocxCSA;
using System.Drawing;
using System.IO;

namespace DocxAutoCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[1] { @"D:\SpecialProjects\DocxAuto\DocxAutoCSA02\temp\t01.md" };
            bool is_build = true;
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Error: number of in params not enough");
                    is_build = false;
                }
                else if (args.Length == 1)
                {
                    if (File.Exists(args[0]))
                    {
                        if (Path.GetExtension(args[0]) == ".md")
                        {
                            Console.WriteLine("mark down file: " + args[0]);
                            string out_file = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(args[0]) + ".docx");
                            Console.WriteLine("docx file: " + out_file);
                            Md2Doc.Fit(args[0], out_file);
                        }
                        else
                        {
                            Console.WriteLine("Error: in file have to be markdown file");
                            is_build = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: in file not find");
                        is_build = false;
                    }
                }
                else
                {
                    if (File.Exists(args[0]))
                    {
                        if (Path.GetExtension(args[0]) == ".md")
                        {
                            if (Path.GetExtension(args[1]) == ".docx")
                            {
                                Console.WriteLine("mark down file: " + args[0]);
                                string out_file = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
                                Console.WriteLine("docx file: " + out_file);
                                Md2Doc.Fit(args[0], out_file);
                            }
                            else
                            {
                                Console.WriteLine("Error: out file have to be docx file");
                                is_build = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: in file have to be markdown file");
                            is_build = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: in file not find");
                        is_build = false;
                    }
                }
            }
            catch (Exception ex)
            {
                is_build = false;
                Console.WriteLine("Error: " + ex.Message.Trim());
            }

            //Md2Doc.Fit(@"D:\code\cs\DocxCSA01\temp\t01.md", @"D:\code\cs\DocxCSA01\temp\t01.docx");
            if (is_build)
            {
                Console.WriteLine("success");
            }
            Console.WriteLine("srt_md2docx [markdown file] [/docx file]");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }
    }
}
