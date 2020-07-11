using Microsoft.AspNetCore.Mvc;
using Transaction.Data.Service.API.ModelBinders;
using Transaction.Data.Service.DTO;

namespace Transaction.Data.Service.API.Models
{
    [ModelBinder(BinderType = typeof(TransactionDataRequestBinder))]
    public class UploadTransactionDataRequest
    {
        public UploadTransactionDataRequest()
        {
            TransactionData = new TransactionDataDto();
        }

        public TransactionDataDto TransactionData { get; set; }
    }
}
