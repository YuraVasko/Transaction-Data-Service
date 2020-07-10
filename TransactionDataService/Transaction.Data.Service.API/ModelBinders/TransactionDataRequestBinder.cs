using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.DAL.Models;

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

            foreach(var file in files.Files)
            {
                ITransactionDataParser parser = _transactionDataParserFactory.CreateParser(file.ContentType);
                byte[] fileContent = await GetContentBytesFromFile(file);
                var transactionData = parser.Parse(fileContent);
                result.TransactionData.AddRange(transactionData);
            }

            bindingContext.Result = ModelBindingResult.Success(result);
        }

        private static async Task<byte[]> GetContentBytesFromFile(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            long bytesLength = stream.Length;
            byte[] bytes = new byte[bytesLength];
            await stream.ReadAsync(bytes);
            return bytes;
        }
    }
}
