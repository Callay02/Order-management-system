using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 点菜管理系统
{
    public partial class Form8 : UserControl
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string name = "账单.XML";
                yeWuTongJi1.show1(name, dateTimePicker1.Value, dateTimePicker2.Value);
            }
            else
            {
                string name1 = "账单.XML", name2 = "服务员名单.XML";
                yeWuTongJi1.show2(name1, name2, dateTimePicker1.Value, dateTimePicker2.Value);
            }
        }
    }
}
