using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Transactions
{
    using System.ComponentModel.DataAnnotations;
    using Models.Transaction;

    public class TransactionGroupViewModel
    {
        public string GroupName { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Total => Transactions.Sum(t => t.Amount);

    }
}