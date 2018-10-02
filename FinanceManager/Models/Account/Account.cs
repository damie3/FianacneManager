using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FinanceManager.Models.Account
{
    public class Account
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId {get; set;}

        [Column(TypeName = "NVARCHAR")]
        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string Description { get; set; }
    }
}