using System;

namespace Transaction.Data.Service.BLL.Exceptions
{
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException(string invalidFileFormat)
            : base($"Invalid file type has been used during transaction data proccessing: {invalidFileFormat}")
        {
        }
    }
}
