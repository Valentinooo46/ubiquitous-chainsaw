using Bogus;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UserEntityNS;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public class MyAppContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("workstation id = FirstData.mssql.somee.com; packet size = 4096; user id = VALEROSII_SQLLogin_2; pwd = *******; data source = FirstData.mssql.somee.com; persist security info = False; initial catalog = FirstData; TrustServerCertificate = True"); 
            //mssql


            /*optionsBuilder.UseSqlite("Data Source=users.db");*/ 
            //sqlite

            //optionsBuilder.UseNpgsql("Host=ep-shrill-surf-abi4tq9c-pooler.eu-west-2.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=*************");
            //PostgreSQL

        }
    }



    public class ManagerUsers
    {
        static Faker<UserEntity> Faker { get; set; }
        static ManagerUsers()
        {
            Faker = new Faker<UserEntity>()
                .RuleFor(u => u.FirstName,f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(18, DateTime.Now.AddYears(-10))));



        }
        public static void SeedData()
        {
            using (var db = new MyAppContext())
            {

                if (!db.Users.Any())  //Якщо в БД немає користувачів
                {
                    var users = new List<UserEntity>()
                        {
                            new UserEntity { FirstName="Іван", LastName="Підкаблучник", Phone="+38(098) 235 22 90", BirthDate = new DateOnly(2000, 2, 27) },
                            new UserEntity { FirstName="Мальвіна", LastName="Комарова", Phone="+38(066) 123 12 34", BirthDate = new DateOnly(2004, 4, 15) },
                            new UserEntity { FirstName="Семен", LastName="Заєць", Phone="+38(097) 342 11 11", BirthDate = new DateOnly(2003, 6, 1) },
                            new UserEntity { FirstName="Орел", LastName="Білка", Phone="+38(096) 234 11 45", BirthDate = new DateOnly(2006, 7, 12) },
                            new UserEntity { FirstName="Денис", LastName="Їжак", Phone="+38(098) 205 20 10", BirthDate = new DateOnly(2010, 5, 19) }
                        };
                    db.Users.AddRange(users);
                    db.SaveChanges();

                }
            }
        }
        public static void SeedData(int count)
        {
            using (var db = new MyAppContext())
            {
                var users = Faker.Generate(count);
                foreach (var user in users)
                {
                    if (user.FirstName.Length > 20)
                    {
                        user.FirstName = user.FirstName.Substring(0, 20);
                    }
                    if(user.LastName.Length > 20)
                    {
                        user.LastName = user.LastName.Substring(0, 20);
                    }
                    if (user.Phone.Length > 20)
                    {
                        user.Phone = user.Phone.Substring(0, 20);
                    }
                }
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
        public static List<UserEntity> GetUsers()
        {
            using (var db = new MyAppContext())
            {
                return db.Users.ToList();
            }
        }
        public static UserEntity? UserEntity(int id)
        {
            using (var db = new MyAppContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }
        public static void DeleteUser(int id)
        {
            using (var db = new MyAppContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Користувача з таким Id не знайдено");
                    db.SaveChanges();
                }
            }
        }
        public static void AddUser(UserEntity user)
        {
            using (var db = new MyAppContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static void UpdateUser(UserEntity user)
        {
            using (var db = new MyAppContext())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
    }
}