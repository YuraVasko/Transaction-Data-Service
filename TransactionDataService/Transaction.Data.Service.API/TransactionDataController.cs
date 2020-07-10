using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Interfaces;

namespace Transaction.Data.Service.API
{
    [ApiController]
    [Route("api/transaction-data")]
    public class TransactionDataController : ControllerBase
    {
        private readonly ITransactionDataService _transactionDataService;
        private readonly ILogger<TransactionDataController> _logger;

        public TransactionDataController(
            ITransactionDataService transactionDataService,
            ILogger<TransactionDataController> logger)
        {
            _transactionDataService = transactionDataService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> UploadTransactionDataCustomBinder(UploadTransactionDataRequest request)
        {
            await _transactionDataService.AddRangeAsync(request.TransactionData);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactionData()
        {
            var transactionData = await _transactionDataService.GetAllAsync();
            return Ok(transactionData);
        }
    }
}
