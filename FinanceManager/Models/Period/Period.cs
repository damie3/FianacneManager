namespace FinanceManager.Models.Period
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Period
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeriodId { get; set; }
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}