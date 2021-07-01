using PromotionEngine.Library.Models;

namespace PromotionEngine.Library.Business.PromotionEngines
{
    public class MultipleTheSameItemsBundlePromotionEngine : IPromotionEngine
    {
        public SKU SKU { get; set; }
        public int Quantity { get; set; }
        public double FixedPrice { get; set; }
        public MultipleTheSameItemsBundlePromotionEngine(
            SKU sku,
            int quantitiy,
            double fixedPrice
        )
        {
            SKU = sku;
            Quantity = quantitiy;
            FixedPrice = fixedPrice;
        }
        public double Run(Basket basket)
        {
            var discountAmount = 0.0;
            foreach (var item in basket.ProductItems)
            {
                if (SKU == item.ProductItem.SKU && Quantity <= item.Quantity)
                {
                    var numberOfPromotion = item.Quantity / Quantity;
                    var numberOfNonPromotion = item.Quantity % Quantity;
                    discountAmount +=
                        item.Amount -
                        FixedPrice * numberOfPromotion -
                        item.ProductItem.Price * numberOfNonPromotion;
                }
            }
            return discountAmount;
        }
    }
}
