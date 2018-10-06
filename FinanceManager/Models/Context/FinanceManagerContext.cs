using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Context
{
    using Budget;
    using Category;
    using Period;

    public class FinanceManagerContext : DbContext
    {
        public FinanceManagerContext(): base("name=FinanceManagerContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Category> Categories { get; set; }
        public System.Data.Entity.DbSet<Period>Periods { get; set; }
        public System.Data.Entity.DbSet<FinanceManager.Models.Account.Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<FinanceManager.Models.Transaction.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<FinanceManager.Models.Budget.BudgetItem> BudgetItems { get; set; }
    }
}
