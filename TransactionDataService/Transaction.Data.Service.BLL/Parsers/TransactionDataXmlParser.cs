using System.IO;
using System.Xml.Serialization;
using Transaction.Data.Service.BLL.Parsers.Interfaces;
using Transaction.Data.Service.DTO;

namespace Transaction.Data.Service.BLL.Parsers
{
    class TransactionDataXmlParser : ITransactionDataParser
    {
        TransactionDataDto ITransactionDataParser.Parse(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TransactionDataDto));
            using TextReader reader = new StringReader(data);
            TransactionDataDto result = (TransactionDataDto)serializer.Deserialize(reader);
            return result;
        }
    }
}
