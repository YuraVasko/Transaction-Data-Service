using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Transaction.Data.Service.BLL.Exceptions;
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
            try
            {
                var rowData = row.Split(DataSplitter);
                return new TransactionDto()
                {
                    Id = GetTransactionId(rowData),
                    Payment = GetPayment(rowData),
                    TransactionDate = GetTransactionDate(rowData),
                    Status = GetTransactionTransactionStatus(rowData),
                }; ;
            }
            catch(Exception ex)
            {
                throw new InvalidTransactionDataException(row, ex);
            }
        }

        private PaymentDto GetPayment(string[] rowData)
        {
            return new PaymentDto
            {
                Amount = GetTransactionAmount(rowData),
                CurrencyCode = GetTransactionCurrencyCode(rowData),
            };
        }

        private string GetTransactionId(string[] rowData)
        {
            const int IdPossition = 0;
            string transactionId = Regex.Replace(rowData[IdPossition], "[^\\w\\d]", string.Empty);
            return transactionId;
        }

        private decimal GetTransactionAmount(string[] rowData)
        {
            const int AmountPossition = 1;
            var cultureInfo = new CultureInfo("en-US");
            string decimalWithoutComa = rowData[AmountPossition].Replace(Coma, string.Empty);
            return decimal.Parse(decimalWithoutComa, cultureInfo);
        }

        private string GetTransactionCurrencyCode(string[] rowData)
        {
            const int CurrencyCodePossition = 2;
            return rowData[CurrencyCodePossition];
        }

        private DateTime GetTransactionDate(string[] rowData)
        {
            const int DatePossition = 3;
            DateTime date = DateTime.ParseExact(rowData[DatePossition], "dd/MM/yyyy hh:mm:ss", null);
            return date;
        }

        private string GetTransactionTransactionStatus(string[] rowData)
        {
            const int StatusPossition = 4;
            string transactionStatus = Regex.Replace(rowData[StatusPossition], "[^\\w\\d]", string.Empty);
            return transactionStatus;
        }
    }
}
