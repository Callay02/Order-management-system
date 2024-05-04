﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 点菜管理系统
{
    public partial class Form5 : Form
    {
        Form4 f4;
        public Form5(Form4 f4)
        {
            InitializeComponent();
            this.f4 = f4;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            f4.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("0", "菜名");
            treeView1.Nodes["0"].Nodes.Add("0-0", "蔬菜类");
            treeView1.Nodes["0"].Nodes.Add("0-1", "荤菜类");
            treeView1.Nodes["0"].Nodes.Add("0-2", "汤类");
        }
    }
}
