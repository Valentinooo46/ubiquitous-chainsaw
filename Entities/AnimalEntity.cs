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
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string? Phone { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        [ForeignKey("AnimalId")]
        public AnimalEntity Animal { get; set; } = null!;
    }

    [Table("MedicalRecords")]
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public DateTime VisitDate { get; set; }
        [Required]
        public string Diagnosis { get; set; } = null!;
        

        [ForeignKey("AnimalId")]
        public AnimalEntity Animal { get; set; } = null!;
    }
}

