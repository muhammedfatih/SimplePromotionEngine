using PromotionEngine.Library.Models;
using System.Collections.Generic;

namespace PromotionEngine.Library.Business
{
    public class BasketManager
    {
        public Basket Basket { get; set; }
        public List<IPromotionEngine> Promotions { get; set; }
        public BasketManager()
        {
            Basket = new Basket();
            Promotions = new List<IPromotionEngine>();
        }
        public double Add(BasketProductItem basketProductItem)
        {
            return 0.0;
        }
        public void AddPromotion(IPromotionEngine promotion)
        {
            Promotions.Add(promotion);
        }
    }
}
