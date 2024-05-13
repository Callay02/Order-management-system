using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;

namespace 点菜管理系统
{
    public partial class JieZhang : System.Windows.Forms.ListView
    {
        public JieZhang()
        {
            InitializeComponent();
        }

        public JieZhang(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ListView listview;
        string name;
        string billsXmlPath;
        public int init(ListView listview,string name,string billsXmlPath)
        {
            this.listview = listview;
            this.name = name;
            this.billsXmlPath = billsXmlPath;
            this.Columns.Add("菜名", 140);
            this.Columns.Add("价格", 100);
            this.View = View.Details;

            for (int i = 0; i < listview.Items.Count; i++)
            {
                this.Items.Add(listview.Items[i].Text);
                int count = int.Parse(listview.Items[i].SubItems[1].Text);
                int price = int.Parse(listview.Items[i].SubItems[2].Text);
                this.Items[i].SubItems.Add((count * price).ToString());
            }
            //计算总价
            int total = 0;
            for (int i = 0; i < this.Items.Count; i++)
            {
                total += int.Parse(this.Items[i].SubItems[1].Text);
            }
            return total;
        }

        public bool save()
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
            catch
            {
                return false;
            }
        }
    }
}
