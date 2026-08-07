using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Services;
using ScrapDealer.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapDealer.Infrastructure.EF.Repositories.Base;
using ScrapDealer.Domain.Repositories.Base;
using ScrapDealer.Shared.Abstractions.Domain;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Infrastructure.Options;

namespace ScrapDealer.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DbInitializer>();

            Console.WriteLine(configuration.GetConnectionString("SQL"));

            var sqlConnection = configuration.GetConnectionString("SQL")
                                ?? configuration.GetValue<string>("SQL:ConnectionString")
                                ?? "Server=localhost;Database=ScrapDealerDB;User Id=sa;Password=Scr@pDe@1er!31372;TrustServerCertificate=True;";

            // Register DbContexts
            services.AddDbContext<ReadDbContext>(ctx => ctx.UseSqlServer(sqlConnection));
            services.AddDbContext<WriteDbContext>(ctx => ctx.UseSqlServer(sqlConnection));

            services.AddScoped<IRoleReadService, RoleReadService>();
            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IBuyerReadService, BuyerReadService>();
            services.AddScoped<ITicketReadService, TicketReadService>();
            services.AddScoped<ISellerReadService, SellerReadService>();
            services.AddScoped<ICategoryReadService, CategoryReadService>();
            services.AddScoped<ISaleOrderReadService, SaleOrderReadService>();
            services.AddScoped<ISaleOrderRequestReadService, SaleOrderRequestReadService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<ISubCategoryReadService, SubCategoryReadService>();
            services.AddScoped<IContractReadService, ContractReadService>();
            services.AddScoped<IInvoiceReadService, InvoiceReadService>();


            services.Scan(scan => scan
           .FromAssemblyOf<GenericRepository<Entity<Guid>>>()
           .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime()
           .AddClasses(classes => classes.AssignableTo(typeof(GenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());


            return services;
        }
    }
}
