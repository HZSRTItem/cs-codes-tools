using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRTFmtArg;
using CodeFileManager_V1;
using System.IO;

namespace CodeFileManagerCSA
{
    class Program
    {

        static void Main(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();
            sarg_coll.Name = "srt_cfm";
            sarg_coll.Description = "Manage written code files.\n" +
                "    Use `srt_cfm mark --h` to get help for each mark[add|find|load|update]";
            sarg_coll.Add("add", help_info: "Add a code file");
            sarg_coll.Add("find", help_info: "Find a code file and show file info");
            sarg_coll.Add("load", help_info: "Load a code file into a folder");
            sarg_coll.Add("update", help_info: "Update a file in the code base");

            //args = new string[] { "find", "tf", "-n", "2", "-ext", ".cs" };
            //args = new string[] { "load", @"sutils.py" };
            string config_fn = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"cfm_config.txt");
            if (File.Exists(config_fn))
            {
                CONST_VAR.CODE_FILE_DIR = File.ReadAllText(config_fn);
                CONST_VAR.CODE_FILE_DIR = CONST_VAR.CODE_FILE_DIR.Trim();
            }
            else
            {
                Console.WriteLine("Can not find config file: " + config_fn);
                return;
            }

            if (args.Length < 1)
            {
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            CodeFileManager cfm = new CodeFileManager();

            if (args[0] == "add")
            {
                cfm.Add(args);
            }
            else if (args[0] == "find")
            {
                cfm.Find(args);
            }
            else if (args[0] == "load")
            {
                cfm.Load(args);
            }
            else if (args[0] == "update")
            {
                cfm.Update(args);
            }
            else
            {
                Console.WriteLine("Can not format arg: `{0}`", args[0]);
                Console.WriteLine(sarg_coll.Usage());
            }

        }


    }
}
