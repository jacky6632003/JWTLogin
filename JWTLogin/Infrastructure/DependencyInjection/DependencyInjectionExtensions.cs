using JWTLogin.Repository.Implement;
using JWTLogin.Repository.Interface;
using JWTLogin.Service.Implement;
using JWTLogin.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Infrastructure.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDendencyInjection(this IServiceCollection services)
        {
            // Repository

            services.AddScoped<IAccountRepository, AccountRepository>();

            // Service
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
