using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports
{
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureViewModelItem
    {
        public Period Period { get; set; }
        public Category Category { get; set; }
        public double TransactionAmount { get; set; }
        public double BudgetAmount { get; set; }

    }
}