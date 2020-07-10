using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Transaction.Data.Service.BLL.Interfaces;

namespace Transaction.Data.Service.API
{
    [ApiController]
    [Route("api/transaction-data")]
    public class TransactionDataController : ControllerBase
    {
        private readonly ITransactionDataService _transactionDataService;

        public TransactionDataController(ITransactionDataService transactionDataService)
        {
            _transactionDataService = transactionDataService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadTransactionData()
        {
            var files = Request.Form.Files;
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
