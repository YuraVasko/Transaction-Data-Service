using Microsoft.Extensions.DependencyInjection;
using Transaction.Data.Service.BLL.Factories;
using Transaction.Data.Service.BLL.Factories.Interfaces;
using Transaction.Data.Service.BLL.Services;
using Transaction.Data.Service.BLL.Services.Interfaces;

namespace Transaction.Data.Service.BLL
{
    public static class BusinessLogicLayerRegistry
    {
        public static void RegisterBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<ITransactionDataParserFactory, TransactionDataParserFactory>();
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
