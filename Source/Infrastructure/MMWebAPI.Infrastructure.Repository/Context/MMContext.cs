using Microsoft.EntityFrameworkCore;
using MMWebAPI.Domain.Models;

namespace MMWebAPI.Infrastructure.Repository.Context
{
    public class MMContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<FinancialTransactionGroup> FinancialTransactionGroup { get; set; }
        public DbSet<FinancialTransaction> FinancialTransaction { get; set; }

        public MMContext()
        {

        }
        public MMContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MM;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialTransactionGroup>()
                .Ignore(p => p.TotalValue)
                .HasKey(ftg  => ftg.Id);
            modelBuilder.Entity<FinancialTransactionGroup>()
                .HasMany(ftg => ftg.FinancialTransactions).
                WithOne(ft => ft.Group);
        }
    }
}
