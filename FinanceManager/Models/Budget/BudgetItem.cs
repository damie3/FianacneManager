using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models.Budget
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Category;
    using Period;

    public class BudgetItem
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BudgetItemId { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Period Period { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}