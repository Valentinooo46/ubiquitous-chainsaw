using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AnimalHouse.Entities;

namespace AnimalHouse.Implements
{
    public class MyAppContext(DbContextOptions<MyAppContext> options) : DbContext(options)
    {
        public DbSet<AnimalEntity> AnimalEntities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<CustomerAdoptInfo> CustomerAdoptInfos { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Employee> employees { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("***");
        //}

    }
}
