/*------------------------------------------------------------------------------
 * File    : Program.cs
 * Time    : 2022-4-15 20:40:52
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of
 * 
   版权声明：本文为CSDN博主「CodeRecently」的原创文章，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
   原文链接：https://blog.csdn.net/weixin_44790046/article/details/103879605
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuZuZhuanTuPianCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            //string text = "djhWH0K980";    //将获取到的字符串赋值到text字符串中
            for (int k = 0; k < 10000; k++)
            {
                Bitmap bmp = new Bitmap(200, 200);      //定义画布大小
                for (int i = 0; i < 200; i++)
                {
                    for (int j = 0; j < 200; j++)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(200, 200, 253));
                    }
                }
                //Graphics g = Graphics.FromImage(bmp);      //封装一个GDI+绘图图面
                //Random r = new Random();
                //g.Clear(ColorTranslator.FromHtml("#FFF"));  //背景色为白色
                //for (int j = 0; j < 10; j++)
                //{
                //    String[] fonts = { "微软雅黑", "Viner Hand ITC", "Tempus Sans ITC", "汉仪长艺体简", "汉仪双线体简", "汉仪花蝶体简" };//随机设置字体的样式
                //    Point p = new Point((j + 1) * 38, 80);  //每个字母的坐标
                //    Color[] colors = { Color.Red, Color.Green, Color.Black, Color.Yellow, Color.LightSkyBlue, Color.Blue };//随机设置字体的颜色
                //    g.DrawString(text[j].ToString(), new Font(fonts[r.Next(fonts.Length)], 40, FontStyle.Bold), new SolidBrush(colors[r.Next(colors.Length)]), p);//画图
                //}
                string strFullName = string.Format(@".\t01\test{0}.jpg", k);  //存储位置+图片名
                bmp.Save(strFullName, ImageFormat.Jpeg);    //以指定的格式保存图片文件
                bmp.Dispose();
                if(k%100 == 0)
                {
                    Console.Write(k);
                    Console.Write(" ");
                    Console.WriteLine(DateTime.Now - dateTime);
                }
            }

            Console.WriteLine(DateTime.Now - dateTime);
            Console.ReadLine();
        }
    }
}
