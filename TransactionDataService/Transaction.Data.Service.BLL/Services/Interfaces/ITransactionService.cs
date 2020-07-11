using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyCollection<TransactionModel>> GetAllAsync();

        Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByStatusAsync(string status);

        Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByCurrencyAsync(string currency);

        Task<IReadOnlyCollection<TransactionModel>> GetTransactionsByDateRangeAsync(DateTime from, DateTime to);

        Task AddTransactionsAsync(IEnumerable<TransactionModel> transactions);
    }
}
