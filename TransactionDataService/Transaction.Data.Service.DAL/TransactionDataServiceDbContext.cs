using Microsoft.EntityFrameworkCore;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.DAL
{
    public class TransactionDataServiceDbContext : DbContext
    {
        public DbSet<TransactionData> TransactionData { get; set; }

        public TransactionDataServiceDbContext(DbContextOptions<TransactionDataServiceDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
