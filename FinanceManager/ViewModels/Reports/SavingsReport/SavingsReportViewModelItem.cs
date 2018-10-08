using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports.SavingsReport
{
    using System.ComponentModel.DataAnnotations;
    using Models.Period;

    public class SavingsReportViewModelItem
    {
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double AggregatedAmount { get; set; }
        public Period Period { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Savings2BudgetedAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Savings1BudgetedAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Savings1Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Savings2Amount { get; set; }
    }
}