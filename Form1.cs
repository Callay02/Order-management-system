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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            string userType;
            if(radioButton1.Checked)
                userType =radioButton1.Text;
            else
                userType =radioButton2.Text;

            if(textBox1.Text=="" || textBox2.Text == "")
            {
                MessageBox.Show("请填写登录信息");
                return;
            }
            string name;
            string lastDate;
            int result = Login(userType, textBox1.Text, textBox2.Text, out name, out lastDate);
            if (result == -1)
            {
                MessageBox.Show("用户已离职");
                return ;
            }else if(result == 0)
            {
                MessageBox.Show("用户不存在");
                return;
            }else if(result == 1)
            {
                if (userType == "服务员")
                {
                    MessageBox.Show(name + "欢迎回来,上次的登录时间为" + lastDate);
                    this.Hide();
                    Form2 f2 = new Form2(this, name);
                    f2.Show();
                }
                else
                {
                    MessageBox.Show(name + "欢迎回来");
                    this.Hide();
                    Form4 f4 = new Form4(this,name);
                    f4.Show();
                }
                
            }
        }

        //注册按钮
        private void button2_Click(object sender, EventArgs e)
        {

        }

        //登录方法
        private int Login(string userType,string userName,string password,out string name,out string lastDate)
        {
            name = "";
            lastDate = "";
            if (userType == "服务员")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("服务员名单.xml");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Waiter");
                foreach(XmlNode node in nodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.ChildNodes[1].InnerText==userName && xe.ChildNodes[2].InnerText==password)
                    {
                        if(xe.Attributes[0].Value == "在职")
                        {
                            name = xe.ChildNodes[0].InnerText;
                            lastDate = xe.ChildNodes[3].InnerText;
                            xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            xmlDoc.Save("服务员名单.xml");
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                return 0;
            }else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("管理员名单.xml");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Employer");

                foreach (XmlNode node in nodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.ChildNodes[1].InnerText == userName && xe.ChildNodes[2].InnerText == password)
                    {
                        
                        name = xe.ChildNodes[0].InnerText;
                        lastDate = xe.ChildNodes[3].InnerText;
                        xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        xmlDoc.Save("管理员名单.xml");
                        return 1;
                    }
                }
                return 0;
            }
        }
    }
}
