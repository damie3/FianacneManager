using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Reports
{
    using System.ComponentModel.DataAnnotations;

    public class ExpenditurePerCategoryPerPeriodReportItem
    {
        public string Category { get; set; }
        public string Period { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; } = 0;


    }
}