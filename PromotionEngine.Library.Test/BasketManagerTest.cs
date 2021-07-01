using NUnit.Framework;
using PromotionEngine.Library.Business;
using PromotionEngine.Library.Business.PromotionEngines;
using PromotionEngine.Library.Models;

namespace PromotionEngine.Library.Test
{
    public class BasketManagerTest
    {
        [Test]
        public void ReturnBasketProductItemAmountWhenThereIsOnlyOneProductItemAndNoPromotion()
        {
            // Given
            var sku = new SKU('A');
            var productItem = new ProductItem(sku, 1.0);
            var basketProductItem = new BasketProductItem(productItem, 10);
            var basketManager = new BasketManager();

            // When
            basketManager.Add(basketProductItem);

            // Then
            Assert.AreEqual(10.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnBasketProductItemAmountWhenThereIsOnlyOneProductItemWithMultipleAdditionAndNoPromotion()
        {
            // Given
            var sku = new SKU('A');
            var productItem = new ProductItem(sku, 1.0);
            var basketProductItem = new BasketProductItem(productItem, 10);
            var basketManager = new BasketManager();

            // When
            basketManager.Add(basketProductItem);
            basketManager.Add(basketProductItem);

            // Then
            Assert.AreEqual(20.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnSumOfBasketProductItemAmountWhenNoPromotion()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var basketManager = new BasketManager();

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(16.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnSumOfBasketProductItemAmountWhenMultipleAdditionAndNoPromotion()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);


            var basketProductItemB2 = new BasketProductItem(productItemB, 1);

            var basketManager = new BasketManager();

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemB2);

            // Then
            Assert.AreEqual(18.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereIsOnlyOneMultipleTheSameItemBundlePromotionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);
            var promotion = new MultipleTheSameItemsBundlePromotionEngine(skuA, 10, 9.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);

            // Then
            Assert.AreEqual(9, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereIsOnlyOneMultipleTheSameItemBundlePromotionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);
            var promotion = new MultipleTheSameItemsBundlePromotionEngine(skuA, 9, 7.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);

            // Then
            Assert.AreEqual(8, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereAreTwoMultipleTheSameItemBundlePromotionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var promotionA = new MultipleTheSameItemsBundlePromotionEngine(skuA, 10, 9.0);
            var promotionB = new MultipleTheSameItemsBundlePromotionEngine(skuB, 3, 1.5);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA);
            basketManager.AddPromotion(promotionB);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(10.5, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereAreTwoMultipleTheSameItemBundlePromotionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var promotionA = new MultipleTheSameItemsBundlePromotionEngine(skuA, 9, 7.0);
            var promotionB = new MultipleTheSameItemsBundlePromotionEngine(skuB, 2, 1.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA);
            basketManager.AddPromotion(promotionB);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(11.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereIsOnlyOneMultipleTheSameItemBundlePromotionWithMultipleAdditionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);
            var promotion = new MultipleTheSameItemsBundlePromotionEngine(skuA, 10, 9.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);

            // Then
            Assert.AreEqual(18.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereIsOnlyOneMultipleTheSameItemBundlePromotionWithMultipleAdditionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);
            var promotion = new MultipleTheSameItemsBundlePromotionEngine(skuA, 9, 7.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);

            // Then
            Assert.AreEqual(16.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereAreTwoMultipleTheSameItemBundlePromotionWithMultipleAdditionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var promotionA = new MultipleTheSameItemsBundlePromotionEngine(skuA, 10, 9.0);
            var promotionB = new MultipleTheSameItemsBundlePromotionEngine(skuB, 3, 1.5);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA);
            basketManager.AddPromotion(promotionB);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(21.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenThereAreTwoMultipleTheSameItemBundlePromotionWithMultipleAdditionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var promotionA = new MultipleTheSameItemsBundlePromotionEngine(skuA, 9, 7.0);
            var promotionB = new MultipleTheSameItemsBundlePromotionEngine(skuB, 2, 1.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA);
            basketManager.AddPromotion(promotionB);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(19.0, basketManager.GetTotalAmount());
        }
    }
}