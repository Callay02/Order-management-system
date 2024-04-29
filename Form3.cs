using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 点菜管理系统
{
    public partial class Form3 : Form
    {
        Form2 f2;
        ListView listview;
        public Form3(Form2 f2, ListView listView)
        {
            InitializeComponent();
            this.f2 = f2;
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
            listView1.Items.Add(" ");
            listView1.Items.Add(" ");
            listView1.Items.Add(" ");
            ListViewItem item = new ListViewItem("总价格为");
            item.SubItems.Add(total.ToString());
            listView1.Items.Add(item);
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

        }
    }
}
