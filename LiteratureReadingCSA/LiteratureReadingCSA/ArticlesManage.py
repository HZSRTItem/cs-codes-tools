# _*_ coding:utf-8 _*_
r"""----------------------------------------------------------------------------
@File    : ArticlesManage.py
@Time    : 2023/3/3 9:52
@Author  : Zheng Han 
@Contact : hzsongrentou1580@gmail.com
@License : (C)Copyright 2023, ZhengHan. All rights reserved.
@Desc    : ArticlesManager of ArticlesManage
pyinstaller -D -c -n srt_am ArticlesManage.py
-----------------------------------------------------------------------------"""

import json
import os
import sqlite3
import sys
import time
import yaml
import csv

from SRTArgs import SRTArgColl
from SRTReadWrite import SRTFileRW
from SRTReadWrite import SRTInfoFileRW

ARTICLES_JSON_FILENAME = os.path.join(os.path.split(sys.argv[0])[0], r"articles.json")
MAX_LEN_MARK = 16


class SRTArticle:

    def __init__(self, oid=None, title=None):
        self.title = ""  # 文献名
        self.oid = ""  # 文献唯一标识符
        self.mark_infos = {}  # 文献信息
        self.init(oid, title)

    def addMarkInfo(self, mark, info):
        if mark not in self.mark_infos:
            self.mark_infos[mark] = []
        if not (info == "" or info is None):
            if isinstance(info, list):
                self.mark_infos[mark] += info
            else:
                self.mark_infos[mark].append(info)

    def init(self, oid=None, title=None):
        if oid is None:
            oid = time.strftime('%Y%m%d%H%M%S', time.localtime())
        if title is None:
            title = ""
        self.oid = oid
        self.title = title

    def toDict(self):
        return {"title": self.title, "oid": self.oid, "mark_infos": self.mark_infos}

    def initDict(self, d: dict):
        self.title = d["title"]
        self.oid = d["oid"]
        self.mark_infos = d["mark_infos"]

    def show(self, front_str=""):
        print(front_str + "OID: {0}".format(self.oid))
        print(front_str + "Title: {0}".format(self.title))
        for i, k in enumerate(self.mark_infos):
            print(front_str + self.fmtMarkInfo(k, front_str=" " * (len(front_str) + 2)))

    def fmtMarkInfo(self, mark, front_str="  "):
        infos = self.mark_infos[mark]
        o_str = ""
        o_str += "MARK[\"{0}\"]: {1}".format(mark, len(infos))
        for i, info in enumerate(infos):
            o_str += "\n{2}{0:<3d}{1}".format(i + 1, info, front_str)
        return o_str

    def deleteMark(self, mark):
        if mark in self.mark_infos:
            return self.mark_infos.pop(mark)
        else:
            return None

    def deleteInfo(self, mark, n_info):
        infos = self.mark_infos[mark]
        if len(infos) == 0:
            return None
        if n_info < 0:
            n_info += len(self.mark_infos[mark])
        if n_info < 0 or n_info >= len(infos):
            return None
        return self.mark_infos[mark].pop(n_info)

    def __contains__(self, item):
        return item in self.mark_infos


