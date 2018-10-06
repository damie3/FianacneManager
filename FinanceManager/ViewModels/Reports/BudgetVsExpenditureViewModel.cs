namespace FinanceManager.ViewModels.Reports
{
    using System.Collections;
    using System.Collections.Generic;
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureViewModel
    {
        public Period Period { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<BudgetVsExpenditureViewModelItem> ViewModelItems { get; set; }
    }
}