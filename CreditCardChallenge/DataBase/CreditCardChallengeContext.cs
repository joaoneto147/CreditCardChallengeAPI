using CreditCardChallenge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.DataBase
{
    public class CreditCardChallengeContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public CreditCardChallengeContext(DbContextOptions<CreditCardChallengeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=localhost;database=credit_cards;user=root;password=aff123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CreditCard>().HasKey(c => c.Id).HasName("PK_CREDIT_CARD");
            modelBuilder.Entity<CreditCard>().HasIndex(c => new { c.Number, c.UserId }).HasName("IDX_CREDIT_CARD_UID_NUMB").IsUnique();

            modelBuilder.Entity<Purchase>()
                .HasOne(c => c.CreditCard)
                .WithMany(c => c.Purchases);
        }


    }
}