class SRTArticles:

    def __init__(self):
        self.articles = {}

    def addArticle(self, oid=None, title=None):
        art = SRTArticle(oid, title)
        if art.oid in self.articles:
            return False
        else:
            self.articles[art.oid] = art
            return True

    def addByDict(self, d: dict, oid=None, title=None):
        if title is None:
            if "title" in d:
                title = d["title"]
                d.pop("title")
        art = SRTArticle(oid, title)
        if art.oid in self.articles:
            return False
        for k in d:
            for d0 in d[k]:
                art.addMarkInfo(k.upper(), d0)
        self.articles[art.oid] = art
        return True

    def save(self, filename, filetype="json"):
        # csv|json|txt|yaml
        filetype = filetype.lower()
        if filetype == "json":
            filename = os.path.splitext(filename)[0] + ".json"
            with open(filename, "w") as f:
                d = {}
                for k in self.articles:
                    d[k] = self.articles[k].toDict()
                json.dump(d, f)
        elif filetype == "csv":
            filename = os.path.splitext(filename)[0] + ".csv"
            with open(filename, "w", newline="") as f:
                csv_writer = csv.writer(f)
                csv_writer.writerow(["OID", "TITLE", "MARK", "INFO"])
                for oid in self.articles:
                    marks = self.articles[oid].mark_infos
                    if len(marks) == 0:
                        csv_writer.writerow(fmtcsvline([oid, self.articles[oid].title, "", ""]))
                    else:
                        for mark in self.articles[oid].mark_infos:
                            infos = self.articles[oid].mark_infos[mark]
                            if len(infos) == 0:
                                csv_writer.writerow(fmtcsvline([oid, self.articles[oid].title, mark, ""]))
                            else:
                                for info in infos:
                                    csv_writer.writerow(fmtcsvline([oid, self.articles[oid].title, mark, info]))
        elif filetype == "yaml":
            filename = os.path.splitext(filename)[0] + ".yaml"
            with open(filename, "w") as f:
                d = {}
                for k in self.articles:
                    d[k] = self.articles[k].toDict()
                yaml.dump(d, f)
        elif filetype == "txt":
            filename = os.path.splitext(filename)[0] + ".txt"
            with open(filename, "w") as f:
                for i, oid in enumerate(self.articles):
                    f.write("Article[{0}]:\n".format(i + 1))
                    f.write("  * OID: {0}\n".format(oid))
                    f.write("  * Title: {0}\n".format(self.articles[oid].title))
                    for k in self.articles[oid].mark_infos:
                        f.write("  * " + self.articles[oid].fmtMarkInfo(k, front_str=" " * 6) + "\n")
        elif filetype == "sqlite3":
            filename = os.path.splitext(filename)[0] + ".db"
            try:
                con = sqlite3.connect(filename)
                cur = con.cursor()
                cur.execute("drop table if exists SRT_ARTS;")
                cur.execute("""
                CREATE TABLE SRT_ARTS
                (
                    N    int PRIMARY KEY NOT NULL,
                    OID   CHAR(30)        NOT NULL,
                    TITLE TEXT,
                    MARK  TEXT,
                    INFO  TEXT
                );  
                """)
                # cur.executemany('INSERT INTO SRT_ARTS VALUES (?,?,?,?)', ())
                sdl_line = 'INSERT INTO SRT_ARTS (N, OID, TITLE, MARK, INFO) VALUES '
                n = 1
                for oid in self.articles:
                    marks = self.articles[oid].mark_infos
                    if len(marks) == 0:
                        cur.execute(sdl_line + str((n, oid, self.articles[oid].title, "", "")))
                        n += 1
                    else:
                        for mark in self.articles[oid].mark_infos:
                            infos = self.articles[oid].mark_infos[mark]
                            if len(infos) == 0:
                                cur.execute(sdl_line + str((n, oid, self.articles[oid].title, mark, "")))
                                n += 1
                            else:
                                for info in infos:
                                    cur.execute(sdl_line + str((n, oid, self.articles[oid].title, mark, info)))
                                    n += 1
                con.commit()
                con.close()
            except Exception as ex:
                print("Save to Sqlite3 fault.\nFile: {0}\nError: {1}".format(filename, ex))
        else:
            print("Warning: not support save file type: {0}".format(filetype))
            return False
        return True

    def load(self, filename):
        with open(filename, "r") as f:
            d_str = f.read()
            d_str = d_str.strip()
            if d_str != "":
                try:
                    d = json.loads(d_str)
                    for k in d:
                        art = SRTArticle()
                        art.initDict(d[k])
                        if k != art.oid:
                            print("Warning: load k=`{0}` not equal art.oid=`{1}`".format(k, art.oid))
                        self.articles[k] = art
                except Exception as ex:
                    print(ex)

    def showAll(self):
        arts = self.articles
        print("{0:<5} {1:<16} {2}".format("No", "OID", "TITLE"))
        print("{0:->5} {1:->16} {2:->26}".format(" ", " ", " "))
        for i, k in enumerate(arts):
            print("{0:<5d} {1:<16} {2}".format(i + 1, k, arts[k].title))

    @staticmethod
    def printArts(arts):
        print("{0:<5} {1:<16} {2}".format("No", "OID", "TITLE"))
        print("{0:->5} {1:->16} {2:->26}".format(" ", " ", " "))
        for i, k in enumerate(arts):
            print("{0:<5d} {1:<16} {2}".format(i + 1, k.oid, k.title))

    def __getitem__(self, item):
        return self.articles[item]

    def __contains__(self, item):
        return item in self.articles

    def changeTitle(self, oid, new_title):
        self.articles[oid].title = new_title

    def addMark(self, oid, mark, info=None):
        self.articles[oid].addMarkInfo(mark, info)

    def deleteMark(self, oid, mark, info=None):
        return self.articles[oid].deleteMark(mark)

    def deleteInfo(self, oid, mark, n_info):
        return self.articles[oid].deleteInfo(mark, n_info)

    def deleteOne(self, oid):
        return self.articles.pop(oid)

    def findByOid(self, oid, n):
        arts = []
        for k in self.articles:
            if oid in k:
                arts.append(self.articles[k])
        if len(arts) == 0:
            print("Can not find!")
            return None
        return arts[:n]

    def findByTitle(self, title, n=10):
        c_strs = fenci(title)
        similars = [[k, strSimilar(self.articles[k].title, c_strs)] for k in self.articles]
        similars.sort(key=lambda x: x[1], reverse=True)
        if len(similars) == 0:
            print("Can not find!")
            return None
        n = n if n < len(similars) else len(similars)
        arts = [self.articles[k[0]] for k in similars[:n]]
        return arts

    def findByInfo(self, info, n=10):
        c_strs = fenci(info)
        similars = []
        for oid in self.articles:
            for mark in self.articles[oid].mark_infos:
                for i, v in enumerate(self.articles[oid].mark_infos[mark]):
                    similars.append({
                        "oid": oid,
                        "mark": mark,
                        "iv": i,
                        "similar": strSimilar(v, c_strs)
                    })
        if len(similars) == 0:
            print("Can not find!")
            return None

        similars.sort(key=lambda x: x["similar"], reverse=True)

        if n == -1:
            n = len(similars)

        print("{0:<5} {1:<16} {2:<33} {3:<9} {4:<44}".format("No", "OID", "TITLE", "MARK", "INFO"))
        print("{0:->5} {1:->16} {2:->33} {3:->9} {4:->46}".format(" ", " ", " ", " ", " "))
        for i, k in enumerate(similars[:n]):
            title0 = self.articles[k["oid"]].title
            if len(title0) > 26:
                title0 = title0[:26] + " ..."
            v0 = self.articles[k["oid"]].mark_infos[k["mark"]][k["iv"]]
            if len(v0) > 40:
                v0 = v0[:40] + " ..."
            print("{0:<5d} {1:<16} {2:<33} {3:<9} {4:<44}".format(i + 1, k["oid"], title0, k["mark"], v0))

    def findByMark(self, mark: str, n=10):
        mark = mark.upper()
        similars = []
        for oid in self.articles:
            for mark0 in self.articles[oid].mark_infos:
                if mark in mark0:
                    similars.append({"oid": oid, "mark": mark})

        if len(similars) == 0:
            print("Can not find!")
            return None

        if n == -1:
            n = len(similars)

        print("{0:<5} {1:<16} {2:<53} {3:<9}".format("No", "OID", "TITLE", "MARK"))
        print("{0:->5} {1:->16} {2:->53} {3:->9}".format(" ", " ", " ", " ", " "))
        for i, k in enumerate(similars[:n]):
            title0 = self.articles[k["oid"]].title
            if len(title0) > 46:
                title0 = title0[:46] + " ..."
            print("{0:<5d} {1:<16} {2:<53} {3:<9}".format(i + 1, k["oid"], title0, k["mark"]))


