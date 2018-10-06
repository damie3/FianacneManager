namespace FinanceManager.ViewModels.Reports
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureViewModel
    {
        public Period Period { get; set; }

        public IEnumerable<BudgetVsExpenditureViewModelItem> ViewModelItems { get; set; }

        public double Total => ViewModelItems.Sum(s => s.BudgetAmount + s.TransactionAmount);
    }
}