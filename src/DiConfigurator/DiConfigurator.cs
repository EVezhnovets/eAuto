using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Services;
using eAuto.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DiConfiguration
{
    public sealed class DiConfigurator
    {
        private readonly string _connectionString;

        public DiConfigurator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterBuisnessPart(services);
            RegisterDataPart(services);
        }

        private void RegisterBuisnessPart(IServiceCollection services)
        {
            services.AddTransient<IBodyTypeService, BodyTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IGenerationService, GenerationService>();
            services.AddTransient<ITransmissionService, TransmissionService>();
            services.AddTransient<IDriveTypeService, DriveTypeService>();
        }

        private void RegisterDataPart(IServiceCollection services)
        {
            services.AddTransient(s => new EAutoContext(_connectionString));
            EAutoContextConfigurator.RegisterContext(services, _connectionString);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}