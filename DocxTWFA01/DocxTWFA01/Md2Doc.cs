/**-----------------------------------------------------------------------------
 * File     : Md2Doc.cs
 * Time     : 2022/2/4 16:51:48
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [*.md 文件转为 *.docx 文件]
 **---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocLib;
using System.Collections;
using Wd = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace DocxTWFA01
{
    public class Md2Doc
    {
        public static void Fit(string mdFileName, object DocFileName)
        {
            // 读取md文件
            List<string> mdlines = ReadMd(mdFileName);
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
            }
            // WdSaveFormat为Word 2003文档的保存格式，office 2007就是wdFormatDocumentDefault
            object format = Wd.WdSaveFormat.wdFormatDocument;
            // 将wordDoc文档对象的内容保存为DOCX文档
            WordDoc.SaveAs(ref DocFileName, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N);
            // 关闭wordDoc文档对象，看是不是要打印 wordDoc.PrintOut();
            WordDoc.Close(ref N, ref N, ref N);
            // 关闭wordApp组件对象
            WordApp.Quit(ref N, ref N, ref N);
            // 打开文档
            System.Diagnostics.Process.Start((string)DocFileName);
        }

        private static ArrayList FmtDoc(List<string> mdlines)
        {
            // 文档的主体列表
            ArrayList BodyList = new ArrayList();
            string line = "";

            // 第一遍循环找参考文献
            List<string> mdfindrefer = new List<string>();
            for (int i = 0; i < mdlines.Count; i++)
            {
                //if (mdlines[i] == "")
                //{
                //    mdfindrefer.Add(line);
                //    line = "";
                //    continue;
                //}

                if (mdlines[i] == "")
                {
                    if (line != "")
                    {
                        mdfindrefer.Add(line);
                        line = "";
                    }
                }
                else if(mdlines[i][0] == '#' |
                    mdlines[i][0] == '*' |
                    mdlines[i][0] == '+' |
                    mdlines[i][0] == '-' |
                    mdlines[i][0] == '!' )
                {
                    mdfindrefer.Add(line);
                    mdfindrefer.Add(mdlines[i]);
                    line = "";
                }
                else if (mdlines[i][0] == '>')
                {
                    if(line[line.Length-1] == '。')
                    {
                        line += line.Substring(0, line.Length-2) +  Refering.Fit(mdlines[i].Substring(1)) + "。";
                    }
                    else
                    {
                        line += Refering.Fit(mdlines[i].Substring(1));
                    }
                }
                else
                {
                    line += mdlines[i];
                }


            }

            //string haha = "";
            //foreach (string s in mdfindrefer)
            //{
            //    haha += s;
            //    haha += "\n";
            //}

            for (int i = 0; i < mdfindrefer.Count; i++)
            {

                // 首先判断标题
                if (mdfindrefer[i] == "")
                {
                    continue;
                }

                string[] mdfindrefers = mdfindrefer[i].Split(' ');
                if (mdfindrefers[0] == "#")
                {
                    BodyList.Add(new Heading(mdfindrefer[i].Substring(2), 1));
                    
                }
                else if (mdfindrefers[0] == "##")
                {
                    BodyList.Add(new Heading(mdfindrefer[i].Substring(3), 2));
                }
                else if (mdfindrefers[0] == "###")
                {
                    BodyList.Add(new Heading(mdfindrefer[i].Substring(4), 3));
                }
                else if (mdfindrefer[i][0] == '!')  // 图片
                {
                    BodyList.Add(new Picturing(mdfindrefer[i]));
                }
                else
                {
                    BodyList.Add(new Bodying(mdfindrefer[i]));
                }

            }

            return BodyList;
        }

        private static List<string> ReadMd(string mdFileName)
        {
            StreamReader sr = new StreamReader(mdFileName);
            string buffer = sr.ReadLine();
            bool isqiankonghang = false;
            List<string> mdlines = new List<string>();

            while (buffer != null)
            {
                if(buffer == "")
                {
                    if(isqiankonghang)
                    {
                        buffer = sr.ReadLine();
                        continue;
                    }
                    isqiankonghang = true;
                }
                else
                {
                    isqiankonghang = false;
                }

                mdlines.Add(buffer);
                buffer = sr.ReadLine();
            }

            sr.Close();
            return mdlines;
        }

    }



    /// <summary>
    /// 标题
    /// </summary>
    public class Heading
    {
        public ParaOne  p = new ParaOne();

        public static List<int[]> HeadNov = new List<int[]>();

        public Heading(string text, int nheading)
        {
            /*

            if not self.headnov:
                if head == 1:
                    self.headnov.append([1])
                else:
                    raise Exception("第一个标题必须为一级标题")
            else:
                if head == 1:
                    self.headnov.append([self.headnov[-1][0]+1])
                elif head == 2:
                    self.headnov.append(
                        [self.headnov[-1][0], self.headnov[-1][1]+1])
                elif head == 3:
                    if len(self.headnov[-1]) == 1:
                        raise Exception("一级标题之后不能放三级标题\n")
                    else:
                        self.headnov.append(
                            [self.headnov[-1][0], self.headnov[-1][1], self.headnov[-1][2]+1])

             */
            HeadNov.Add(new int[5]);
            if (HeadNov.Count == 1)
            {
                if (nheading == 1)
                {
                    HeadNov[HeadNov.Count-1][0] = 1;
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
                    if (HeadNov[HeadNov.Count-1].Length == 1)
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
            if (nheading == 1)
            {
                //  一级: 三号黑体居中，中间空两个空格，1.5倍行间距，段前后0.5行距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("第 {0} 章  {1}", number[0], text));
                text2Fmt.Size = 16.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5倍行距
                p.SpaceBefore = 8; // 0.5倍行距
                p.SpaceAfter = 8; // 0.5倍行距
                p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter; // 居中对齐
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel1; // 一级文本
            }
            else if (nheading == 2)
            {
                //  二级: 序数后空一格写标题，四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1} {2}", number[0], number[1], text));
                text2Fmt.Size = 14.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5倍行距
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel2; // 二级文本
                
            }
            else if (nheading == 3)
            {
                //  三级: 序数后空一格写标题，小四号黑体，1.5倍行间距。
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1}.{2} {3}", number[0], number[1], number[2], text));
                text2Fmt.Size = 12.0f; // 字号
                text2Fmt.FontName = "黑体";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5倍行距
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

            p.FirstLineIndent= 24.0f;
            p.CharacterUnitFirstLineIndent = 2; // 首行缩进两字符
            p.LineSpacing = -15; // 1.5倍行距
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

            if(PicNov.Count == 0)
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

            // 居中对齐
            wdoc.Paragraphs.Last.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;//居中显示图片

            //设置图片宽高的绝对大小
            Image pic = Image.FromFile(PicFileName);//strFilePath是该图片的绝对路径
            int intWidth = pic.Width;//长度像素值
            int intHeight = pic.Height;//高度像素值 
            picInlineShapes.Width = (int)(28.35 * 13);
            picInlineShapes.Height = (int)(picInlineShapes.Width / intWidth * intHeight);

            //在图下方居中添加图片标题
            wdoc.Paragraphs.Last.Range.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透'
            //Wd.Paragraph p = wdoc.Paragraphs.Last;
            //wapp.Selection.EndKey(ref unite, ref N);


            //p.Range.Text = string.Format("图{0}-{1} {2}", n_s, n_pic, PicName);
            //p.Format.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            //p.Range.Font.Size = 10.2f; //字体大小
            //p.Range.Font.Name = "宋体";

            ParaOne p = new ParaOne();
            p.Text2Fmts.Add(new Text2Fmt(string.Format("图{0}-{1} {2}", PicNov[n][0], PicNov[n][1], PicName)));
            p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevelBodyText;
            utils.AddTextP(p, wdoc, wapp);
        }
    }

    public class utils
    {
        public static void AddTextP(ParaOne p, Wd.Document WordDoc, Wd.ApplicationClass  WordApp)
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
            else
            {
                wdp.Format.LineSpacing = p.LineSpacing;
            }

            wdp.Range.InsertAfter("\n");
        }
    }

}
