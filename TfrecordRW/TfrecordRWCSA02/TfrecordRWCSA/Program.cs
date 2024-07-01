using System;
using System.IO;
using Tensorflow;
using System.Collections;
using System.Collections.Generic;

namespace TfrecordRWCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "info")
            {
                IGetInfo(args);
            }
            else
            {
                Console.WriteLine("Error");
            }

            // if (args.Length == 0)
            // {
            //     return;
            // }
            // string tfrecord_file = Path.GetFullPath(args[0]); // "gba_spl17_test2_1.tfrecord"
            // string out_file = Path.GetFullPath(args[1]);
            // // Console.WriteLine(tfrecord_file);
            // // Console.WriteLine(out_file);
            // TfrecordUtils.TfrecordTo1Txt(tfrecord_file, out_file);
            // string ss = TfrecordUtils.GetInfo(@"D:\GraduationProject\Framework\1Sample\TestSample\JieYi\Data\gba_spl_test\gba_spl_test0.tfrecord");
            // Console.WriteLine(ss);
        }

        private static void IGetInfo(string[] args)
        {
            Console.WriteLine(TfrecordUtils.GetInfo(args[1]));
        }


    }
}
