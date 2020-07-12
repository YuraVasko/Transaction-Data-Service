using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidFileTypeException : TransactionBaseException
    {
        public InvalidFileTypeException()
            : base("Unknown format")
        {
        }
    }
}
