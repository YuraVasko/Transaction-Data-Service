using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DAL.Repositories.Interfaces;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Transaction.Data.Service.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDataServiceDbContext _databaseContext;

        public TransactionRepository(TransactionDataServiceDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByFilter(Expression<Func<TransactionModel, bool>> filter)
        {
            return await _databaseContext
                .Transactions
                .Where(filter)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetAllTransactionsAsync()
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
