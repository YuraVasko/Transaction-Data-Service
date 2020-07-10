using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.DAL.Interfaces
{
    public interface ITransactionDataRepository
    {
        Task<IReadOnlyCollection<TransactionData>> GetTransactionDataAsync();

        Task InsertAsync(TransactionData transactionData);

        Task InsertRangeAsync(IEnumerable<TransactionData> transactionData);

        Task DeleteAsync(string id);
    }
}
