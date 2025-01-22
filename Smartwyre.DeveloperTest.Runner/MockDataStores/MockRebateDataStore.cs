using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner.MockDataStores
{
    // Mock RebateDataStore to simulate getting rebate data
    public class MockRebateDataStore : IRebateDataStore
    {
        public Rebate GetRebate(string rebateIdentifier)
        {
            return new Rebate
            {
                Identifier = rebateIdentifier,
                Amount = 100m,
                Percentage = 0.1m,
                Incentive = (IncentiveType)Enum.Parse(typeof(IncentiveType), rebateIdentifier)
            };
        }

        public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
        {
            // Store the result (For this mock, we will just print to the console)
            Console.WriteLine($"Storing calculation result for {rebate.Identifier}: {rebateAmount}");
        }
    }
}
