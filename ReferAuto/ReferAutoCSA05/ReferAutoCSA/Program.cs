using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReferAutoCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 0)
            {
                try
                {
                    ReferRis.DcodeFile(args[0]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                for (int i = 0; i < ReferRis.ReferRiss.Count; i++)
                {
                    try
                    {
                        Console.WriteLine(ReferRis.ReferRiss[i].GB_T7714_2015(i + 1));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            //D:\SpecialProjects\ReferAuto\ReferAutoCSA05\temp\savedrecs.txt

            Console.WriteLine("\nsrt_referris [ris file]");
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            //string[] lines = File.ReadAllLines(@"t01.txt");
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string[] line = lines[i].Split('\t');
            //    Console.WriteLine(string.Format("/// <summary>"));
            //    Console.WriteLine(string.Format("/// {0} {1}", line[0], line[2]));
            //    Console.WriteLine(string.Format("/// </summary>"));
            //    Console.WriteLine(string.Format("public string[] {0} = new string[1]", line[3].Replace(" ", "")) + " { \"\" };");
            //}

            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string[] line = lines[i].Split('\t');
            //    Console.WriteLine(string.Format("case \"{0}\":", line[0]));
            //    Console.WriteLine(string.Format("{0} = AddOneAttr({0}, info);", line[3].Replace(" ", "")));
            //    Console.WriteLine(string.Format("return true;\n"));
            //}

            //for (int i = 0; i < lines.Length; i++)
            //{
            //    //string[] line = lines[i].Split('\t');
            //    Console.WriteLine(string.Format("Console.WriteLine(\"{0}\");", lines[i]));
            //}

            //switch (lines[0])
            //{
            //    case "sd":
            //        string t;
            //        break;

            //    default:
            //        break;
            //}
            //Console.ReadLine();
        }
    }
}
