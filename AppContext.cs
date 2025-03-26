
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Mobizon
{
    public class AppContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserNotificationEntity> UserNotifications { get; set; }
        public DbSet<UserEventEntity> UserEvents { get; set; }
        public DbSet<EventTypeEntity> EventTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("*");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.UserNotifications)
                .WithOne(un => un.User)
                .HasForeignKey(un => un.UserId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.UserEvents)
                .WithOne(ue => ue.User)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<UserEventEntity>()
                .HasOne(ue => ue.EventType)
                .WithMany()
                .HasForeignKey(ue => ue.EventTypeId);
        }
    }
}
