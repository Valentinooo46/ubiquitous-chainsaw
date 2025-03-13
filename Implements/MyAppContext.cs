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
        

    }
}
