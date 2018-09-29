using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Transaction
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; private set; }
        public TransactionPeriod Period { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public TransactionCategory TransactionCategory{ get;set; }
        public double Amount { get; set; }
        public TransactionSource TransactionSource { get; set; }
    }
}