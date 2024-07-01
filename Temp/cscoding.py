# -*- encoding: utf-8 -*-
r'''----------------------------------------------------------------------------
@File    :   cscoding.py
@Time    :   2022/02/04 12:41:04
@Author  :   Zheng Han 
@Contact :   hzsongrentou1580@gmail.com
@License :   (C)Copyright 2022, Zheng Han. All rights reserved.
@Refer   :   None
@Desc    :   
-----------------------------------------------------------------------------'''

import enum
from errno import ENETUNREACH


def coding01():

    with open(r"t01.txt", "r", encoding = "utf-8") as f:
        infos = f.readlines()
        pass
    for i in range(len(infos)):
        infos[i] = infos[i].strip()
    for i, info in enumerate(infos):
        if i%3==0:
            printsummary(infos[i+1])
            print("public int", info,end=";")

def printsummary(commendinfo):
    print("\n\n/// <summary>\n/// ", end="")
    ii = 0
    for i, info in enumerate(commendinfo):
        print(info, end="")
        if ii > 80:
            print("\n/// ", end="")
            ii = 0
        if u'\u4e00' <= info <= u'\u9fff':
            ii += 2
        else:
            ii += 1
    print("\n/// </summary>")

            
def main():
    coding01()
    return 0


if __name__ == '__main__':
    print("start" + "-"*75)
    main()
    print("\nend" + "-"*77)
    pass
