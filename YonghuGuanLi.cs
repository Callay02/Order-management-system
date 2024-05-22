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
    public partial class YonghuGuanLi : UserControl
    {
        public YonghuGuanLi()
        {
            InitializeComponent();
        }

        XmlDocument xmlDoc;
        string filePath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            xmlDoc = new XmlDocument();
            if (radioButton1.Checked)
            {
                filePath = "服务员名单.xml";
                DataTable dt = new DataTable();
                dt.Columns.Add("姓名");
                dt.Columns.Add("用户名（ID）");
                dt.Columns.Add("密码");
                dt.Columns.Add("上次登录时间");
                dt.Columns.Add("是否在职");
                dt.Columns.Add("OnOff");
                xmlDoc.Load(filePath);
                XmlNodeList xnl=xmlDoc.SelectNodes("//Waiter");
                foreach(XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    DataRow dr=dt.NewRow();
                    dr["姓名"] = xe.ChildNodes[0].InnerText;
                    dr["用户名（ID）"] = xe.ChildNodes[1].InnerText;
                    dr["密码"]= xe.ChildNodes[2].InnerText;
                    dr["上次登录时间"] = xe.ChildNodes[3].InnerText;
                    dr["是否在职"] = xe.Attributes[0].Value;
                    dr["OnOff"] = xe.ChildNodes[4].InnerText;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            else
            {
                filePath = "管理员名单.xml";
                DataTable dt = new DataTable();
                dt.Columns.Add("姓名");
                dt.Columns.Add("用户名（ID）");
                dt.Columns.Add("密码");
                dt.Columns.Add("上次登录时间");
                xmlDoc.Load(filePath);
                XmlNodeList xnl = xmlDoc.SelectNodes("//Employer");
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    DataRow dr = dt.NewRow();
                    dr["姓名"] = xe.ChildNodes[0].InnerText;
                    dr["用户名（ID）"] = xe.ChildNodes[1].InnerText;
                    dr["密码"] = xe.ChildNodes[2].InnerText;
                    dr["上次登录时间"] = xe.ChildNodes[3].InnerText;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void YonghuGuanLi_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (filePath == "")
            {
                return;
            }else if (radioButton1.Checked)
            {
                XmlNode root = xmlDoc.SelectSingleNode("Waiters");
                root.RemoveAll();
                xmlDoc.Save(filePath);
                DataTable dt = (DataTable)dataGridView1.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement xe = xmlDoc.CreateElement("Waiter");
                    XmlElement xesub1 = xmlDoc.CreateElement("name");
                    xesub1.InnerText = dr["姓名"].ToString();
                    XmlElement xesub2 = xmlDoc.CreateElement("UserName");
                    xesub2.InnerText = dr["用户名（ID）"].ToString();
                    XmlElement xesub3 = xmlDoc.CreateElement("Password");
                    xesub3.InnerText = dr["密码"].ToString();
                    XmlElement xesub4 = xmlDoc.CreateElement("Date");
                    xesub4.InnerText = dr["上次登录时间"].ToString();
                    XmlElement xesub5 = xmlDoc.CreateElement("OnOff");
                    xesub5.InnerText = dr["OnOff"].ToString();

                    XmlAttribute a=xmlDoc.CreateAttribute("enable");
                    a.Value = dr["是否在职"].ToString();

                    xe.AppendChild(xesub1);
                    xe.AppendChild(xesub2);
                    xe.AppendChild(xesub3);
                    xe.AppendChild(xesub4);
                    xe.AppendChild(xesub5);
                    xe.Attributes.Append(a);
                    root.AppendChild(xe);
                }
                xmlDoc.Save(filePath);
                MessageBox.Show("保存成功");
            }
            else
            {
                XmlNode root = xmlDoc.SelectSingleNode("Employee");
                root.RemoveAll();
                xmlDoc.Save(filePath);
                DataTable dt = (DataTable)dataGridView1.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement xe = xmlDoc.CreateElement("Employer");
                    XmlElement xesub1 = xmlDoc.CreateElement("EmployerName");
                    xesub1.InnerText = dr["姓名"].ToString();
                    XmlElement xesub2 = xmlDoc.CreateElement("UserName");
                    xesub2.InnerText = dr["用户名（ID）"].ToString();
                    XmlElement xesub3 = xmlDoc.CreateElement("Password");
                    xesub3.InnerText = dr["密码"].ToString();
                    XmlElement xesub4 = xmlDoc.CreateElement("Date");
                    xesub4.InnerText = dr["上次登录时间"].ToString();


                    xe.AppendChild(xesub1);
                    xe.AppendChild(xesub2);
                    xe.AppendChild(xesub3);
                    xe.AppendChild(xesub4);
                    root.AppendChild(xe);
                }
                xmlDoc.Save(filePath);
                MessageBox.Show("保存成功");
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1 || dataGridView1.CurrentCell.ColumnIndex ==3)
            {
                e.Control.KeyPress += new KeyPressEventHandler(keyPass);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(keyPass);
            }
            
        }
        private void keyPass(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
