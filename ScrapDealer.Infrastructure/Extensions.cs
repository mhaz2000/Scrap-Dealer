using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ScrapDealer.Application;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.ExternalServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.Caching;
using ScrapDealer.Infrastructure.EF;
using ScrapDealer.Infrastructure.EF.SeedData;
using ScrapDealer.Infrastructure.Logging;
using ScrapDealer.Infrastructure.Options;
using ScrapDealer.Infrastructure.Profiles;
using ScrapDealer.Infrastructure.Services;
using ScrapDealer.Infrastructure.Services.ExternalServices;

namespace ScrapDealer.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                var redisConnection = configuration["Redis:ConnectinString"]
                                      ?? "localhost:6379,password=123456";
                options.Configuration = redisConnection;
                options.InstanceName = "ScrapDealer";
            });

            services.AddSingleton<IMinioClient>(sp =>
            {
                var settings = new MinioSettings
                {
                    Endpoint = configuration["Minio:Endpoint"] ?? "localhost:9000",
                    AccessKey = configuration["Minio:AccessKey"] ?? "admin",
                    SecretKey = configuration["Minio:SecretKey"] ?? "admin123",
                    BucketName = configuration["Minio:BucketName"] ?? "filestorage",
                    UseSSL = bool.Parse(configuration["Minio:UseSSL"] ?? "false")
                };

                return new MinioClient()
                    .WithEndpoint(settings.Endpoint)
                    .WithCredentials(settings.AccessKey, settings.SecretKey)
                    .WithSSL(settings.UseSSL)
                    .Build();
            });

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddSql(configuration);
            services.AddApplication(configuration);

            services.AddAutoMapper(typeof(UserMappingProfile)); 

            services.AddSingleton<LoggingMiddleware>();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICaptchaService, CaptchaService>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IFileStorageService, MinioFileStorageService>();

            services.AddHttpClient<IGreenParsSmsService, GreenParsSmsService>();
            services.AddHttpClient<IMelliPayamakSmsService, MelliPayamakSmsService>();
            services.AddHttpClient<INeshanRevertGeocodingService, NeshanRevertGeocodingService>();
            services.AddHttpClient<IMapIRRevertGeocodingService, MapIRRevertGeocodingService>();


            services.AddTransient<DatabaseSeeder>();

            return services;
        }

    }
}
