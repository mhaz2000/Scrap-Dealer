namespace ScrapDealer.Shared.Models
{
    public record PaginationQuery
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 0;

        public string? SortBy { get; set; } = string.Empty;
        public string? Search { get; set; } = string.Empty;
    }
}
