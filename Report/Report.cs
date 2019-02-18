using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    public class Report
    {
        public ExcelReport ExcelReport;

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
        }
    }
}
