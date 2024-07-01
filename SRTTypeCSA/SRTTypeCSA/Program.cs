using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SRTFmtArg;

namespace SRTTypeCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            SRTArgCollection sarg = new SRTArgCollection();
            sarg.Name = "srt_type";
            sarg.Description = "Type text file to cmd";
            sarg.Add("file", "in text file");
            sarg.Add("out_file", "output text file", is_optional: true, arg_type: SRTArgType.MarkInfo, mark_name: "o");
            sarg.FmtArgs(args);

            string in_fn = sarg["file"][0];
            string out_fn = sarg["out_file"][0];

            if (in_fn == null)
            {
                Console.WriteLine("Can not find in file.");
                Console.WriteLine(sarg.Usage());
            }
            else
            {
                if (File.Exists(in_fn))
                {
                    Encoding encoding = EnCodingFmt.GetEncoding(args[0]);
                    string text = File.ReadAllText(in_fn, encoding);
                    if (out_fn != null)
                    {
                        try
                        {
                            File.WriteAllText(out_fn, text, encoding);
                        }
                        catch
                        {
                            Console.WriteLine("Can not make file: " + out_fn);
                            Console.WriteLine(sarg.Usage());
                        }
                    }
                    else
                    {
                        Console.WriteLine(text);
                    }
                }
                else
                {
                    Console.WriteLine("Can not find file: " + args[0]);
                    Console.WriteLine(sarg.Usage());
                }
            }
           
        }


    }

    class EnCodingFmt
    {
        /// <summary>
        /// 获取文件的编码格式
        /// </summary>
        public static Encoding GetEncoding(string file_name)
        {
            FileStream fs = new FileStream(file_name, FileMode.Open, FileAccess.Read);
            Encoding r = GetEncoding(fs);
            fs.Close();
            return r;
        }

        /// <summary>
        /// 通过给定的文件流，判断文件的编码类型
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        private static Encoding GetEncoding(FileStream fs)
        {
            //文件的字符集在Windows下有两种，一种是ANSI，一种Unicode。
            //对于Unicode，Windows支持了它的三种编码方式，一种是小尾编码（Unicode)，一种是大尾编码(BigEndianUnicode)，一种是UTF - 8编码。
            //byte[] Unicode = new byte[] { 0xFF, 0xFE };
            //byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF };
            //byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //BOM头

            if (fs.Length < 3)
                return Encoding.Default;

            byte[] bytes = new byte[3];
            fs.Read(bytes, 0, 3);

            Encoding reVal = Encoding.GetEncoding("GB2312");

            if (bytes[0] == 0xFE && bytes[1] == 0xFF)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (bytes[0] == 0xFF && bytes[1] == 0xFE)
            {
                reVal = Encoding.Unicode;
            }
            else
            {
                if (!(bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF))
                {
                    fs.Position = 0;
                }
                if (IsUTF8Bytes(fs))
                {
                    if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                        reVal = new UTF8Encoding(false);
                    else
                        reVal = Encoding.UTF8;
                }
            }

            return reVal;
        }

        private static byte UTF8_BYTE_MASK = 0b1100_0000;
        private static byte UTF8_BYTE_VALID = 0b1000_0000;
        private static bool IsUTF8Bytes(FileStream fs)
        {
            //BinaryReader r = new BinaryReader(fs);
            byte[] bytes = new byte[1];
            fs.Read(bytes, 0, 1);

            //1字节 0xxxxxxx
            //2字节 110xxxxx 10xxxxxx
            //3字节 1110xxxx 10xxxxxx 10xxxxxx
            //4字节 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
            //5字节 111110xx 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx
            //6字节 1111110x 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx 10xxxxxx
            while (fs.Read(bytes, 0, 1) > 0)
            {
                if (bytes[0] < 0x80)
                    continue;

                int cnt = 0;
                byte b = bytes[0];
                while ((b & 0b1000_0000) != 0)
                {
                    cnt++;
                    b <<= 1;
                }
                cnt -= 1;

                for (int i = 0; i < cnt; i++)
                {
                    if (fs.Read(bytes, 0, 1) <= 0)
                        return false;
                    if ((bytes[0] & UTF8_BYTE_MASK) != UTF8_BYTE_VALID)
                        return false;
                }
            }

            return true;
        }
    }
}
