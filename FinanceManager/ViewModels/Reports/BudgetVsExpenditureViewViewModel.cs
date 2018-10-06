using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports
{
    using Models.Period;

    public class BudgetVsExpenditureViewViewModel
    {
        public IEnumerable<Period> Periods {get;set;}
    }
}