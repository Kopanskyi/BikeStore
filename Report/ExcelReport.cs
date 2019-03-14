using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using DataManager;
using System.Data;
using System.Windows.Forms;

namespace ReportManager
{
    public class ExcelReport
    {
        private Excel.Application ExcelApp;
        int activeColumn;
        int activeRow;
        string path = System.IO.Directory.GetCurrentDirectory();

        public void CreateExcelReport()
        {
            ExcelApp = new Excel.Application();

            ExcelApp.Workbooks.Open(path + "\\rep\\template.xlsx");

            activeRow = 1;

            foreach (DataRow row in Report.Instance.DataTableForReport.Rows)
            {
                activeColumn = 1;

                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (activeRow == 1)
                    {
                        CreateColumn(activeRow, activeColumn, row.Table.Columns[i].ColumnName);
                    }

                    CreateColumn(activeRow + 1, activeColumn, row.ItemArray[i].ToString());
                    activeColumn++;
                }

                activeRow++;
            }            

            CloseReport();
        }

        private void CreateColumn(int row, int column, string value)
        {
            Excel.Range cell = ExcelApp.Cells[row, column];
            cell.Value = value;
            cell.BorderAround();
        }

        private void CloseReport()
        {
            ExcelApp.Workbooks[1].SaveAs(path + "\\Excel_Report.xls");
            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();
            MessageBox.Show("Report created successfully");
        }
    }
}
