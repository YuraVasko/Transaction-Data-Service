using System;
using System.Xml.Serialization;

namespace Transaction.Data.Service.DTO
{
    [XmlRoot(ElementName = "Transaction")]
    public class TransactionDto
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "PaymentDetails")]
        public PaymentDto Payment { get; set; }

        public TransactionDto()
        {
            Payment = new PaymentDto();
        }
    }
}
