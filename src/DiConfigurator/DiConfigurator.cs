using AccountManager;
using eAuto.Data;
using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Services;
using eAuto.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiConfiguration
{
    public sealed class DiConfigurator
    {
        private readonly string _identityConnection;
        private readonly string _appDbConnection;
        private readonly IConfiguration _configuration;

		public DiConfigurator(string identityConnection, string appDbConnection, IConfiguration configuration)
        {
            _identityConnection = identityConnection;
            _appDbConnection = appDbConnection;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, ILoggingBuilder loggingBuilder)
        {
            RegisterBuisnessPart(services);
            RegisterDataPart(services);
        }

        private void RegisterBuisnessPart(IServiceCollection services)
        {
            IdentityConfigurator.Configure(services, _identityConnection);

            services.AddTransient<IBodyTypeService, BodyTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IGenerationService, GenerationService>();
            services.AddTransient<ITransmissionService, TransmissionService>();
            services.AddTransient<IDriveTypeService, DriveTypeService>();
            services.AddTransient<ICarService, CarService>();
        }

        private void RegisterDataPart(IServiceCollection services)
        {
            services.AddTransient(s => new EAutoContext(_appDbConnection));
            EAutoContextConfigurator.RegisterContext(services, _appDbConnection);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        }
    }
}