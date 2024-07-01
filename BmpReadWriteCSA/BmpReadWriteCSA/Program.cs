using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
namespace BmpReadWriteCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string bmp_file = @"QQ截图20220803215033.png";
            // BmpRW bmpRW = new BmpRW(@"D:\SpecialProjects\TfrecordRW\TfrInfoCSA\TfrInfoCSA\bin\Debug\net5.0\t0.bmp");
            Bitmap bitmap = new Bitmap(@"t0.bmp");

            //byte[] d = new byte[60000];
            //for (int i = 0; i < 100; i++)
            //{
            //    for (int j = 0; j < 200; j++)
            //    {
            //        int n = (200 * i + j) * 3;
            //        d[n] = (byte)(200 - i % 20);
            //        d[n + 1] = 50;
            //        d[n + 2] = 20;
            //    }
            //}
            //DateTime dateTime = DateTime.Now;
            //for (int k = 0; k < 5000; k++)
            //{
            //    BmpRW bmpRW1 = new BmpRW(200, 100);
            //    bmpRW1.Save("t0.bmp", d);
            //    Bitmap bitmap1 = new Bitmap("t0.bmp");
            //    bitmap1.Save($".\\t1\\t_{k}.png", ImageFormat.Png);
            //    bitmap1.Dispose();
            //}
            //Console.WriteLine(DateTime.Now - dateTime);
            //Console.ReadLine();
        }

        /// <summary>
        /// 将BitMap转换成bytes数组
        /// </summary>
        /// <param name="bitmap">要转换的图像</param>
        /// <returns></returns>
        private static byte[] BitmapToByte(Bitmap bitmap)
        {
            // 1.先将BitMap转成内存流
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            // 2.再将内存流转成byte[]并返回
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Dispose();
            return bytes;
        }
    }
}
