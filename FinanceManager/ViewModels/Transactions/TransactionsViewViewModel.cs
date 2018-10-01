namespace FinanceManager.ViewModels.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Models.Account;
    using Models.Period;
    using Models.Transaction;

    public class TransactionsViewViewModel
    {
        public Period Period { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime PeriodStart => Period?.PeriodStart ?? Transactions.OrderBy(t=>t.Period.PeriodStart).Select(t => t.Period.PeriodStart).First();
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime PeriodEnd => Period?.PeriodEnd ?? Transactions.OrderByDescending(t => t.Period.PeriodEnd).Select(t => t.Period.PeriodEnd).First();
        public Account Account { get; set; }
        public string AccountName => Account?.Name ?? "All";
        public TransactionCategory TransactionCategory { get; set; }
        public string CategoryName => TransactionCategory?.Name ?? "All";
        public IEnumerable<Transaction> Transactions { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Total => Transactions.Sum(t => t.Amount);

        public object GetParameters(string account = null, string period = null, string category = null)
        {
            return new
            {
                account = account ?? AccountName,
                category = category ?? CategoryName
            };
        }
    }
}