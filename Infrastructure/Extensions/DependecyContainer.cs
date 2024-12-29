using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DependecyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configiration)
        {
            //dependency for Ef core context
            services.AddDbContext<EFDbContext>(options =>
                options.UseSqlServer(configiration.GetConnectionString("DefaultConnection")));
            //dependency for dapper context
            services.AddScoped<DapperDbContext>();
        }
    }
}
