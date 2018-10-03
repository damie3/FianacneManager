namespace FinanceManager.ViewModels.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Models.Account;
    using Models.Category;
    using Models.Period;
    using Models.Transaction;

    public class TransactionsViewViewModel
    {
        public string GroupType { get; set; }
        public IEnumerable<TransactionGroupViewModel> TransactionGroupViewModels { get; set; }

    }
}