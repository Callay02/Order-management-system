using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;

namespace 点菜管理系统
{
    public partial class Form7 : UserControl
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
            DateTime dTP1 = dateTimePicker1.Value.Date;
            DateTime dTP2 = dateTimePicker2.Value.Date;
            string cbB = comboBox1.SelectedItem.ToString();
            genRenXiaoShouMingXi2.search("账单.xml", dTP1, dTP2, cbB);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelDataOperation.ExportToExcel(genRenXiaoShouMingXi2, comboBox1.SelectedItem.ToString());
        }
    }
}
