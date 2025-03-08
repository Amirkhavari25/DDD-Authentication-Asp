using Application.Interface;
using Application.Mapper;
using Application.Services;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configiration)
        {
            //dependency for Ef core context
            services.AddDbContext<EFDbContext>(options =>
                options.UseSqlServer(configiration.GetConnectionString("DefaultConnection")));
            //dependency for dapper context
            services.AddScoped<DapperDbContext>();
            //dependency for auto mappe 
            services.AddAutoMapper(typeof(UserMapper));

            //service and interface injections
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
