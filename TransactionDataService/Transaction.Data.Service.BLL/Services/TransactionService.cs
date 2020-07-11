using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Data.Service.BLL.Mappers;
using Transaction.Data.Service.BLL.Services.Interfaces;
using Transaction.Data.Service.DAL.Repositories.Interfaces;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper<TransactionDto, TransactionModel> _transactionMapper;

        public TransactionService(
            ITransactionRepository transactionDataRepository,
            ILogger<TransactionService> logger,
            IMapper<TransactionDto, TransactionModel> transactionMapper)
        {
            _transactionRepository = transactionDataRepository;
            _logger = logger;
            _transactionMapper = transactionMapper;
        }

        public async Task AddRangeAsync(IEnumerable<TransactionDto> transactionData)
        {
            if (transactionData == null)
            {
                throw new ArgumentNullException(nameof(transactionData));
            }

            try
            {
                _logger.LogInformation($"Try to save new transaction data");
                var transactions = transactionData.Select(t => _transactionMapper.Map(t));
                await _transactionRepository.InsertRangeAsync(transactions);

                _logger.LogInformation($"New transaction data has been successfully saved");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Could not save transaction data due to {ex.Message}");
                throw ex;
            }
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetAllAsync()
        {
            return await _transactionRepository.GetTransactionDataAsync();
        }
    }
}
