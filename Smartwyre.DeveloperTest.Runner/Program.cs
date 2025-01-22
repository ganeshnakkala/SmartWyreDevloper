using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IncentiveCalculators;
using Smartwyre.DeveloperTest.Runner.MockDataStores;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build service provider and get required services
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var rebateService = serviceProvider.GetRequiredService<IRebateService>();
            Console.WriteLine("Welcome to the SmartWyre Rebate Service!");

            // Accept inputs from the user
            Console.Write("Enter Rebate Identifier: ");
            string rebateIdentifier = Console.ReadLine();

            Console.Write("Enter Product Identifier: ");
            string productIdentifier = Console.ReadLine();

            Console.Write("Enter Volume: ");
            int volume = int.Parse(Console.ReadLine());          
            // Prepare the request
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = rebateIdentifier,
                ProductIdentifier = productIdentifier,
                Volume = volume
            };

            // Call the CalculateRebate method
            var result = rebateService.CalculateRebate(request);

            // Output the result
            if (result.Success)
            {
                Console.WriteLine($"Rebate Calculation Successful! Rebate Amount: {result.RebateAmount}");
            }
            else
            {
                Console.WriteLine("Rebate Calculation Failed!");
            }
        }

        // Configure DI container with necessary services
        private static void ConfigureServices(IServiceCollection services)
        {
            // Register data stores
            services.AddScoped<IProductDataStore, MockProductDataStore>();
            services.AddScoped<IRebateDataStore, MockRebateDataStore>();

            // Register incentive calculators
            services.AddScoped<FixedCashAmountCalculator>();
            services.AddScoped<FixedRateRebateCalculator>();
            services.AddScoped<AmountPerUomCalculator>();

            // Register rebate service
            services.AddScoped<IRebateService, RebateService>();
        }
    }       
}
