using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public class TransactionPeriod
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionPeriodId { get; private set; }
        public string Name { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}