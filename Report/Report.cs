using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    public class Report
    {
        public DataTable DataTableForReport;
        public ExcelReport ExcelReport;
        public PDFReport PDFReport;
        public WordReport WordReport;

        private static Report instance;
        public static Report Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Report();
                }

                return instance;
            }
        }

        private Report()
        {
            ExcelReport = new ExcelReport();
            PDFReport = new PDFReport();
            WordReport = new WordReport();
        }
    }
}
