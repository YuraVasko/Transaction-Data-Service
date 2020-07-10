using System.Collections.Generic;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.BLL.Parsers
{
    class TransactionDataXmlParser : ITransactionDataParser
    {
        public IEnumerable<TransactionData> Parse(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}
