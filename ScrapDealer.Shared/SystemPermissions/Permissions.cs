namespace ScrapDealer.Shared.SystemPermissions
{
    public static class Permissions
    {
        public static class Menu
        {
            public const string Dashboard = "Permissions.Menu.Dashboard";
            public const string Sellers = "Permissions.Menu.Sellers";
            public const string SellersVerification = "Permissions.Menu.SellersVerification";
            public const string RetailBuyer = "Permissions.Menu.RetailBuyer";
            public const string WholeSaleBuyer = "Permissions.Menu.WholeSaleBuyer";
            public const string BuyersVerification = "Permissions.Menu.BuyersVerification";
            public const string Supports = "Permissions.Menu.Supports";
            public const string Permissions = "Permissions.Menu.Permissions";
            public const string Requests = "Permissions.Menu.Permissions";
            public const string ReviewedRequests = "Permissions.Menu.ReviewedRequests";
            public const string Invoices = "Permissions.Menu.Invoices";
            public const string FAQ = "Permissions.Menu.FAQ";
            public const string News = "Permissions.Menu.News";
            public const string Settings = "Permissions.Menu.Settings";
            public const string Rewards = "Permissions.Menu.Rewards";
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
