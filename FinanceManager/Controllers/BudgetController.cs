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
    using Models.Budget;
    using Models.Category;
    using Models.Period;
    using ViewModels.Budget;
    using ViewModels.Transactions;

    public class BudgetController : Controller
    {
        private FinanceManagerContext db = new FinanceManagerContext();


        public async Task<ActionResult> Index()
        {

            var budgetItems = await db.BudgetItems.Include(t => t.Period).Include(t => t.Category).OrderBy(bi=> bi.Period.Name).ToListAsync();
            var groupBy = budgetItems.GroupBy(i => i.Period);
            var result = groupBy.Select(s => new BudgetPeriodViewModel()
            {
                Period = s.Key,
                Items = s.AsEnumerable()
            });
            return View("BudgetView", new BudgetViewViewModel()
            {
                Items = result
            });

        }
        public async Task<ActionResult> Create()
        {
            var categories = await db.Categories.ToListAsync();
            var periods = await db.Periods.ToListAsync();

            return View(new CreateBudgetItemViewModel()
            {
                Periods = periods,
                Categories = categories
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Description,Amount,SelectedPeriod,SelectedCategory")] CreateBudgetItemViewModel budgetItem)
        {
            if (ModelState.IsValid)
            {
                var category = await db.Categories.Where(a => a.Name == budgetItem.SelectedCategory)
                    .FirstAsync();

                var period = await db.Periods.Where(p => p.Name == budgetItem.SelectedPeriod)
                    .FirstAsync();
                var model = new BudgetItem() { Amount = budgetItem.Amount, Category = category, Period = period, Description = budgetItem.Description };
                db.BudgetItems.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BudgetItem budgetItem = await db.BudgetItems.FindAsync(id);
            db.BudgetItems.Remove(budgetItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetItem budgetItem = await db.BudgetItems.FindAsync(id);
            if (budgetItem == null)
            {
                return HttpNotFound();
            }
            return View(budgetItem);
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
