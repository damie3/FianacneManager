using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports.SavingsReport
{
    using Models.Category;
    using Models.Period;

    public class SavingsReportBudgetItemViewModel
    {
        public Period Period { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public double AggregatedAmount { get; set; }
    }
}