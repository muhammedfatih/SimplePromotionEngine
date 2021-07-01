using PromotionEngine.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Library.Business.PromotionEngines
{
    public class DifferentItemsBundlePromotionEngine : IPromotionEngine
    {
        public List<BasketProductItem> ProductItems;
        public double FixedPrice { get; set; }
        public DifferentItemsBundlePromotionEngine(
            List<BasketProductItem> productItems,
            double fixedPrice
        )
        {
            ProductItems = productItems;
            FixedPrice = fixedPrice;
        }
        public double Run(Basket basket)
        {
            var discountAmount = 0.0;
            var minOccurence = int.MaxValue;
            var doesAllBundleItemExist = true;

            foreach (var item in ProductItems)
            {
                var itemInBasket =
                    basket.ProductItems.
                        Where(
                            x => x.ProductItem == item.ProductItem &&
                            x.Quantity >= item.Quantity
                        ).
                        FirstOrDefault();
                if (itemInBasket == null)
                {
                    doesAllBundleItemExist = false;
                    break;
                }
                else
                {
                    minOccurence =
                        minOccurence < itemInBasket.Quantity / item.Quantity ?
                        minOccurence : itemInBasket.Quantity / item.Quantity;
                }
            }

            if (doesAllBundleItemExist)
            {
                discountAmount = 0.0;
                var totalAmount =
                    basket.TotalAmount + minOccurence * FixedPrice;
                foreach (var item in ProductItems)
                {
                    var numberOfPromotion = item.Quantity * minOccurence;
                    totalAmount -= numberOfPromotion * item.ProductItem.Price;
                }
                discountAmount = basket.TotalAmount - totalAmount;
            }
            else
            {
                discountAmount = 0.0;
            }
            return discountAmount;
        }
    }
}
