using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Services;
using eAuto.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiConfiguration
{
    public sealed class DiConfigurator
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DiConfigurator(string connectionString, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterBuisnessPart(services);
            RegisterDataPart(services);
        }

        private void RegisterBuisnessPart(IServiceCollection services)
        {
            services.AddTransient<IBodyTypeService, BodyTypeService>();
        }
        
        private void RegisterDataPart(IServiceCollection services)
        {
            services.AddTransient(s => new EAutoContext(_connectionString));
            EAutoContextConfigurator.RegisterContext(services, _connectionString);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }


    }
}
