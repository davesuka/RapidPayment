using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.Data.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Card> Card { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<PaymentFee> PaymentFee { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(k => k.CardId);
            modelBuilder.Entity<Card>().Property(k => k.CardId).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<Card>().Property(k => k.Number).HasMaxLength(15);
            modelBuilder.Entity<Card>().Property(k => k.Number).IsRequired();
            modelBuilder.Entity<Card>().Property(k => k.Balance).IsRequired();
            modelBuilder.Entity<Card>()
               .HasIndex(u => u.Number)
               .IsUnique();

            modelBuilder.Entity<PaymentHistory>().HasKey(k => k.PaymentHistoryId);
            modelBuilder.Entity<PaymentHistory>().Property(k => k.PaymentHistoryId).ValueGeneratedOnAdd().UseIdentityColumn();

            modelBuilder.Entity<Card>()
            .HasMany(c => c.PaymentHistories)
            .WithOne(e => e.Card);

            modelBuilder.Entity<User>().HasKey(k => k.UserId);
            modelBuilder.Entity<User>().Property(k => k.UserId).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<User>().Property(k => k.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(k => k.Password).IsRequired();

            modelBuilder.Entity<User>().HasData(
            new User { UserId = 1, UserName = "davesuka", Password = "Qwerty12345" });


            modelBuilder.Entity<PaymentFee>().HasKey(k => k.PaymentFeeId);
            modelBuilder.Entity<PaymentFee>().Property(k => k.PaymentFeeId).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<PaymentFee>().Property(k => k.Fee).IsRequired();
            modelBuilder.Entity<PaymentFee>().Property(k => k.UpdatedDate).IsRequired();

            var randomValue = new Random();
            var next = randomValue.NextDouble();
            modelBuilder.Entity<PaymentFee>().HasData(
            new PaymentFee { PaymentFeeId = 1, Fee = (decimal)(0 + (next * (2 - 0))), UpdatedDate = DateTime.Now });
        }
    }
}
