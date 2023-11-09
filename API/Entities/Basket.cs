namespace API.Entities
{
    public class Basket
    {
        public long Id { get; set; }

        public string BuyerId { get; set; }

        public List<BasketItem> Items { get; set; } = new();     

        public void AddItem(Product product, int quantity) 
        {
            if (Items.All(lb => lb.ProductId != product.Id)) 
            {
                Items.Add(new BasketItem{Product = product, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(lb => lb.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity; 
        }   

        public void RemoveItem(long productId, int quantity) {
            var item = Items.FirstOrDefault(lb => lb.ProductId == productId);
            if (item == null) return;

            item.Quantity -= quantity;
            if (item.Quantity == 0) Items.Remove(item);
        }
    }
}