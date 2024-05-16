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
            DataTable table1 = new DataTable();
            if (radioButton1.Checked)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("账单.XML");
                XmlNodeList nodeList = xmlDoc.SelectNodes("//Bill");
                table1.Columns.Add("菜名");
                table1.Columns.Add("平均单价");
                table1.Columns.Add("份数");
                table1.Columns.Add("总计金额");
                List<string> dish = new List<string> { };
                foreach (XmlNode xn in nodeList)
                {
                    string data = xn.Attributes[0].Value;
                    XmlElement xe = (XmlElement)xn;
                    for (int j = 0; j < xe.ChildNodes.Count; j++)
                    {
                        if (Convert.ToDateTime(data).Date < dateTimePicker1.Value.Date || Convert.ToDateTime(data).Date > dateTimePicker2.Value.Date)
                        {
                            continue;
                        }
                        if (dish.Contains(xe.ChildNodes[j].InnerText))
                        {
                            continue;
                        }
                        else
                        {
                            dish.Add(xe.ChildNodes[j].InnerText);
                        }
                    }
                }
                int num, sum;
                double price;
                for (int i = 0; i < dish.Count; i++)
                {
                    num = 0; sum = 0;
                    foreach (XmlNode xn in nodeList)
                    {
                        string data = xn.Attributes[0].Value;
                        XmlElement xe = (XmlElement)xn;
                        for (int k = 0; k < xe.ChildNodes.Count; k++)
                        {
                            if (Convert.ToDateTime(data).Date < dateTimePicker1.Value.Date || Convert.ToDateTime(data).Date > dateTimePicker2.Value.Date)
                            {
                                continue;
                            }
                            if (dish[i] == xe.ChildNodes[k].InnerText)
                            {
                                num += int.Parse(xe.ChildNodes[k].Attributes[1].Value);
                                sum += int.Parse(xe.ChildNodes[k].Attributes[0].Value) * int.Parse(xe.ChildNodes[k].Attributes[1].Value);
                            }
                        }
                    }
                    price = 1.00 * sum / num;
                    DataRow dr = table1.NewRow();
                    dr["菜名"] = dish[i];
                    dr["份数"] = num;
                    dr["平均单价"] = price;
                    dr["总计金额"] = sum;
                    table1.Rows.Add(dr);
                }
                dataGridView1.DataSource = table1;
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("服务员名单.XML");
                XmlNodeList nodeList1 = xmlDoc.SelectNodes("//Waiter");
                table1.Columns.Add("点菜员");
                table1.Columns.Add("总计桌数");
                table1.Columns.Add("总计份数");
                table1.Columns.Add("总计金额");
                foreach (XmlNode xn in nodeList1)
                {
                    XmlElement xe = (XmlElement)xn;
                    //comboBox1.Items.Add(xe.ChildNodes[0].InnerText);
                    xmlDoc.Load("账单.XML");
                    XmlNodeList nodeList = xmlDoc.SelectNodes("//Bill");
                    int num = 0, count = 0, sum = 0;
                    foreach(XmlNode yn in nodeList)
                    {
                        string data = yn.Attributes[0].Value;
                        string user = yn.Attributes[1].Value;
                        if (Convert.ToDateTime(data).Date < dateTimePicker1.Value.Date || Convert.ToDateTime(data).Date > dateTimePicker2.Value.Date)
                        {
                            continue;
                        }
                        XmlElement ye = (XmlElement)yn;
                        if (user.ToString() == xe.ChildNodes[0].InnerText)
                        {
                            num++;
                            for (int i = 0; i < ye.ChildNodes.Count; i++)
                            {
                                count += int.Parse(ye.ChildNodes[i].Attributes[1].Value);
                                sum += int.Parse(ye.ChildNodes[i].Attributes[0].Value) * int.Parse(ye.ChildNodes[i].Attributes[1].Value);
                            }
                        }
                    }
                    DataRow dr = table1.NewRow();
                    dr["点菜员"] = xe.ChildNodes[0].InnerText;
                    dr["总计桌数"] = num;
                    dr["总计份数"] = count;
                    dr["总计金额"] = sum;
                    table1.Rows.Add(dr);
                }
                dataGridView1.DataSource = table1;
            }
        }
    }
}
