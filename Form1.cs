using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 点菜管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fwy = "服务员名单.xml";
        string gly = "管理员名单.xml";
        private void Form1_Load(object sender, EventArgs e)
        {
            if(!File.Exists(fwy) || !File.Exists(gly))
            {
                login1.disable();
            }
            else
            {
                button1.Text = "已激活";
                button1.Enabled = false;
                login1.show(this, "服务员名单.xml", "管理员名单.xml");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JiHuo j = new JiHuo(this);
            j.Show();
        }
    }
}
