﻿namespace FinanceManager.Models.Period
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Period
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeriodId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public DateTime PeriodStart { get; set; }

        [Required]
        public DateTime PeriodEnd { get; set; }
    }
}