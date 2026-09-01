namespace ScrapDealer.Shared.SystemPermissions
{
    public static class Permissions
    {
        public static class Menu
        {
            public const string MainPage = "Permissions.Menu.MainPage";
            public const string MainPage_Dashboard = "Permissions.Menu.MainPage.Dashboard";

            public const string SellersManagement = "Permissions.Menu.SellersManagement";
            public const string SellersManagement_Sellers = "Permissions.Menu.SellersManagement.Sellers";
            public const string SellersManagement_SellersVerification = "Permissions.Menu.SellersManagement.SellersVerification";

            public const string BuyersManagement = "Permissions.Menu.BuyersManagement";
            public const string BuyersManagement_Buyers = "Permissions.Menu.BuyersManagement.Buyers";
            public const string BuyersManagement_BuyersVerification = "Permissions.Menu.BuyersManagement.BuyersVerification";

            public const string SaleOrdersManagement = "Permissions.Menu.SaleOrdersManagement";
            public const string SaleOrdersManagement_SaleOrders = "Permissions.Menu.SaleOrdersManagement.SaleOrders";

            public const string SupportsManagement = "Permissions.Menu.SupportsManagement";
            public const string SupportsManagement_Supports = "Permissions.Menu.SupportsManagement.Supports";
            public const string SupportsManagement_Accesses = "Permissions.Menu.SupportsManagement.Accesses";

            public const string FinancialManagement = "Permissions.Menu.FinancialManagement";
            public const string FinancialManagement_Rewards = "Permissions.Menu.FinancialManagement.Rewards";
            public const string FinancialManagement_Invoices = "Permissions.Menu.FinancialManagement.Invoices";

            public const string ReferralCodesManagement = "Permissions.Menu.ReferralCodesManagement";
            public const string ReferralCodesManagement_ReferralCodes = "Permissions.Menu.ReferralCodesManagement.ReferralCodes";

            public const string TicketsManagement = "Permissions.Menu.TicketsManagement";
            public const string TicketsManagement_Tickets = "Permissions.Menu.TicketsManagement.Tickets";

            public const string NotificationsManagement = "Permissions.Menu.NotificationsManagement";
            public const string NotificationsManagement_NotificationsList = "Permissions.Menu.NotificationsManagement.NotificationsList";
            public const string NotificationsManagement_NotificationsManagement = "Permissions.Menu.NotificationsManagement.NotificationsManagement";

            public const string NewsManagement = "Permissions.Menu.NewsManagement";
            public const string NewsManagement_News = "Permissions.Menu.NewsManagement.News";

            public const string SettingsManagement = "Permissions.Menu.SettingsManagement";
            public const string SettingsManagement_Settings = "Permissions.Menu.SettingsManagement.Settings";
        }

        //public static class SaleOrders
        //{
        //    public const string ViewAll = "Permissions.SaleOrders.ViewAll";
        //    public const string Verification = "Permissions.SaleOrders.Verification";
        //    public const string Review = "Permissions.SaleOrders.Review";
        //}
        //public static class News
        //{
        //    public const string Create = "Permissions.News.Create";
        //    public const string Update = "Permissions.News.Update";
        //    public const string Delete = "Permissions.News.Delete";
        //}
        //public static class SubCategories
        //{
        //    public const string Create = "Permissions.SubCategories.Create";
        //    public const string Update = "Permissions.SubCategories.Update";
        //    public const string Delete = "Permissions.SubCategories.Delete";
        //}
        //public static class Categories
        //{
        //    public const string Create = "Permissions.Categories.Create";
        //    public const string Update = "Permissions.Categories.Update";
        //    public const string Delete = "Permissions.Categories.Delete";
        //}
        //public static class Buyers
        //{
        //    public const string ToggleActivation = "Permissions.Buyers.ToggleActivation";
        //    public const string Verify = "Permissions.Buyers.Verify";
        //    public const string State = "Permissions.Buyers.State";
        //    public const string ViewAll = "Permissions.Buyers.ViewAll";
        //}

        //public static class Sellers
        //{
        //    public const string ToggleActivation = "Permissions.Sellers.ToggleActivation";
        //    public const string Verify = "Permissions.Sellers.Verify";
        //    public const string State = "Permissions.Sellers.State";
        //    public const string ViewAll = "Permissions.Sellers.ViewAll";
        //}
        //public static class Users
        //{
        //    public const string ViewAll = "Permissions.Users.ViewAll";
        //    public const string State = "Permissions.Users.State";
        //}

        public static IEnumerable<string> GetAllPermissions()
        {
            return typeof(Permissions)
                .GetNestedTypes()
                .SelectMany(t => t.GetFields().Select(f => f.GetValue(null)?.ToString()))
                .Where(p => p != null)!;
        }
    }
}
