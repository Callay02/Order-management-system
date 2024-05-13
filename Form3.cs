using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 点菜管理系统
{
    public partial class Form3 : Form
    {
        Form2 f2;
        string name;
        ListView listview;
        string billsXmlPath = "账单.xml";
        public Form3(Form2 f2,string name,ListView listView)
        {
            InitializeComponent();
            this.ControlBox=false;
            this.f2 = f2;
            this.name = name;
            this.listview = listView;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text="总价："+jieZhang1.init(listview,name,billsXmlPath).ToString()+"元";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (jieZhang1.save())
            {
                MessageBox.Show("结账成功");
                this.Close();
            }
            else
                MessageBox.Show("结账失败");
        }
    }
}
