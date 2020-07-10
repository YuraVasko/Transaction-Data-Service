using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.DAL.Interfaces;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.BLL.Services
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly ITransactionDataRepository _transactionDataRepository;

        public TransactionDataService(ITransactionDataRepository transactionDataRepository)
        {
            _transactionDataRepository = transactionDataRepository;
        }

        public async Task AddRangeAsync(IEnumerable<TransactionData> transactionData)
        {
             await _transactionDataRepository.InsertRangeAsync(transactionData);
        }

        public async Task<IReadOnlyCollection<TransactionData>> GetAllAsync()
        {
            return await _transactionDataRepository.GetTransactionDataAsync();
        }
    }
}
