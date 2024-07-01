//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SRTFmtArgs
//{

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            if (args.Length < 3)
//            {
//                Console.WriteLine("srt_cscode prop data_type name\n" +
//                    "    prop: mark of this\n" +
//                    "    data_type: data type of property\n" +
//                    "    name: name of property\n" +
//                    "(C)Copyright 2023, ZhengHan. All rights reserved.");
//                return;
//            }
//            Console.WriteLine("please input comment for property `--exit` to end: ");
//            string infos = SRTReadLines.ReadLines();
//            Console.WriteLine("");
//            codeShuXing(infos, args[1], args[2]);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="notes"></param>
//        /// <param name="nei_name"></param>
//        /// <param name="wai_name"></param>
//        static void codeShuXing(string note, string data_type, string name)
//        {
//            string nei_name = "m_" + name;
//            string wai_name = name;
//            string initd = "null";
//            switch (data_type)
//            {
//                case "int":
//                case "double":
//                    initd = "0";
//                    break;

//                default:
//                    break;
//            }
//            string line = "";
//            // 内部属性
//            line += "/// <summary>";
//            string[] notes = note.Split('\n');
//            for (int i = 0; i < notes.Length; i++)
//            {
//                if (i == 0)
//                {
//                    line += $"\n/// Internal variable {name}: ";
//                }

//                if (notes[i].Trim() == "")
//                {
//                    continue;
//                }
//                line += "\n///     ";
//                line += notes[i].Trim();

//            }
//            line += "\n/// </summary>\n";
//            line += string.Format("private {0} {1} = {2};\n\n", data_type, nei_name, initd);
//            // 外部属性
//            line += "/// <summary>";
//            for (int i = 0; i < notes.Length; i++)
//            {
//                if (i == 0)
//                {
//                    line += $"\n/// Property {name}: ";
//                }
//                if (notes[i].Trim() == "")
//                {
//                    continue;
//                }
//                line += "\n///     ";
//                line += notes[i].Trim();
//            }
//            line += "\n/// </summary>\n";
//            line += string.Format("public {0} {1}", data_type, wai_name);
//            line += "\n{"
//                  + $"\n    get {{ return Get{name}(); }}"
//                  + $"\n    set {{ Set{name}(value);}}"
//                  + "\n}\n\n";
//            // 设置属性
//            line += "/// <summary>";
//            for (int i = 0; i < notes.Length; i++)
//            {
//                if (i == 0)
//                {
//                    line += $"\n/// Set {name}: ";
//                }
//                if (notes[i].Trim() == "")
//                {
//                    continue;
//                }
//                line += "\n///     ";
//                line += notes[i].Trim();
//            }
//            line += "\n/// </summary>";
//            line += "\n/// <param name=\"v_count\">external incoming variable</param>\n";
//            line += string.Format("private void Set{0}({1} v_{2})", name, data_type, name.ToLower());
//            line += "\n{"
//                  + $"\n{nei_name} = v_{name.ToLower()};"
//                  + "\n}\n\n";
//            // 获得属性
//            line += "/// <summary>";
//            for (int i = 0; i < notes.Length; i++)
//            {
//                if (i == 0)
//                {
//                    line += $"\n/// Get {name}: ";
//                }
//                if (notes[i].Trim() == "")
//                {
//                    continue;
//                }
//                line += "\n///     ";
//                line += notes[i].Trim();
//            }
//            line += "\n/// </summary>\n";
//            line += string.Format("private {0} Get{1}()", data_type, name);
//            line += "\n{"
//                  + $"\nreturn {nei_name};"
//                  + "\n}\n\n";
//            Console.WriteLine(line);
//        }
//    }

//}
