using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner.MockDataStores
{
    // Mock MockProductDataStore to simulate getting rebate data
    public class MockProductDataStore : IProductDataStore
    {
        public Product GetProduct(string productIdentifier)
        {
            return new Product
            {
                SupportedIncentives = (SupportedIncentiveType)Enum.Parse(typeof(SupportedIncentiveType), productIdentifier),
                Identifier = productIdentifier,
                Price = 200m
            };
        }
    }
}
