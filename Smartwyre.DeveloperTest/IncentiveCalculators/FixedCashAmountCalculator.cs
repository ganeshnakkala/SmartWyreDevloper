using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public class FixedCashAmountCalculator : IIncentiveCalculator
    {
        public bool IsEligibleForRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) && rebate.Amount > 0;
        }

        public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
