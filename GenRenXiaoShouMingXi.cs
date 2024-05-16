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
    public partial class GenRenXiaoShouMingXi : DataGridView
    {
        public GenRenXiaoShouMingXi()
        {
            InitializeComponent();
        }

        public GenRenXiaoShouMingXi(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        string filePath;
        public void search(string filePath,DateTime dTP1,DateTime dTP2,string cbB)
        {
            this.filePath = filePath;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
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
                    if (Convert.ToDateTime(data).Date < dTP1 || Convert.ToDateTime(data).Date > dTP2)
                    {
                        continue;
                    }
                    if (cbB != "所有点菜员" && cbB != user)
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
            this.DataSource = table1;
        }
    }
}
