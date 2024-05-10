using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 点菜管理系统
{
    public class ExcelDataOperation
    {
        public static void ExportToExcel(DataGridView dataGridView,string sheetName)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = @"Excel|*.xls|WPS_Excel|*.xlsx|所有文件|*.*";//允许导入的文件类型
            fileDialog.InitialDirectory = @"D:";// 默认地址
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            dataGridView.AllowUserToAddRows = false;
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow rowHead = (IRow)sheet.CreateRow(0);

            for(int i = 0;i<dataGridView.Columns.Count;i++)
            {
                rowHead.CreateCell(i, CellType.String).SetCellValue(dataGridView.Columns[i].HeaderText.ToString());
            }
            for(int i = 0; i < dataGridView.Rows.Count;i++)
            {
                IRow row = sheet.CreateRow(i+1);
                for(int j = 0;j<dataGridView.Columns.Count;j++)
                {
                    row.CreateCell(j, CellType.String).SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());
                }
            }
            using(FileStream stream = File.OpenWrite(fileDialog.FileName))
            {
                workbook.Write(stream);
                stream.Close();
            }
            MessageBox.Show("导出书籍成功!","提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            GC.Collect();
        }
    }
}
