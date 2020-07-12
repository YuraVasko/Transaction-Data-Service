using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.API.Validators.Interfaces;
using Transaction.Data.Service.API.Validators.Models;
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
        private readonly IValidator<UploadTransactionDataRequest> _validator;

        public TransactionController(
            ITransactionService transactionDataService,
            IValidator<UploadTransactionDataRequest> validator,
            IMapper mapper)
        {
            _transactionService = transactionDataService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> UploadTransactionDataCustomBinder(UploadTransactionDataRequest request)
        {
            ValidationResult validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            try
            {
                var transactions = _mapper.Map<IEnumerable<TransactionModel>>(request.TransactionData.Transactions);
                await _transactionService.AddTransactionsAsync(transactions);
                return Ok();
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
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
            try
            {
                var transactions = await _transactionService.GetTransactionsByCurrencyAsync(currency);
                var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
                return Ok(result);
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpGet("by/status/{status}")]
        public async Task<ActionResult> GetTransactionDataByStatus(string status)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByStatusAsync(status);
                var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpGet("by/date/{from}/{to}")]
        public async Task<ActionResult> GetTransactionDataByDateRannge(DateTime from, DateTime to)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByDateRangeAsync(from, to);
                var result = _mapper.Map<IReadOnlyCollection<TransactionDetailsResponse>>(transactions);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
    }
}
