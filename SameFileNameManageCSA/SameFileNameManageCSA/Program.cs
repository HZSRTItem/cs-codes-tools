using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameFileNameManageCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(Usage());
                return;
            }

            string file_name = null;
            string out_dir = null;
            bool is_cut = false;
            bool is_overwrite = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o_dir" & i < args.Length - 1)
                {
                    
                }
            }

        }

        static string Usage()
        {
            string usage = "srt_samefile [file_name] [opt:-o_dir +dirname] [opt:--cut] [opt:--overwrite]\n" +
                "    file_name: file to same dir\n" +
                "    [opt:-o_dir +dirname]: out dirname default:in_file_dir\n" +
                "    [opt:--cut]: whether to manage by cutting default:false\n" +
                "    [opt:--overwrite]: whether to write in overwrite mode default:false";
            return usage;
        }
    }
}
