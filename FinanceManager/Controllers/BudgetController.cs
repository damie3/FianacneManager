﻿using System.Data.Entity;
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
    using Models.Category;
    using Models.Period;
    using ViewModels.Budget;
    using ViewModels.Transactions;

    public class BudgetController : Controller
    {
        private FinanceManagerContext db = new FinanceManagerContext();

        
        public async Task<ActionResult> Index()
        {
            
            var budgetItems = await db.BudgetItems.Include(t=>t.Period).Include(t=>t.Category).ToListAsync();
           
            return View("BudgetView",  new BudgetViewViewModel()
            {
                Items = budgetItems
            });

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
