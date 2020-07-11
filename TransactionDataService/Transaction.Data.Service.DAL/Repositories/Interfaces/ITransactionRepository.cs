using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.DAL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IReadOnlyCollection<TransactionModel>> GetTransactionDataAsync();

        Task InsertRangeAsync(IEnumerable<TransactionModel> transactionData);
    }
}
