using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public class AmountPerUomCalculator : IIncentiveCalculator
    {
        public bool IsEligibleForRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
                   && rebate.Amount > 0
                   && request.Volume > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }
    }

}
