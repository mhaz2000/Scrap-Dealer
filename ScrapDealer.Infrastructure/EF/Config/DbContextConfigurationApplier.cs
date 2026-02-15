using Microsoft.EntityFrameworkCore;
using Minio.DataModel.Notification;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Config.Buyers;
using ScrapDealer.Infrastructure.EF.Config.Categories;
using ScrapDealer.Infrastructure.EF.Config.CategoryPriceHistories;
using ScrapDealer.Infrastructure.EF.Config.Contracts;
using ScrapDealer.Infrastructure.EF.Config.NewsConfig;
using ScrapDealer.Infrastructure.EF.Config.NotificationConfig;
using ScrapDealer.Infrastructure.EF.Config.Rewards;
using ScrapDealer.Infrastructure.EF.Config.RolePermissions;
using ScrapDealer.Infrastructure.EF.Config.SaleOrders;
using ScrapDealer.Infrastructure.EF.Config.Tickets;
using ScrapDealer.Infrastructure.EF.Config.Users;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config
{
    public static class DbContextConfigurationApplier
    {
        public static void ApplyReadConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<RoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<CategoryReadModel>(new CategoryReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<SubCategoryReadModel>(new CategoryReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<TicketReadModel>(new TicketReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<TicketMessageReadModel>(new TicketReadEntityConfiguration());

            modelBuilder.ApplyConfiguration(new NewsReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SellerReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RewardReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContractReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderItemReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryPriceHistoryReadEntityConfiguration());
        }

        public static void ApplyWriteConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Ticket>(new TicketWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<SubCategory>(new CategoryWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<TicketMessage>(new TicketWriteEntityConfiguration());

            modelBuilder.ApplyConfiguration(new NewsWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RewardWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SellerWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContractWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderItemWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryPriceHistoryWriteEntityConfiguration());

        }
    }
}
