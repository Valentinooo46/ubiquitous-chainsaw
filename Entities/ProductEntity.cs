using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLX.Entities
{
    [Table("Products")]
    public class ProductEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        CategoryEntity Category { get; set; } = null!;
        [Required,ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
