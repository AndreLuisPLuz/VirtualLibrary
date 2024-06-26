using Api.Domain.Interfaces.Services;
using Api.Services.AutoMapper;
using Api.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));

            builder.Services.AddControllers();
            // Additional configurations like DbContext, Authentication, etc.

            var app = builder.Build();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
