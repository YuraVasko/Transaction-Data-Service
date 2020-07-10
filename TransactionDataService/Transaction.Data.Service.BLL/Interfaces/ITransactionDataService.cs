using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.BLL.Interfaces
{
    public interface ITransactionDataService
    {
        Task<IReadOnlyCollection<TransactionData>> GetAllAsync();
        Task AddRangeAsync(IEnumerable<TransactionData> transactionData);
    }
}
