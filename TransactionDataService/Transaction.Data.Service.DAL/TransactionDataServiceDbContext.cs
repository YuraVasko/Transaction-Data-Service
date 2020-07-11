using Microsoft.EntityFrameworkCore;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.DAL
{
    public class TransactionDataServiceDbContext : DbContext
    {
        public DbSet<TransactionModel> Transactions { get; set; }

        public TransactionDataServiceDbContext(DbContextOptions<TransactionDataServiceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
