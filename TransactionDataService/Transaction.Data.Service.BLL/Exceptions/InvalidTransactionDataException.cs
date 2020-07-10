using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidTransactionDataException : Exception
    {
        public InvalidTransactionDataException()
            : base("Invalid transaction data exception")
        {
        }
    }
}
