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
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
        Form f;
        string path1;
        string path2;
        public void show(Form f,string path1,string path2)
        {
            this.f=f;
            this.path1=path1;
            this.path2=path2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userType;
            if (radioButton1.Checked)
                userType = radioButton1.Text;
            else
                userType = radioButton2.Text;

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请填写登录信息");
                return;
            }
            string name;
            string lastDate;
            int result = UserLogin(userType, textBox1.Text, textBox2.Text, out name, out lastDate);
            if (result == -1)
            {
                MessageBox.Show("用户已离职");
                return;
            }
            else if (result == 0)
            {
                MessageBox.Show("用户不存在");
                return;
            }
            else if (result == 1)
            {
                if (userType == "服务员")
                {
                    MessageBox.Show(name + "欢迎回来,上次的登录时间为" + lastDate);
                    this.f.Hide();
                    Form2 f2 = new Form2((Form1)f, name);
                    f2.Show();
                }
                else
                {
                    MessageBox.Show(name + "欢迎回来,上次的登录时间为" + lastDate);
                    this.f.Hide();
                    Form4 f4 = new Form4((Form1)f, name);
                    f4.Show();
                }

            }
        }

        //登录方法
        private int UserLogin(string userType, string userName, string password, out string name, out string lastDate)
        {
            name = "";
            lastDate = "";
            if (userType == "服务员")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path1);
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Waiter");
                foreach (XmlNode node in nodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.ChildNodes[1].InnerText == userName && xe.ChildNodes[2].InnerText == password)
                    {
                        if (xe.Attributes[0].Value == "在职")
                        {
                            name = xe.ChildNodes[0].InnerText;
                            lastDate = xe.ChildNodes[3].InnerText;
                            xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            xmlDoc.Save(path1);
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                return 0;
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path2);
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Employer");

                foreach (XmlNode node in nodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.ChildNodes[1].InnerText == userName && xe.ChildNodes[2].InnerText == password)
                    {

                        name = xe.ChildNodes[0].InnerText;
                        lastDate = xe.ChildNodes[3].InnerText;
                        xe.ChildNodes[3].InnerText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        xmlDoc.Save(path2);
                        return 1;
                    }
                }
                return 0;
            }
        }
    }
}
