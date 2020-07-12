using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Transaction.Data.Service.BLL.Parsers.Interfaces;
using Transaction.Data.Service.DTO;

namespace Transaction.Data.Service.BLL.Parsers
{
    public class TransactionDataCsvParser : ITransactionDataParser
    {
        private const string RowSplitter = "\r\n";
        private const string DataSplitter = "\", \"";
        private const string Coma = ",";

        public TransactionDataDto Parse(string data)
        {
            TransactionDataDto result = new TransactionDataDto();
            var rows = data.Split(RowSplitter);

            foreach (var row in rows)
            {
                var transaction = ParseCsvRowToTransactionDto(row);

                result.Transactions.Add(transaction);
            }

            return result;
        }

        private TransactionDto ParseCsvRowToTransactionDto(string row)
        {
            var rowData = row.Split(DataSplitter);

            var result = new TransactionDto();
            TrySetTransactionId(rowData, result);
            TrySetTransactionCurrencyCode(rowData, result);
            TryParseTransactionAmount(rowData, result);
            TrySetTransactionDate(rowData, result);
            TrySetTransactionStatus(rowData, result);

            return result;
        }

        private bool TrySetTransactionId(string[] rowData, TransactionDto transaction)
        {
            const int IdPossition = 0;
            try
            {
                transaction.Id = Regex.Replace(rowData[IdPossition], "[^\\w\\d]", string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryParseTransactionAmount(string[] rowData, TransactionDto transaction)
        {
            const int AmountPossition = 1;
            try
            {
                string decimalWithoutComa = rowData[AmountPossition].Replace(Coma, string.Empty);
                transaction.Payment.Amount = decimal.Parse(decimalWithoutComa, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TrySetTransactionCurrencyCode(string[] rowData, TransactionDto transaction)
        {
            const int CurrencyCodePossition = 2;
            try
            {
                transaction.Payment.CurrencyCode = rowData[CurrencyCodePossition];
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TrySetTransactionDate(string[] rowData, TransactionDto transaction)
        {
            const int DatePossition = 3;
            const string DateTimeFormat = "dd/MM/yyyy hh:mm:ss";
            try
            {
                transaction.TransactionDate = DateTime.ParseExact(rowData[DatePossition], DateTimeFormat, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TrySetTransactionStatus(string[] rowData, TransactionDto transaction)
        {
            const int StatusPossition = 4;
            try
            {
                transaction.Status = Regex.Replace(rowData[StatusPossition], "[^\\w\\d]", string.Empty);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
