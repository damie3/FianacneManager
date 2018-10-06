using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports
{
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureItem
    {
        public Period Period { get; set; }
        public Category Category { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }

    }
}