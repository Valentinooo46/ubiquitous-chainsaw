using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnimalHouse.Entities
{
    [Table("Animals")]
    public class AnimalEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = null!;
        [Required, StringLength(200)]
        public string Description { get; set; } = null!;
        [Required]
        public int AGE { get; set; }
        //[ForeignKey("Specie")]
        //public int ShelterId { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, Age: {AGE}";
        }
    }
}
