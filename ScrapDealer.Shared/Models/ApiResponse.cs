namespace ScrapDealer.Shared.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int? Total { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}
