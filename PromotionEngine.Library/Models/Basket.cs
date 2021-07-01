using System.Collections.Generic;

namespace PromotionEngine.Library.Models
{
    public class Basket
    {
        public List<BasketProductItem> ProductItems { get; set; }
        public double TotalAmount { get; set; }
        public Basket()
        {
            ProductItems = new List<BasketProductItem>();
        }
    }
}
