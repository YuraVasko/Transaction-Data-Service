using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidTransactionDataException : TransactionBaseException
    {
        public InvalidTransactionDataException(string row, Exception innerException)
            : base($"Invalid transaction data exception in row: {row}", innerException)
        {
        }
    }
}
