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
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            services.AddScoped<INewsFactory, NewsFactory>();
            services.AddScoped<IBuyerFactory, BuyerFactory>();
            services.AddScoped<IRewardFactory, RewardFactory>();
            services.AddScoped<ISellerFactory, SellerFactory>();
            services.AddScoped<ITicketFactory, TicketFactory>();
            services.AddScoped<IWalletFactory, WalletFactory>();
            services.AddScoped<IInvoiceFactory, InvoiceFactory>();
            services.AddScoped<IContractFactory, ContractFactory>();
            services.AddScoped<ICategoryFactory, CategoryFactory>();
            services.AddScoped<ISettingsFactory, SettingsFactory>();
            services.AddScoped<ISaleOrderFactory, SaleOrderFactory>();
            services.AddScoped<ISubCategoryFactory, SubCategoryFactory>();
            services.AddScoped<IScoreHistoryFactory, ScoreHistoryFactory>();
            services.AddScoped<INotificationFactory, NotificationFactory>();
            services.AddScoped<IRolePermissionFactory, RolePermissionFactory>();
            services.AddScoped<ISaleOrderRequestFactory, SaleOrderRequestFactory>();
            services.AddScoped<ICategoryPriceHistoryFactory, CategoryPriceHistoryFactory>();

            services.AddShared(configuration);


            return services;
        }
    }
}
