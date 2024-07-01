/*------------------------------------------------------------------------------
 * File    : AttrCol
 * Time    : 2022/5/3 11:15:07
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[AttrCol]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
    class AttrCol
    {
        private List<string> info = new List<string>();
        private int n = 0;
        public string GetOne()
        {
            return info[n++];
        }

        public string GetOne(int i)
        {
            return info[i];
        }

        public void N(int n)
        {
            this.n = n;
        }
    }
}
