using eAuto.Data.Context;
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
            RegisterDataPart(services);
        }
        
        private void RegisterDataPart(IServiceCollection services)
        {
            services.AddTransient(s => new eAutoContext(_connectionString));
            eAutoContextConfigurator.RegisterContext(services, _connectionString);
        }
    }
}
