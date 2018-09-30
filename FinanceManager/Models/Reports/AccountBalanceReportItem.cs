using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Reports
{
    public class AccountBalanceReportItem
    {
        public string AccountName { get; set; }
        public double Amount { get; set; }
    }
}