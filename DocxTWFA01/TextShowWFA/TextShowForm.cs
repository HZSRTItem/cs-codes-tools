﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextShowWFA
{
    public partial class TextShowForm : Form
    {
        public TextShowForm(string text)
        {
            richTextBox1.Text = text;
            InitializeComponent();
        }
    }
}
