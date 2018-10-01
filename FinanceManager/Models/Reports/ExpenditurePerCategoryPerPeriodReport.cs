using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Reports
{
    public class ExpenditurePerCategoryPerPeriodReport
    {
        public IEnumerable<string> Periods => Items.OrderBy(i=> i.Period).GroupBy(i => i.Period).Select(i => i.Key);

        public IEnumerable<string> Categories => Items.OrderBy(i=>i.Category).GroupBy(i => i.Category).Select(i => i.Key);
        public IEnumerable<ExpenditurePerCategoryPerPeriodReportItem> Items { get; set; }

        public static ExpenditurePerCategoryPerPeriodReportItem EmptyItem = new ExpenditurePerCategoryPerPeriodReportItem();
        public ExpenditurePerCategoryPerPeriodReportItem GetAmount(string period, string category)
        {
            var query = Items.Where(i=> i.Period == period && i.Category == category).ToList();
            return query.Any() ? query.First() : EmptyItem;
        }
    }
}