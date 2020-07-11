using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Services.Interfaces;

namespace Transaction.Data.Service.API
{
    [ApiController]
    [Route("api/transaction-data")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(
            ITransactionService transactionDataService,
            ILogger<TransactionController> logger)
        {
            _transactionService = transactionDataService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> UploadTransactionDataCustomBinder(UploadTransactionDataRequest request)
        {
            await _transactionService.AddRangeAsync(request.TransactionData.Transactions);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactionData()
        {
            var transactionData = await _transactionService.GetAllAsync();
            return Ok(transactionData);
        }
    }
}
