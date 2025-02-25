
using API.Middleware;
using Infrastructure.Extensions;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //handle the cors error in developement enviroment
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Add services to the container.
            DependecyContainer.RegisterServices(builder.Services, builder.Configuration);



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //use cors error
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //user authentication middleware
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/auth/register") &&
                                !context.Request.Path.StartsWithSegments("/api/auth/loginbyemail"),
             builder =>
             {
                 builder.UseMiddleware<AuthenticationMiddleware>();
             });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
