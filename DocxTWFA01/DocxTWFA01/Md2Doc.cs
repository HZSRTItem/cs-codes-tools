/**-----------------------------------------------------------------------------
 * File     : Md2Doc.cs
 * Time     : 2022/2/4 16:51:48
 * Author   : Zheng Han
 * License  : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Contact  : hzsongrentou1580@gmail.com 
 * Refer    : None
 * Desc     : Class [*.md �ļ�תΪ *.docx �ļ�]
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
            // ��ȡmd�ļ�
            List<string> mdlines = ReadMd(mdFileName);
            // ����ͼ���б�
            ArrayList BodyList = FmtDoc(mdlines);
            // ����ȱʡֵ������ʹ�õ���COM�⣬�������������Ҫ��Missing.Value����
            object N = Missing.Value;
            // ����ĵ��ļ��Ѵ��ڣ���ɾ��
            if (File.Exists((string)DocFileName))
            {
                File.Delete((string)DocFileName);
            }
            // WordӦ�ó������
            Wd.ApplicationClass WordApp = new Wd.ApplicationClass();
            // Word�ĵ�����
            Wd.Document WordDoc = WordApp.Documents.Add(ref N, ref N, ref N, ref N);
            // �ĵ��������̿ɼ�
            WordApp.Visible = true;
            // �����ĵ�����
            for (int i = 0; i < BodyList.Count; i++)
            {
                // ����
                if (BodyList[i] is Heading)
                {
                    Heading bodyone = BodyList[i] as Heading;
                    bodyone.Add(WordDoc, WordApp);
                }
                // ����
                if (BodyList[i] is Bodying)
                {
                    Bodying bodyone = BodyList[i] as Bodying;
                    bodyone.Add(WordDoc, WordApp);
                }
                // ͼƬ
                if (BodyList[i] is Picturing)
                {
                    Picturing bodyone = BodyList[i] as Picturing;
                    bodyone.Add(WordDoc, WordApp);
                }
            }
            // WdSaveFormatΪWord 2003�ĵ��ı����ʽ��office 2007����wdFormatDocumentDefault
            object format = Wd.WdSaveFormat.wdFormatDocument;
            // ��wordDoc�ĵ���������ݱ���ΪDOCX�ĵ�
            WordDoc.SaveAs(ref DocFileName, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N, ref N);
            // �ر�wordDoc�ĵ����󣬿��ǲ���Ҫ��ӡ wordDoc.PrintOut();
            WordDoc.Close(ref N, ref N, ref N);
            // �ر�wordApp�������
            WordApp.Quit(ref N, ref N, ref N);
            // ���ĵ�
            System.Diagnostics.Process.Start((string)DocFileName);
        }

        private static ArrayList FmtDoc(List<string> mdlines)
        {
            // �ĵ��������б�
            ArrayList BodyList = new ArrayList();
            string line = "";

            // ��һ��ѭ���Ҳο�����
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
                    if(line[line.Length-1] == '��')
                    {
                        line += line.Substring(0, line.Length-2) +  Refering.Fit(mdlines[i].Substring(1)) + "��";
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

                // �����жϱ���
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
                else if (mdfindrefer[i][0] == '!')  // ͼƬ
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
    /// ����
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
                    raise Exception("��һ���������Ϊһ������")
            else:
                if head == 1:
                    self.headnov.append([self.headnov[-1][0]+1])
                elif head == 2:
                    self.headnov.append(
                        [self.headnov[-1][0], self.headnov[-1][1]+1])
                elif head == 3:
                    if len(self.headnov[-1]) == 1:
                        raise Exception("һ������֮���ܷ���������\n")
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
                    throw new Exception("��һ���������Ϊһ������");
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
                        throw new Exception("һ������֮���ܷ���������\n");
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
                    throw new Exception("��������������\n");
                }
            }

            int[] number = HeadNov[HeadNov.Count - 1];
            if (nheading == 1)
            {
                //  һ��: ���ź�����У��м�������ո�1.5���м�࣬��ǰ��0.5�оࡣ
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("�� {0} ��  {1}", number[0], text));
                text2Fmt.Size = 16.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5���о�
                p.SpaceBefore = 8; // 0.5���о�
                p.SpaceAfter = 8; // 0.5���о�
                p.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter; // ���ж���
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel1; // һ���ı�
            }
            else if (nheading == 2)
            {
                //  ����: �������һ��д���⣬�ĺź��壬1.5���м�ࡣ
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1} {2}", number[0], number[1], text));
                text2Fmt.Size = 14.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5���о�
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel2; // �����ı�
                
            }
            else if (nheading == 3)
            {
                //  ����: �������һ��д���⣬С�ĺź��壬1.5���м�ࡣ
                Text2Fmt text2Fmt = new Text2Fmt(string.Format("{0}.{1}.{2} {3}", number[0], number[1], number[2], text));
                text2Fmt.Size = 12.0f; // �ֺ�
                text2Fmt.FontName = "����";
                p.Text2Fmts.Add(text2Fmt);

                p.LeftIndent = 0f;
                p.CharacterUnitFirstLineIndent = 0;
                p.LineSpacing = -15; // 1.5���о�
                p.OutlineLevel = Wd.WdOutlineLevel.wdOutlineLevel3; // �����ı�
            }
            else
            {
                throw new Exception("��������������\n");
            }
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            utils.AddTextP(p, wdoc, wapp);
        }
    }

    /// <summary>
    /// ��Ҫ�ı�
    /// </summary>
    public class Bodying
    {
        ParaOne p = new ParaOne();

        public Bodying(string text)
        {
            Text2Fmt text2Fmt = new Text2Fmt(text);
            text2Fmt.Size = 12.0f; // �ֺ�С��
            p.Text2Fmts.Add(text2Fmt);

            p.FirstLineIndent= 24.0f;
            p.CharacterUnitFirstLineIndent = 2; // �����������ַ�
            p.LineSpacing = -15; // 1.5���о�
        }

        public void Add(Wd.Document wdoc, Wd.ApplicationClass wapp)
        {
            utils.AddTextP(p, wdoc, wapp);
        }
    }


    /// <summary>
    /// �ο�����
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
    /// ���ͼƬ
    /// </summary>
    public class Picturing
    {

        public static List<int[]> PicNov = new List<int[]>();
        public int n = 0;
        public string PicFileName = "";
        public string PicName = "";

        public Picturing(string info)
        {
            // ȡ��ͼ����ͼƬ·��
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
            wapp.Selection.EndKey(ref unite, ref N); //������ƶ����ĵ�ĩβ

            //ͼƬ�ļ���·��
            string filename = PicFileName;
            //Ҫ��Word�ĵ��в���ͼƬ��λ��
            Object range = wdoc.Paragraphs.Last.Range;
            //����ò����ͼƬ�Ƿ�Ϊ�ⲿ����
            Object linkToFile = false;               //Ĭ��,����ò������Ϊbool���͸�����һЩ
            //����Ҫ�����ͼƬ�Ƿ���Word�ĵ�һ�𱣴�
            Object saveWithDocument = true;              //Ĭ��
            //ʹ��InlineShapes.AddPicture����(������Ƕ���͡���)����ͼƬ
            Wd.InlineShape picInlineShapes = wdoc.InlineShapes.AddPicture(filename, ref linkToFile, ref saveWithDocument, ref range);

            // ���ж���
            wdoc.Paragraphs.Last.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;//������ʾͼƬ

            //����ͼƬ��ߵľ��Դ�С
            Image pic = Image.FromFile(PicFileName);//strFilePath�Ǹ�ͼƬ�ľ���·��
            int intWidth = pic.Width;//��������ֵ
            int intHeight = pic.Height;//�߶�����ֵ 
            picInlineShapes.Width = (int)(28.35 * 13);
            picInlineShapes.Height = (int)(picInlineShapes.Width / intWidth * intHeight);

            //��ͼ�·��������ͼƬ����
            wdoc.Paragraphs.Last.Range.InsertAfter("\n");//��һ������һ���˳���ܵߵ���ԭ��û��͸'
            //Wd.Paragraph p = wdoc.Paragraphs.Last;
            //wapp.Selection.EndKey(ref unite, ref N);


            //p.Range.Text = string.Format("ͼ{0}-{1} {2}", n_s, n_pic, PicName);
            //p.Format.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphCenter;
            //p.Range.Font.Size = 10.2f; //�����С
            //p.Range.Font.Name = "����";

            ParaOne p = new ParaOne();
            p.Text2Fmts.Add(new Text2Fmt(string.Format("ͼ{0}-{1} {2}", PicNov[n][0], PicNov[n][1], PicName)));
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
