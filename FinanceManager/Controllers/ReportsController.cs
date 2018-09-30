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
    using Models.Context;
    using Models.Reports;

    public class ReportsController : Controller
    {
        // GET: Reports
        private FinanceManagerContext db = new FinanceManagerContext();
        public async Task<ActionResult> AccountBalanceReport()
        {
            var result = db.Transactions.GroupBy(t => t.Account.Name).Select(g => new AccountBalanceReportItem()
                {AccountName = g.Key, Amount = g.Sum(t => t.Amount)});
            return  View(await result.ToListAsync());
        }

        public async Task<ActionResult> ExpenditurePerCategoryPerPeriodReport()
        {
            var result = db.Transactions.GroupBy(t => new {PeriodName = t.Period.Name, TransactionCategoryName = t.TransactionCategory.Name}).Select(g => new ExpenditurePerCategoryPerPeriodReportItem()
                { Category = g.Key.TransactionCategoryName, Period = g.Key.PeriodName, Amount = g.Sum(t => t.Amount) });
            return View(await result.ToListAsync());
        }
    }
}