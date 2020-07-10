using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DAL.Interfaces;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.DAL.Repositories
{
    public class TransactionDataRepository : ITransactionDataRepository
    {
        private readonly TransactionDataServiceDbContext _databaseContext;

        public TransactionDataRepository(TransactionDataServiceDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task DeleteAsync(string id)
        {
            var transactionData = await _databaseContext.TransactionData.FindAsync();
            _databaseContext.TransactionData.Remove(transactionData);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TransactionData>> GetTransactionDataAsync()
        {
            return await _databaseContext.TransactionData.ToListAsync();
        }

        public async Task InsertAsync(TransactionData transactionData)
        {
            await _databaseContext.TransactionData.AddAsync(transactionData);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task InsertRangeAsync(IEnumerable<TransactionData> transactionData)
        {
            _databaseContext.TransactionData.AddRange(transactionData);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
