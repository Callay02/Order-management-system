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
    public partial class GeRenXinXiGuanLi : Form
    {
        string userName;
        string userType;
        string p;
        public GeRenXinXiGuanLi(string userType,string userName)
        {
            InitializeComponent();
            this.userName = userName;
            this.userType = userType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="" || textBox3.Text == "")
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                if (textBox3.Text != p)
                {
                    if(textBox4.Text == "")
                    {
                        textBox4.Enabled = true;
                        checkBox2.Enabled = true;
                        MessageBox.Show("请再次输入密码");
                        return;
                    }
                    else if (textBox4.Text != textBox3.Text)
                    {
                        MessageBox.Show("两次密码不一致");
                        return ;
                    }
                    
                }
                if (userType == "管理员")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("管理员名单.xml");
                    XmlNodeList xnl = xmlDoc.SelectNodes("//Employer");
                    foreach (XmlNode xn in xnl)
                    {
                        XmlElement xe = (XmlElement)xn;
                        if (xe.ChildNodes[1].InnerText == userName)
                        {
                            xe.ChildNodes[0].InnerText = textBox2.Text;
                            xe.ChildNodes[2].InnerText = textBox3.Text;
                            xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        }

                    }
                    xmlDoc.Save("管理员名单.xml");
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("服务员名单.xml");
                    XmlNodeList xnl = xmlDoc.SelectNodes("//Waiter");
                    foreach (XmlNode xn in xnl)
                    {
                        XmlElement xe = (XmlElement)xn;
                        if (xe.ChildNodes[1].InnerText == userName)
                        {
                            xe.ChildNodes[0].InnerText = textBox2.Text;
                            xe.ChildNodes[2].InnerText = textBox3.Text;
                            xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        }

                    }
                    xmlDoc.Save("服务员名单.xml");
                }
                MessageBox.Show("修改成功");
                this.Close();
            }
        }

        private void GeRenXinXiGuanLi_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = userName;
            textBox3.PasswordChar = '*';
            textBox4.Enabled = false;
            textBox4.PasswordChar = '*';
            checkBox2.Enabled = false;
            if (userType == "管理员")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("管理员名单.xml");
                XmlNodeList xnl = xmlDoc.SelectNodes("//Employer");
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.ChildNodes[1].InnerText == userName)
                    {
                        textBox2.Text = xe.ChildNodes[0].InnerText;
                        textBox3.Text = xe.ChildNodes[2].InnerText;
                        p = xe.ChildNodes[2].InnerText;
                    }

                }
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("服务员名单.xml");
                XmlNodeList xnl = xmlDoc.SelectNodes("//Waiter");
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.ChildNodes[1].InnerText == userName)
                    {
                        textBox2.Text = xe.ChildNodes[0].InnerText;
                        textBox3.Text = xe.ChildNodes[2].InnerText;
                        p = xe.ChildNodes[2].InnerText;
                    }

                }
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox3.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox4.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
            }
        }
    }
}
