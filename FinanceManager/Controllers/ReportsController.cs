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

    public class ReportsController : Controller
    {
        // GET: Reports
        private FinanceManagerContext db = new FinanceManagerContext();
        public async Task<ActionResult> AccountBalanceReport()
        {
            var result = db.Transactions.GroupBy(t => t.Account.Name).Select(g => new AccountBalanceReportItem()
            { AccountName = g.Key, Amount = g.Sum(t => t.Amount) });
            return View(await result.ToListAsync());
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
                        .Where(t => t.Category.CategoryId == pc.c.CategoryId && t.Period.PeriodId == pc.p.PeriodId)
                        .DefaultIfEmpty(), (pc, t) => new { pc.p, pc.c, t })
                .SelectMany(
                    pct => db.BudgetItems.Where(bi =>
                            bi.Category.CategoryId == pct.c.CategoryId && bi.Period.PeriodId == pct.p.PeriodId)
                        .DefaultIfEmpty(), (pct, bi) => new { pct.p, pct.c, pct.t, bi })
                .Where(pctbi => !(pctbi.t == null && pctbi.bi == null)).ToListAsync();

            var result = selectMany
                .GroupBy(pctbi => new { pctbi.p, pctbi.c })
                .Select(g => new BudgetVsExpenditureViewModelItem()
                {
                    Period = g.Key.p,
                    Category = g.Key.c,
                    TransactionAmount = g.Where(w => w.t != null).Sum(t => t.t.Amount),
                    BudgetAmount = g.AsEnumerable().Any(a => a.bi != null) ? g.AsEnumerable().First(f => f.bi != null).bi.Amount : 0

                }).GroupBy(g => g.Period).Select(s => new BudgetVsExpenditureViewModel()
                {
                    Period = s.Key,
                    ViewModelItems = s.AsEnumerable()
                }).OrderBy(o => o.Period.Name);

            return View("BudgetVsExpenditureView", result);
        }
    }
}