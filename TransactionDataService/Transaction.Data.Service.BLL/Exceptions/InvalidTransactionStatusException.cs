using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidTransactionStatusException : TransactionBaseException
    {
        public InvalidTransactionStatusException(string invalidStatus)
            : base($"Invalid transaction status: {invalidStatus}")
        {
        }
    }
}
