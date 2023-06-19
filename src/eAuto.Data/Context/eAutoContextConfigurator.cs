﻿using eAuto.Data.Interfaces.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eAuto.Data.Context
{
    public class EAutoContextConfigurator
    {
        public static void RegisterContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EAutoContext>(options =>
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<EAutoContext>();
        }
    }
}