using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Commands;
using ScrapDealer.Shared.Exceptions;
using ScrapDealer.Shared.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ScrapDealer.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ScrapDealer.Shared.Options;


namespace ScrapDealer.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {

            // Register MediatR
            var applicationAssembly = Assembly.Load("ScrapDealer.Application"); // Replace with your actual assembly name
            var infrastructureAssembly = Assembly.Load("ScrapDealer.Infrastructure"); // Replace with your actual assembly name

            // Register MediatR and include the dynamically loaded assembly
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); // Shared project
                cfg.RegisterServicesFromAssembly(applicationAssembly); // Application project (loaded dynamically)
                cfg.RegisterServicesFromAssembly(infrastructureAssembly); // Infrastructure project (loaded dynamically)
            });

            // Register all ICommandHandler<> implementations after MediatR
            services.Scan(scan => scan
                .FromAssemblies(applicationAssembly, infrastructureAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());


            // Register command and query dispatchers
            services.AddScoped<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.AddScoped<IQueryDispatcher, InMemoryQueryDispatcher>();

            // Add middleware
            services.AddScoped<ExceptionMiddleware>();

            services.AddAuthenticationConfig(configuration);

            return services;
        }

        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = configuration.GetOptions<JwtSettings>("Jwt");

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                };
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Admin");
                    });

                options.AddPolicy("User",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("User");
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseShared(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

    }
}
