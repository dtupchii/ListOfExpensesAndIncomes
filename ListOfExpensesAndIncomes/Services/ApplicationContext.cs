using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfExpensesAndIncomes.Models;
using Microsoft.EntityFrameworkCore;

namespace ListOfExpensesAndIncomes.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\user\source\repos\ListOfExpensesAndIncomes\ListOfExpensesAndIncomes\Transfers.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne<User>(t => t.User)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<User>()
                .HasOne<Currency>(c => c.Currency)
                .WithMany(u => u.Users)
                .HasForeignKey(c => c.CurrencyId);
        }
    }
}