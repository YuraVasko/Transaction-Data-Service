using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Data.Service.BLL.Services.Interfaces;
using Transaction.Data.Service.DAL.Models;
using Transaction.Data.Service.DAL.Repositories.Interfaces;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(
            ITransactionRepository transactionDataRepository,
            ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionDataRepository;
            _logger = logger;
        }

        public async Task AddTransactionsAsync(IEnumerable<TransactionModel> transactions)
        {
            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            try
            {
                _logger.LogInformation($"Try to save new transaction data");

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
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByCurrencyAsync(string currency)
        {
            return await _transactionRepository.GetTransactionsByFilter(t => t.CurrencyCode == currency);
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _transactionRepository.GetTransactionsByFilter(t => t.TransactionDate > from && t.TransactionDate < to);
        }

        public async Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByStatusAsync(string status)
        {
            Enum.TryParse(status, out TransactionStatus transactionStatus);
            return await _transactionRepository.GetTransactionsByFilter(t => t.Status == transactionStatus);
        }
    }
}
