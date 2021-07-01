using PromotionEngine.Library.Models;

namespace PromotionEngine.Library.Business
{
    public class BasketManager
    {
        public Basket Basket { get; set; }
        public BasketManager()
        {
            Basket = new Basket();
        }
        public double Add(BasketProductItem basketProductItem)
        {
            return 0.0;
        }
    }
}
