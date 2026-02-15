using ScrapDealer.Shared;
using Microsoft.Extensions.DependencyInjection;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Factories;
using Microsoft.Extensions.Configuration;

namespace ScrapDealer.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRolePermissionFactory, RolePermissionFactory>();
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IBuyerFactory, BuyerFactory>();
            services.AddScoped<IRewardFactory, RewardFactory>();
            services.AddScoped<ISellerFactory, SellerFactory>();
            services.AddScoped<IContractFactory, ContractFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            services.AddScoped<ISubCategoryFactory, SubCategoryFactory>();
            services.AddScoped<ICategoryFactory, CategoryFactory>();
            services.AddScoped<ICategoryPriceHistoryFactory, CategoryPriceHistoryFactory>();
            services.AddScoped<ISaleOrderFactory, SaleOrderFactory>();
            services.AddScoped<INewsFactory, NewsFactory>();
            services.AddScoped<INotificationFactory, NotificationFactory>();
            services.AddScoped<ISettingsFactory, SettingsFactory>();
            services.AddScoped<ITicketFactory, TicketFactory>();

            services.AddShared(configuration);


            return services;
        }
    }
}
