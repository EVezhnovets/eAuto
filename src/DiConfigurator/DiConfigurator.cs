using eAuto.Data;
using eAuto.Data.Context;
using eAuto.Data.Identity;
using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
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
            services.AddTransient<IEngineTypeService, EngineTypeService>();
            services.AddTransient<IMotorOilService, MotorOilService>();
            services.AddTransient<IProductBrandService, ProductBrandService>();
            services.AddTransient<IShoppingCartService<ShoppingCartDataModel>, ShoppingCartService>();
            services.AddTransient<IOrderHeaderRepository, OrderHeaderRepository>();
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        private void RegisterDataPart(IServiceCollection services)
        {
            services.AddScoped(s => new EAutoContext(_appDbConnection));
            EAutoContextConfigurator.RegisterContext(services, _appDbConnection);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        }
    }
}