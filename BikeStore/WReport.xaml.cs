using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ReportManager;


namespace BikeStore
{
    /// <summary>
    /// Interaction logic for WReport.xaml
    /// </summary>

    enum ReportType
    {
        Excel,
        PDF,
        Word
    }

    public partial class WReport : Window
    {
        ReportType report;

        public WReport()
        {
            InitializeComponent();
        }

        private void Excel_Checked(object sender, RoutedEventArgs e)
        {
            report = ReportType.Excel;
        }

        private void PDF_Checked(object sender, RoutedEventArgs e)
        {
            report = ReportType.PDF;
        }

        private void Word_Checked(object sender, RoutedEventArgs e)
        {
            report = ReportType.Word;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            switch (report)
            {
                case ReportType.Excel:
                    {
                        Report.Instance.ExcelReport.CreateExcelReport();
                    }
                    break;
                case ReportType.PDF:
                    {

                    }
                    break;
                case ReportType.Word:
                    {
                        Report.Instance.WordReport.CreateWordReport();
                    }
                    break;
                default:
                    break;
            }

            Close();
        }


    }
}
