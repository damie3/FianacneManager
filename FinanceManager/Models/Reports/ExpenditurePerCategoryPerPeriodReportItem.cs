using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Reports
{
    public class ExpenditurePerCategoryPerPeriodReportItem
    {
        public string Category { get; set; }
        public string Period { get; set; }
        public double Amount { get; set; }
    }
}