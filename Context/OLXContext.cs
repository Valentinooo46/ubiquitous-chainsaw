using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLX.Entities;

namespace OLX.Context
{
    public class OLXContext : DbContext
    {
        public DbSet<Entities.CategoryEntity> _categories { get; set; }
        public DbSet<Entities.ProductEntity> _products { get; set; }
        public DbSet<Entities.Category> _newcategories { get; set; }
        public DbSet<Entities.Subcategory> _subcategories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("**");
        //}
        public OLXContext(DbContextOptions<OLXContext> contextOptions) : base(contextOptions)
        {

        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<Entities.ProductEntity>().ToTable("Products");
            modelBuilder.Entity<Entities.Category>().HasMany<Subcategory>("Subcategories");
        }
    }
}
