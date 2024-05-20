using System;
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
    public partial class Form4 : Form
    {
        Form1 f1;
        string name;
        string userName;
        public Form4(Form1 f1,string name, string userName)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.f1 = f1;
            this.name = name;
            this.userName = userName;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = name + "欢迎您！";
            this.toolStripStatusLabel3.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            f5 = new Form5();
            f6 = new Form6();
            f7 = new Form7();
            f8 = new Form8();

        }

        UserControl f5;
        UserControl f6 ;
        UserControl f7 ;
        UserControl f8 ;
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(f5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(f8);
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(f6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(f7);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            f1.Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel3.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            GeRenXinXiGuanLi g = new GeRenXinXiGuanLi("管理员",userName);
            g.Show();
        }
    }
}
