using System.Collections.Generic;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.API.Validators.Interfaces;
using Transaction.Data.Service.API.Validators.Models;
using Transaction.Data.Service.DTO;
using Transaction.Data.Service.BLL.Constants;
using System;
using Microsoft.Extensions.Logging;

namespace Transaction.Data.Service.API.Validators
{
    public class UploadTransactionDataRequestValidator : IValidator<UploadTransactionDataRequest>
    {
        private readonly Dictionary<TransactionDto, List<string>> _validationErrors;
        private readonly List<string> _validTransactionStatuses;
        private readonly ILogger<UploadTransactionDataRequestValidator> _logger;

        public UploadTransactionDataRequestValidator(ILogger<UploadTransactionDataRequestValidator> logger)
        {
            _logger = logger;
            _validationErrors = new Dictionary<TransactionDto, List<string>>();
            _validTransactionStatuses = new List<string>
            {
                TransactionCsvStatus.Approved,
                TransactionCsvStatus.Failed,
                TransactionCsvStatus.Finished,
                TransactionXmlStatus.Done,
                TransactionXmlStatus.Rejected
            };
        }

        public ValidationResult Validate(UploadTransactionDataRequest request)
        {
            if (request != null)
            {
                _logger.LogInformation("Start request validation");

                request.TransactionData.Transactions.ForEach(ValidateTransaction);               
                ValidationResult result = GetValidationResult();

                _logger.LogInformation($"Finish Validation. Is request valid: {result.IsValid}");                
                if(!result.IsValid)
                {
                    _logger.LogInformation($"Validation Errors:\n{result}");
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private void ValidateTransaction(TransactionDto transaction)
        {
            ValidateTransactionId(transaction);
            ValidateTransactionAmount(transaction);
            ValidateTransactionCurrency(transaction);
            ValidateTransactionDate(transaction);
            ValidateTransactionStatus(transaction);
        }

        private void ValidateTransactionId(TransactionDto transaction)
        {
            const string InvalidTransactionId = "Invalid transaction id";
            if (string.IsNullOrEmpty(transaction?.Id))
            {
                AddValidationError(transaction, InvalidTransactionId);
            }
        }

        private void ValidateTransactionAmount(TransactionDto transaction)
        {
            const int Zero = 0;
            const string InvalidTransactionAmount = "Invalid transaction amount";
            if (transaction?.Payment?.Amount == Zero)
            {
                AddValidationError(transaction, InvalidTransactionAmount);
            }
        }

        private void ValidateTransactionCurrency(TransactionDto transaction)
        {
            const string InvalidTransactionCurrency = "Invalid transaction currency";
            if (string.IsNullOrEmpty(transaction?.Payment?.CurrencyCode))
            {
                AddValidationError(transaction, InvalidTransactionCurrency);
            }
        }

        private void ValidateTransactionDate(TransactionDto transaction)
        {
            const string InvalidTransactionDate = "Invalid transaction date";
            if (transaction?.TransactionDate == DateTime.MinValue)
            {
                AddValidationError(transaction, InvalidTransactionDate);
            }
        }

        private void ValidateTransactionStatus(TransactionDto transaction)
        {
            const string InvalidTransactionDate = "Invalid transaction status";
            if (!_validTransactionStatuses.Contains(transaction?.Status))
            {
                AddValidationError(transaction, InvalidTransactionDate);
            }
        }

        private void AddValidationError(TransactionDto transaction, string errorMessage)
        {
            _validationErrors.TryGetValue(transaction, out var errorMessages);
            if (errorMessages == null)
            {
                errorMessages = new List<string>();
                _validationErrors.Add(transaction, errorMessages);
            }
            errorMessages.Add(errorMessage);
        }

        private ValidationResult GetValidationResult()
        {
            const string Separator = "\n\t";
            List<string> validationResultErrorMessages = new List<string>();
            foreach(var error in _validationErrors)
            {
                string messagePrefix = $"The following transaction data:\n" +
                    $"\tId: {error.Key.Id}\n" +
                    $"\tAmount: {error.Key.Payment?.Amount}\n" +
                    $"\tCurrency: {error.Key.Payment?.CurrencyCode}\n" +
                    $"\tDate: {error.Key.TransactionDate}\n" +
                    $"\tStatus: {error.Key.Status}\n" +
                    " Has the following validation errors:\n\t";

                string errorMeesages = string.Join(Separator, error.Value.ToArray());
                string fullMessage = string.Concat(messagePrefix, errorMeesages);
                validationResultErrorMessages.Add(fullMessage);
            }
            return new ValidationResult(validationResultErrorMessages);
        }
    }
}
