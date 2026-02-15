namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class CategoryPriceHistoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public CategoryReadModel? Category { get; set; }
        public SubCategoryReadModel? SubCategory { get; set; }
    }
}
