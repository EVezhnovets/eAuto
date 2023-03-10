using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eAuto.Data.Context
{
    public class eAutoContextConfigurator
    {
        public static void RegisterContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<eAutoContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
