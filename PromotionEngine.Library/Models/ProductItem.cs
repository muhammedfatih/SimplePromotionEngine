namespace PromotionEngine.Library.Models
{
    public class ProductItem
    {
        public SKU SKU { get; set; }
        public double Price { get; set; }
        public ProductItem(SKU sku, double price)
        {
            SKU = sku;
            Price = price;
        }
    }
}
