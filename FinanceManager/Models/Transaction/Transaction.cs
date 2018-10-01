using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Transaction
{
    using Account;
    using Period;

    public class Transaction
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime TransactionDate { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public TransactionCategory TransactionCategory{ get;set; } = new TransactionCategory();
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; }
        public Account Account { get; set; } = new Account();
        public Period Period { get; set; } = new Period();

        [NotMapped]
        public string TransactionCategoryName
        {
            get => TransactionCategory.Name;
            set => TransactionCategory.Name = value;
        }
        [NotMapped]
        public string AccountName
        {
            get => Account.Name;
            set => Account.Name = value;
        }
    }
}