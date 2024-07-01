using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRTFmtArg;

namespace SRTFmtArgs
{

    class Program
    {
        static void Main(string[] args)
        {
            SRTArgCollection srte = new SRTArgCollection();
            List<string> str_list = new List<string>(66);
            srte.Name = "srt_test";
            srte.Description = "This is a test of a procedure, and it is uncertain whether it will succeed.";
            // 测试位置参数
            str_list.Add("-m3");
            str_list.Add("-info3_1");
            str_list.Add("-m2");
            str_list.Add("-info2");
            str_list.Add("-m3");
            str_list.Add("-info3_2");

            srte.Add("location1");
            str_list.Add("info:location1");
            srte.Add("location2", help_info: "location2 help info", max_number: 3);
            str_list.Add("info:location2_1");
            str_list.Add("info:location2_2");
            str_list.Add("info:location2_3");
            srte.Add("location3", is_optional: true);
            str_list.Add("info:location3");
            srte.Add("location4", max_number: 2, is_optional: true);
            str_list.Add("info:location4");
            str_list.Add("info:location4_2");
            str_list.Add("info:location4_3");
            // 测试标签参数
            srte.Add("markinfo1", arg_type:SRTArgType.MarkInfo);
            str_list.Add("-markinfo1");
            str_list.Add("-info1");
            srte.Add("markinfo2", mark_name:"m2", arg_type: SRTArgType.MarkInfo);

            srte.Add("markinfo3", mark_name: "m3", max_number:3, arg_type: SRTArgType.MarkInfo);

            srte.Add("markinfo4", mark_name: "m4", max_number: 2, arg_type: SRTArgType.MarkInfo, is_optional:true);
            str_list.Add("-m4");
            str_list.Add("-info4_1");
            str_list.Add("-m4");
            str_list.Add("-info4_2");
            // 测试bool参数
            srte.Add("bool1", arg_type: SRTArgType.Bool);
            str_list.Add("--bool1");
            srte.Add("bool2", mark_name: "b2", arg_type: SRTArgType.Bool);
            str_list.Add("--b2");
          
            srte.Add("in_raster_fn", help_info: "input raster file name", max_number: 256);
            srte.Add("out_fn", help_info: "out put file name", mark_name: "o", arg_type: SRTArgType.MarkInfo, max_number: 3);
            srte.Add("in_ft", help_info: "input file type", arg_type: SRTArgType.MarkInfo, max_number: 1);
            srte.Add("debug", help_info: "is debug", arg_type: SRTArgType.Bool, max_number: 3);
            for (int i = 0; i < str_list.Count; i++)
            {
                Console.Write(str_list[i] + " ");
            }
            Console.WriteLine("\n");
            Console.WriteLine(srte.Usage());
            srte.FmtArgs(str_list.ToArray());
            string s1 = srte["markinfo2"][0];
            int t = srte["markinfo2"].Count;
        }

    }

}
