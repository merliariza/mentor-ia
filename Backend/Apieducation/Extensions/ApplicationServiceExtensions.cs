using System.Text;
using System.Threading.RateLimiting;
using ApiPortfolio.Helpers;
using ApiPortfolio.Helpers.Errors;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiPortfolio.Extensions;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });

    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(Infrastructure.Repositories.GenericRepository<>));
        services.AddScoped<IProgressRepository, Infrastructure.Repositories.ProgressRepository>();
        services.AddScoped<IUserMemberRepository, Infrastructure.Repositories.UserMemberRepository>();
        services.AddScoped<IEvaluationSessionRepository, Infrastructure.Repositories.EvaluationSessionRepository>();
        services.AddScoped<IFlashcardRepository, Infrastructure.Repositories.FlashcardRepository>();

        services.AddScoped<IPasswordHasher<UserMember>, PasswordHasher<UserMember>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();

    }

    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.OnRejected = async (context, token) =>
            {
                var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "desconocida";
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.ContentType = "application/json";
                var mensaje = $"{{\"message\": \"Demasiadas peticiones desde la IP {ip}. Intenta más tarde.\"}}";
                await context.HttpContext.Response.WriteAsync(mensaje, token);
            };

            options.AddPolicy("ipLimiter", httpContext =>
            {
                var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromSeconds(10),
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                });
            });
        });

        return services;
    }

public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = new JWT();
    configuration.Bind("JWT", jwtSettings);
    services.Configure<JWT>(configuration.GetSection("JWT"));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
}


    public static void AddValidationErrors(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                                .Where(u => u.Value.Errors.Count > 0)
                                .SelectMany(u => u.Value.Errors)
                                .Select(u => u.ErrorMessage)
                                .ToArray();

                var errorResponse = new ApiValidation() { Errors = errors };
                return new BadRequestObjectResult(errorResponse);
            };
        });
    }
}
