using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 点菜管理系统
{
    public partial class CaiPinGuanLi : DataGridView
    {
        public CaiPinGuanLi()
        {
            InitializeComponent();
        }

        public CaiPinGuanLi(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        XmlNode root;
        XmlDocument xmlDoc;
        string filePath;
        public void show(string filePath)
        {
            this.filePath = filePath;
            DataTable dt = new DataTable();
            dt.Columns.Add("菜名");
            dt.Columns.Add("价格");
            dt.Columns.Add("是否在售");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
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
            this.DataSource = dt;
        }

        public void save()
        {
            root.RemoveAll();
            xmlDoc.Save(filePath);
            DataTable dt = (DataTable)this.DataSource;
            foreach (DataRow dr in dt.Rows)
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
            xmlDoc.Save(filePath);
            MessageBox.Show("保存成功");
        }

        private void CaiPinGuanLi_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.CurrentCell.ColumnIndex == 1)
            {
                
                e.Control.KeyPress -= new KeyPressEventHandler(CaiPinGuanLi_KeyPress1);
                e.Control.KeyPress += new KeyPressEventHandler(CaiPinGuanLi_KeyPress);

            }
            else if (this.CurrentCell.ColumnIndex == 2)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(CaiPinGuanLi_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(CaiPinGuanLi_KeyPress1);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(CaiPinGuanLi_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(CaiPinGuanLi_KeyPress1);
            }
        }

        private void CaiPinGuanLi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar <'0' || e.KeyChar>'9') && e.KeyChar !='\b') e.Handled = true;
        }

        private void CaiPinGuanLi_KeyPress1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '是' && e.KeyChar != '否' && e.KeyChar != '\b') e.Handled = true;
        }
    }
}
