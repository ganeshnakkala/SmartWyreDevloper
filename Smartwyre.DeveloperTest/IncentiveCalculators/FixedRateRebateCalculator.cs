using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public class FixedRateRebateCalculator : IIncentiveCalculator
    {
        public bool IsEligibleForRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
                   && rebate.Percentage > 0
                   && product.Price > 0
                   && request.Volume > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }

}
