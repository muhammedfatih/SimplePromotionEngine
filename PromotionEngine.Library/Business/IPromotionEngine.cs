using PromotionEngine.Library.Models;

namespace PromotionEngine.Library.Business
{
    public interface IPromotionEngine
    {
        public double Run(Basket basket);
    }
}
