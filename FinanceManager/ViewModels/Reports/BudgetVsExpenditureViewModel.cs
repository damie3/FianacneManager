namespace FinanceManager.ViewModels.Reports
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Models.Category;
    using Models.Period;

    public class BudgetVsExpenditureViewModel
    {
        public Period Period { get; set; }

        public IEnumerable<BudgetVsExpenditureViewModelItem> ViewModelItems { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Total => ViewModelItems.Sum(s => s.BudgetAmount + s.TransactionAmount);
    }
}