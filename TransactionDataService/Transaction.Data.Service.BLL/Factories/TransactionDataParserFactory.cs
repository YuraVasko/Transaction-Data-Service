using Microsoft.Extensions.Logging;
using Transaction.Data.Service.BLL.Constants;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.BLL.Factories.Interfaces;
using Transaction.Data.Service.BLL.Parsers;
using Transaction.Data.Service.BLL.Parsers.Interfaces;

namespace Transaction.Data.Service.BLL.Factories
{
    public class TransactionDataParserFactory : ITransactionDataParserFactory
    {
        private readonly ILogger<TransactionDataParserFactory> _logger;

        public TransactionDataParserFactory(ILogger<TransactionDataParserFactory> logger)
        {
            _logger = logger;
        }

        public ITransactionDataParser CreateParser(string fileType)
        {
            _logger.LogInformation($"Try to create parser for {fileType}");

            switch (fileType)
            {
                case SupportedFileType.XmlType: return new TransactionDataXmlParser();
                case SupportedFileType.CsvType: return new TransactionDataCsvParser();
                default:
                    _logger.LogError($"Invalid file type: {fileType}");
                    throw new InvalidFileTypeException();
            }
        }
    }
}
