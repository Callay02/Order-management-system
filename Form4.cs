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
        public Form4(Form1 f1,string name)
        {
            InitializeComponent();
            this.f1 = f1;
            this.name = name;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.Text = name+"欢迎您！";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.Show();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.Show();
        }
    }
}
