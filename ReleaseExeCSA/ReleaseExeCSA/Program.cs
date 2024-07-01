using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReleaseExeCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("srt_release exe_file code_bin_name");
                return;
            }

            try
            {
                string exe_name = Path.GetFullPath(args[0]);
                string code_bin_name = args[1];
                string code_bin_filename = Path.Combine(@"D:\code\bin", code_bin_name + ".bat");
                StreamWriter sr = new StreamWriter(code_bin_filename);
                sr.WriteLine("@\"" + exe_name + "\" %*");
                sr.Close();
                Console.WriteLine("Exe Name: " + exe_name);
                Console.WriteLine("Release Name: " + code_bin_name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

       
        }
    }
}
