using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FinanceManager.Models.Context;
using FinanceManager.Models.Transaction;

namespace FinanceManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.WebPages;
    using Microsoft.Ajax.Utilities;
    using Models.Account;
    using Models.Category;
    using Models.Period;
    using ViewModels;
    using ViewModels.Transactions;

    public class TransactionsController : Controller
    {
        private FinanceManagerContext db = new FinanceManagerContext();

        // GET: Transactions
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Transactions.Include(x => x.Account).Include(x => x.Category).OrderByDescending(x => x.TransactionDate).ToListAsync());
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

        public async Task<ActionResult> Index(string groupType)
        {
            var dbTransactions = await FetchTransactionsAndDependents();
            dbTransactions = ApplySorting(dbTransactions);
            var groupedTransactions = ApplyGrouping(groupType, dbTransactions);

            var results = groupedTransactions.Select(t => new TransactionGroupViewModel()
            {
                GroupName = t.Key,
                Transactions = t.AsEnumerable()
            });

            return View("TransactionsView", new TransactionsViewViewModel()
            {
                GroupType = groupType.IsEmpty() ? "Type" : groupType,
                TransactionGroupViewModels = results,
                SelectedGrouping = groupType.IsEmpty() ? "Transaction" : groupType

            });

        }

        private async Task<IEnumerable<Transaction>> FetchTransactionsAndDependents()
        {
            return await db.Transactions.Include(t => t.Period).Include(t => t.Account).Include(t => t.Category).ToListAsync();
        }

        private IEnumerable<Transaction> ApplySorting(IEnumerable<Transaction> dbTransactions)
        {
            return dbTransactions.OrderByDescending(t => t.TransactionDate);
        }

        private IEnumerable<IGrouping<string, Transaction>> ApplyGrouping(string groupType, IEnumerable<Transaction> dbTransactions)
        {
            IEnumerable<IGrouping<string, Transaction>> grouped = null;
            switch (groupType)
            {
                case "Category":
                    grouped = dbTransactions.GroupBy(t => t.Category.Name).OrderBy(g => g.Key);
                    break;
                case "Period":
                    grouped = dbTransactions.GroupBy(t => t.Period.Name).OrderByDescending(g => g.Key);
                    break;
                case "Account":
                    grouped = dbTransactions.GroupBy(t => t.Account.Name).OrderBy(g => g.Key);
                    break;
                default:
                    grouped = dbTransactions.GroupBy(t => "Transactions");
                    break;
            }

            return grouped;
        }

        // GET: Transactions/Create
        public async Task<ActionResult> Create()
        {
            var categories = await db.Categories.ToListAsync();
            var accounts = await db.Accounts.ToListAsync();
            return View(new CreateTransactionViewModel() { Accounts = accounts, Categories = categories });
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TransactionDate,Description,Amount,SelectedAccount,SelectedCategory")] CreateTransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                var account = await db.Accounts.Where(a => a.Name == transaction.SelectedAccount).FirstAsync();
                var category = await db.Categories.Where(a => a.Name == transaction.SelectedCategory)
                    .FirstAsync();

                var period = await db.Periods.Where(p =>
                        p.PeriodStart <= transaction.TransactionDate && p.PeriodEnd >= transaction.TransactionDate)
                    .FirstAsync();
                var model = new Transaction() { Account = account, TransactionDate = transaction.TransactionDate, Amount = transaction.Amount, Category = category, Period = period, Description = transaction.Description };
                db.Transactions.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return await Index(string.Empty);
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
