namespace API.DTOs
{
    public class BasketDto
    {
        public long Id { get; set; }

        public string BuyerId { get; set; }

        public List<BasketItemDto> Items { get; set; }
    }
}