def fenci(c_str: str):
    c_str = c_str.strip()
    ret = []
    c_ret = ""
    for ch in c_str:
        if ch == " " or ch == "\t" or ch == "\n":
            if c_ret != "":
                ret.append(c_ret)
                c_ret = ""
        elif '\u4e00' <= ch <= '\u9fff':
            if c_ret != "":
                ret.append(c_ret)
            ret.append(ch)
            c_ret = ""
        else:
            c_ret += ch
    if c_ret != "":
        ret.append(c_ret)
    return ret


def strSimilar(o_str, c_strs):
    similar = 0
    for ch in c_strs:
        if ch in o_str:
            similar += 1
    return similar * 1.0 / len(c_strs)


def fmtcsvline(line):
    # line[0] = "{0}".format(line[0])
    # line[1] = "\"{0}\"".format(line[1])
    # line[2] = "\"{0}\"".format(line[2])
    # line[3] = "\"{0}\"".format(line[3])
    return line


def askYes(info):
    print(info, end="")
    line = input(" [y/n]: ")
    if len(line) == 0:
        return False
    if line[0] == "y":
        return True
    return False


def tiaoMark(mark: str):
    if mark is not None:
        mark = mark.upper()
        if len(mark) > MAX_LEN_MARK:
            mark = mark[:MAX_LEN_MARK]
    return mark


