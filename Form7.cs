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
    public partial class Form7 : Form
    {
        Form4 f4;
        public Form7(Form4 f4)
        {
            InitializeComponent();
            this.f4 = f4;
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            f4.Show();
        }
    }
}
