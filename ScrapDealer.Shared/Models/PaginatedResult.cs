namespace ScrapDealer.Shared.Models
{
    public record PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public PaginatedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageIndex)
        {
            Data = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }

}
