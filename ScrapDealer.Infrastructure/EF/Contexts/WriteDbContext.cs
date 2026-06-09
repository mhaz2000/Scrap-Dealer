using Microsoft.EntityFrameworkCore;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Config;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Shared.Abstractions.Domain;
using System.Linq.Expressions;

namespace ScrapDealer.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SaleOrderItem> SaleOrderItems { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SaleOrderRequest> SaleOrderRequests { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<CategoryPriceHistory> CategoryPriceHistories { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextConfigurationApplier.ApplyWriteConfigurations(modelBuilder);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateSoftDeleteFilter(entityType.ClrType));
                }
            }
        }

        private static LambdaExpression CreateSoftDeleteFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            var filter = Expression.Lambda(Expression.Not(property), parameter);
            return filter;
        }
    }
}
