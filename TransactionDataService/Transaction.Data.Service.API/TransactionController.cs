using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Services.Interfaces;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.API
{
    [ApiController]
    [Route("api/transaction-data")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(
            ITransactionService transactionDataService,
            IMapper mapper)
        {
            _transactionService = transactionDataService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> UploadTransactionDataCustomBinder(UploadTransactionDataRequest request)
        {
            var transactions = _mapper.Map<IEnumerable<TransactionModel>>(request.TransactionData.Transactions);
            await _transactionService.AddTransactionsAsync(transactions);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactionData()
        {
            var transactions = await _transactionService.GetAllAsync();
            var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
            return Ok(result);
        }

        [HttpGet("by/currency/{currency}")]
        public async Task<ActionResult> GetTransactionDataByCurrency(string currency)
        {
            var transactions = await _transactionService.GetTransactionsByCurrencyAsync(currency);
            var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
            return Ok(result);
        }

        [HttpGet("by/status/{status}")]
        public async Task<ActionResult> GetTransactionDataByStatus(string status)
        {
            var transactions = await _transactionService.GetTransactionsByStatusAsync(status);
            var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
            return Ok(result);
        }

        [HttpGet("by/date/{from}/{to}")]
        public async Task<ActionResult> GetTransactionDataByDateRannge(DateTime from, DateTime to)
        {
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(from, to);
            var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
            return Ok(result);
        }
    }
}
