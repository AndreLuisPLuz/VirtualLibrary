using Api.Domain.DataTransfer.Session;
using Api.Domain.Interfaces.Services;
using Api.Services.AutoMapper;
using Api.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<String>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<String>();
            var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<String>();

            if (jwtKey is null)
            {
                throw new Exception("Bad server-side configuration: missing secret key.");
            }

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidIssuer = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddSingleton<AuthSession>(new AuthSession(jwtKey, jwtIssuer));
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(IGenderService), typeof(GenderService));
            builder.Services.AddScoped(typeof(ILoginService), typeof(LoginService));
            builder.Services.AddScoped(typeof(IAuthorService), typeof(AuthorService));
            builder.Services.AddScoped(typeof(IBookService), typeof(BookService));

            builder.Services.AddControllers();
            // Additional configurations like DbContext, Authentication, etc.

            var app = builder.Build();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
