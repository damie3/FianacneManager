using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports.SavingsReport
{
    using Models.Period;

    public class SavingsReportTransactionViewModel
    {
        public Period Period { get; set; }
        public string AccountName { get; set; }
        public double Amount { get; set; }
    }
}