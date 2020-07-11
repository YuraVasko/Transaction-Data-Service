using Transaction.Data.Service.BLL.Parsers.Interfaces;

namespace Transaction.Data.Service.BLL.Factories.Interfaces
{
    public interface ITransactionDataParserFactory
    {
        public ITransactionDataParser CreateParser(string fileType);
    }
}
