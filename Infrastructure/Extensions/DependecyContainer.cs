using Infrastructure.Data;
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
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<DapperDbContext>();
        }
    }
}
