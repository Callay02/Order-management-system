using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;

namespace 点菜管理系统
{
    public partial class Form5 : UserControl
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("0", "菜名");
            treeView1.Nodes["0"].Nodes.Add("0-0", "蔬菜类");
            treeView1.Nodes["0"].Nodes.Add("0-1", "荤菜类");
            treeView1.Nodes["0"].Nodes.Add("0-2", "汤类");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string file = "null";
            switch (treeView1.SelectedNode.Name)
            {
                case "0-0":
                    file="菜品-蔬菜类.xml";
                    break;
                case "0-1":
                    file = "菜品-荤菜类.xml";
                    break;
                case "0-2":
                    file = "菜品-汤类.xml";
                    break;
            }
            if (file != "null")
            {
                caiPinGuanLi1.show(file);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                caiPinGuanLi1.save();
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
