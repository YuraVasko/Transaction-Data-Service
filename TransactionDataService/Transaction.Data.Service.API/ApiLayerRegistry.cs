using Microsoft.Extensions.DependencyInjection;
using Transaction.Data.Service.API.Models;
using Transaction.Data.Service.API.Validators;
using Transaction.Data.Service.API.Validators.Interfaces;

namespace Transaction.Data.Service.API
{
    public static class ApiLayerRegistry
    {
        public static void RegisterApiLayer(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UploadTransactionDataRequest>, UploadTransactionDataRequestValidator>();
        }
    }
}
