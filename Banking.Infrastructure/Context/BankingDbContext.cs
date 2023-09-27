using Microsoft.EntityFrameworkCore;
using Banking.Domain.AggregateModels.AccountModels;
using Banking.Domain.AggregateModels.CustomerModels;

namespace Banking.Infrastructure.Context
{

    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany<Account>(g => g.Accounts)
                .WithOne(s => s.Customer )
                .HasForeignKey(s => s.HolderId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Account>().HasOne(s=>s.Customer)
	            .WithMany(s=>s.Accounts)
                .HasPrincipalKey(e => e.Id);

        }
    }
}
