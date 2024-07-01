using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ImageTypeConversionCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            NewMethod(args);
        }

        private static void NewMethod(string[] args)
        {
            string in_filename = null;
            Bitmap bitmap = null;
            string fmt = null;
            System.Drawing.Imaging.ImageFormat imageFormat = null;
            string save_filename = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-o" & i < args.Length - 1)
                {
                    save_filename = args[++i];
                }
                else if (args[i] == "-fmt" & i < args.Length - 1)
                {
                    imageFormat = GetImageFormat(args[++i]);
                    if (imageFormat == null)
                    {
                        Console.WriteLine("Error: can not convert image type to " + args[i++]);
                        Usage();
                        return;
                    }
                    fmt = args[i++];
                }
                else
                {
                    try
                    {
                        bitmap = new Bitmap(args[i]);
                        in_filename = args[i];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(ex.Message);
                        Usage();
                        return;
                    }
                }
            }

            if (bitmap == null)
            {
                Console.WriteLine("Error: can not find input image file name");
                Usage();
                return;
            }

            if (save_filename == null & imageFormat == null)
            {
                Console.WriteLine("Error: arg `-o` or `-fmt` have to get one of them");
                Usage();
                return;
            }

            if (save_filename == null)
            {
                save_filename = Path.Combine(Path.GetDirectoryName(in_filename)
                    , Path.GetFileNameWithoutExtension(in_filename) + "." + fmt.ToLower());
            }

            Console.WriteLine(save_filename);

            if (in_filename == save_filename)
            {
                return;
            }

            if (imageFormat == null)
            {
                bitmap.Save(save_filename);
            }
            else
            {
                bitmap.Save(save_filename, imageFormat);
            }
        }

        static System.Drawing.Imaging.ImageFormat GetImageFormat(string fmt_str)
        {
            fmt_str = fmt_str.ToUpper();
            if (fmt_str == "PNG")
            {
                return System.Drawing.Imaging.ImageFormat.Png;
            }
            else if (fmt_str == "JPEG")
            {
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            else if (fmt_str == "TIFF")
            {
                return System.Drawing.Imaging.ImageFormat.Tiff;
            }
            else if (fmt_str == "BMP")
            {
                return System.Drawing.Imaging.ImageFormat.Bmp;
            }
            else
            {
                return null;
            }
        }

        static void Usage()
        {
            Console.WriteLine("srt_imagetypeconvert image_file opt:-o opt:-fmt\n" +
                "    image_file: input image file name\n" +
                "    opt:-o: output image file name default:`image_file`.fmt\n" +
                "    -fmt: output file image type support:`PNG|JPEG|TIFF|BMP` defalut:-o\n" +
                "(C)Copyright 2023, ZhengHan. All rights reserved.");
        }

    }
}
