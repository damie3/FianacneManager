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
        public IEnumerable<BudgetItem> Items { get; set; }
        public IEnumerable<Period> Periods => Items.GroupBy(i => i.Period).Select(i => i.Key);

        public IEnumerable<BudgetItem> GetBudgetItemsByPeriod(Period period)
        {
            return Items.Where(i => i.Period.PeriodId == period.PeriodId);
        }
    }
}