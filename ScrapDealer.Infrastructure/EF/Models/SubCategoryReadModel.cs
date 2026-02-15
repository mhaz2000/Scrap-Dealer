namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SubCategoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Guid> Images { get; private set; } = [];
        public CategoryReadModel Category { get; set; }
    }
}
