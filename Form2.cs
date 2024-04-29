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
    public partial class Form2 : Form
    {
        Form1 f1;
        string userName;
        public Form2(Form1 f1, string userName)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            this.f1 = f1;
            this.userName = userName;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("0", "菜名");
            treeView1.Nodes["0"].Nodes.Add("0-0", "蔬菜类");
            treeView1.Nodes["0"].Nodes.Add("0-1", "荤菜类");
            treeView1.Nodes["0"].Nodes.Add("0-2", "汤类");

            listView1.Columns.Add("菜名", 90);
            listView1.Columns.Add("单价", 70);
            listView1.View = View.Details;

            listView2.Columns.Add("菜名", 90);
            listView2.Columns.Add("单价", 70);
            listView2.Columns.Add("份数", 70);
            listView2.View = View.Details;
        }

        //加载菜单
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView1.Items.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList nodeList;
            switch (treeView1.SelectedNode.Name)
            {
                case "0-0":
                    
                    xmlDoc.Load("菜品-蔬菜类.xml");
                    nodeList = xmlDoc.SelectNodes("//Dish");
                    foreach(XmlNode node in nodeList)
                    {
                        XmlElement xe = (XmlElement)node;
                        ListViewItem item = new ListViewItem(xe.ChildNodes[0].InnerText);
                        item.SubItems.Add(xe.ChildNodes[1].InnerText);
                        listView1.Items.Add(item);
                    }
                    break;

                case "0-1":
                    xmlDoc.Load("菜品-荤菜类.xml");
                    nodeList = xmlDoc.SelectNodes("//Dish");
                    foreach (XmlNode node in nodeList)
                    {
                        XmlElement xe = (XmlElement)node;
                        ListViewItem item = new ListViewItem(xe.ChildNodes[0].InnerText);
                        item.SubItems.Add(xe.ChildNodes[1].InnerText);
                        listView1.Items.Add(item);
                    }
                    break;

                case "0-2":
                    xmlDoc.Load("菜品-汤类.xml");
                    nodeList = xmlDoc.SelectNodes("//Dish");
                    foreach (XmlNode node in nodeList)
                    {
                        XmlElement xe = (XmlElement)node;
                        ListViewItem item = new ListViewItem(xe.ChildNodes[0].InnerText);
                        item.SubItems.Add(xe.ChildNodes[1].InnerText);
                        listView1.Items.Add(item);
                    }
                    break;
            }
        }

        //清空listview1
        private void clearListView1()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                listView1.Items.Remove(item);
            }
        }

        //清空listview2
        private void clearListView2()
        {
            foreach (ListViewItem item in listView2.Items)
            {
                listView2.Items.Remove(item);
            }
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            clearListView1 ();
            clearListView2 ();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            
            clearListView2 () ;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选择菜品");
                return;
            }
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                int index = -1;
                for (int j = 0; j < listView2.Items.Count; j++)
                {
                    if (listView1.SelectedItems[i].Text == listView2.Items[j].Text)
                    {
                        index = j;
                        break;
                    }
                }
                if (index == -1)
                {
                    ListViewItem item = new ListViewItem(listView1.SelectedItems[i].Text);
                    item.SubItems.Add(listView1.SelectedItems[i].SubItems[1].Text);
                    item.SubItems.Add("1");
                    listView2.Items.Add(item);
                }
                else
                {
                    int count = int.Parse(listView2.Items[index].SubItems[2].Text);
                    count++;
                    listView2.Items[index].SubItems[2].Text = count.ToString();
                }
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            for (int i = listView2.Items.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < listView2.SelectedItems.Count; j++)
                {
                    if (listView2.Items[i].Text == listView2.SelectedItems[j].Text)
                    {
                        int count = int.Parse(listView2.Items[i].SubItems[2].Text);
                        if (count == 1)
                            listView2.Items[i].Remove();
                        else
                        {
                            count--;
                            listView2.Items[i].SubItems[2].Text = count.ToString();
                        }
                    }
                }
            }
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            this.Close();
            f1.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
