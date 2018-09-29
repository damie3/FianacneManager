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
        public int TransactionId { get; set; }
        public TransactionPeriod Period { get; set; } = new TransactionPeriod();
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public TransactionCategory TransactionCategory{ get;set; } = new TransactionCategory();
        public double Amount { get; set; }
        public TransactionSource TransactionSource { get; set; } = new TransactionSource();

        [NotMapped]
        public string TransactionPeriodName
        {
            get => Period.Name;
            set => Period.Name = value;
        }
        [NotMapped]
        public string TransactionCategoryName
        {
            get => TransactionCategory.Name;
            set => TransactionCategory.Name = value;
        }
        [NotMapped]
        public string TransactionSourceName
        {
            get => TransactionSource.Name;
            set => TransactionSource.Name = value;
        }
    }
}