def backFile(back_file):
    if not os.path.isfile(back_file):
        return
    o_dir, fn = os.path.split(back_file)
    back_dir = os.path.join(o_dir, "back_articles")
    if not os.path.isdir(back_dir):
        os.mkdir(back_dir)
    fn0, ext = os.path.splitext(fn)
    to_file = os.path.join(back_dir, "{0}_{1}{2}".format(fn0, time.strftime('%Y%m%d', time.localtime()), ext))
    if not os.path.isfile(to_file):
        with open(to_file, "w") as f:
            fs = open(back_file, "r")
            f.write(fs.read())
            fs.close()


def fmtRIS(ris_fn):
    ris_fmt_fn = os.path.join(os.path.dirname(sys.argv[0]), "ris.format")
    # print(ris_fmt_fn)
    if not os.path.isfile(ris_fmt_fn):
        print("Warning: can not find ris format file as ris.format.")
        with open(ris_fn, "r",  encoding="utf-8") as f:
            d = {}
            for line in f:
                line = line.strip()
                if line == "ER  -":
                    break
                lines = line.split("-", 1)
                mark0 = lines[0].strip()
                info0 = None
                is_add = False
                if len(lines) == 2:
                    info0 = lines[1].strip()
                if mark0 == "TY":
                    mark0 = "type"
                    is_add = True
                elif mark0 == "T1":
                    mark0 = "title"
                    is_add = True
                elif mark0 == "AU":
                    mark0 = "authors"
                    is_add = True
                elif mark0 == "JO":
                    mark0 = "journal"
                    is_add = True
                elif mark0 == "PY":
                    mark0 = "date"
                    is_add = True
                elif mark0 == "DA":
                    mark0 = "date"
                    is_add = True
                elif mark0 == "DO":
                    mark0 = "doi"
                    is_add = True
                elif mark0 == "UR":
                    mark0 = "url"
                    is_add = True
                elif mark0 == "KW":
                    mark0 = "keywords"
                    is_add = True
                elif mark0 == "AB":
                    mark0 = "abstract"
                    is_add = True
                if is_add:
                    if mark0 not in d:
                        d[mark0] = []
                    if info0 is not None:
                        d[mark0].append(info0)
            return d
    else:
        sf = SRTInfoFileRW(ris_fmt_fn)
        ris_fmt = sf.readAsDict()
        with open(ris_fn, "r", encoding="utf-8") as f:
            d = {}
            for line in f:
                line = line.strip()
                if line == "ER  -":
                    break
                lines = line.split("-", 1)
                mark0 = lines[0].strip()
                info0 = None
                if len(lines) == 2:
                    info0 = lines[1].strip()
                if mark0 in ris_fmt:
                    mark0 = ris_fmt[mark0]
                    if mark0 not in d:
                        d[mark0] = []
                    if info0 is not None:
                        d[mark0].append(info0)
        return d


