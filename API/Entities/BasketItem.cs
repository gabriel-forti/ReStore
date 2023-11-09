using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("BasketItems")]
    public class BasketItem
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long BasketId { get; set; }

        public Basket Basket { get; set; }
    }
}