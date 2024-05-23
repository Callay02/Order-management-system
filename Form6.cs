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
    public partial class Form6 : UserControl
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void loadComboBox(string file)
        {
            comboBox1.Items.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            XmlNodeList nodelist = xmlDoc.SelectNodes("//Dish");
            foreach (XmlNode node in nodelist)
            {
                XmlElement xe = (XmlElement)node;
                comboBox1.Items.Add(xe.ChildNodes[0].InnerText);
            }
        }

        string type;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked) {
                type = "荤菜";
                loadComboBox("菜品-荤菜类.xml");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                type = "素菜";
                loadComboBox("菜品-蔬菜类.xml");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                type = "汤类";
                loadComboBox("菜品-汤类.xml");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("菜名");
            dt.Columns.Add("单价");
            dt.Columns.Add("份数");
            dt.Columns.Add("点菜员");
            dt.Columns.Add("消费时间");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("账单.xml");
            XmlNodeList nodelist = xmlDoc.SelectNodes("//Bill");
            foreach(XmlNode node in nodelist)
            {
                
                XmlElement xe = (XmlElement)node;
                //判断时间
                DateTime time = Convert.ToDateTime(xe.Attributes[0].Value);
                if(time>=dateTimePicker1.Value && time <= dateTimePicker2.Value)
                {
                    DataRow dr = dt.NewRow();
                    dr["消费时间"] = xe.Attributes[0].Value;
                    dr["点菜员"] = xe.Attributes[1].Value;
                    foreach (XmlElement xe1 in xe.ChildNodes)
                    {
                        if (xe1.InnerText == comboBox1.Text)
                        {
                            dr["菜名"] = xe1.InnerText;
                            dr["单价"] = xe1.Attributes[0].Value;
                            dr["份数"] = xe1.Attributes[1].Value;
                            dt.Rows.Add(dr);
                            break;
                        }
                    }
                }
                
            }
            dataGridView1.DataSource = dt;
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TuBiao tb = new TuBiao("菜品销售明细",(DataTable)this.dataGridView1.DataSource);
            tb.Show();
        }
    }
}
