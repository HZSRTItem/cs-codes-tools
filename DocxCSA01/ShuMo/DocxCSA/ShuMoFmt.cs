/*------------------------------------------------------------------------------
 * File    : ShuMoFmt
 * Time    : 2022/9/20 8:47:00
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ShuMoFmt]
 * 
 * 每个参赛队可以从A、B、C、D、E、F题中任选一题完成论文。
 * 论文题目和摘要写在论文摘要上，摘要页的下一页开始论文正文。
 * 论文从摘要页开始编写页码，页码必须位于每页页脚中部，用阿拉伯数字从“1 ”开始连续编号。
 * 论文不能有页眉，论文中不能有任何可能显示答题人身份的标志。
 * 论文题目用三号黑体字、一级标题用四号黑体字，并居中。论文中其他汉字一律采用小四号宋体字，
 *     行距用单倍行距。计算机结果和源程序需在规定时间内上传竞赛系统以备检查。
 * 请大家注意：摘要应该是一份简明扼要的详细摘要（包括关键词），请认真书写（注意篇幅一般
 *     不超过两页，且无需译成英文）。全国评阅时对摘要和论文都会审阅。
 * 引用别人的成果或其他公开的资料(包括网上甚至在“博客”上查到的资料) 必须按照规定的参考文
 *     献的表述方式在正文引用处和参考文献中明确列出。正文引用处用方括号标示参考文献的编号，如[1][3]等；
 *     引用书籍还必须指出页码。参考文献按正文中的引用次序列出，其中书籍的表述方式为：

------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocLib;
using System.Collections;
using Wd = Microsoft.Office.Interop.Word; // Microsoft Word 16.0 Object Library
using System.Reflection;
using System.IO;
using System.Drawing;

namespace DocxCSA
{


    /// <summary>
    /// markdown 文件转docx
    /// </summary>
    public class ShuMoFmt
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="mdFileName"></param>
        /// <param name="DocFileName"></param>
        public static void Fit(string mdFileName, object DocFileName)
        {
            // 读取md文件
            List<string[]> mdlines = ReadMd(mdFileName);
            // 构造图像列表
            ArrayList BodyList = FmtDoc(mdlines);
            // 函数缺省值，由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            object N = Missing.Value;
            // 如果文档文件已存在，则删除
            if (File.Exists((string)DocFileName))
            {
                File.Delete((string)DocFileName);
            }
            // Word应用程序变量
            Wd.ApplicationClass WordApp = new Wd.ApplicationClass();
            // Word文档变量
            Wd.Document WordDoc = WordApp.Documents.Add(ref N, ref N, ref N, ref N);
            // 文档工作过程可见
            WordApp.Visible = true;
            // 构造文档过程
            for (int i = 0; i < BodyList.Count; i++)
            {
                // 标题
                if (BodyList[i] is Heading)
                {
                    Heading bodyone = BodyList[i] as Heading;
                    bodyone.Add(WordDoc, WordApp);
                }
                // 主体
                if (BodyList[i] is Bodying)
                {
                    Bodying bodyone = BodyList[i] as Bodying;
                    bodyone.Add(WordDoc, WordApp);
                }
                // 图片
                if (BodyList[i] is Picturing)
                {
                    Picturing bodyone = BodyList[i] as Picturing;
                    bodyone.Add(WordDoc, WordApp);
                }
                // 参考文献
                if (BodyList[i] is ReferEnd)
                {
                    ReferEnd bodyone = BodyList[i] as ReferEnd;
                    bodyone.Add(WordDoc, WordApp);
                }
            }
            // WdSaveFormat为Word 2003文档的保存格式，office 2007就是wdFormatDocumentDefault
            //object format = Wd.WdSaveFormat.wdFormatDocument;
            // 将wordDoc文档对象的内容保存为DOCX文档
            //Console.WriteLine("2-----------------------");
            WordDoc.SaveAs(ref DocFileName, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N);
            // 关闭wordDoc文档对象，看是不是要打印 wordDoc.PrintOut();
            WordDoc.Close(ref N, ref N, ref N);
            // 关闭wordApp组件对象
            WordApp.Quit(ref N, ref N, ref N);
            // 打开文档
            //Console.WriteLine("1-----------------------");
            System.Diagnostics.Process.Start((string)DocFileName);
        }

        /// <summary>
        /// 格式化文档
        /// </summary>
        /// <param name="mdlines">md文件列表</param>
        /// <returns></returns>
        private static ArrayList FmtDoc(List<string[]> mdlines)
        {
            // 文档的主体列表
            ArrayList BodyList = new ArrayList();
            string[] line;
            for (int i = 0; i < mdlines.Count; i++)
            {
                line = mdlines[i];
                if (line.Length == 1)
                {
                    string[] line_mark = line[0].Split(' ');
                    if (line_mark[0] == "#")
                    {
                        BodyList.Add(new Heading(line[0].Substring(2), 1));
                    }
                    else if (line_mark[0] == "##")
                    {
                        BodyList.Add(new Heading(line[0].Substring(3), 2));
                    }
                    else if (line_mark[0] == "###")
                    {
                        BodyList.Add(new Heading(line[0].Substring(4), 3));
                    }
                    else if (line[0][0] == '!')  // 图片
                    {
                        BodyList.Add(new Picturing(line[0]));
                    }
                    else
                    {
                        BodyList.Add(new Bodying(line[0]));
                    }
                }
                else
                {
                    string line0 = "";
                    string line_j = "";
                    for (int j = 0; j < line.Length; j++)
                    {
                        line_j = line[j];
                        if (line_j[0] == '>')
                        {
                            if (line0.Length != 0)
                            {
                                if (line0[line0.Length - 1] == '。')
                                {
                                    line0 = line0.Substring(0, line0.Length - 1) + Refering.Fit(line_j.Substring(1)) + "。";
                                }
                                else
                                {
                                    line0 += Refering.Fit(line_j.Substring(1));
                                }
                            }
                        }
                        else
                        {
                            line0 += line_j;
                        }
                    }
                    BodyList.Add(new Bodying(line0));
                }
            }

            // 添加参考文献
            BodyList.Add(new Heading("参考文献", 1));
            for (int i = 0; i < Refering.refers.Count; i++)
            {
                BodyList.Add(new ReferEnd(string.Format("[{0}] {1}", i + 1, Refering.refers[i].Trim())));
            }
            return BodyList;
        }

        /// <summary>
        /// d读取markdown文件为数据列表
        /// </summary>
        /// <param name="mdFileName"></param>
        /// <returns></returns>
        private static List<string[]> ReadMd(string mdFileName)
        {
            // 原始行
            string[] lines = File.ReadAllLines(mdFileName);
            // 平衡空行
            List<string> md_lines0 = ReAjKongLine(lines);
            // 去除空行
            List<string> md_lines1 = RemoveKongLine(md_lines0);
            // 分段
            List<string[]> p_lines = FenDuan(md_lines1);
            return p_lines;
        }

        private static List<string[]> FenDuan(List<string> md_lines1)
        {
            List<string[]> p_lines = new List<string[]>();
            List<string> line = new List<string>();
            for (int i = 0; i < md_lines1.Count; i++)
            {
                if (md_lines1[i] == "")
                {
                    p_lines.Add(line.ToArray());
                    line.Clear();
                }
                else
                {
                    line.Add(md_lines1[i]);
                }
            }

            return p_lines;
        }

        private static List<string> RemoveKongLine(List<string> md_lines)
        {
            bool isqiankonghang = false;
            List<string> mdlines = new List<string>();
            for (int i = 0; i < md_lines.Count; i++)
            {
                if (md_lines[i] == "")
                {
                    if (isqiankonghang)
                    {
                        continue;
                    }
                    isqiankonghang = true;
                }
                else
                {
                    isqiankonghang = false;
                }

                mdlines.Add(md_lines[i]);
            }
            md_lines.Clear();

            //string ss = "";
            //for (int i = 0; i < mdlines.Count; i++)
            //{
            //    ss += mdlines[i] + "\n";
            //}

            if (mdlines[0] == "")
            {
                mdlines.RemoveAt(0);
            }
            return mdlines;
        }

        private static List<string> ReAjKongLine(string[] lines)
        {
            string line = "";
            List<string> md_lines = new List<string>();
            // 平衡空行
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i];
                if (line != "")
                {
                    // 标题
                    if (line[0] == '#')
                    {
                        md_lines.Add("");
                        md_lines.Add(line);
                        md_lines.Add("");
                    }
                    // 图片
                    else if (line[0] == '!')
                    {
                        md_lines.Add("");
                        md_lines.Add(line);
                        md_lines.Add("");
                    }
                    else if (line[0] == '*' |
                            line[0] == '+' |
                            line[0] == '-')
                    {
                        md_lines.Add("");
                        md_lines.Add(line.Substring(2));
                        md_lines.Add("");
                    }
                    else if (IsDigit(line.Split('.')[0]))
                    {
                        md_lines.Add("");
                        md_lines.Add(line);
                        md_lines.Add("");
                    }
                    else if (line.Length > 6)
                    {
                        if (line.Substring(0, 6) == "table:")
                        {
                            // 表格
                        }
                        md_lines.Add(line);
                    }
                    else
                    {
                        md_lines.Add(line);
                    }

                }
                else
                {
                    md_lines.Add(line);
                }
            }
            //string ss = "";
            //for (int i = 0; i < md_lines.Count; i++)
            //{
            //    ss += md_lines[i] + "\n";
            //}
            return md_lines;
        }

        private static bool IsDigit(string info)
        {
            try
            {
                Convert.ToInt16(info);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 标题
    /// </summary>
    public class Heading
    {
        public ParaOne p = new ParaOne();

        public static List<int[]> HeadNov = new List<int[]>();

        public Heading(string text, int nheading)
        {
            HeadNov.Add(new int[5]);
            if (HeadNov.Count == 1)
            {
                if (nheading == 1)
                {
                    HeadNov[HeadNov.Count - 1][0] = 1;
                }
                else
                {
                    throw new Exception("第一个标题必须为一级标题");
                }
            }
            else
            {
                if (nheading == 1)
                {
                    HeadNov[HeadNov.Count - 1][0] = HeadNov[HeadNov.Count - 2][0] + 1;
                }
                else if (nheading == 2)
                {
                    HeadNov[HeadNov.Count - 1][0] = HeadNov[HeadNov.Count - 2][0];
                    HeadNov[HeadNov.Count - 1][1] = HeadNov[HeadNov.Count - 2][1] + 1;
                }
                else if (nheading == 3)
                {
                    if (HeadNov[HeadNov.Count - 1].Length == 1)
                    {
                        throw new Exception("一级标题之后不能放三级标题\n");
                    }
                    else
                    {
                        HeadNov[HeadNov.Count - 1][0] = HeadNov[HeadNov.Count - 2][0];
                        HeadNov[HeadNov.Count - 1][1] = HeadNov[HeadNov.Count - 2][1];
                        HeadNov[HeadNov.Count - 1][2] = HeadNov[HeadNov.Count - 2][2] + 1;
                    }
                }
                else
                {
                    throw new Exception("最多操作三级标题\n");
                }
            }

            int[] number = HeadNov[HeadNov.Count - 1];
            // 论文题目用三号黑体字、一级标题用四号黑体字，并居中。论文中其他汉字一律采用小四号宋体字
            if (nheading == 1)
            {
                //  一级: 三号黑体居中，中间空两个空格，1.5倍行间距，段前后0.5行距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0} {1}", number[0], text));
                text2Fmt.Size = 14.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -10; // 0倍行距
                p.SpaceBefore = 0; // 0.5倍行距
                p.SpaceAfter = 0; // 0.5倍行距
                p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter; // 居中对齐
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel1; // 一级文本
            }
            else if (nheading == 2)
            {
                //  二级: 序数后空一格写标题，四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1} {2}", number[0], number[1], text));
                text2Fmt.Size = 12.0f; // 字号
                text2Fmt.FontName = "宋体";
                text2Fmt.Bold = 1;
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -10; // 0倍行距
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel2; // 二级文本

            }
            else if (nheading == 3)
            {
                //  三级: 序数后空一格写标题，小四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1}.{2} {3}", number[0], number[1], number[2], text));
                text2Fmt.Size = 12.0f; // 字号
                text2Fmt.FontName = "宋体";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -10; // 0倍行距
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel3; // 三级文本
            }
            else
            {
                throw new Exception("最多操作三级标题\n");
            }
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            utils.AddTextP(p, wdoc, wapp);
        }
    }

    /// <summary>
    /// 主要文本
    /// </summary>
    public class Bodying
    {
        ParaOne p = new ParaOne();

        public Bodying(string text)
        {
            Text2Fmt text2Fmt = new Text2Fmt(text);
            text2Fmt.Size = 12.0f; // 字号小四
            p.Text2Fmts.Add(text2Fmt);

            //p.FirstLineIndent = 24.0f;
            p.CharacterUnitFirstLineIndent = 2; // 首行缩进两字符
            p.LineSpacing = -10; // 1.5倍行距
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            utils.AddTextP(p, wdoc, wapp);
        }
    }


    /// <summary>
    /// 参考文献
    /// </summary>
    public class Refering
    {
        public static List<string> refers = new List<string>();

        public static string Fit(string info)
        {
            for (int i = 0; i < refers.Count; i++)
            {
                if (refers[i] == info)
                {
                    return string.Format("[{0}]", i + 1);
                }
            }

            refers.Add(info);
            return string.Format("[{0}]", refers.Count);
        }
    }

    public class ReferEnd
    {
        ParaOne p = new ParaOne();

        public ReferEnd(string text)
        {
            Text2Fmt text2Fmt = new Text2Fmt(text);
            text2Fmt.Size = 12f; // 字号小四
            p.Text2Fmts.Add(text2Fmt);

            //p.FirstLineIndent = 24.0f;
            //p.CharacterUnitFirstLineIndent = 0; // 首行缩进两字符
            p.LineSpacing = -10; // 1.5倍行距
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            utils.AddTextP(p, wdoc, wapp);
        }
    }

    /// <summary>
    /// 添加图片
    /// </summary>
    public class Picturing
    {

        public static List<int[]> PicNov = new List<int[]>();
        public int n = 0;
        public string PicFileName = "";
        public string PicName = "";

        public Picturing(string info)
        {
            // 取出图例和图片路径
            for (int i = info.Length - 1; i >= 2; i--)
            {
                if (info[i] == '(')
                {
                    PicName = info.Substring(2, i - 3);
                    PicFileName = info.Substring(i + 1, info.Length - i - 2);
                    break;
                }
            }

            if (PicNov.Count == 0)
            {
                PicNov.Add(new int[2]);
            }
            PicNov.Add(new int[2]);
            if (PicNov[PicNov.Count - 2][0] == Heading.HeadNov[Heading.HeadNov.Count - 1][0])
            {
                PicNov[PicNov.Count - 1][0] = PicNov[PicNov.Count - 2][0];
                PicNov[PicNov.Count - 1][1] = PicNov[PicNov.Count - 2][1] + 1;
            }
            else
            {
                PicNov[PicNov.Count - 1][0] = PicNov[PicNov.Count - 2][0] + 1;
                PicNov[PicNov.Count - 1][1] = 1;
            }
            n = PicNov.Count - 1;
        }

        public Picturing(string picfilename, string picname)
        {
            PicFileName = picfilename;
            PicName = picname;
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            object unite = Wd.WdUnits.wdStory;
            object N = Missing.Value;
            wapp.Selection.EndKey(ref unite, ref N); //将光标移动到文档末尾

            //ParaOne p1 = new ParaOne();
            //Text2Fmt text2Fmt1 = new Text2Fmt(string.Format(" ", PicNov[n][0], PicNov[n][1], PicName));
            //text2Fmt1.FontName = "宋体";
            //text2Fmt1.Size = 10.2f;
            //p1.Text2Fmts.Add(text2Fmt1);
            //p1.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            //p1.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevelBodyText;
            //p1.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;

            wdoc.Paragraphs.Last.Range.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透'
            wdoc.Paragraphs.Last.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;//居中显示图片
            wdoc.Paragraphs.Last.FirstLineIndent = 0;

            //图片文件的路径
            string filename = PicFileName;
            //要向Word文档中插入图片的位置
            Object range = wdoc.Paragraphs.Last.Range;
            //定义该插入的图片是否为外部链接
            Object linkToFile = false;               //默认,这里貌似设置为bool类型更清晰一些
            //定义要插入的图片是否随Word文档一起保存
            Object saveWithDocument = true;              //默认
            //使用InlineShapes.AddPicture方法(【即“嵌入型”】)插入图片
            Wd.InlineShape picInlineShapes = wdoc.InlineShapes.AddPicture(filename, ref linkToFile, ref saveWithDocument, ref range);



            //设置图片宽高的绝对大小
            Image pic = Image.FromFile(PicFileName);//strFilePath是该图片的绝对路径
            int intWidth = pic.Width;//长度像素值
            int intHeight = pic.Height;//高度像素值 
            picInlineShapes.Width = (int)(28.35 * 13);
            picInlineShapes.Height = (int)(picInlineShapes.Width / intWidth * intHeight);

            //在图下方居中添加图片标题
            wdoc.Paragraphs.Last.Range.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透'
                                                         // 居中对齐

            ParaOne p = new ParaOne();
            Text2Fmt text2Fmt = new Text2Fmt(string.Format("图{0}-{1} {2}", PicNov[n][0], PicNov[n][1], PicName));
            text2Fmt.FontName = "宋体";
            text2Fmt.Size = 10.2f;
            p.Text2Fmts.Add(text2Fmt);
            p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevelBodyText;
            p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            utils.AddTextP(p, wdoc, wapp);
        }
    }

    public class utils
    {
        public static void AddTextP(ParaOne p, Wd.Document WordDoc, Wd.ApplicationClass WordApp)
        {
            object N = Missing.Value;

            WordDoc.Paragraphs.Last.Range.Text = "";
            Wd.Paragraph wdp = WordDoc.Paragraphs.Last;

            for (int i = 0; i < p.Text2Fmts.Count; i++)
            {
                Text2Fmt text2Fmt = p.Text2Fmts[i];
                wdp.Range.Text = text2Fmt.Text;
                wdp.Range.Font.Bold = text2Fmt.Bold;
                wdp.Range.Font.Name = text2Fmt.FontName;
                wdp.Range.Font.Color = text2Fmt.Color;
                wdp.Range.Font.DoubleStrikeThrough = text2Fmt.DoubleStrikeThrough;
                wdp.Range.Font.Italic = text2Fmt.Italic;
                wdp.Range.Font.Size = text2Fmt.Size;
                wdp.Range.Font.Subscript = text2Fmt.Subscript;
                wdp.Range.Font.Superscript = text2Fmt.Superscript;
            }

            wdp.Format.FirstLineIndent = p.FirstLineIndent;
            wdp.Format.Alignment = p.Alignment;
            wdp.Format.CharacterUnitFirstLineIndent = p.CharacterUnitFirstLineIndent;
            wdp.Format.CharacterUnitLeftIndent = p.CharacterUnitLeftIndent;
            wdp.Format.CharacterUnitRightIndent = p.CharacterUnitRightIndent;
            wdp.Format.FirstLineIndent = p.FirstLineIndent;
            wdp.Format.LeftIndent = p.LeftIndent;
            wdp.Format.OutlineLevel = p.OutlineLevel;
            wdp.Format.PageBreakBefore = p.PageBreakBefore;
            wdp.Format.RightIndent = p.RightIndent;
            wdp.Format.SpaceAfter = p.SpaceAfter;
            wdp.Format.SpaceBefore = p.SpaceBefore;

            if (p.LineSpacing == -15)
            {
                wdp.Format.Space15();
            }
            else if (p.LineSpacing == -10)
            {
                wdp.Format.Space1();
            }

            wdp.Range.InsertAfter("\n");
        }
    }
}
