namespace ScrapDealer.Application.DTO;

public class TopSubCategoryDto : TopItemDto
{
    public Guid SubCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
