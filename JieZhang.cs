using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public int init(ListView listview)
        {
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
    }
}
