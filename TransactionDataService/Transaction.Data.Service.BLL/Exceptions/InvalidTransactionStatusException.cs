using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    class InvalidTransactionStatusException : Exception
    {
        public InvalidTransactionStatusException(string invalidStatus)
            : base($"Invalid transaction status: {invalidStatus}")
        {
        }
    }
}
