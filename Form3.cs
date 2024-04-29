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
    public partial class Form3 : Form
    {
        Form2 f2;
        string name;
        ListView listview;
        string billsXmlPath = "账单.xml";
        public Form3(Form2 f2,string name,ListView listView)
        {
            InitializeComponent();
            this.ControlBox=false;
            this.f2 = f2;
            this.name = name;
            this.listview = listView;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("菜名", 140);
            listView1.Columns.Add("价格", 100);
            listView1.View = View.Details;

            for (int i = 0; i < listview.Items.Count; i++)
            {
                listView1.Items.Add(listview.Items[i].Text);
                int count = int.Parse(listview.Items[i].SubItems[1].Text);
                int price = int.Parse(listview.Items[i].SubItems[2].Text);
                listView1.Items[i].SubItems.Add((count * price).ToString());
            }
            //计算总价
            int total = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                total += int.Parse(listView1.Items[i].SubItems[1].Text);
            }
            label1.Text += total.ToString()+" 元";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (addBill())
            {
                MessageBox.Show("结账成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("结账失败");
            }
        }

        //添加账单(结账)
        private bool addBill()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(billsXmlPath);
                XmlNode root = xmlDoc.SelectSingleNode("Bills");

                //创建Bill节点
                XmlElement xe = xmlDoc.CreateElement("Bill");
                xe.SetAttribute("Time", DateTime.Now.ToString("yyyy-MM-dd"));
                xe.SetAttribute("Waiter", name);
                //遍历listview添加Dish节点
                for (int i = 0; i < listview.Items.Count; i++)
                {
                    XmlElement subXe = xmlDoc.CreateElement("Dish");
                    subXe.InnerText = listview.Items[i].Text;
                    subXe.SetAttribute("Price", listview.Items[i].SubItems[1].Text);
                    subXe.SetAttribute("Num", listview.Items[i].SubItems[2].Text);
                    xe.AppendChild(subXe);
                }
                root.AppendChild(xe);
                xmlDoc.Save(billsXmlPath);
                return true;
            }
            catch { 
                return false;
            }
            
        }
    }
}
