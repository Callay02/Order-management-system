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
    public partial class JiHuo : Form
    {
        Form1 f1;
        public JiHuo(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "88888888")
            {
                File.Move("服务员未激活", "服务员名单.xml");
                File.Move("管理员未激活", "管理员名单.xml");

                MessageBox.Show("激活成功！");
                f1.Controls["button1"].Text = "已激活";
                f1.Controls["button1"].Enabled = false;
                Login login =  (Login)f1.Controls["login1"];
                login.show(f1, "服务员名单.xml", "管理员名单.xml");
                this.Close();
            }
            else
            {
                MessageBox.Show("激活码错误！");
            }
        }
    }
}
