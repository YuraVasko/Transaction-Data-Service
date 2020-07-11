using System.Collections.Generic;
using System.Xml.Serialization;

namespace Transaction.Data.Service.DTO
{
    [XmlRoot(ElementName = "Transactions")]
    public class TransactionDataDto
    {
        public TransactionDataDto()
        {
            Transactions = new List<TransactionDto>();
        }

        [XmlElement(ElementName = "Transaction")]
        public List<TransactionDto> Transactions { get; set; }
    }
}
