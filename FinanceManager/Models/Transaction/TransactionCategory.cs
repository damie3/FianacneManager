using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Models
{
    public class TransactionCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionCategoryId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}