using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FinanceManager.Models.Context;
using FinanceManager.Models.Transaction;

namespace FinanceManager.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.Ajax.Utilities;
    using Models.Account;
    using Models.Period;
    using ViewModels.Transactions;

    public class TransactionsController : Controller
    {
        private FinanceManagerContext db = new FinanceManagerContext();

        // GET: Transactions
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Transactions.Include(x => x.Account).Include(x => x.TransactionCategory).OrderByDescending(x => x.TransactionDate).ToListAsync());
        //}

        // GET: Transactions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        public async Task<ActionResult> Index(string account, string category, string period)
        {
            Account accountModel = null;
            TransactionCategory categoryModel = null;
            Period periodModel = null;
            var dbTransactions = db.Transactions.Include(t=>t.Period).Include(t=>t.Account).Include(t=>t.TransactionCategory);
            if (!account.IsNullOrWhiteSpace() && account != "All")
            {
                accountModel = await db.Accounts.Where(a => a.Name == account).FirstAsync();
                dbTransactions = dbTransactions.Where(t => t.Account.AccountId == accountModel.AccountId);
            }
            if (!category.IsNullOrWhiteSpace() && category != "All")
            {
                categoryModel = await db.TransactionCategories.Where(a => a.Name == category).FirstAsync();
                dbTransactions = dbTransactions.Where(t => t.TransactionCategory.TransactionCategoryId== categoryModel.TransactionCategoryId);
            }
            if (!period.IsNullOrWhiteSpace() && period != "All")
            {
                periodModel = await db.Periods.Where(a => a.Name == period).FirstAsync();
                dbTransactions = dbTransactions.Where(t => t.Period.PeriodId == periodModel.PeriodId);
            }

            var results = await dbTransactions.OrderByDescending(t => t.TransactionDate).ToListAsync();
            return View("TransactionsView",  new TransactionsViewViewModel()
            {
                Account = accountModel, Period = periodModel, TransactionCategory = categoryModel,
                Transactions = results
            });

        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TransactionDate,Description,Amount,AccountName,TransactionCategoryName")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TransactionId,TransactionDate,Description,Amount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
