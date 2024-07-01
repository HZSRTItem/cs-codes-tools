/*------------------------------------------------------------------------------
 * File    : Program,cs
 * Time    : 2022-9-29 10:19:33
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : Class of
 * 
 * add: add a article
 *  article name: 
 *  info files: 
 *  
 * find: find a article
 *  article name: 
 *  
 * list: list articles
 *  
 * 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using JiebaNet.Segmenter;

namespace SrtArticleCSA
{
    class Program
    {
        private static SrtArt OSrtArt;

        static void Main(string[] args)
        {
            //string s = "Quantifying the spatiotemporal patterns of impervious\n surfaces (ISs) 量化不透水表面（ISs）的时空模式对于评估城市化对环境的影响至关重要。夜光 （NTL） 数据提供了一种在大比例上绘制城市 IS 的新方法。然而，这种方法的准确性目前很低，因此非常需要进一步改进。在这里，我们使用植被调整NTL城市指数（VANUI）估计了1992年至2009年中国的城市IS动态，该指数结合了NTL和归一化差值植被指数（NDVI）数据。借助VANUI，我们能够以比以前的方法更高的精度量化中国IS的时空模式。这种改进的关键是VANUI能够缓解与NTL数据固有的饱和问题。\n我们的研究表明，中国的城市IS总面积从10，614.23公里大幅增加。21992年为31，147.63公里22009年，以每年6.54%的速度增长。中国的城市IS扩张表现出明显的区域差异，其中六个大型“热点”地区是城市IS扩张最为实质性的地区。这些热点地区占中国国土总面积的0.87%，但占1992-2009年城市IS扩张总面积的37.66%。城市IS的测量不仅对于表征城市化模式和过程很重要，而且对于评估城市化的生态和环境影响也是必不可少的。我们的研究结果为更好地了解中国近期城市化的速度和范围，以及在中国及其他地区设计和开发可持续城市提供了宝贵的数据集和新见解。is crucial for assessing the environmental impacts of \nurbanization. " +
            //    "\nNighttime light (NTL) data provide a new way of mapping urban IS on broad scales. However, the accuracy of this approach is currently low and thus further improvements are much needed. Here we have estimated the urban IS dynamics of China from 1992 to 2009 using the Vegetation Adjusted量化不透水表面（ISs）的时空模式对于评估城市化对环境的影响至关重要。夜光 （NTL） 数据提供了一种在大比例上绘制城市 IS 的新方法。然而，这种方法的准确性目前很低，因此非常需要进一步改进。在这里，我们使用植被调整NTL城市指数（VANUI）估计了1992年至2009年中国的城市IS动态，该指数结合了NTL和归一化差值植被指数（NDVI）数据。借助VANUI，我们能够以比以前的方法更高的精度量化中国IS的时空模式。这种改进的关键是VANUI能够缓解与NTL数据固有的饱和问题。我们的研究表明，中国的城市IS总面积从10，614.23公里大幅增加。21992年为31，147.63公里22009年，以每年6.54%的速度增长。中国的城市IS扩张表现出明显的区域差异，其中六个大型“热点”地区是城市IS扩张最为实质性的地区。这些热点地区占中国国土总面积的0.87%，但占1992-2009年城市IS扩张总面积的37.66%。城市IS的测量不仅对于表征城市化模式和过程很重要，而且对于评估城市化的生态和环境影响也是必不可少的。我们的研究结果为更好地了解中国近期城市化的速度和范围，以及在中国及其他地区设计和开发可持续城市提供了宝贵的数据集和新见解。 NTL Urban Index (VANUI), which combines NTL and Normalized Difference Vegetation Index (NDVI) data. With VANUI, we were able to quantify the spatiotemporal patterns of IS in China with a much higher accuracy than previous methods. Key to this improvement was VANUI's ability to alleviate the problem of saturation inherently associated with NTL data. Our study shows that the total urban IS area of China increased substantially from 10,614.23 km2 in 1992 to 31,147.63 km2 in 2009, at an annual increase rate of 6.54%. China's urban IS expansion exhibited pronounced regional differences, with six large “hotspot” areas where urban IS expanded most substantially. These hotspot regions accounted for 0.87% of China's total land area, but 37.66% of the total area of urban IS expansion during 1992–2009. Measures of urban IS are not only important for characterizing urbanization patterns and processes, but also essential for assessing ecological and environmental impacts of urbanization. Our results provide a valuable dataset and new insights for better understanding the speed and scope of China's recent urbanization, as well as for designing and developing sustainable cities in China and beyond.";
            //string ss = UUtils.FmtStr(s, n: 4);
            //Console.WriteLine(s);
            //Console.WriteLine("\n");
            //Console.WriteLine(ss);
            OSrtArt = new SrtArt();
            //args = new string[]
            //{
            //    "add", "Impervious surface growth and its inter-relationship with " +
            //    "vegetation cover and land surface temperature in peri-urban areas of Delhi",
            //    "-i", "LKF", @"D:\GroupMeeting\20221002\Article\1-s2.0-S2212095521000298-main.pdf",
            //    "-i", "DOI", "https://doi.org/10.1016/j.uclim.2021.100799",
            //    "Nighttime light (NTL) data provide a new way of mapping urban IS on broad scales. However, the accuracy of this approach is currently low and thus further improvements are much needed. Here we have estimated the urban IS dynamics of China from 1992 to 2009 using the Vegetation Adjusted",
            //    "德里市郊不透水地表生长及其与植被覆盖和地表温度的相互关系",
            //    "已经添加到endnote中"
            //};
            //args = new string[]
            //{
            //    "find"
            //};
            if (args.Length == 0)
            {
                Usage();
            }
            else
            {
                if (args[0] == "add")
                {
                    Add(args);
                }
                else if (args[0] == "find")
                {
                    Find(args);
                }
                else if (args[0] == "delete")
                {
                    Find(args);
                }
                else
                {
                    Console.WriteLine("Not find mark: {0}\n", args[0]);
                    Usage();
                }
            }
            Console.WriteLine("End");
            //Console.ReadLine();
        }

        private static bool Usage()
        {
            Console.WriteLine("srt_article [add] [find] ***\n\n" +
                "    add:    [article title] [info*] [opt:-i mark info]\n" +
                "            +info [n] [mark] [info]\n\n" +
                "    find:   [opt:-n + n] \n" +
                "            -n [n] find number==n and print information\n\n" +
                "    delete: +info");
            return true;
        }

        private static bool Add(string[] args)
        {
            if (args.Length < 2)
            {
                return false;
            }

            if (args[1] == "info")
            {
                if (args.Length < 5)
                {
                    Console.WriteLine("Add info args number is not enough");
                    return false;
                }
                int n = int.Parse(args[2]);
                OSrtArt.AddInfo(n, args[3], args[4]);
            }
            else
            {
                string art_title = "";
                List<string> infos = new List<string>(20);

                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i] == "-i" & i < args.Length - 2)
                    {
                        infos.Add(UUtils.MarkInfoToStr(args[i + 1], args[i + 2]));
                        i += 2;
                    }
                    else if (art_title == "")
                    {
                        art_title = args[i];
                    }
                    else
                    {
                        infos.Add(UUtils.MarkInfoToStr("INFO", args[i]));
                    }
                }
                string[] tt = infos.ToArray();
                if (infos.Count == 0)
                {
                    tt = null;
                }
                OSrtArt.Add(art_title, infos: tt);
            }

            return true;
        }

        private static bool Find(string[] args)
        {
            if (args.Length <= 1)
            {
                OSrtArt.PrintAll();
                return false;
            }

            string art_title = "";
            

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-n")
                {
                    int n = int.Parse(args[++i]);
                    OSrtArt.PrintFindInfo(n);
                    return true;
                }
                else if (art_title == "")
                {
                    art_title = args[i];
                }

            }

            return true;
        }

        private static bool Delete(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Number of args is not enough");
                Console.WriteLine("Usage:\n    delete info [n] [n of marks]");
                return false;
            }

            if(args[1] == "info")
            {
                if (args.Length <4)
                {
                    Console.WriteLine("number of `delete info [n] [n of marks]` args is not enough");
                    return false;
                }
                try
                {
                    int n = int.Parse(args[2]);
                    int n_mark = int.Parse(args[3]);
                    OSrtArt.DeleteInfo(n, n_mark);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return true;
        }
    }

    class StaticInfo
    {
        /// <summary>
        /// 文章保存的路径
        /// </summary>
        public static string ArticleDirName = @"D:\GroupMeeting\Articles";
        //public static string ArticleDirName = @"D:\code\cs\SrtArticleCSA\SrtArticleCSA\bin\Debug\Test";
        /// <summary>
        /// 信息文件
        /// </summary>
        public static string InfoFileName = Path.Combine(ArticleDirName, "Info.xml");
        /// <summary>
        /// 初始的可添加的全部的文献量
        /// </summary>
        public static int N_ARTS_INIT = 1000;
        /// <summary>
        /// 初始可添加的信息的条数
        /// </summary>
        public static int N_INFOS_INIT = 100;
        /// <summary>
        /// 初始标签的数量
        /// </summary>
        public static int n_MARKS_INIT = 100;

    }


}
