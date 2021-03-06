﻿using System;

namespace Transaction.Data.Service.DAL.Models
{
    public class Transaction
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
