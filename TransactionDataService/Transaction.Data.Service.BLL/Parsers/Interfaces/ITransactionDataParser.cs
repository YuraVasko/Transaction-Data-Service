using Transaction.Data.Service.DTO;

namespace Transaction.Data.Service.BLL.Parsers.Interfaces
{
    public interface ITransactionDataParser
    {
        TransactionDataDto Parse(string data);
    }
}
