# _*_ coding:utf-8 _*_
r"""----------------------------------------------------------------------------
@File    : JieXiSD.py
@Time    : 2022/04/15 09:33:34
@Author  : Zheng Han 
@Contact : hzsongrentou1580@gmail.com
@License : (C)Copyright 2022, ZhengHan. All rights reserved.
@Desc    : 解析 https://www.sciencedirect.com/ html
-----------------------------------------------------------------------------"""

import re
from bs4 import BeautifulSoup
from bs4.element import Tag


class JieXiSD:
    """ 解析
    """

    def __init__(self) -> None:
        self.JournalTitle = ""  # 期刊名称
        self.Title = ""  # 文章题目
        self.Authors = []  # 作者
        self.DOI = ""  # DOI
        self.Abstract = ""  # 摘要
        self.Keywords = []  # 关键词
        self.MainBody = ""  # 文章主体
        self.bodys = []
        self.Refers = []

    def JieXi(self, html_file, out_file):
        with open(html_file, "r", encoding="utf-8") as f:
            soup = BeautifulSoup(f.read())
        artile_ele = soup.find("article")
        # 期刊名称 document.querySelector("#publication-title")
        j_h2 = artile_ele.find("h2", {"id": "publication-title"})
        self.JournalTitle = RemoveSpace(j_h2.get_text())
        # 题目 document.querySelector("#screen-reader-main-title")
        title_h1 = artile_ele.find(
            "h1", {"id": "screen-reader-main-title"}).span
        self.Title = RemoveSpace(title_h1.get_text())
        # 作者 document.querySelector("#author-group")
        author_div_a = artile_ele.find(
            "div", {"id": "author-group"}).find_all("a")
        self.Authors = [RemoveSpace(a.text) for a in author_div_a]
        self._XiuAu()
        # DOI document.querySelector("#article-identifier-links > a.doi")
        doi_div = artile_ele.find(
            "div", {"id": "article-identifier-links"}).find("a", {"class": "doi"})
        self.DOI = doi_div.text
        # 摘要 document.querySelector("#abstracts")
        abstract_div = artile_ele.find("div", {"id": "abstracts"})
        self.Abstract = RemoveSpace(abstract_div.get_text().replace("•", " "))
        # 关键词 document.querySelector("#kwrds0010")
        keywords_divs = artile_ele.find_all("div", {"class": "keyword"})
        self.Keywords = [RemoveSpace(keywords_div.text)
                         for keywords_div in keywords_divs]
        # document.querySelector("#body > div:nth-child(1)")
        body_div = artile_ele.find("div", {"id": "body"})
        self._GetBodys(body_div)
        self._XiuBody()
        # 参考文献
        refer_dl = artile_ele.find("dl", {"class":"references"})
        refer_dl = [dl0 for dl0 in refer_dl.contents if isinstance(dl0, Tag)]
        for i in range(0, len(refer_dl) -1, 2):
            self.Refers.append(RemoveSpace(refer_dl[i].get_text()+ " " + refer_dl[i+1].get_text()))
        self._WriteToTxt(out_file)

    def _WriteToTxt(self, txt_file):
        with open(txt_file, "w", encoding="utf-8") as f:
            f.write(self.Title + "\n")
            f.write("Authors: " + ", ".join(self.Authors) + "\n")
            f.write("Journal: " + self.JournalTitle + "\n")
            f.write("DOI: " + self.DOI + "\n")
            f.write("Abstract: " + self.Abstract + "\n")
            f.write("Keywords: " + ", ".join(self.Keywords) + "\n")
            f.write("\n" + self.MainBody)
            f.write("\nReferences\n" + "\n".join(self.Refers))

    def _XiuAu(self):
        ii = 0
        for au in self.Authors:
            au_x = ""
            for i in au:
                if i.isupper():
                    au_x += " " + i
                else:
                    au_x += i
            self.Authors[ii] = au_x
            ii += 1

    def _GetBodys(self, element0: Tag):
        for content in element0.contents:
            if isinstance(content, Tag):
                if content.name == "section":
                    self._GetBodys(content)
                elif content.name == "div":
                    self._GetBodys(content)
                else:
                    self.bodys.append(RemoveSpace(content.get_text()))
            else:
                self.bodys.append(content)

    def _XiuBody(self):
        #  Download : Download high-res image
        self.MainBody = "\n".join(self.bodys)
        self.MainBody = re.sub("\n+", "\n", self.MainBody)

        self.MainBody = self.MainBody.replace(
            "Download : Download full-size image ", "\n")
        ss = ""
        for line in self.MainBody.split("\n"):
            if line[:36] != " Download : Download high-res image ":
                ss += line + "\n"
        self.MainBody = ss
        self.MainBody = self.MainBody.replace(
            "下载：下载全尺寸图片 ", "\n")
        ss = ""
        for line in self.MainBody.split("\n"):
            if line[:14] != " 下载 ： 下载高分辨率图片":
                ss += line + "\n"
        self.MainBody = ss

        self.MainBody = self.MainBody.replace("\n ", "\n")



def RemoveSpace(text=""):
    t01 = re.sub("\s|\t|\n", " ", text)
    t02 = re.sub(" +", " ", t01)
    return t02


def main():
    # 期刊名称
    ss = JieXiSD()
    ss.JieXi("t02.html", "t03.txt")
    pass


if __name__ == "__main__":
    main()
