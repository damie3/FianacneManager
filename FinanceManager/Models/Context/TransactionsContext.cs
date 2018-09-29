using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Context
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext(): base("name=FinanceManagerContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<FinanceManager.Models.Transaction.TransactionCategory> TransactionCategories { get; set; }
        public System.Data.Entity.DbSet<FinanceManager.Models.Transaction.TransactionPeriod> TransactionPeriodss { get; set; }
        public System.Data.Entity.DbSet<FinanceManager.Models.Transaction.TransactionSource> TransactionSources { get; set; }
        public System.Data.Entity.DbSet<FinanceManager.Models.Transaction.Transaction> Transactions { get; set; }
    }
}
