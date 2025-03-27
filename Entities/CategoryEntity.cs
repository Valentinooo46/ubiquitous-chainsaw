using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLX.Entities
{
    [Table("Categories")]
    public class CategoryEntity : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Description { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
        public CategoryEntity Parent { get; set; } = null!;
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
    }
    [Table("NewCategory")] public class Category
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(50)] public string Name { get; set; } = null!;
        [Required, StringLength(50)] public string Slug { get; set; } = null!;
        public ICollection<Subcategory>? Subcategories { get; set; }
    }
    [Table("SubCategory")] public class Subcategory
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(50)] public string Name { get; set; } = null!;
        [Required, StringLength(50)] public string Slug { get; set; } = null!;
    }

    
}
