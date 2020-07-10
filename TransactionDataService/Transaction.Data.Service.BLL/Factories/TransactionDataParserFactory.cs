using Transaction.Data.Service.BLL.Constants;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.BLL.Parsers;

namespace Transaction.Data.Service.BLL.Factories
{
    public class TransactionDataParserFactory : ITransactionDataParserFactory
    {
        public ITransactionDataParser CreateParser(string fileType)
        {
            switch (fileType)
            {
                case SupportedFileType.XmlType: return new TransactionDataXmlParser();
                case SupportedFileType.CsvType: return new TransactionDataCsvParser();
                default: throw new InvalidFileTypeException(fileType);
            }
        }
    }
}
