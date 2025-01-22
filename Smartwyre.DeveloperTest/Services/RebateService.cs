using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IncentiveCalculators;
using Smartwyre.DeveloperTest.Types;
namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;    

    public RebateService(IRebateDataStore rebateRepository, IProductDataStore productRepository)
    {
        _rebateDataStore = rebateRepository;
        _productDataStore = productRepository;        
    }

    public CalculateRebateResult CalculateRebate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult()
        {
            Success = false,
            RebateAmount = 0m
        };

        if (rebate != null && product != null)
        {
            var client = new IncentiveClient(rebate.Incentive);
            if (client.CheckEligibilityForRebate(rebate, product, request))
            {
                result.RebateAmount = client.ExecuteRebateCalculation(rebate, product, request);
                result.Success = true;
                _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
            }
        }
        return result;
    }
}
