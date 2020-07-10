using Microsoft.Extensions.DependencyInjection;
using Transaction.Data.Service.BLL.Factories;
using Transaction.Data.Service.BLL.Interfaces;
using Transaction.Data.Service.BLL.Services;

namespace Transaction.Data.Service.BLL
{
    public static class BusinessLogicLayerRegistry
    {
        public static void RegisterBusinessLogicLayer(
            this IServiceCollection services)
        {
            services.AddScoped<ITransactionDataParserFactory, TransactionDataParserFactory>();
            services.AddScoped<ITransactionDataService, TransactionDataService>();
        }
    }
}
