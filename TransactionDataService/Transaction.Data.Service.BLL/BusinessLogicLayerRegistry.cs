using Microsoft.Extensions.DependencyInjection;
using Transaction.Data.Service.BLL.Factories;
using Transaction.Data.Service.BLL.Factories.Interfaces;
using Transaction.Data.Service.BLL.Mappers;
using Transaction.Data.Service.BLL.Services;
using Transaction.Data.Service.BLL.Services.Interfaces;
using Transaction.Data.Service.DTO;
using TransactionModel = Transaction.Data.Service.DAL.Models.Transaction;

namespace Transaction.Data.Service.BLL
{
    public static class BusinessLogicLayerRegistry
    {
        public static void RegisterBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<ITransactionDataParserFactory, TransactionDataParserFactory>();
            services.AddScoped<IMapper<TransactionDto, TransactionModel>, TransactionDtoToTransactionMapper>();
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
