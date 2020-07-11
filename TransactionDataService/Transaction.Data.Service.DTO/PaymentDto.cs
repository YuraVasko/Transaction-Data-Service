using System.Xml.Serialization;

namespace Transaction.Data.Service.DTO
{
    [XmlRoot(ElementName = "PaymentDetails")]
    public class PaymentDto
    {
        [XmlElement(ElementName = "Amount")]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
