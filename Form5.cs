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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("0", "菜名");
            treeView1.Nodes["0"].Nodes.Add("0-0", "蔬菜类");
            treeView1.Nodes["0"].Nodes.Add("0-1", "荤菜类");
            treeView1.Nodes["0"].Nodes.Add("0-2", "汤类");
        }

        XmlNode root;
        XmlDocument xmlDoc;
        string file ;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
            switch (treeView1.SelectedNode.Name)
            {
                case "0-0":
                    file="菜品-蔬菜类.xml";
                    break;
                case "0-1":
                    file = "菜品-荤菜类.xml";
                    break;
                case "0-2":
                    file = "菜品-汤类.xml";
                    break;
            }
            if (file != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("菜名");
                dt.Columns.Add("价格");
                dt.Columns.Add("是否在售");
                xmlDoc = new XmlDocument();
                xmlDoc.Load(file);
                root = xmlDoc.SelectSingleNode("//Menu");
                XmlNodeList nodelist = xmlDoc.SelectNodes("//Dish");
                foreach (XmlNode node in nodelist)
                {
                    DataRow dr = dt.NewRow();
                    XmlElement xe = (XmlElement)node;
                    dr["菜名"] = xe.ChildNodes[0].InnerText;
                    dr["价格"] = xe.ChildNodes[1].InnerText;
                    dr["是否在售"] = xe.ChildNodes[2].InnerText;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9' )&& e.KeyChar!='\b') e.Handled = true;
        }

        private void dataGridView1_KeyPress1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '是' && e.KeyChar != '否' && e.KeyChar!='\b') e.Handled = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress1);
                e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);

            }
            else if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress1);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            root.RemoveAll();
            xmlDoc.Save(file);
            DataTable dt = (DataTable)dataGridView1.DataSource;
            foreach(DataRow dr in dt.Rows)
            {
                XmlElement xe = xmlDoc.CreateElement("Dish");
                XmlElement xesub1 = xmlDoc.CreateElement("Name");
                xesub1.InnerText = dr["菜名"].ToString();
                XmlElement xesub2 = xmlDoc.CreateElement("Price");
                xesub2.InnerText = dr["价格"].ToString();
                XmlElement xesub3 = xmlDoc.CreateElement("OnOff");
                xesub3.InnerText = dr["是否在售"].ToString();
                xe.AppendChild(xesub1);
                xe.AppendChild(xesub2);
                xe.AppendChild(xesub3);
                root.AppendChild(xe);
            }
            xmlDoc.Save(file);
            MessageBox.Show("保存成功");
        }
    }
}
