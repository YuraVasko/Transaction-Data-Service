using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transaction.Data.Service.API;
using Transaction.Data.Service.BLL;
using Transaction.Data.Service.DAL;

namespace Transaction.Data.Service.Host
{
    public class Startup
    {
        private const string ConnectionConfigPath = "DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(ConnectionConfigPath);

            services.AddAutoMapper(cfg => 
            {
                cfg.AddProfile<TransactionMappings>();
            });

            services.RegisterDataAccessLayer(connectionString);
            services.RegisterBusinessLogicLayer();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
