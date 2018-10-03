using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Budget
{
    using Models.Budget;
    using Models.Period;

    public class BudgetViewViewModel
    {
        public IEnumerable<BudgetPeriodViewModel> Items { get; set; }
    }
}