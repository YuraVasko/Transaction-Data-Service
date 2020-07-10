using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Transaction.Data.Service.API.ModelBinders;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.API.Models
{
    [ModelBinder(BinderType = typeof(TransactionDataRequestBinder))]
    public class UploadTransactionDataRequest
    {
        public UploadTransactionDataRequest()
        {
            TransactionData = new List<TransactionData>();
        }

        public List<TransactionData> TransactionData { get; set; }
    }
}
