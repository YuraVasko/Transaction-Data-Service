using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Exceptions;
using Transaction.Data.Service.BLL.Factories.Interfaces;
using Transaction.Data.Service.BLL.Parsers.Interfaces;
using Transaction.Data.Service.DTO;

namespace Transaction.Data.Service.API.ModelBinders
{
    public class TransactionDataRequestBinder : IModelBinder
    {
        private readonly ITransactionDataParserFactory _transactionDataParserFactory;

        public TransactionDataRequestBinder(ITransactionDataParserFactory transactionDataParserFactory)
        {
            _transactionDataParserFactory = transactionDataParserFactory;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            IFormCollection files = await bindingContext.HttpContext.Request.ReadFormAsync();
            UploadTransactionDataRequest result = new UploadTransactionDataRequest();

            foreach (var file in files.Files)
            {
                ITransactionDataParser parser = _transactionDataParserFactory.CreateParser(file.ContentType);
                string fileContent = await GetFileContent(file);
                TransactionDataDto transactionData = parser.Parse(fileContent);
                result.TransactionData.Transactions.AddRange(transactionData.Transactions);
            }
            bindingContext.Result = ModelBindingResult.Success(result);
        }

        private static async Task<string> GetFileContent(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            long bytesLength = stream.Length;
            byte[] bytes = new byte[bytesLength];
            await stream.ReadAsync(bytes);
            string data = Encoding.UTF8.GetString(bytes);
            return data;
        }
    }
}
