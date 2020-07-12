using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    abstract public class TransactionBaseException : Exception
    {
        public TransactionBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public TransactionBaseException(string message)
            : base(message)
        {
        }
    }
}
