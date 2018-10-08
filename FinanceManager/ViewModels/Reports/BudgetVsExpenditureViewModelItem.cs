using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports
{
    using System.ComponentModel.DataAnnotations;
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureViewModelItem
    {
        public Period Period { get; set; }
        public Category Category { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double TransactionAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double BudgetAmount { get; set; }

    }
}