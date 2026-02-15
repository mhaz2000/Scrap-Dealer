namespace ScrapDealer.Application.DTO
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Seen { get; set; }
    }

}
