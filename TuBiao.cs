using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace 点菜管理系统
{
    public partial class TuBiao : Form
    {
        DataTable dt;
        string type;
        public TuBiao(string type,DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
            this.type = type;
        }

        private void TuBiao_Load(object sender, EventArgs e)
        {
            try
            {
                switch (type)
                {
                    case "菜品销售明细":
                        Line_Load(dt);
                        break;
                    case "个人销售明细":
                        Chart_Load(dt);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                this.Close();
            }
        }

        //折线图
        private void Line_Load(DataTable dt)
        {   
            chart1.Series.Clear();
            string name = dt.Rows[0]["菜名"].ToString();
            chart1.Series.Add(name);
            chart1.Titles.Add(name+"销量趋势");
            List<string> x = new List<string>();
            List<int> y = new List<int>();
            foreach (DataRow dr in dt.Rows)
            {
                x.Add(dr["消费时间"].ToString());
                y.Add(Convert.ToInt32(dr["份数"]));
            }
            chart1.ChartAreas[0].AxisX.Title = "时间";
            chart1.ChartAreas[0].AxisY.Title = "份数";
            chart1.Series[name].IsValueShownAsLabel = true;
            chart1.Series[name].Color=Color.Red;
            chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[name].Points.DataBindXY(x, y);
        }

        //柱状图
        private void Chart_Load(DataTable dt)
        {
            chart1.Series.Clear();

            List<string> names = new List<string>();
            foreach(DataRow dr in dt.Rows)
            {
                if (names.Contains(dr["菜名"].ToString())){
                    continue;
                }
                chart1.Series.Add(dr["菜名"].ToString());
                names.Add(dr["菜名"].ToString());

            }

            foreach(string name in names)
            {
                List<string> x = new List<string> ();
                List<int> y = new List<int>();
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["菜名"].Equals(name))
                    {
                        if (x.Contains(dr["点菜员"].ToString()))
                        {
                            //查找已存在元素下标
                            for (int i = 0; i < x.Count; i++)
                            {
                                if (x[i].Equals(dr["点菜员"].ToString()))
                                {
                                    y[i] += Convert.ToInt32(dr["份数"].ToString());
                                    continue;
                                }
                            }
                            continue;
                        }
                        x.Add(dr["点菜员"].ToString());
                        y.Add(Convert.ToInt32(dr["份数"].ToString()));
                        foreach(string n in x)
                        {
                            Debug.WriteLine(n);
                        }
                       
                    }
                }
                chart1.Series[name].IsValueShownAsLabel = true;
                chart1.Series[name].Points.DataBindXY(x, y);
            }
            chart1.ChartAreas[0].AxisX.Title = "点菜员";
            chart1.ChartAreas[0].AxisY.Title = "份数";
        }
    }
}
