using Microsoft.EntityFrameworkCore;
using ScrapDealer.Infrastructure.EF.Config;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<NewsReadModel> News { get; set; }
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<RoleReadModel> Roles { get; set; }
        public DbSet<BuyerReadModel> Buyers { get; set; }
        public DbSet<RewardReadModel> Rewards { get; set; }
        public DbSet<WalletReadModel> Wallets { get; set; }
        public DbSet<TicketReadModel> Tickets { get; set; }
        public DbSet<SellerReadModel> Sellers { get; set; }
        public DbSet<InvoiceReadModel> Invoices { get; set; }
        public DbSet<SettingsReadModel> Settings { get; set; }
        public DbSet<UserRoleReadModel> UserRoles { get; set; }
        public DbSet<ContractReadModel> Contracts { get; set; }
        public DbSet<CategoryReadModel> Categories { get; set; }
        public DbSet<SaleOrderReadModel> SaleOrders { get; set; }
        public DbSet<SubCategoryReadModel> SubCategories { get; set; }
        public DbSet<NotificationReadModel> Notifications { get; set; }
        public DbSet<ScoreHistoryReadModel> ScoreHistories { get; set; }
        public DbSet<TicketMessageReadModel> TicketMessages { get; set; }
        public DbSet<SaleOrderItemReadModel> SaleOrderItems { get; set; }
        public DbSet<RolePermissionReadModel> RolePermissions { get; set; }
        public DbSet<SaleOrderRequestReadModel> SaleOrderRequests  { get; set; }
        public DbSet<WalletTransactionReadModel> WalletTransactions { get; set; }
        public DbSet<CategoryPriceHistoryReadModel> CategoryPriceHistories { get; set; }
        public DbSet<ReferralReadModel> Referrals { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextConfigurationApplier.ApplyReadConfigurations(modelBuilder);
        }
    }
}
