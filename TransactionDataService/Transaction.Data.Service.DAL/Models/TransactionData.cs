using System;

namespace Transaction.Data.Service.DAL.Models
{
    public class TransactionData
    {
        public string Id { get; set; }

        public double Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionDataStatus Status { get; set; }
    }
}
