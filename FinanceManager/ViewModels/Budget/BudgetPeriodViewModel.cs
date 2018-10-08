using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Budget
{
    using System.ComponentModel.DataAnnotations;
    using Models.Budget;
    using Models.Period;

    public class BudgetPeriodViewModel
    {
        public Period Period { get; set; }
        public IEnumerable<BudgetItem> Items { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Total => Items.Sum(i => i.Amount);
    }
}