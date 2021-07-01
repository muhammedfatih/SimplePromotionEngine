﻿using PromotionEngine.Library.Business.PromotionEngines;
using PromotionEngine.Library.Models;
using System.Collections.Generic;
using System.Linq;

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
            var index =
                Basket.ProductItems.
                    FindIndex(
                        x => x.ProductItem == basketProductItem.ProductItem
                    );
            if (index == -1)
            {
                Basket.ProductItems.Add(basketProductItem);
            }
            else
            {
                Basket.ProductItems[index].Quantity += 
                    basketProductItem.Quantity;
                Basket.ProductItems[index].Amount +=
                    basketProductItem.Amount;
            }
            Basket.TotalAmount += basketProductItem.Amount;

            var discountAmount = 0.0;
            foreach(var promotion in Promotions)
            {
                discountAmount += promotion.Run(Basket);
            }

            return Basket.TotalAmount - discountAmount;
        }
        public void AddPromotion(IPromotionEngine promotion)
        {
            Promotions.Add(promotion);
        }
    }
}
