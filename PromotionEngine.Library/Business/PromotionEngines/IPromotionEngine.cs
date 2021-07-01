using PromotionEngine.Library.Models;

namespace PromotionEngine.Library.Business.PromotionEngines
{
    public interface IPromotionEngine
    {
        public double Run(Basket basket);
    }
}
