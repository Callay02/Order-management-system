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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("所有点菜员");
            comboBox1.SelectedItem = "所有点菜员";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("服务员名单.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Waiter");
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                comboBox1.Items.Add(xe.ChildNodes[0].InnerText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("账单.XML");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//Bill");
            DataTable table1 = new DataTable();
            table1.Columns.Add("菜名");
            table1.Columns.Add("单价");
            table1.Columns.Add("份数");
            table1.Columns.Add("点菜员");
            table1.Columns.Add("消费时间");
            foreach (XmlNode xn in nodeList)
            {
                string data = xn.Attributes[0].Value;
                string user = xn.Attributes[1].Value;
                XmlElement xe = (XmlElement)xn;
                for (int i = 0; i < xe.ChildNodes.Count; i++)
                {
                    if (Convert.ToDateTime(data).Date < dateTimePicker1.Value.Date || Convert.ToDateTime(data).Date > dateTimePicker2.Value.Date)
                    {
                        continue;
                    }
                    if (comboBox1.SelectedItem.ToString() != "所有点菜员" && comboBox1.SelectedItem.ToString() != user)
                    {
                        continue;
                    }
                    DataRow dr = table1.NewRow();
                    dr["菜名"] = xe.ChildNodes[i].InnerText;
                    dr["份数"] = xe.ChildNodes[i].Attributes[1].Value.ToString();
                    dr["单价"] = xe.ChildNodes[i].Attributes[0].Value.ToString();
                    dr["消费时间"] = data.ToString();
                    dr["点菜员"] = user.ToString();
                    table1.Rows.Add(dr);
                }
            }
            dataGridView1.DataSource = table1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelDataOperation.ExportToExcel(dataGridView1, comboBox1.SelectedItem.ToString());
        }
    }
}
