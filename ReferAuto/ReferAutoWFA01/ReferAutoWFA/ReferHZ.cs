/*------------------------------------------------------------------------------
 * File    : ReferHZ
 * Time    : 2022/3/18 16:44:57
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ReferHZ] 参考文献
 * 
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace ReferAutoWFA
{
    /// <summary>
    /// 文献的属性类别
    /// </summary>
    enum RefAttr
    {
        E_AuthorKeywords,
        E_BookGroupAuthors,
        E_ResearchAreas,
        E_AuthorFullNames,
        E_Authors,
        E_BookAuthors,
        E_BookEditors,
        E_BookAuthorFullNames,
        E_GroupAuthors,
        E_EmailAddresses,
        E_FundingOrgs,
        E_WoSCategories,
        E_Abstract,
        E_BookSeriesSubtitle,
        E_Addresses,
        E_Affiliations,
        E_ConferenceLocation,
        E_CitedReferences,
        E_ConferenceTitle,
        E_ConferenceDate,
        E_BookDOI,
        E_DateofExport,
        E_DOI,
        E_DocumentType,
        E_EarlyAccessDate,
        E_eISSN,
        E_FundingText,
        E_IDSNumber,
        E_HighlyCitedStatus,
        E_ConferenceHost,
        E_HotPaperStatus,
        E_KeywordsPlus,
        E_Issue,
        E_JournalAbbreviation,
        E_JournalISOAbbreviation,
        E_Language,
        E_MeetingAbstract,
        E_OpenAccessDesignations,
        E_ORCIDs,
        E_PublisherAddress,
        E_PublicationDate,
        E_PublisherCity,
        E_PubmedId,
        E_PartNumber,
        E_PublicationType,
        E_Publisher,
        E_ResearcherIds,
        E_ReprintAddresses,
        E_BookSeriesTitle,
        E_SpecialIssue,
        E_ISSN,
        E_SourceTitle,
        E_ConferenceSponsor,
        E_Supplement,
        E_ArticleTitle,
        E_UTUniqueWOSID,
        E_WebofScienceIndex,
        E_ISBN,
        E_TimesCitedAllDatabases,
        E_ArticleNumber,
        E_StartPage,
        E_EndPage,
        E_CitedReferenceCount,
        E_NumberofPages,
        E_PublicationYear,
        E_TimesCitedWoSCore,
        E_180DayUsageCount,
        E_Since2013UsageCount,
        E_Volume,
        E_Error,
    };
    
    class ReferHZ
    {
        #region 文献的属性
        /// <summary>
        /// ID: DE, desc: 作者关键词 多行，连成一行，分号分割
        /// </summary>
        public string[] AuthorKeywords = null;
        /// <summary>
        /// ID: GP, desc: 图书组作者 None
        /// </summary>
        public string[] BookGroupAuthors = null;
        /// <summary>
        /// ID: CR, desc: 被引参考文献 每行一个，找DOI
        /// </summary>
        public string[] CitedReferences = null;
        /// <summary>
        /// ID: SC, desc: 研究领域 多行，连成一行，分号分割
        /// </summary>
        public string[] ResearchAreas = null;
        /// <summary>
        /// ID: AF, desc: 作者全名 每行一个作者
        /// </summary>
        public string[] AuthorFullNames = null;
        /// <summary>
        /// ID: AU, desc: 作者 每行一个作者
        /// </summary>
        public string[] Authors = null;
        /// <summary>
        /// ID: BA, desc: 图书作者 每行一个作者
        /// </summary>
        public string[] BookAuthors = null;
        /// <summary>
        /// ID: BE, desc: 图书编辑 每行一个作者
        /// </summary>
        public string[] BookEditors = null;
        /// <summary>
        /// ID: BF, desc: 书籍作者全名 每行一个作者
        /// </summary>
        public string[] BookAuthorFullNames = null;
        /// <summary>
        /// ID: CA, desc: 团体作者 每行一个作者
        /// </summary>
        public string[] GroupAuthors = null;
        /// <summary>
        /// ID: EM, desc: 电子邮件地址 多行，连成一行，分号分割
        /// </summary>
        public string[] EmailAddresses = null;
        /// <summary>
        /// ID: FU, desc: 资助机构 多行，连成一行，分号分割
        /// </summary>
        public string[] FundingOrgs = null;
        /// <summary>
        /// ID: WC, desc: WoS 分类 多行，连成一行，分号分割
        /// </summary>
        public string[] WoSCategories = null;
        /// <summary>
        /// ID: AB, desc: 摘要 一行
        /// </summary>
        public string Abstract = null;
        /// <summary>
        /// ID: BS, desc: 丛书副标题 添加
        /// </summary>
        public string BookSeriesSubtitle = null;
        /// <summary>
        /// ID: C1, desc: 地址 None
        /// </summary>
        public string Addresses = null;
        /// <summary>
        /// ID: C3, desc: 隶属关系 None
        /// </summary>
        public string Affiliations = null;
        /// <summary>
        /// ID: CL, desc: 会议地点 None
        /// </summary>
        public string ConferenceLocation = null;

        /// <summary>
        /// ID: CT, desc: 会议名称 None
        /// </summary>
        public string ConferenceTitle = null;
        /// <summary>
        /// ID: CY, desc: 会议日期 None
        /// </summary>
        public string ConferenceDate = null;
        /// <summary>
        /// ID: D2, desc: 预订 DOI None
        /// </summary>
        public string BookDOI = null;
        /// <summary>
        /// ID: DA, desc: 出口日期 None
        /// </summary>
        public string DateofExport = null;
        /// <summary>
        /// ID: DI, desc: DOI https://www.doi.org/
        /// </summary>
        public string DOI = null;
        /// <summary>
        /// ID: DT, desc: 文件类型 None
        /// </summary>
        public string DocumentType = null;
        /// <summary>
        /// ID: EA, desc: 抢先体验日期 None
        /// </summary>
        public string EarlyAccessDate = null;
        /// <summary>
        /// ID: EI, desc: eISSN None
        /// </summary>
        public string eISSN = null;
        /// <summary>
        /// ID: FX, desc: 资金文本 多行，连成一行 
        /// </summary>
        public string FundingText = null;
        /// <summary>
        /// ID: GA, desc: 身份证号码 None
        /// </summary>
        public string IDSNumber = null;
        /// <summary>
        /// ID: HC, desc: 高被引状态 None
        /// </summary>
        public string HighlyCitedStatus = null;
        /// <summary>
        /// ID: HO, desc: 会议主持人 None
        /// </summary>
        public string ConferenceHost = null;
        /// <summary>
        /// ID: HP, desc: 热点论文状态 None
        /// </summary>
        public string HotPaperStatus = null;
        /// <summary>
        /// ID: ID, desc: 关键字加 None
        /// </summary>
        public string KeywordsPlus = null;
        /// <summary>
        /// ID: IS, desc: 问题 None
        /// </summary>
        public string Issue = null;
        /// <summary>
        /// ID: J9, desc: 期刊缩写 None
        /// </summary>
        public string JournalAbbreviation = null;
        /// <summary>
        /// ID: JI, desc: ISO 期刊缩写 None
        /// </summary>
        public string JournalISOAbbreviation = null;
        /// <summary>
        /// ID: LA, desc: 语言 None
        /// </summary>
        public string Language = null;
        /// <summary>
        /// ID: MA, desc: 会议摘要 None
        /// </summary>
        public string MeetingAbstract = null;
        /// <summary>
        /// ID: OA, desc: 开放存取名称 None
        /// </summary>
        public string OpenAccessDesignations = null;
        /// <summary>
        /// ID: OI, desc: ORCID任务是通过允许研究人员与他们所有的研究活动相关联，包括出版物、
        /// 数据集、与研究机构的隶属关系以及资金支持等，进而改善信息流 None
        /// </summary>
        public string ORCIDs = null;
        /// <summary>
        /// ID: PA, desc: 发布者地址 None
        /// </summary>
        public string PublisherAddress = null;
        /// <summary>
        /// ID: PD, desc: 发布日期 None
        /// </summary>
        public string PublicationDate = null;
        /// <summary>
        /// ID: PI, desc: 出版商城市 None
        /// </summary>
        public string PublisherCity = null;
        /// <summary>
        /// ID: PM, desc: 已发布 ID None
        /// </summary>
        public string PubmedId = null;
        /// <summary>
        /// ID: PN, desc: 零件号 None
        /// </summary>
        public string PartNumber = null;
        /// <summary>
        /// ID: PT, desc: 出版物类型 None
        /// </summary>
        public string PublicationType = null;
        /// <summary>
        /// ID: PU, desc: 出版商 None
        /// </summary>
        public string Publisher = null;
        /// <summary>
        /// ID: RI, desc: 研究员 ID None
        /// </summary>
        public string ResearcherIds = null;
        /// <summary>
        /// ID: RP, desc: 转载地址 None
        /// </summary>
        public string ReprintAddresses = null;
        /// <summary>
        /// ID: SE, desc: 丛书名称 None
        /// </summary>
        public string BookSeriesTitle = null;
        /// <summary>
        /// ID: SI, desc: 特刊 None
        /// </summary>
        public string SpecialIssue = null;
        /// <summary>
        /// ID: SN, desc: ISSN None
        /// </summary>
        public string ISSN = null;
        /// <summary>
        /// ID: SO, desc: 来源标题 None
        /// </summary>
        public string SourceTitle = null;
        /// <summary>
        /// ID: SP, desc: 会议赞助商 None
        /// </summary>
        public string ConferenceSponsor = null;
        /// <summary>
        /// ID: SU, desc: 补充 None
        /// </summary>
        public string Supplement = null;
        /// <summary>
        /// ID: TI, desc: 文章标题 None
        /// </summary>
        public string ArticleTitle = null;
        /// <summary>
        /// ID: UT, desc: UT（唯一 WOS ID） None
        /// </summary>
        public string UTUniqueWOSID = null;
        /// <summary>
        /// ID: WE, desc: Web of Science 索引 None
        /// </summary>
        public string WebofScienceIndex = null;
        /// <summary>
        /// ID: BN, desc: 国际标准书号 None
        /// </summary>
        public int ISBN = -1;
        /// <summary>
        /// ID: Z9, desc: 被引频次，所有数据库 None
        /// </summary>
        public int TimesCitedAllDatabases = -1;
        /// <summary>
        /// ID: AR, desc: 文章编号 None
        /// </summary>
        public int ArticleNumber = -1;
        /// <summary>
        /// ID: BP, desc: 首页 None
        /// </summary>
        public int StartPage = -1;
        /// <summary>
        /// ID: EP, desc: 结束页 None
        /// </summary>
        public int EndPage = -1;
        /// <summary>
        /// ID: NR, desc: 被引参考文献计数 None
        /// </summary>
        public int CitedReferenceCount = -1;
        /// <summary>
        /// ID: PG, desc: 页数 None
        /// </summary>
        public int NumberofPages = -1;
        /// <summary>
        /// ID: PY, desc: 出版年份 None
        /// </summary>
        public int PublicationYear = -1;
        /// <summary>
        /// ID: TC, desc: 被引频次，WoS 核心 None
        /// </summary>
        public int TimesCitedWoSCore = -1;
        /// <summary>
        /// ID: U1, desc: 180 天使用次数 None
        /// </summary>
        public int Day180UsageCount = -1;
        /// <summary>
        /// ID: U2, desc: 自 2013 年以来的使用次数 None
        /// </summary>
        public int Since2013UsageCount = -1;
        /// <summary>
        /// ID: VL, desc: 体积 None
        /// </summary>
        public int Volume = -1;
        #endregion

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="Identification"></param>
        /// <param name="RefInfo"></param>
        /// <returns></returns>
        public string AddInfo(RefAttr Identification, List<string> RefInfo)
        {
            try
            {
                switch (Identification)
                {
                    case RefAttr.E_ArticleNumber:
                        ArticleNumber = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_StartPage:
                        StartPage = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_EndPage:
                        EndPage = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_CitedReferenceCount:
                        CitedReferenceCount = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_NumberofPages:
                        NumberofPages = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_PublicationYear:
                        PublicationYear = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_TimesCitedWoSCore:
                        TimesCitedWoSCore = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_180DayUsageCount:
                        Day180UsageCount = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_Since2013UsageCount:
                        Since2013UsageCount = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_Volume:
                        Volume = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_ISBN:
                        ISBN = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_TimesCitedAllDatabases:
                        TimesCitedAllDatabases = int.Parse(RefInfo[0].Trim());
                        break;

                    case RefAttr.E_Abstract:
                        Abstract = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_BookSeriesSubtitle:
                        BookSeriesSubtitle = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_FundingText:
                        FundingText = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Addresses:
                        Addresses = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Affiliations:
                        Affiliations = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ConferenceLocation:
                        ConferenceLocation = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ConferenceTitle:
                        ConferenceTitle = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ConferenceDate:
                        ConferenceDate = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_BookDOI:
                        BookDOI = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_DateofExport:
                        DateofExport = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_DocumentType:
                        DocumentType = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_EarlyAccessDate:
                        EarlyAccessDate = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_eISSN:
                        eISSN = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_IDSNumber:
                        IDSNumber = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_HighlyCitedStatus:
                        HighlyCitedStatus = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ConferenceHost:
                        ConferenceHost = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_HotPaperStatus:
                        HotPaperStatus = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_KeywordsPlus:
                        KeywordsPlus = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Issue:
                        Issue = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_JournalAbbreviation:
                        JournalAbbreviation = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_JournalISOAbbreviation:
                        JournalISOAbbreviation = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Language:
                        Language = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_MeetingAbstract:
                        MeetingAbstract = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_OpenAccessDesignations:
                        OpenAccessDesignations = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ORCIDs:
                        ORCIDs = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PublisherAddress:
                        PublisherAddress = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PublicationDate:
                        PublicationDate = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PublisherCity:
                        PublisherCity = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PubmedId:
                        PubmedId = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PartNumber:
                        PartNumber = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_PublicationType:
                        PublicationType = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Publisher:
                        Publisher = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ResearcherIds:
                        ResearcherIds = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ReprintAddresses:
                        ReprintAddresses = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_BookSeriesTitle:
                        BookSeriesTitle = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_SpecialIssue:
                        SpecialIssue = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ISSN:
                        ISSN = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_SourceTitle:
                        SourceTitle = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ConferenceSponsor:
                        ConferenceSponsor = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_Supplement:
                        Supplement = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_ArticleTitle:
                        ArticleTitle = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_UTUniqueWOSID:
                        UTUniqueWOSID = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_WebofScienceIndex:
                        WebofScienceIndex = ListString2String(RefInfo);
                        break;

                    case RefAttr.E_DOI:
                        DOI = "https://www.doi.org/" + ListString2String(RefInfo);
                        break;

                    case RefAttr.E_CitedReferences:
                        CitedReferences = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_AuthorFullNames:
                        AuthorFullNames = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_Authors:
                        Authors = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_BookAuthors:
                        BookAuthors = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_BookEditors:
                        BookEditors = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_BookAuthorFullNames:
                        BookAuthorFullNames = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_GroupAuthors:
                        GroupAuthors = ListString2OnePerLine(RefInfo);
                        break;

                    case RefAttr.E_EmailAddresses:
                        EmailAddresses = ListString2Split(RefInfo);
                        break;

                    case RefAttr.E_FundingOrgs:
                        FundingOrgs = ListString2Split(RefInfo);
                        break;

                    case RefAttr.E_WoSCategories:
                        WoSCategories = ListString2Split(RefInfo);
                        break;

                    case RefAttr.E_AuthorKeywords:
                        AuthorKeywords = ListString2Split(RefInfo);
                        break;

                    case RefAttr.E_ResearchAreas:
                        ResearchAreas = ListString2Split(RefInfo);
                        break;

                    case RefAttr.E_BookGroupAuthors:
                        BookGroupAuthors = ListString2OnePerLine(RefInfo);
                        break;
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public static RefAttr RIS2RefAttr(string RISIdentification)
        {
            switch (RISIdentification)
            {
                case "AR":
                    return RefAttr.E_ArticleNumber;

                case "BP":
                    return RefAttr.E_StartPage;

                case "EP":
                    return RefAttr.E_EndPage;

                case "NR":
                    return RefAttr.E_CitedReferenceCount;

                case "PG":
                    return RefAttr.E_NumberofPages;

                case "PY":
                    return RefAttr.E_PublicationYear;

                case "TC":
                    return RefAttr.E_TimesCitedWoSCore;

                case "U1":
                    return RefAttr.E_180DayUsageCount;

                case "U2":
                    return RefAttr.E_Since2013UsageCount;

                case "VL":
                    return RefAttr.E_Volume;

                case "BN":
                    return RefAttr.E_ISBN;

                case "Z9":
                    return RefAttr.E_TimesCitedAllDatabases;

                case "AB":
                    return RefAttr.E_Abstract;

                case "BS":
                    return RefAttr.E_BookSeriesSubtitle;

                case "FX":
                    return RefAttr.E_FundingText;

                case "C1":
                    return RefAttr.E_Addresses;

                case "C3":
                    return RefAttr.E_Affiliations;

                case "CL":
                    return RefAttr.E_ConferenceLocation;

                case "CT":
                    return RefAttr.E_ConferenceTitle;

                case "CY":
                    return RefAttr.E_ConferenceDate;

                case "D2":
                    return RefAttr.E_BookDOI;

                case "DA":
                    return RefAttr.E_DateofExport;

                case "DT":
                    return RefAttr.E_DocumentType;

                case "EA":
                    return RefAttr.E_EarlyAccessDate;

                case "EI":
                    return RefAttr.E_eISSN;

                case "GA":
                    return RefAttr.E_IDSNumber;

                case "HC":
                    return RefAttr.E_HighlyCitedStatus;

                case "HO":
                    return RefAttr.E_ConferenceHost;

                case "HP":
                    return RefAttr.E_HotPaperStatus;

                case "ID":
                    return RefAttr.E_KeywordsPlus;

                case "IS":
                    return RefAttr.E_Issue;

                case "J9":
                    return RefAttr.E_JournalAbbreviation;

                case "JI":
                    return RefAttr.E_JournalISOAbbreviation;

                case "LA":
                    return RefAttr.E_Language;

                case "MA":
                    return RefAttr.E_MeetingAbstract;

                case "OA":
                    return RefAttr.E_OpenAccessDesignations;

                case "OI":
                    return RefAttr.E_ORCIDs;

                case "PA":
                    return RefAttr.E_PublisherAddress;

                case "PD":
                    return RefAttr.E_PublicationDate;

                case "PI":
                    return RefAttr.E_PublisherCity;

                case "PM":
                    return RefAttr.E_PubmedId;

                case "PN":
                    return RefAttr.E_PartNumber;

                case "PT":
                    return RefAttr.E_PublicationType;

                case "PU":
                    return RefAttr.E_Publisher;

                case "RI":
                    return RefAttr.E_ResearcherIds;

                case "RP":
                    return RefAttr.E_ReprintAddresses;

                case "SE":
                    return RefAttr.E_BookSeriesTitle;

                case "SI":
                    return RefAttr.E_SpecialIssue;

                case "SN":
                    return RefAttr.E_ISSN;

                case "SO":
                    return RefAttr.E_SourceTitle;

                case "SP":
                    return RefAttr.E_ConferenceSponsor;

                case "SU":
                    return RefAttr.E_Supplement;

                case "TI":
                    return RefAttr.E_ArticleTitle;

                case "UT":
                    return RefAttr.E_UTUniqueWOSID;

                case "WE":
                    return RefAttr.E_WebofScienceIndex;

                case "DI":
                    return RefAttr.E_DOI;

                case "CR":
                    return RefAttr.E_CitedReferences;

                case "AF":
                    return RefAttr.E_AuthorFullNames;

                case "AU":
                    return RefAttr.E_Authors;

                case "BA":
                    return RefAttr.E_BookAuthors;

                case "BE":
                    return RefAttr.E_BookEditors;

                case "BF":
                    return RefAttr.E_BookAuthorFullNames;

                case "CA":
                    return RefAttr.E_GroupAuthors;

                case "EM":
                    return RefAttr.E_EmailAddresses;

                case "FU":
                    return RefAttr.E_FundingOrgs;

                case "WC":
                    return RefAttr.E_WoSCategories;

                case "DE":
                    return RefAttr.E_AuthorKeywords;

                case "SC":
                    return RefAttr.E_ResearchAreas;

                case "GP":
                    return RefAttr.E_BookGroupAuthors;

                default:
                    return RefAttr.E_Error;
            }
        }

        #region 解析字符串
        private string ListString2String(List<string> info)
        {
            string outstr = "";
            for (int i = 0; i < info.Count - 1; i++)
            {
                outstr += info[i];
                outstr += " ";
            }
            outstr += info[info.Count - 1];
            outstr += "\n";
            return outstr;
        }
        private string[] ListString2OnePerLine(List<string> info)
        {
            string[] outstrlist = new string[info.Count];
            for (int i = 0; i < info.Count; i++)
            {
                outstrlist[i] = info[i].Trim();
            }
            return outstrlist;
        }
        private string[] ListString2Split(List<string> info)
        {
            string line = ListString2String(info);
            string[] lines = line.Split(';');
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }
            return lines;
        }
        #endregion

    }
}
