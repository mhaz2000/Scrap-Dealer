namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class NewsReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
}
