using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public interface IIncentiveCalculator
    {
        bool IsEligibleForRebate(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
    }

}