class SRTArticlesManager:

    def __init__(self):
        self.args = SRTArgColl("srt_am", "A tool for managing documents, including adding, deleting, and checking, \n"
                                         "is mainly used to manage the information of documents related to documents")
        """
        add: Add a code file
        find: Find a code file and show file info
        load: Load a code file into a folder
        update: Update a file in the code base
        """
        self.args.add_arg("add", help_info="add a article by title")
        self.args.add_arg("find", help_info="find a article by title or oid")
        self.args.add_arg("update", help_info="update a article information")
        self.args.add_arg("delete", help_info="add a article by oid")
        self.args.add_arg("show", help_info="show a article information by oid")
        self.args.add_arg("export", help_info="export all article to output file")
        self.args.add_arg("help", help_info="get help of this", mark="--h", atype=3)
        self.articles = SRTArticles()

        if not os.path.isfile(ARTICLES_JSON_FILENAME):
            with open(ARTICLES_JSON_FILENAME, "w") as f:
                f.write("\n")
        else:
            back_file = os.path.splitext(ARTICLES_JSON_FILENAME)[0] + "_back.json"
            try:
                with open(back_file, "w") as f:
                    fs = open(ARTICLES_JSON_FILENAME, "r")
                    f.write(fs.read())
                    fs.close()
            except Exception as ex:
                print("Can not back `{0}` ".format(ARTICLES_JSON_FILENAME))
                print(ex)
                return
        backFile(ARTICLES_JSON_FILENAME)
        self.articles.load(ARTICLES_JSON_FILENAME)

    def add(self, argv):
        print("/------* Add *------/")
        add_args = SRTArgColl(self.args.name + " add", self.args["add"].help_info)
        add_args.add_arg("title", help_info="add a article by title", n_max=256)
        add_args.add_arg("oid", help_info="only one id of this article", mark="-oid", atype=2)
        add_args.add_arg("ris_file", help_info="input as ris file", mark="-ris", atype=2)
        add_args.add_arg("yes", help_info="Whether to update", mark="--y", atype=3)
        add_args.add_arg("help", help_info="get help of add", mark="--h", atype=3)
        if len(argv) == 0:
            print(add_args.usage())
            return
        add_args.fmt(argv)
        is_yes = add_args["yes"].is_bool
        if add_args["help"].is_bool:
            print(add_args.usage())
            return

        oid = add_args["oid"].get_element()
        if oid is None:
            oid = time.strftime('%Y%m%d%H%M%S', time.localtime())
        if oid in self.articles:
            print("OID: \"{0}\" have in articles.".format(oid))
            return

        ris_file = add_args["ris_file"].get_element()
        title_list = add_args["title"].get_elements()
        if ris_file is not None:
            d = fmtRIS(ris_file)
            title = None
            if "title" not in d:
                if title_list:
                    title = " ".join(list(map(str, title_list)))
            else:
                if len(d["title"]) >= 1:
                    title = d["title"][-1]
            if not is_yes:
                ask_str = "Whether to add article of \n  * OID: {0} \n  * Title: \"{1}\"\n".format(oid, title)
                for k in d:
                    ask_str += "  * Mark[{0}]:\n".format(k.upper())
                    tt = 1
                    for t in d[k]:
                        ask_str += "      {0:<2} {1}:\n".format(tt, t)
                        tt += 1
                is_yes = askYes(ask_str)
            if is_yes:
                self.articles.addByDict(d, oid=oid, title=title)
                print("Success Add!")

            return

        if title_list:
            title = " ".join(list(map(str, title_list)))
            if not is_yes:
                is_yes = askYes("Whether to add article of \n  * OID: {0} \n  * Title: \"{1}\" \n  ".format(oid, title))
            if is_yes:
                self.articles.addArticle(oid, title)
                print("Success Add!")
        else:
            print(add_args.usage())

    def delete(self, argv):
        print("/------* Delete *------/")
        delete_args = SRTArgColl(self.args.name + " delete", self.args["add"].help_info)
        delete_args.add_arg("oid", help_info="only one id of this article")
        delete_args.add_arg("yes", help_info="Whether to delete", mark="--y", atype=3)
        delete_args.add_arg("help", help_info="get help of delete", mark="--h", atype=3)
        if len(argv) == 0:
            print(delete_args.usage())
            return
        delete_args.fmt(argv)
        if delete_args["help"].is_bool:
            print(delete_args.usage())
            return
        is_yes = delete_args["yes"].is_bool
        oid = delete_args["oid"].get_element()
        if oid is None:
            print("Can not input oid of update article")
            return
        if oid not in self.articles:
            print("Can not find oid:`{0}` in articles.\n "
                  "Use `{1} show --a` to look oid".format(oid, self.args.name))
            return
        self.articles[oid].show("  * ")
        if not is_yes:
            is_yes = askYes("Whether to delete article of OID: {0}".format(oid))
        if is_yes:
            art = self.articles.deleteOne(oid)
            print("Success Delete!")
        pass

    def update(self, argv):
        print("/------* Update *------/")
        update_args = SRTArgColl(self.args.name + " update", self.args["update"].help_info)
        update_args.add_arg("oid", help_info="only one id of this article")
        update_args.add_arg("title", help_info="title of article", mark="-title", atype=2)
        update_args.add_arg("mark", help_info="mark of article", mark="-mark", atype=2)
        update_args.add_arg("info", help_info="info of mark mark", mark="-info", atype=2)
        update_args.add_arg("info_n", help_info="number info of mark", mark="-info-n", atype=2)
        update_args.add_arg("change_title", help_info="update of change title", mark="--change-title", atype=3)
        update_args.add_arg("add_mark_info", help_info="update of add mark", mark="--add-mark-info", atype=3)
        update_args.add_arg("delete_mark", help_info="update of delete mark", mark="--delete-mark", atype=3)
        update_args.add_arg("delete_info", help_info="update of delete info", mark="--delete-info", atype=3)
        update_args.add_arg("help", help_info="get help of add", mark="--h", atype=3)
        update_args.add_arg("yes", help_info="Whether to update", mark="--y", atype=3)
        if len(argv) == 0:
            print(update_args.usage())
            return
        update_args.fmt(argv)
        if update_args["help"].is_bool:
            print(update_args.usage())
            return

        oid = update_args["oid"].get_element()
        if oid is None:
            print("Can not input oid of update article")
            return
        if oid not in self.articles:
            print("Can not find oid:`{0}` in articles.\n "
                  "Use `{1} show --a` to look oid".format(oid, self.args.name))
            return
        art: SRTArticle = self.articles[oid]
        print("Update article as follow:")
        art.show(front_str="  * ")
        title = update_args["title"].get_element()
        mark = update_args["mark"].get_element()
        mark = tiaoMark(mark)
        info = update_args["info"].get_elements()
        info_n = update_args["info_n"].get_element()
        is_yes = update_args["yes"].is_bool

        if update_args["change_title"].is_bool:
            if title is None:
                print("Can not get update title.\nUse `-title title` to input.")
                return
            if not is_yes:
                is_yes = askYes("Whether to change title to\n  \"{0}\"\n".format(title))
            if is_yes:
                self.articles.changeTitle(oid, title)
                print("Success change title!")

        elif update_args["add_mark_info"].is_bool:
            if mark is None:
                print("`--add-mark` have to input `-mark`")
            else:
                if not is_yes:
                    print("Update: \n  * Mark: \"{0}\"\n  * Info: ".format(mark))
                    for i_info, info0 in enumerate(info):
                        print("      {0:<2} {1}".format(i_info + 1, info0))
                    is_yes = askYes("Whether to add mark: \"{0}\"".format(mark))
                if is_yes:
                    self.articles.addMark(oid, mark, info)
                    print("Success add mark info!")

        elif update_args["delete_mark"].is_bool:
            if mark is None:
                print("`--delete-mark` have to input `-mark`")
            else:
                if mark not in art:
                    print("Can not find mark:\"{0}\" in this article.".format(mark))
                    return
                if not is_yes:
                    is_yes = askYes("Whether to add delete mark:\"{0}\" all information".format(mark))
                if is_yes:
                    infos = self.articles.deleteMark(oid, mark)
                    print("Success delete mark: \"{0}\"\nInfos:{1}".format(mark, infos))

        elif update_args["delete_info"].is_bool:
            if mark is None:
                print("`--delete-mark` have to input `-mark`")
            else:
                if mark not in art:
                    print("Can not find mark:\"{0}\" in this article.")
                    return
                n = 0
                try:
                    n = int(info_n)
                except Exception as ex:
                    print("Can not `-info-n`:\"{0}\" as int.".format(info_n))
                    print(ex)
                    return
                if not is_yes:
                    is_yes = askYes(
                        "Whether to add delete mark:\"{0}\" of information number:{1}\n".format(mark, info_n))
                if is_yes:
                    info1 = self.articles.deleteInfo(oid, mark, n)
                    print("Success delete mark: \"{0}\"\n  Info:{1}".format(mark, info1))

        else:
            print("Please input a style: \n"
                  "`--change-title` or `--add-mark-info` or `--delete-mark` or `--delete-info`")
        pass

    def find(self, argv):
        print("/------* Find *------/")
        find_args = SRTArgColl(self.args.name + " find", self.args["update"].help_info)
        find_args.add_arg("oid", help_info="only one id of this article, find contain", mark="-oid", atype=2)
        find_args.add_arg("title", help_info="title of article, find similar", mark="-title", atype=2)
        find_args.add_arg("mark", help_info="mark of article, find contain", mark="-mark", atype=2)
        find_args.add_arg("info", help_info="info of mark mark, find similar", mark="-info", atype=2)
        find_args.add_arg("n", help_info="number of output default: 10", mark="-n", atype=2)
        # find_args.add_arg("all", help_info="output all find default", mark="--a", atype=3)
        # find_args.add_arg("so", help_info="output brief information", mark="--so", atype=3)
        # find_args.add_arg("more", help_info="output more information", mark="--m", atype=3)
        find_args.add_arg("help", help_info="get help of find", mark="--h", atype=3)
        if len(argv) == 0:
            print(find_args.usage())
            return
        find_args.fmt(argv)
        if find_args["help"].is_bool:
            print(find_args.usage())
            return

        n_ = find_args["n"].get_element()
        n = 10
        if n_ is not None:
            try:
                n = int(n_)
            except:
                print("Warning: Can not `-n`:\"{0}\" as int.".format(n_))

        oid = find_args["oid"].get_element()
        title = find_args["title"].get_element()
        mark = find_args["mark"].get_element()
        info = find_args["info"].get_element()

        arts_oid = []
        if oid is not None:
            print("\nFind By OID: \"{0}\"\n".format(oid))
            arts_oid = self.articles.findByOid(oid, n)
            if arts_oid is not None:
                SRTArticles.printArts(arts_oid)

        arts_title = []
        if title is not None:
            print("\nFind By Title: \"{0}\"\n".format(title))
            arts_title = self.articles.findByTitle(title, n)
            if arts_oid is not None:
                SRTArticles.printArts(arts_title)

        arts_mark = []
        if mark is not None:
            print("\nFind By Mark: \"{0}\"\n".format(mark))
            self.articles.findByMark(mark, n)

        arts_info = []
        if info is not None:
            print("\nFind By Info \"{0}\"\n".format(info))
            self.articles.findByInfo(info, n)

        pass

    def show(self, argv):
        print("/------* Show *------/")
        show_args = SRTArgColl(self.args.name + " show", self.args["show"].help_info)
        show_args.add_arg("oid", help_info="only one id of this article", mark="-oid", atype=2)
        show_args.add_arg("all", help_info="show all title", mark="--a", atype=3)
        show_args.add_arg("help", help_info="get help of add", mark="--h", atype=3)
        if len(argv) == 0:
            print(show_args.usage())
            return
        show_args.fmt(argv)
        if show_args["help"].is_bool:
            print(show_args.usage())
            return
        if show_args["all"].is_bool:
            self.articles.showAll()
            return
        oid = show_args["oid"].get_element()
        if oid is not None:
            if oid in self.articles:
                self.articles[oid].show()
            else:
                print("OID: \"{0}\" not in articles.".format(oid))
        else:
            print("Not get effective arg.")
            print(show_args.usage())

    def export(self, argv):
        print("/------* Export *------/")
        export_args = SRTArgColl(self.args.name + " show", self.args["show"].help_info)
        export_args.add_arg("output_fn", help_info="output file name")
        export_args.add_arg("file_type", help_info="output file format `csv|json|txt|yaml|sqlite3` default:csv",
                            mark="-ft", atype=2)
        export_args.add_arg("help", help_info="get help of add", mark="--h", atype=3)
        if len(argv) == 0:
            print(export_args.usage())
            return
        export_args.fmt(argv)
        if export_args["help"].is_bool:
            print(export_args.usage())
            return

        output_fn = export_args["output_fn"].get_element()
        if output_fn is None:
            print("Can not find output file name.")
            return

        file_type = export_args["file_type"].get_element()
        if file_type is None:
            file_type = csv

        if self.articles.save(output_fn, filetype=file_type):
            print("Success export to `{0}`".format(output_fn))
        else:
            print("Fault export to `{0}`".format(output_fn))

    def usage(self):
        print(self.args.usage())
        pass

    def main(self, argv):
        if len(argv) <= 1:
            print("Please input mark of `add|delete|update|find`\nUse --h get help\n")
            return
        if argv[1] == "--h":
            print(self.args.usage())
            return

        if argv[1] == "add":
            self.add(argv[2:])
        elif argv[1] == "delete":
            self.delete(argv[2:])
        elif argv[1] == "update":
            self.update(argv[2:])
        elif argv[1] == "find":
            self.find(argv[2:])
        elif argv[1] == "show":
            self.show(argv[2:])
        elif argv[1] == "export":
            self.export(argv[2:])
        else:
            print("Please input mark of `add|delete|update|find|show` not `{0}`\nUse --h get help\n".format(argv[1]))
        self.articles.save(ARTICLES_JSON_FILENAME)


