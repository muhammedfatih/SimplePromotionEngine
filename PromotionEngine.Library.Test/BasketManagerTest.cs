using NUnit.Framework;
using PromotionEngine.Library.Business;
using PromotionEngine.Library.Business.PromotionEngines;
using PromotionEngine.Library.Models;
using System.Collections.Generic;

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
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 10);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 3);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(10.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 9);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 2);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(13.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionWithMultipleAdditionExistsWithoutAnyResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 10);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 3);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(20.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionWithMultipleAdditionExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 9);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 2);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemB);

            // Then
            Assert.AreEqual(26.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionWithThirdProductItemExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var skuC = new SKU('C');
            var productItemC = new ProductItem(skuC, 3.0);
            var basketProductItemC = new BasketProductItem(productItemC, 2);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 9);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 2);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemC);

            // Then
            Assert.AreEqual(19.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void ReturnDiscountedAmountWhenDifferentItemsBundlePromotionWithDifferentMinOccurenceExistsWithResidualItem()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 1.0);
            var basketProductItemA = new BasketProductItem(productItemA, 10);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 2.0);
            var basketProductItemB = new BasketProductItem(productItemB, 3);

            var skuC = new SKU('C');
            var productItemC = new ProductItem(skuC, 3.0);
            var basketProductItemC = new BasketProductItem(productItemC, 2);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemA = new BasketProductItem(productItemA, 5);
            var bundleBasketProductItemB = new BasketProductItem(productItemB, 2);

            bundle.Add(bundleBasketProductItemA);
            bundle.Add(bundleBasketProductItemB);

            var promotion = new DifferentItemsBundlePromotionEngine(bundle, 10);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotion);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemC);

            // Then
            Assert.AreEqual(23.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void Return100AsAmountForPrerequestScenarioA()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 50.0);
            var basketProductItemA = new BasketProductItem(productItemA, 1);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 30.0);
            var basketProductItemB = new BasketProductItem(productItemB, 1);

            var skuC = new SKU('C');
            var productItemC = new ProductItem(skuC, 20.0);
            var basketProductItemC = new BasketProductItem(productItemC, 1);

            var skuD = new SKU('D');
            var productItemD = new ProductItem(skuD, 15.0);
            var basketProductItemD = new BasketProductItem(productItemD, 0);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemC = new BasketProductItem(productItemC, 1);
            var bundleBasketProductItemD = new BasketProductItem(productItemD, 1);

            bundle.Add(bundleBasketProductItemC);
            bundle.Add(bundleBasketProductItemD);

            var promotionA3 = new MultipleTheSameItemsBundlePromotionEngine(skuA, 3, 130.0);
            var promotionB2 = new MultipleTheSameItemsBundlePromotionEngine(skuB, 2, 45.0);
            var promotionCD = new DifferentItemsBundlePromotionEngine(bundle, 30.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA3);
            basketManager.AddPromotion(promotionB2);
            basketManager.AddPromotion(promotionCD);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemC);
            basketManager.Add(basketProductItemD);

            // Then
            Assert.AreEqual(100.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void Return370AsAmountForPrerequestScenarioB()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 50.0);
            var basketProductItemA = new BasketProductItem(productItemA, 5);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 30.0);
            var basketProductItemB = new BasketProductItem(productItemB, 5);

            var skuC = new SKU('C');
            var productItemC = new ProductItem(skuC, 20.0);
            var basketProductItemC = new BasketProductItem(productItemC, 1);

            var skuD = new SKU('D');
            var productItemD = new ProductItem(skuD, 15.0);
            var basketProductItemD = new BasketProductItem(productItemD, 0);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemC = new BasketProductItem(productItemC, 1);
            var bundleBasketProductItemD = new BasketProductItem(productItemD, 1);

            bundle.Add(bundleBasketProductItemC);
            bundle.Add(bundleBasketProductItemD);

            var promotionA3 = new MultipleTheSameItemsBundlePromotionEngine(skuA, 3, 130.0);
            var promotionB2 = new MultipleTheSameItemsBundlePromotionEngine(skuB, 2, 45.0);
            var promotionCD = new DifferentItemsBundlePromotionEngine(bundle, 30.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA3);
            basketManager.AddPromotion(promotionB2);
            basketManager.AddPromotion(promotionCD);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemC);
            basketManager.Add(basketProductItemD);

            // Then
            Assert.AreEqual(370.0, basketManager.GetTotalAmount());
        }
        [Test]
        public void Return280AsAmountForPrerequestScenarioC()
        {
            // Given
            var skuA = new SKU('A');
            var productItemA = new ProductItem(skuA, 50.0);
            var basketProductItemA = new BasketProductItem(productItemA, 3);

            var skuB = new SKU('B');
            var productItemB = new ProductItem(skuB, 30.0);
            var basketProductItemB = new BasketProductItem(productItemB, 5);

            var skuC = new SKU('C');
            var productItemC = new ProductItem(skuC, 20.0);
            var basketProductItemC = new BasketProductItem(productItemC, 1);

            var skuD = new SKU('D');
            var productItemD = new ProductItem(skuD, 15.0);
            var basketProductItemD = new BasketProductItem(productItemD, 1);

            var bundle = new List<BasketProductItem>();
            var bundleBasketProductItemC = new BasketProductItem(productItemC, 1);
            var bundleBasketProductItemD = new BasketProductItem(productItemD, 1);

            bundle.Add(bundleBasketProductItemC);
            bundle.Add(bundleBasketProductItemD);

            var promotionA3 = new MultipleTheSameItemsBundlePromotionEngine(skuA, 3, 130.0);
            var promotionB2 = new MultipleTheSameItemsBundlePromotionEngine(skuB, 2, 45.0);
            var promotionCD = new DifferentItemsBundlePromotionEngine(bundle, 30.0);

            var basketManager = new BasketManager();
            basketManager.AddPromotion(promotionA3);
            basketManager.AddPromotion(promotionB2);
            basketManager.AddPromotion(promotionCD);

            // When
            basketManager.Add(basketProductItemA);
            basketManager.Add(basketProductItemB);
            basketManager.Add(basketProductItemC);
            basketManager.Add(basketProductItemD);

            // Then
            Assert.AreEqual(280.0, basketManager.GetTotalAmount());
        }

    }
}