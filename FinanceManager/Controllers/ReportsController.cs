using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    using System.Data.Entity;
    using System.Web.UI.WebControls;
    using Models.Category;
    using Models.Context;
    using Models.Period;
    using Models.Reports;
    using ViewModels.Reports;
    using ViewModels.Reports.SavingsReport;

    public class ReportsController : Controller
    {
        // GET: Reports
        private FinanceManagerContext db = new FinanceManagerContext();
        public async Task<ActionResult> SavingsReport()
        {
            var transactionItems = await db.Transactions.Where(t => t.Account.Name == "Savings1" || t.Account.Name == "Savings2").
                GroupBy(t=> new { t.Period, t.Account})
                .Select(s => new SavingsReportTransactionViewModel { Period = s.Key.Period, AccountName = s.Key.Account.Name, Amount = s.Sum(t=> t.Amount)})
                .ToListAsync();

            var actualSavingsToDate = transactionItems.Sum(t => t.Amount);

            var budgetItems = await db.BudgetItems
                .Where(c => c.Category.Name == "Savings1" || c.Category.Name == "Savings2")
                .Select(b => new SavingsReportBudgetItemViewModel
                    {Period = b.Period, Category = b.Category.Name, Amount = b.Amount})
                .ToListAsync();
            budgetItems.ForEach( b=> b.AggregatedAmount = b.Amount + budgetItems.Where(bi => bi.Period.PeriodStart < b.Period.PeriodStart && bi.Category  == b.Category).Sum(bi=> bi.Amount));

            var expectedSavingsToDate = budgetItems.Where(b =>
                b.Period.PeriodStart <= DateTime.Now && b.Period.PeriodEnd >= DateTime.Now).Sum(b=> b.AggregatedAmount);

            var selectMany = budgetItems.SelectMany(
                b => transactionItems.Where(t => b.Period == t.Period && b.Category == t.AccountName)
                    .DefaultIfEmpty(),
                (b, t) => new {b.Period, b, t});
            var viewModelItems = selectMany
                .GroupBy(bt => bt.Period)
                .Select(g =>
                    new SavingsReportViewModelItem
                    {
                        Period = g.Key,
                        Savings2BudgetedAmount = g.Where(w => w.b.Category == "Savings2").Sum(s => s.b.Amount),
                        Savings1BudgetedAmount = g.Where(w => w.b.Category == "Savings1").Sum(s => s.b.Amount),
                        AggregatedAmount = g.Where(w => w.b.Category == "Savings1" || w.b.Category == "Savings2")
                            .Sum(s => s.b.AggregatedAmount),
                        Savings1Amount = g.Where(w => w.t != null && w.t.AccountName == "Savings1").Sum(s => s.t.Amount),
                        Savings2Amount = g.Where(w => w.t != null && w.t.AccountName == "Savings2").Sum(s => s.t.Amount)
                    });

            return View(new SavingsReportViewModel()
            {
                ViewModelItems = viewModelItems,
                ActualSavingsToDate = actualSavingsToDate,
                ExpectedSavingsToDate = expectedSavingsToDate
            });

        }

        public async Task<ActionResult> ExpenditurePerCategoryPerPeriodReport()
        {
            var result = await db.Transactions.GroupBy(t => new { PeriodName = t.Period.Name, TransactionCategoryName = t.Category.Name }).Select(g => new ExpenditurePerCategoryPerPeriodReportItem()
            { Category = g.Key.TransactionCategoryName, Period = g.Key.PeriodName, Amount = g.Sum(t => t.Amount) }).ToListAsync();

            return View(new ExpenditurePerCategoryPerPeriodReport() { Items = result });
        }

        public async Task<ActionResult> BudgetVsExpenditureReport()
        {
            var selectMany = await db.Periods
                .SelectMany(p => db.Categories.Where(c => db.ReportCategoryExclusions.All(a => a.Category != c)), (p, c) => new { p, c })
                .SelectMany(
                    pc => db.Transactions
                        .Where(t => t.Category.CategoryId == pc.c.CategoryId && t.Period.PeriodId == pc.p.PeriodId).GroupBy(g=> new {g.Period, g.Category}).Select(s=> new {s.Key.Period, s.Key.Category, Amount = s.Sum(r=> r.Amount)})
                        .DefaultIfEmpty(), (pc, t) => new { pc.p, pc.c, t })
                .SelectMany(
                    pct => db.BudgetItems.Where(bi =>
                            bi.Category.CategoryId == pct.c.CategoryId && bi.Period.PeriodId == pct.p.PeriodId)
                        .DefaultIfEmpty(), 
                    (pct, bi) => new BudgetVsExpenditureViewModelItem {Period =  pct.p, Category = pct.c, TransactionAmount =  pct.t != null ? pct.t.Amount :0 , BudgetAmount =  bi != null? bi.Amount :0}) 
                .Where(pctbi => !(pctbi.TransactionAmount == 0  && pctbi.BudgetAmount == 0))
                .ToListAsync();

            var result = selectMany.GroupBy(b => b.Period).Select(s => new BudgetVsExpenditureViewModel()
                {
                    Period = s.Key,
                    ViewModelItems = s.AsEnumerable()
                }).OrderBy(o => o.Period.Name);

            return View("BudgetVsExpenditureView", result);
        }
    }
}