def main():
    am = SRTArticlesManager()
    am.main(sys.argv)
    # am.main(["srt_am", "find", "-title", "Polarimetric Phase and Implications"])
    # print(fmtRIS(r"S0924271621000599.ris"))
    # am.main(["sys.argv", "export", "out", "-ft", "csv"])
    # d = {
    #     "20230303202716": {
    #         "title": "Update Dynamic programming algorithm optimization for spoken word recognition",
    #         "oid": "20230303202716",
    #         "mark_infos": {
    #             "LKF": [
    #                 "D:\\SpecialProjects\\ReferAuto\\ArticlesManager\\ArticlesManage.py",
    #                 "D:\\SpecialProjects\\ReferAuto\\ArticlesManager\\ArticlesManage.py"
    #             ],
    #             "JO": [
    #                 "IEEE Transactions on Acoustics, Speech, and Signal Processing"
    #             ],
    #             "AU": [
    #                 "au 1"
    #             ]
    #         }
    #     },
    #     "20230304162536": {
    #         "title": "Land cover and land use classification performance of machine learning algorithms in "
    #                  "a boreal landscape using Sentinel-2 data",
    #         "oid": "20230304162536",
    #         "mark_infos": {}
    #     }
    # }
    # am.articles.save("save", filetype="json")
    # am.articles.save("save", filetype="yaml")
    # am.articles.save("save", filetype="csv")
    # am.articles.save("save", filetype="txt")
    # am.articles.save("save", filetype="sqle3")

    # cc = """
    # OID: 20230303202716
    # Title: Dynamic programming algorithm optimization for spoken word recognition
    # python在执行代码过程是不知道这个字符是什么意思的、是否是中文，而
    # 是把所有代码翻译成二进制也就是000111这种形式，机器可以看懂的语言。
    # """
    # oo = """
    #   ssaf   20230303202716 a sd sdv sd advdsf sdfefsd
    # Title:programming algorithm for spoken word recognition
    # """
    # print("-" * 60)
    # print(cc)
    # print("-" * 60)
    # print(oo)
    # print("-" * 60)
    # f = fenci(cc)
    # for f0 in f:
    #     print(f0)
    # print(strSimilar(oo, cc))
    pass


if __name__ == "__main__":
    main()
