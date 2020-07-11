using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DAL.Repositories.Interfaces;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDataServiceDbContext _databaseContext;

        public TransactionRepository(TransactionDataServiceDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetTransactionDataAsync()
        {
            return await _databaseContext.Transactions.ToListAsync();
        }

        public async Task InsertRangeAsync(IEnumerable<TransactionModel> transactionData)
        {
            _databaseContext.Transactions.AddRange(transactionData);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
