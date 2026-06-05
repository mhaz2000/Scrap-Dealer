namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class CategoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public ICollection<Guid> Images { get; private set; } = [];
        public ICollection<SubCategoryReadModel> SubCategories { get; set; }
    }
}
