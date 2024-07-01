/*------------------------------------------------------------------------------
 * File    : ReferRis
 * Time    : 2022/4/7 16:24:56
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ReferRis]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReferAutoCSA
{
    public class ReferRis
    {
        public static List<ReferRis> ReferRiss = new List<ReferRis>();

        /// <summary>
        /// 解析文件
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public static int DcodeFile(string file_name)
        {
            int n = 1;
            string[] lines = File.ReadAllLines(file_name);
            string line = "";
            ReferRis referRis = new ReferRis();
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Trim();
                if(line == "")
                {
                    continue;
                }

                if (line == "ER  -")
                {
                    ReferRiss.Add(referRis);
                    referRis = new ReferRis();
                    n++;
                }

                if (line.Trim().Length >= 6)
                {
                    referRis.AddInfo(line.Substring(0, 2), line.Substring(6));
                }
            }
            return n;
        }

        /// <summary>
        /// GB T7714 2015 格式的参考文献
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GB_T7714_2015(int n)
        {
            string outstr = string.Format("[{0}] ", n);
            try
            {
                if (RefType[0] == "JOUR")
                {
                    outstr = string.Format("[{0}] ", n);
                    int i = 0;
                    for (i = 0; i < Author.Length - 1; i++)
                    {
                        outstr += Author[i].Replace(",", "") + ", ";
                    }
                    outstr += Author[i].Replace(",", "") + ". ";

                    if(PrimaryTitle.Length != 0)
                    {
                        outstr += PrimaryTitle[0].Trim();
                    }
                    else if (Title.Length != 0)
                    {
                        outstr += Title[0].Trim();
                    }
                    else
                    {
                        outstr += "Not Find Title";
                    }
                    outstr += "[J]. ";

                    // Journal Name 0
                    if (JournalName0.Length != 0)
                    {
                        outstr += JournalName0[0].Trim();
                    }
                    else if (JournalName1.Length != 0)
                    {
                        outstr += JournalName1[0].Trim();
                    }
                    else if(SecondaryTitle.Length != 0)
                    {
                        outstr += SecondaryTitle[0].Trim();
                    }
                    else
                    {
                        outstr += "Not Find Journal Name";
                    }

                    // PublicationYear
                    outstr += ", ";
                        outstr += PublicationYear[0].Trim() + ", ";

                    string volumeNumber = VolumeNumber.Length == 0 ? "" : VolumeNumber[0];
                    outstr += string.Format("{0}({1}): {2}-{3}."
                        , VolumeNumber.Length == 0 ? "" : VolumeNumber[0]
                        , IssueNumber.Length == 0 ? "" : IssueNumber[0]
                        , StartPage.Length == 0 ? "" : StartPage[0]
                        , EndPage.Length == 0 ? "" : EndPage[0]
                        );
                }
            }
            catch (Exception ex)
            {
                outstr += ex.Message.Replace('\n', ' ') + "\n";
            }

            return outstr.Replace("  ", " ");
        }

        /// <summary>
        /// 向一个文献对象中添加信息
        /// </summary>
        /// <param name="mark">标识</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public bool AddInfo(string mark, string info)
        {
            switch (mark)
            {
                case "TY":
                    RefType = AddOneAttr(RefType, info);
                    return true;

                case "A1":
                    PrimaryAuthors = AddOneAttr(PrimaryAuthors, info);
                    return true;

                case "A2":
                    SecondaryAuthors = AddOneAttr(SecondaryAuthors, info);
                    return true;

                case "A3":
                    TertiaryAuthors = AddOneAttr(TertiaryAuthors, info);
                    return true;

                case "A4":
                    SubsidiaryAuthors = AddOneAttr(SubsidiaryAuthors, info);
                    return true;

                case "AB":
                    Abstract = AddOneAttr(Abstract, info);
                    return true;

                case "AD":
                    AuthorAddress = AddOneAttr(AuthorAddress, info);
                    return true;

                case "AN":
                    AccessionNumber = AddOneAttr(AccessionNumber, info);
                    return true;

                case "AU":
                    Author = AddOneAttr(Author, info);
                    return true;

                case "AV":
                    LocationinArchives = AddOneAttr(LocationinArchives, info);
                    return true;

                case "BT":
                    BT = AddOneAttr(BT, info);
                    return true;

                case "C1":
                    Custom1 = AddOneAttr(Custom1, info);
                    return true;

                case "C2":
                    Custom2 = AddOneAttr(Custom2, info);
                    return true;

                case "C3":
                    Custom3 = AddOneAttr(Custom3, info);
                    return true;

                case "C4":
                    Custom4 = AddOneAttr(Custom4, info);
                    return true;

                case "C5":
                    Custom5 = AddOneAttr(Custom5, info);
                    return true;

                case "C6":
                    Custom6 = AddOneAttr(Custom6, info);
                    return true;

                case "C7":
                    Custom7 = AddOneAttr(Custom7, info);
                    return true;

                case "C8":
                    Custom8 = AddOneAttr(Custom8, info);
                    return true;

                case "CA":
                    Caption = AddOneAttr(Caption, info);
                    return true;

                case "CN":
                    CallNumber = AddOneAttr(CallNumber, info);
                    return true;

                case "CP":
                    CP = AddOneAttr(CP, info);
                    return true;

                case "CT":
                    TitleofUnpublishedReference = AddOneAttr(TitleofUnpublishedReference, info);
                    return true;

                case "CY":
                    PlacePublished = AddOneAttr(PlacePublished, info);
                    return true;

                case "DA":
                    OutDate = AddOneAttr(OutDate, info);
                    return true;

                case "DB":
                    NameofDatabase = AddOneAttr(NameofDatabase, info);
                    return true;

                case "DO":
                    DOI = AddOneAttr(DOI, info);
                    return true;

                case "DP":
                    DatabaseProvider = AddOneAttr(DatabaseProvider, info);
                    return true;

                case "ED":
                    Editor = AddOneAttr(Editor, info);
                    return true;

                case "EP":
                    EndPage = AddOneAttr(EndPage, info);
                    return true;

                case "ET":
                    Edition = AddOneAttr(Edition, info);
                    return true;

                case "ID":
                    ReferenceID = AddOneAttr(ReferenceID, info);
                    return true;

                case "IS":
                    IssueNumber = AddOneAttr(IssueNumber, info);
                    return true;

                case "J1":
                    PeriodicalName1 = AddOneAttr(PeriodicalName1, info);
                    return true;

                case "J2":
                    AlternateTitle = AddOneAttr(AlternateTitle, info);
                    return true;

                case "JA":
                    PeriodicalName = AddOneAttr(PeriodicalName, info);
                    return true;

                case "JF":
                    JournalName0 = AddOneAttr(JournalName0, info);
                    return true;

                case "JO":
                    JournalName1 = AddOneAttr(JournalName1, info);
                    return true;

                case "KW":
                    Keywords = AddOneAttr(Keywords, info);
                    return true;

                case "L1":
                    LinktoPDF = AddOneAttr(LinktoPDF, info);
                    return true;

                case "L2":
                    LinktoFullText = AddOneAttr(LinktoFullText, info);
                    return true;

                case "L3":
                    RelatedRecords = AddOneAttr(RelatedRecords, info);
                    return true;

                case "L4":
                    InImages = AddOneAttr(InImages, info);
                    return true;

                case "LA":
                    Language = AddOneAttr(Language, info);
                    return true;

                case "LB":
                    Label = AddOneAttr(Label, info);
                    return true;

                case "LK":
                    WebsiteLink = AddOneAttr(WebsiteLink, info);
                    return true;

                case "M1":
                    Number = AddOneAttr(Number, info);
                    return true;

                case "M2":
                    Miscellaneous = AddOneAttr(Miscellaneous, info);
                    return true;

                case "M3":
                    TypeofWork = AddOneAttr(TypeofWork, info);
                    return true;

                case "N1":
                    Notes = AddOneAttr(Notes, info);
                    return true;

                case "N2":
                    N2 = AddOneAttr(N2, info);
                    return true;

                case "NV":
                    NumberofVolumes = AddOneAttr(NumberofVolumes, info);
                    return true;

                case "OP":
                    OriginalPublication = AddOneAttr(OriginalPublication, info);
                    return true;

                case "PB":
                    Publisher = AddOneAttr(Publisher, info);
                    return true;

                case "PP":
                    PublishingPlace = AddOneAttr(PublishingPlace, info);
                    return true;

                case "PY":
                    PublicationYear = AddOneAttr(PublicationYear, info);
                    return true;

                case "RI":
                    ReviewedItem = AddOneAttr(ReviewedItem, info);
                    return true;

                case "RN":
                    ResearchNotes = AddOneAttr(ResearchNotes, info);
                    return true;

                case "RP":
                    ReprintEdition = AddOneAttr(ReprintEdition, info);
                    return true;

                case "SE":
                    Section = AddOneAttr(Section, info);
                    return true;

                case "SN":
                    ISBNISSN = AddOneAttr(ISBNISSN, info);
                    return true;

                case "SP":
                    StartPage = AddOneAttr(StartPage, info);
                    return true;

                case "ST":
                    ShortTitle = AddOneAttr(ShortTitle, info);
                    return true;

                case "T1":
                    PrimaryTitle = AddOneAttr(PrimaryTitle, info);
                    return true;

                case "T2":
                    SecondaryTitle = AddOneAttr(SecondaryTitle, info);
                    return true;

                case "T3":
                    TertiaryTitle = AddOneAttr(TertiaryTitle, info);
                    return true;

                case "TA":
                    TranslatedAuthor = AddOneAttr(TranslatedAuthor, info);
                    return true;

                case "TI":
                    Title = AddOneAttr(Title, info);
                    return true;

                case "TT":
                    TranslatedTitle = AddOneAttr(TranslatedTitle, info);
                    return true;

                case "U1":
                    U1 = AddOneAttr(U1, info);
                    return true;

                case "U2":
                    U2 = AddOneAttr(U2, info);
                    return true;

                case "U3":
                    U3 = AddOneAttr(U3, info);
                    return true;

                case "U4":
                    U4 = AddOneAttr(U4, info);
                    return true;

                case "U5":
                    U5 = AddOneAttr(U5, info);
                    return true;

                case "UR":
                    URL = AddOneAttr(URL, info);
                    return true;

                case "VL":
                    VolumeNumber = AddOneAttr(VolumeNumber, info);
                    return true;

                case "VO":
                    PublishedStandardNumber = AddOneAttr(PublishedStandardNumber, info);
                    return true;

                case "Y1":
                    PrimaryDate = AddOneAttr(PrimaryDate, info);
                    return true;

                case "Y2":
                    AccessDate = AddOneAttr(AccessDate, info);
                    return true;

                default:
                    return false;
            }
            
        }

        /// <summary>
        /// 添加一个属性信息
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private string[] AddOneAttr(string[] infos, string info)
        {
            string[] out_infos = new string[infos.Length + 1];
            int i = 0;
            for (; i < infos.Length; i++)
            {
                out_infos[i] = infos[i];
            }
            out_infos[i] = info;
            return out_infos;
        }

        /// <summary>
        /// 打印mark信息
        /// </summary>
        public static void PrintMarkInfo()
        {
            Console.WriteLine("TY	Type of reference (must be the first tag)	引用类型（必须是第一个标签）	Ref Type");
            Console.WriteLine("A1	Primary Authors (each author on its own line preceded by the A1 tag)	主要作者（每个作者在自己的行中，前面带有 A1 标签）	Primary Authors");
            Console.WriteLine("A2	Secondary Authors (each author on its own line preceded by the A2 tag)	次要作者（每个作者在自己的行中，前面带有 A2 标签）	Secondary Authors");
            Console.WriteLine("A3	Tertiary Authors (each author on its own line preceded by the A3 tag)	第三作者（每个作者在自己的行中，前面有 A3 标签）	Tertiary Authors");
            Console.WriteLine("A4	Subsidiary Authors (each author on its own line preceded by the A4 tag)	附属作者（每个作者在自己的行上，前面有 A4 标签）	Subsidiary Authors");
            Console.WriteLine("AB	Abstract	抽象的	Abstract");
            Console.WriteLine("AD	Author Address	作者地址	Author Address");
            Console.WriteLine("AN	Accession Number	入藏号	Accession Number");
            Console.WriteLine("AU	Author (each author on its own line preceded by the AU tag)	作者（每个作者在自己的行中，前面有 AU 标记）	Author");
            Console.WriteLine("AV	Location in Archives	档案中的位置	Location in Archives");
            Console.WriteLine("BT	This field maps to T2 for all reference types except for Whole Book and Unpublished Work references. It can contain alphanumeric characters. There is no practical limit to the length of this field.	对于除整本书和未发布的作品参考之外的所有参考类型，此字段都映射到 T2。它可以包含字母数字字符。该字段的长度没有实际限制。	BT");
            Console.WriteLine("C1	Custom 1	自定义 1	Custom 1");
            Console.WriteLine("C2	Custom 2	自定义 2	Custom 2");
            Console.WriteLine("C3	Custom 3	自定义 3	Custom 3");
            Console.WriteLine("C4	Custom 4	自定义 4	Custom 4");
            Console.WriteLine("C5	Custom 5	自定义 5	Custom 5");
            Console.WriteLine("C6	Custom 6	定制 6	Custom 6");
            Console.WriteLine("C7	Custom 7	自定义 7	Custom 7");
            Console.WriteLine("C8	Custom 8	定制 8	Custom 8");
            Console.WriteLine("CA	Caption	标题	Caption");
            Console.WriteLine("CN	Call Number	索书号	Call Number");
            Console.WriteLine("CP	This field can contain alphanumeric characters. There is no practical limit to the length of this field.	此字段可以包含字母数字字符。该字段的长度没有实际限制。	CP");
            Console.WriteLine("CT	Title of unpublished reference	未发表参考文献的标题	Title of Unpublished Reference");
            Console.WriteLine("CY	Place Published	发表地点	Place Published");
            Console.WriteLine("DA	Date	日期	OutDate");
            Console.WriteLine("DB	Name of Database	数据库名称	Name of Database");
            Console.WriteLine("DO	DOI	DOI	DOI");
            Console.WriteLine("DP	Database Provider	数据库提供者	Database Provider");
            Console.WriteLine("ED	Editor	编辑	Editor");
            Console.WriteLine("EP	End Page	结束页	End Page");
            Console.WriteLine("ET	Edition	版	Edition");
            Console.WriteLine("ID	Reference ID	参考编号	Reference ID");
            Console.WriteLine("IS	Issue number	发行数量	Issue Number");
            Console.WriteLine("J1	Periodical name: user abbreviation 1. This is an alphanumeric field of up to 255 characters.	期刊名称：用户缩写 1。这是一个最多 255 个字符的字母数字字段。	Periodical name 1");
            Console.WriteLine("J2	Alternate Title (this field is used for the abbreviated title of a book or journal name, the latter mapped to T2)	Alternate Title（此字段用于书籍或期刊名称的缩写名称，后者映射到 T2）	Alternate Title");
            Console.WriteLine("JA	Periodical name: standard abbreviation. This is the periodical in which the article was (or is to be, in the case of in-press references) published. This is an alphanumeric field of up to 255 characters.	期刊名称：标准缩写。这是发表文章的期刊（或将要发表的文章，在印刷参考的情况下）。这是一个最多 255 个字符的字母数字字段。	Periodical name");
            Console.WriteLine("JF	Journal/Periodical name: full format. This is an alphanumeric field of up to 255 characters.	期刊/期刊名称：全格式。这是一个最多 255 个字符的字母数字字段。	Journal Name 0");
            Console.WriteLine("JO	Journal/Periodical name: full format. This is an alphanumeric field of up to 255 characters.	期刊/期刊名称：全格式。这是一个最多 255 个字符的字母数字字段。	Journal Name 1");
            Console.WriteLine("KW	Keywords (keywords should be entered each on its own line preceded by the tag)	关键字（关键字应在其自己的行中输入，并以标签开头）	Keywords");
            Console.WriteLine("L1	Link to PDF. There is no practical limit to the length of this field. URL addresses can be entered individually, one per tag or multiple addresses can be entered on one line using a semi-colon as a separator.	链接到 PDF。该字段的长度没有实际限制。 URL 地址可以单独输入，每个标签一个或多个地址可以在一行中输入，使用分号作为分隔符。	Link to PDF");
            Console.WriteLine("L2	Link to Full-text. There is no practical limit to the length of this field. URL addresses can be entered individually, one per tag or multiple addresses can be entered on one line using a semi-colon as a separator.	链接到全文。该字段的长度没有实际限制。 URL 地址可以单独输入，每个标签一个或多个地址可以在一行中输入，使用分号作为分隔符。	Link to Full Text");
            Console.WriteLine("L3	Related Records. There is no practical limit to the length of this field.	相关记录。该字段的长度没有实际限制。	Related Records");
            Console.WriteLine("L4	Image(s). There is no practical limit to the length of this field.	图片）。该字段的长度没有实际限制。	InImages");
            Console.WriteLine("LA	Language	语言	Language");
            Console.WriteLine("LB	Label	标签	Label");
            Console.WriteLine("LK	Website Link	网站链接	Website Link");
            Console.WriteLine("M1	Number	数字	Number");
            Console.WriteLine("M2	Miscellaneous 2. This is an alphanumeric field and there is no practical limit to the length of this field.	其他 2. 这是一个字母数字字段，对该字段的长度没有实际限制。	Miscellaneous");
            Console.WriteLine("M3	Type of Work	工作类型	Type of Work");
            Console.WriteLine("N1	Notes	笔记	Notes");
            Console.WriteLine("N2	Abstract. This is a free text field and can contain alphanumeric characters. There is no practical length limit to this field.	抽象的。这是一个自由文本字段，可以包含字母数字字符。该字段没有实际的长度限制。	N2");
            Console.WriteLine("NV	Number of Volumes	卷数	Number of Volumes");
            Console.WriteLine("OP	Original Publication	原始出版物	Original Publication");
            Console.WriteLine("PB	Publisher	出版商	Publisher");
            Console.WriteLine("PP	Publishing Place	出版地	Publishing Place");
            Console.WriteLine("PY	Publication year (YYYY)	出版年份 (YYYY)	Publication Year");
            Console.WriteLine("RI	Reviewed Item	审查项目	Reviewed Item");
            Console.WriteLine("RN	Research Notes	研究笔记	Research Notes");
            Console.WriteLine("RP	Reprint Edition	再版	Reprint Edition");
            Console.WriteLine("SE	Section	部分	Section");
            Console.WriteLine("SN	ISBN/ISSN	ISBN/ISSN	ISBN ISSN");
            Console.WriteLine("SP	Start Page	首页	Start Page");
            Console.WriteLine("ST	Short Title	短标题	Short Title");
            Console.WriteLine("T1	Primary Title	主要职称	Primary Title");
            Console.WriteLine("T2	Secondary Title (journal title, if applicable)	次要标题（期刊标题，如果适用）	Secondary Title ");
            Console.WriteLine("T3	Tertiary Title	第三职称	Tertiary Title");
            Console.WriteLine("TA	Translated Author	翻译作者	Translated Author");
            Console.WriteLine("TI	Title	标题	Title");
            Console.WriteLine("TT	Translated Title	翻译标题	Translated Title");
            Console.WriteLine("U1	User definable 1. This is an alphanumeric field and there is no practical limit to the length of this field.	用户可定义 1. 这是一个字母数字字段，对该字段的长度没有实际限制。	U1");
            Console.WriteLine("U2	User definable 2. This is an alphanumeric field and there is no practical limit to the length of this field.	用户可定义 2. 这是一个字母数字字段，对该字段的长度没有实际限制。	U2");
            Console.WriteLine("U3	User definable 3. This is an alphanumeric field and there is no practical limit to the length of this field.	用户可定义 3. 这是一个字母数字字段，对该字段的长度没有实际限制。	U3");
            Console.WriteLine("U4	User definable 4. This is an alphanumeric field and there is no practical limit to the length of this field.	用户可定义 4. 这是一个字母数字字段，对该字段的长度没有实际限制。	U4");
            Console.WriteLine("U5	User definable 5. This is an alphanumeric field and there is no practical limit to the length of this field.	用户可定义 5. 这是一个字母数字字段，对该字段的长度没有实际限制。	U5");
            Console.WriteLine("UR	URL	网址	URL");
            Console.WriteLine("VL	Volume number	卷号	Volume Number");
            Console.WriteLine("VO	Published Standard Number	公布的标准号	Published Standard Number");
            Console.WriteLine("Y1	Primary Date	主要日期	Primary Date");
            Console.WriteLine("Y2	Access Date	访问日期	Access Date");

        }

        #region Refer Attrs
        /// <summary>
        /// TY 引用类型（必须是第一个标签）
        /// </summary>
        public string[] RefType = new string[0];
        /// <summary>
        /// A1 主要作者（每个作者在自己的行中，前面带有 A1 标签）
        /// </summary>
        public string[] PrimaryAuthors = new string[0];
        /// <summary>
        /// A2 次要作者（每个作者在自己的行中，前面带有 A2 标签）
        /// </summary>
        public string[] SecondaryAuthors = new string[0];
        /// <summary>
        /// A3 第三作者（每个作者在自己的行中，前面有 A3 标签）
        /// </summary>
        public string[] TertiaryAuthors = new string[0];
        /// <summary>
        /// A4 附属作者（每个作者在自己的行上，前面有 A4 标签）
        /// </summary>
        public string[] SubsidiaryAuthors = new string[0];
        /// <summary>
        /// AB 抽象的
        /// </summary>
        public string[] Abstract = new string[0];
        /// <summary>
        /// AD 作者地址
        /// </summary>
        public string[] AuthorAddress = new string[0];
        /// <summary>
        /// AN 入藏号
        /// </summary>
        public string[] AccessionNumber = new string[0];
        /// <summary>
        /// AU 作者（每个作者在自己的行中，前面有 AU 标记）
        /// </summary>
        public string[] Author = new string[0];
        /// <summary>
        /// AV 档案中的位置
        /// </summary>
        public string[] LocationinArchives = new string[0];
        /// <summary>
        /// BT 对于除整本书和未发布的作品参考之外的所有参考类型，此字段都映射到 T2。它可以包含字母数字字符。该字段的长度没有实际限制。
        /// </summary>
        public string[] BT = new string[0];
        /// <summary>
        /// C1 自定义 1
        /// </summary>
        public string[] Custom1 = new string[0];
        /// <summary>
        /// C2 自定义 2
        /// </summary>
        public string[] Custom2 = new string[0];
        /// <summary>
        /// C3 自定义 3
        /// </summary>
        public string[] Custom3 = new string[0];
        /// <summary>
        /// C4 自定义 4
        /// </summary>
        public string[] Custom4 = new string[0];
        /// <summary>
        /// C5 自定义 5
        /// </summary>
        public string[] Custom5 = new string[0];
        /// <summary>
        /// C6 定制 6
        /// </summary>
        public string[] Custom6 = new string[0];
        /// <summary>
        /// C7 自定义 7
        /// </summary>
        public string[] Custom7 = new string[0];
        /// <summary>
        /// C8 定制 8
        /// </summary>
        public string[] Custom8 = new string[0];
        /// <summary>
        /// CA 标题
        /// </summary>
        public string[] Caption = new string[0];
        /// <summary>
        /// CN 索书号
        /// </summary>
        public string[] CallNumber = new string[0];
        /// <summary>
        /// CP 此字段可以包含字母数字字符。该字段的长度没有实际限制。
        /// </summary>
        public string[] CP = new string[0];
        /// <summary>
        /// CT 未发表参考文献的标题
        /// </summary>
        public string[] TitleofUnpublishedReference = new string[0];
        /// <summary>
        /// CY 发表地点
        /// </summary>
        public string[] PlacePublished = new string[0];
        /// <summary>
        /// DA 日期
        /// </summary>
        public string[] OutDate = new string[0];
        /// <summary>
        /// DB 数据库名称
        /// </summary>
        public string[] NameofDatabase = new string[0];
        /// <summary>
        /// DO DOI
        /// </summary>
        public string[] DOI = new string[0];
        /// <summary>
        /// DP 数据库提供者
        /// </summary>
        public string[] DatabaseProvider = new string[0];
        /// <summary>
        /// ED 编辑
        /// </summary>
        public string[] Editor = new string[0];
        /// <summary>
        /// EP 结束页
        /// </summary>
        public string[] EndPage = new string[0];
        /// <summary>
        /// ET 版
        /// </summary>
        public string[] Edition = new string[0];
        /// <summary>
        /// ID 参考编号
        /// </summary>
        public string[] ReferenceID = new string[0];
        /// <summary>
        /// IS 发行数量
        /// </summary>
        public string[] IssueNumber = new string[0];
        /// <summary>
        /// J1 期刊名称：用户缩写 1。这是一个最多 255 个字符的字母数字字段。
        /// </summary>
        public string[] PeriodicalName1 = new string[0];
        /// <summary>
        /// J2 Alternate Title（此字段用于书籍或期刊名称的缩写名称，后者映射到 T2）
        /// </summary>
        public string[] AlternateTitle = new string[0];
        /// <summary>
        /// JA 期刊名称：标准缩写。这是发表文章的期刊（或将要发表的文章，在印刷参考的情况下）。这是一个最多 255 个字符的字母数字字段。
        /// </summary>
        public string[] PeriodicalName = new string[0];
        /// <summary>
        /// JF 期刊/期刊名称：全格式。这是一个最多 255 个字符的字母数字字段。
        /// </summary>
        public string[] JournalName0 = new string[0];
        /// <summary>
        /// JO 期刊/期刊名称：全格式。这是一个最多 255 个字符的字母数字字段。
        /// </summary>
        public string[] JournalName1 = new string[0];
        /// <summary>
        /// KW 关键字（关键字应在其自己的行中输入，并以标签开头）
        /// </summary>
        public string[] Keywords = new string[0];
        /// <summary>
        /// L1 链接到 PDF。该字段的长度没有实际限制。 URL 地址可以单独输入，每个标签一个或多个地址可以在一行中输入，使用分号作为分隔符。
        /// </summary>
        public string[] LinktoPDF = new string[0];
        /// <summary>
        /// L2 链接到全文。该字段的长度没有实际限制。 URL 地址可以单独输入，每个标签一个或多个地址可以在一行中输入，使用分号作为分隔符。
        /// </summary>
        public string[] LinktoFullText = new string[0];
        /// <summary>
        /// L3 相关记录。该字段的长度没有实际限制。
        /// </summary>
        public string[] RelatedRecords = new string[0];
        /// <summary>
        /// L4 图片）。该字段的长度没有实际限制。
        /// </summary>
        public string[] InImages = new string[0];
        /// <summary>
        /// LA 语言
        /// </summary>
        public string[] Language = new string[0];
        /// <summary>
        /// LB 标签
        /// </summary>
        public string[] Label = new string[0];
        /// <summary>
        /// LK 网站链接
        /// </summary>
        public string[] WebsiteLink = new string[0];
        /// <summary>
        /// M1 数字
        /// </summary>
        public string[] Number = new string[0];
        /// <summary>
        /// M2 其他 2. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] Miscellaneous = new string[0];
        /// <summary>
        /// M3 工作类型
        /// </summary>
        public string[] TypeofWork = new string[0];
        /// <summary>
        /// N1 笔记
        /// </summary>
        public string[] Notes = new string[0];
        /// <summary>
        /// N2 抽象的。这是一个自由文本字段，可以包含字母数字字符。该字段没有实际的长度限制。
        /// </summary>
        public string[] N2 = new string[0];
        /// <summary>
        /// NV 卷数
        /// </summary>
        public string[] NumberofVolumes = new string[0];
        /// <summary>
        /// OP 原始出版物
        /// </summary>
        public string[] OriginalPublication = new string[0];
        /// <summary>
        /// PB 出版商
        /// </summary>
        public string[] Publisher = new string[0];
        /// <summary>
        /// PP 出版地
        /// </summary>
        public string[] PublishingPlace = new string[0];
        /// <summary>
        /// PY 出版年份 (YYYY)
        /// </summary>
        public string[] PublicationYear = new string[0];
        /// <summary>
        /// RI 审查项目
        /// </summary>
        public string[] ReviewedItem = new string[0];
        /// <summary>
        /// RN 研究笔记
        /// </summary>
        public string[] ResearchNotes = new string[0];
        /// <summary>
        /// RP 再版
        /// </summary>
        public string[] ReprintEdition = new string[0];
        /// <summary>
        /// SE 部分
        /// </summary>
        public string[] Section = new string[0];
        /// <summary>
        /// SN ISBN/ISSN
        /// </summary>
        public string[] ISBNISSN = new string[0];
        /// <summary>
        /// SP 首页
        /// </summary>
        public string[] StartPage = new string[0];
        /// <summary>
        /// ST 短标题
        /// </summary>
        public string[] ShortTitle = new string[0];
        /// <summary>
        /// T1 主要职称
        /// </summary>
        public string[] PrimaryTitle = new string[0];
        /// <summary>
        /// T2 次要标题（期刊标题，如果适用）
        /// </summary>
        public string[] SecondaryTitle = new string[0];
        /// <summary>
        /// T3 第三职称
        /// </summary>
        public string[] TertiaryTitle = new string[0];
        /// <summary>
        /// TA 翻译作者
        /// </summary>
        public string[] TranslatedAuthor = new string[0];
        /// <summary>
        /// TI 标题
        /// </summary>
        public string[] Title = new string[0];
        /// <summary>
        /// TT 翻译标题
        /// </summary>
        public string[] TranslatedTitle = new string[0];
        /// <summary>
        /// U1 用户可定义 1. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] U1 = new string[0];
        /// <summary>
        /// U2 用户可定义 2. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] U2 = new string[0];
        /// <summary>
        /// U3 用户可定义 3. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] U3 = new string[0];
        /// <summary>
        /// U4 用户可定义 4. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] U4 = new string[0];
        /// <summary>
        /// U5 用户可定义 5. 这是一个字母数字字段，对该字段的长度没有实际限制。
        /// </summary>
        public string[] U5 = new string[0];
        /// <summary>
        /// UR 网址
        /// </summary>
        public string[] URL = new string[0];
        /// <summary>
        /// VL 卷号
        /// </summary>
        public string[] VolumeNumber = new string[0];
        /// <summary>
        /// VO 公布的标准号
        /// </summary>
        public string[] PublishedStandardNumber = new string[0];
        /// <summary>
        /// Y1 主要日期
        /// </summary>
        public string[] PrimaryDate = new string[0];
        /// <summary>
        /// Y2 访问日期
        /// </summary>
        public string[] AccessDate = new string[0];
        #endregion
    }
}
