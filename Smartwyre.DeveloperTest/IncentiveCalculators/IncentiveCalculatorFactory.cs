using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.IncentiveCalculators
{
    public class IncentiveCalculatorFactory
    {
        private static readonly Dictionary<IncentiveType, IIncentiveCalculator> _incentiveCalculators;

        static IncentiveCalculatorFactory()
        {
            
            _incentiveCalculators = new Dictionary<IncentiveType, IIncentiveCalculator>
        {
            { IncentiveType.FixedRateRebate, new FixedRateRebateCalculator() },
            { IncentiveType.AmountPerUom, new AmountPerUomCalculator() },
            { IncentiveType.FixedCashAmount, new FixedCashAmountCalculator() }
            // New incentive types can be added here easily
        };
        }

        // Retrieve the correct calculator based on the incentive type
        public static IIncentiveCalculator GetCalculator(IncentiveType incentiveType)
        {
            if (_incentiveCalculators.TryGetValue(incentiveType, out var calculator))
            {
                return calculator;
            }

            throw new InvalidOperationException("Invalid incentive type");
        }
    }

}
