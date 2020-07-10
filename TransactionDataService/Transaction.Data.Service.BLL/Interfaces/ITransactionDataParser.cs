using System.Collections.Generic;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.BLL.Interfaces
{
    public interface ITransactionDataParser
    {
        IEnumerable<TransactionData> Parse(byte[] data);
    }
}
