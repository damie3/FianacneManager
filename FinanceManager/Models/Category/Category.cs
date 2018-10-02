namespace FinanceManager.Models.Category
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Index(IsUnique = true)]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(20)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string Description { get; set; }
    }
}