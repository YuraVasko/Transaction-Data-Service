using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidTransactionDataException : Exception
    {
        public InvalidTransactionDataException(string row, Exception innerException)
            : base($"Invalid transaction data exception in row: {row}", innerException)
        {
        }
    }
}
