using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Data.Service.DAL.Repositories;
using Transaction.Data.Service.DAL.Repositories.Interfaces;

namespace Transaction.Data.Service.DAL
{
    public static class DataAccessLayerRegistry
    {
        public static void RegisterDataAccessLayer(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<TransactionDataServiceDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
