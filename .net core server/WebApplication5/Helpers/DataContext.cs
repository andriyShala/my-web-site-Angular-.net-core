using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Entities;

namespace WebApplication5.Helpers
{
    public class DataContext:DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Entities.Message> Messages { get; set; }
        public DbSet<MessageRom> MessageRoms { get; set; }
        public DbSet<Proposition> Propositions { get; set; }
        public DbSet<MessageRomAndUser> MessageRomsAndUsers { get; set; }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=testWebSite;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                 .Property(e => e.Name)
                 .IsUnicode(false);
            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);
         
            modelBuilder.Entity<User>()
                .HasMany(x => x.Images)
                .WithOne(x => x.User)
                .HasForeignKey(e=>e.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MessageRom>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.MessageRom)
                .HasForeignKey(x => x.MessageRomId);
            modelBuilder.Entity<User>()
                .HasMany(x => x.MessageAndUsers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<MessageRomAndUser>()
                .HasOne(x => x.MessageRom).WithMany(x => x.MessageRomAndUsers);
            modelBuilder.Entity<MessageRomAndUser>()
             .HasOne(x => x.User).WithMany(x => x.MessageAndUsers);

        }
    }
}
