using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackFileCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[]
            //{
            //    @"D:\GroupMeeting\20220731"
            //};

            // -dir
            //Console.WriteLine("\n");
            BackFileM(args);
            Console.WriteLine("End\n(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        private static void BackFileM(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("srt_backfile [file] [opt:-bdir back dir] [opt: -y] ");
                return;
            }

            SRTBackFile backFile = new SRTBackFile();
            bool b_y = false;
            string back_file = null;
            string back_dir = null;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-y")
                {
                    b_y = true;
                }
                else if (args[i] == "-bdir" & i<args.Length-1)
                {
                    back_dir = args[i];
                    i++;
                }
                else
                {
                    back_file = args[i];
                }
            }
            if (back_file == null)
            {
                Console.WriteLine("End");
                return;
            }
            back_file = Path.GetFullPath(back_file);
            backFile.Back(back_file, b_y, back_dir);
        }

        private static void NewMethod(string[] args)
        {
            string back_dir = Path.GetFullPath("back");
            // 没有标识的
            List<string> back_file_dir = new List<string>(16);

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-dir" & i < args.Length - 1)
                {
                    back_dir = args[i++];
                }
                else
                {
                    back_file_dir.Add(Path.GetFullPath(args[i]));
                }
            }

            // 当前时间保存的备份文件夹
            string back_dir_save = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            back_dir_save = Path.Combine(back_dir, back_dir_save);
            if (!Directory.Exists(back_dir_save))
            {
                Directory.CreateDirectory(back_dir_save);
            }
            if (!Directory.Exists(back_dir))
            {
                Directory.CreateDirectory(back_dir);
            }
            // 备份当前文件夹下的所有内容
            if (back_file_dir.Count == 0)
            {
                Director(Directory.GetCurrentDirectory(), back_dir_save);
            }
            // 备份指定的文件夹或文件
            for (int i = 0; i < back_file_dir.Count; i++)
            {
                string s_name = back_file_dir[i];
                string to_name = Path.Combine(back_dir_save, Path.GetFileName(s_name));
                if (File.Exists(s_name))
                {
                    File.Copy(s_name, to_name);
                }
                if (Directory.Exists(s_name))
                {
                    Director(s_name, to_name);
                }
            }
            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
            //Console.ReadLine();
        }

        private static void Director(string in_dir, string to_name)
        {
            if (!Directory.Exists(to_name))
            {
                Directory.CreateDirectory(to_name);
            }

            string root_dir = Path.GetDirectoryName(to_name);
            Console.WriteLine("mkdir: " + in_dir + " -> " + to_name);
            FileInfo[] fsinfos;
            DirectoryInfo[] directoryInfos;

            try
            {
                DirectoryInfo d = new DirectoryInfo(in_dir);
                fsinfos = d.GetFiles();
                directoryInfos = d.GetDirectories();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message.Replace("\n", "    \n"));
                return;
            }

            foreach (FileInfo item in fsinfos)
            {
                try
                {
                    string s_fname = Path.Combine(in_dir, item.Name);
                    string to_fname = Path.Combine(to_name, item.Name);
                    if (File.Exists(to_fname))
                    {
                        File.Delete(to_fname);
                    }
                    File.Copy(s_fname, to_fname);
                    Console.WriteLine("    " + item.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message.Replace("\n", "    \n"));
                }
            }

            foreach (DirectoryInfo item in directoryInfos)
            {
                if (Path.GetFileName(item.FullName) != "back")
                {
                    Director(item.FullName, Path.Combine(to_name, Path.GetFileName(item.FullName)));
                }
            }
        }
    }
}
