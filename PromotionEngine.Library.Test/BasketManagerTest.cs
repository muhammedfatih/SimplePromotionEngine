using NUnit.Framework;
using PromotionEngine.Library.Business;
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
    }
}