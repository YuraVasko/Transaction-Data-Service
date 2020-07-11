using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.DAL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IReadOnlyCollection<TransactionModel>> GetAllTransactionsAsync();

        Task InsertRangeAsync(IEnumerable<TransactionModel> transactionData);

        Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByFilter(Expression<Func<TransactionModel, bool>> filter);
    }
}
