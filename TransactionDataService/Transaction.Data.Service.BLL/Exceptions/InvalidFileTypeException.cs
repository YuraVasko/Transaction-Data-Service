using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException()
            : base("Unknown format")
        {
        }
    }
}
