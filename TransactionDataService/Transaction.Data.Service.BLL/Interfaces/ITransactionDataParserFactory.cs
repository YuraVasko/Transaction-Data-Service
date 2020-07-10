namespace Transaction.Data.Service.BLL.Interfaces
{
    public interface ITransactionDataParserFactory
    {
        public ITransactionDataParser CreateParser(string fileType);
    }
}
