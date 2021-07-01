namespace PromotionEngine.Library.Models
{
    public class BasketProductItem
    {
        public ProductItem ProductItem { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public BasketProductItem(ProductItem productItem, int quantity)
        {
            ProductItem = productItem;
            Quantity = quantity;
            Amount = ProductItem.Price * quantity;
        }
    }
}
