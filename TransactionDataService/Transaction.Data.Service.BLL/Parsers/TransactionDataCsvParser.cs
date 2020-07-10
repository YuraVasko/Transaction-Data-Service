using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Transaction.Data.Service.BLL.Constants;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.DAL.Models;

namespace Transaction.Data.Service.BLL.Parsers
{
    public class TransactionDataCsvParser : ITransactionDataParser
    {
        private const string RowSplitter = "\r\n";
        private const string DataSplitter = "\", \"";
        private const string Coma = ",";

        public IEnumerable<TransactionData> Parse(byte[] transactionDataBytes)
        {
            List<TransactionData> result = new List<TransactionData>();
            string transactionDataString = Encoding.UTF8.GetString(transactionDataBytes);
            var rows = transactionDataString.Split(RowSplitter);
            
            foreach (var row in rows)
            {
                var rowData = row.Split(DataSplitter);
                TryPopulateTransactionData(rowData, out var transactionData);
                
                result.Add(transactionData ?? throw new InvalidTransactionDataException());
            }

            return result;
        }

        private bool TryPopulateTransactionData(string[] rowData, out TransactionData transactionData)
        {
            transactionData = null;
            try
            {
                transactionData = new TransactionData()
                {
                    Id = GetTransactionDataId(rowData),
                    Amount = GetTransactionDataAmount(rowData),
                    CurrencyCode = GetTransactionDataCurrencyCode(rowData),
                    Status = GetTransactionDataTransactionStatus(rowData),
                    TransactionDate = GetTransactionDataTransactionDate(rowData)
                };
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetTransactionDataId(string[] rowData)
        {
            const int IdPossition = 0;
            string transactionId = Regex.Replace(rowData[IdPossition], "[^\\w\\d]", string.Empty);
            return transactionId;
        }

        private double GetTransactionDataAmount(string[] rowData)
        {
            const int AmountPossition = 1;
            var cultureInfo = new CultureInfo("en-US");
            string decimalWithoutComa = rowData[AmountPossition].Replace(Coma, string.Empty);
            return double.Parse(decimalWithoutComa, cultureInfo);
        }

        private string GetTransactionDataCurrencyCode(string[] rowData)
        {
            const int CurrencyCodePossition = 2;
            return rowData[CurrencyCodePossition];
        }

        private DateTime GetTransactionDataTransactionDate(string[] rowData)
        {
            const int DatePossition = 3;
            return Convert.ToDateTime(rowData[DatePossition]);
        }

        private TransactionDataStatus GetTransactionDataTransactionStatus(string[] rowData)
        {
            const int StatusPossition = 4;
            string transactionStatus = Regex.Replace(rowData[StatusPossition], "[^\\w\\d]", string.Empty);
            switch (transactionStatus)
            {
                case TransactionCsvStatus.Approved: return TransactionDataStatus.A;
                case TransactionCsvStatus.Failed: return TransactionDataStatus.R;
                case TransactionCsvStatus.Finished: return TransactionDataStatus.D;
                default: throw new InvalidTransactionStatusException(transactionStatus);
            }
        }
    }
}
