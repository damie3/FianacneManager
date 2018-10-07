using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Transaction
{
    using Account;
    using Category;
    using Period;

    public class Transaction
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TransactionId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string Description { get; set; }

        [Required]
        public Category Category{ get;set; } = new Category();
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; }

        [Required]
        public Account Account { get; set; } = new Account();

        [Required]
        public Period Period { get; set; } = new Period();

        [NotMapped]
        public string TransactionCategoryName
        {
            get => Category.Name;
            set => Category.Name = value;
        }
        [NotMapped]
        public string AccountName
        {
            get => Account.Name;
            set => Account.Name = value;
        }
    }
}