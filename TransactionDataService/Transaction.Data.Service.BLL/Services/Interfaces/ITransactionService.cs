using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyCollection<TransactionModel>> GetAllAsync();
        Task AddRangeAsync(IEnumerable<TransactionDto> transactionData);
    }
}
