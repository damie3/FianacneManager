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
            //db.Periods.Join(db.Transactions, period => period, transaction => transaction.Period,
            //        (period, transaction) => new {Period = period, Transaction = transaction})
            //    .Where(r => r.Period == r.Transaction.Period)
            //    .Join()
            //var results = (from p in db.Periods
            //               join t in db.Transactions on p.PeriodId equals t.Period.PeriodId into ptran
            //               from pt in ptran.DefaultIfEmpty()
            //               join  b in db.BudgetItems on p.PeriodId equals b.Period.PeriodId into pbud
            //               from pb in pbud.DefaultIfEmpty()
            //               join c in db.Categories on pt.Category.CategoryId equals c.CategoryId || pb.
            //               select new
            //               {

            //               }
            //join b in db.BudgetItems on p.PeriodId equals b.Period.PeriodId into pb
            //     join cat in db.Categories on cat.CategoryId equals 1 into c

            //)

            //var results = await (from p in db.Periods
            //        from c in db.Categories
            //        from t in db.Transactions
            //        select new {p, c, t})
            //    .Where(r => (r.p.PeriodId == r.t.Period.PeriodId && r.c.CategoryId == r.t.Category.CategoryId ) 
            //    //            //|| (r.b.Category.CategoryId == r.c.CategoryId && r.p.PeriodId == r.b.Period.PeriodId)
            //    //            )
            //    //.GroupBy(r => new {r.p, r.c})
            //    //.Select(r => new BudgetVsExpenditureViewModelItem
            //    //{
            //    //    Period = r.Key.p, Category = r.Key.c, TransactionAmount = r.Sum(t => t.t.Amount),
            //    //    BudgetAmount = r.Sum(b => b.b.Amount)
            //    //})
            //    .ToListAsync();

            var selectMany = await db.Periods
                .SelectMany(p => db.Categories, (p, c) => new {p, c})
                .SelectMany(
                    pc => db.Transactions
                        .Where(t => t.Category.CategoryId == pc.c.CategoryId && t.Period.PeriodId == pc.p.PeriodId)
                        .DefaultIfEmpty(), (pc, t) => new {pc.p, pc.c, t})
                .SelectMany(
                    pct => db.BudgetItems.Where(bi =>
                            bi.Category.CategoryId == pct.c.CategoryId && bi.Period.PeriodId == pct.p.PeriodId)
                        .DefaultIfEmpty(), (pct, bi) => new {pct.p, pct.c, pct.t, bi})
                .Where(pctbi => !(pctbi.t == null && pctbi.bi == null)).ToListAsync();

            var result = selectMany
                .GroupBy(pctbi => new {pctbi.p, pctbi.c})
                .Select(g => new BudgetVsExpenditureViewModelItem()
                {
                    Period = g.Key.p,
                    Category = g.Key.c,
                    TransactionAmount = g.Where(w => w.t != null).Sum(t => t.t.Amount),
                    BudgetAmount = g.Where(w => w.bi != null).Sum(bi => bi.bi.Amount)

                });


            //var selectMany = db.Periods.SelectMany(p => db.Categories, (p, c) => new Tuple<Period, Category>(p, c))
            //    .Join(db.Transactions, pc => new {pc.Item1, pc.Item2}, t => new {t.Period, t.Category},
            //        (pc, t) => pc).Select(t=> t.)




            //var transactionItems = await db.Transactions.GroupBy(t => new {Period = t.Period, Category = t.Category}).Select(g =>
            //    new BudgetVsExpenditureItem
            //        {Period = g.Key.Period, Category = g.Key.Category, Amount = g.Sum(t => t.Amount)}).ToListAsync();
            //var budgetItems = await db.BudgetItems.GroupBy(t => new { Period = t.Period, Category = t.Category, Type = "Transaction" }).Select(g =>
            //    new BudgetVsExpenditureItem
            //        { Period = g.Key.Period, Category = g.Key.Category, Amount = g.Sum(t => t.Amount), Type = "BudgetItem"}).ToListAsync();



            //transactionItems.Join(budgetItems, new {})
            //var result = transactionItems.Union(budgetItems);

            //result.GroupBy(r=> new {r.Period, r.Category}).Select(g=> new BudgetVsExpenditureViewModelItem()
            //{
            //    Period = g.Key.Period,
            //    Category = g.Key.Category,
            //    TransactionAmount = 
            //})
            return View();
        }
    }
}