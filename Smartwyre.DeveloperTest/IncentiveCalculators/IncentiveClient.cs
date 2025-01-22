using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public class IncentiveClient
    {
        private readonly IIncentiveCalculator _incentiveCalculator;

        // Assigns the strategy based on the provided RebateType enum
        public IncentiveClient(IncentiveType rebateType)
        {
            _incentiveCalculator = IncentiveCalculatorFactory.GetCalculator(rebateType);
        }

        //Checks the Eligibility for Rebate
        public bool CheckEligibilityForRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
          return _incentiveCalculator.IsEligibleForRebate(rebate, product, request);
        }

        // Executes the strategy's CalculateRebate method
        public decimal ExecuteRebateCalculation(Rebate rebate, Product product, CalculateRebateRequest request)
        {
           return _incentiveCalculator.CalculateRebate(rebate, product, request);
        }
    }

}
