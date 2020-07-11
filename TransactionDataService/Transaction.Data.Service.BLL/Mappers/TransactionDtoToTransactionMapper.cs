using System;
using Transaction.Data.Service.BLL.Constants;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.DAL.Models;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL.Mappers
{
    class TransactionDtoToTransactionMapper : IMapper<TransactionDto, TransactionModel>
    {
        public TransactionModel Map(TransactionDto model)
        {
            return new TransactionModel
            {
                Amount = model.Payment.Amount,
                CurrencyCode = model.Payment.CurrencyCode,
                Id = model.Id,
                TransactionDate = model.TransactionDate,
                Status = GetTransactionStatus(model.Status)
            };
        }

        private TransactionStatus GetTransactionStatus(string status)
        {
            switch (status)
            {
                case TransactionCsvStatus.Approved: return TransactionStatus.A;
                case TransactionCsvStatus.Failed: return TransactionStatus.R;
                case TransactionCsvStatus.Finished: return TransactionStatus.D;
                case TransactionXmlStatus.Rejected: return TransactionStatus.R;
                case TransactionXmlStatus.Done: return TransactionStatus.D;
                default: throw new InvalidTransactionStatusException(status);
            }
        }
    }
}
