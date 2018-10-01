using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Reports
{
    using System.ComponentModel.DataAnnotations;

    public class AccountBalanceReportItem
    {
        public string AccountName { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; }
    }
}